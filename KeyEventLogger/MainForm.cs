using KeyEventLogger.Logging;

namespace KeyEventLogger;

public partial class MainForm : Form
{
    private const int TopKeyCount = 5;

    private readonly KeyEventLog _log = new();

    // null ise filtre kapalıdır ve tüm kayıtlar gösterilir.
    private (DateTime From, DateTime To)? _filter;

    public MainForm()
    {
        InitializeComponent();

        dtpFrom.Value = DateTime.Today;
        dtpTo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

        AttachPreviewKeyDown(this);
        UpdateRecordingUi();
        UpdateStatistics();
    }

    /// <summary>
    /// Yön tuşları, Tab ve Enter normalde "diyalog tuşu" olarak odağı taşır ve KeyDown olayına ulaşmaz.
    /// PreviewKeyDown içinde IsInputKey = true yapılınca bu tuşlar da normal olay akışına girer.
    /// </summary>
    private void AttachPreviewKeyDown(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            control.PreviewKeyDown += Control_PreviewKeyDown;
            AttachPreviewKeyDown(control);
        }
    }

    private void Control_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
    {
        if (_log.IsRecording)
        {
            e.IsInputKey = true;
        }
    }

    // Form.KeyPreview = true olduğu için bu olaylar odak hangi kontroldeyse olsun önce forma gelir.
    // Olaylar yalnızca bu pencere etkinken oluşur; sistem geneli bir kanca kullanılmaz.
    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        KeyEventRecord? record = _log.AddKeyDown(e.KeyCode, e.Modifiers, DateTime.Now);
        if (record is null)
        {
            return;
        }

        ShowRecord(record);
        UpdateStatistics();

        // Kayıt sırasında tuşlar düğmeleri tetiklemesin. Alt kombinasyonlarına dokunulmaz (Alt+F4 çalışsın).
        if (!e.Alt)
        {
            e.SuppressKeyPress = true;
        }
    }

    private void MainForm_KeyUp(object? sender, KeyEventArgs e)
    {
        KeyEventRecord? record = _log.AddKeyUp(e.KeyCode, e.Modifiers, DateTime.Now);
        if (record is null)
        {
            return;
        }

        ShowRecord(record);

        if (!e.Alt)
        {
            e.Handled = true;
        }
    }

    private void MainForm_Activated(object? sender, EventArgs e) => UpdateRecordingUi();

    private void MainForm_Deactivate(object? sender, EventArgs e)
    {
        _log.ForgetPressedKeys();
        UpdateRecordingUi();
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        _log.Start();
        UpdateRecordingUi();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _log.Stop();
        UpdateRecordingUi();
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        _log.Clear();
        lstRecords.Items.Clear();
        UpdateStatistics();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        IReadOnlyList<KeyEventRecord> records = GetVisibleRecords();
        if (records.Count == 0)
        {
            MessageBox.Show(this, "Kaydedilecek kayıt yok.", "Kaydet",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        saveFileDialog.FileName = $"tus-kayitlari-{DateTime.Now:yyyyMMdd-HHmmss}.txt";
        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            KeyLogFileWriter.Write(saveFileDialog.FileName, records);
            lblInfo.Text = $"{records.Count} kayıt yazıldı: {saveFileDialog.FileName}";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(this, $"Dosya kaydedilemedi:\n{ex.Message}", "Kaydet",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnApplyFilter_Click(object? sender, EventArgs e)
    {
        if (dtpFrom.Value > dtpTo.Value)
        {
            MessageBox.Show(this, "Başlangıç zamanı bitiş zamanından sonra olamaz.", "Filtre",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _filter = (dtpFrom.Value, dtpTo.Value);
        ReloadList();
    }

    private void BtnClearFilter_Click(object? sender, EventArgs e)
    {
        _filter = null;
        ReloadList();
    }

    private IReadOnlyList<KeyEventRecord> GetVisibleRecords() =>
        _filter is { } range ? _log.GetRange(range.From, range.To) : _log.Records;

    private void ShowRecord(KeyEventRecord record)
    {
        if (_filter is { } range && (record.Timestamp < range.From || record.Timestamp > range.To))
        {
            return;
        }

        lstRecords.Items.Add(record.ToLogLine());
        lstRecords.TopIndex = lstRecords.Items.Count - 1; // Son kayıt görünür kalsın
    }

    private void ReloadList()
    {
        IReadOnlyList<KeyEventRecord> records = GetVisibleRecords();

        lstRecords.BeginUpdate();
        lstRecords.Items.Clear();
        foreach (KeyEventRecord record in records)
        {
            lstRecords.Items.Add(record.ToLogLine());
        }
        lstRecords.EndUpdate();

        if (lstRecords.Items.Count > 0)
        {
            lstRecords.TopIndex = lstRecords.Items.Count - 1;
        }

        lblInfo.Text = _filter is null
            ? "Filtre kapalı: tüm kayıtlar gösteriliyor."
            : $"Filtre açık: {records.Count} / {_log.Records.Count} kayıt gösteriliyor.";
    }

    private void UpdateRecordingUi()
    {
        btnStart.Enabled = !_log.IsRecording;
        btnStop.Enabled = _log.IsRecording;

        if (!_log.IsRecording)
        {
            SetStatus("KAYIT KAPALI", Color.FromArgb(75, 85, 99));
        }
        else if (ActiveForm == this)
        {
            SetStatus("KAYIT AÇIK - tuşlar yakalanıyor", Color.FromArgb(21, 128, 61));
        }
        else
        {
            SetStatus("KAYIT AÇIK - pencere odakta değil, tuş yakalanmıyor", Color.FromArgb(180, 83, 9));
        }
    }

    private void SetStatus(string text, Color color)
    {
        lblStatus.Text = text;
        lblStatus.BackColor = color;
    }

    private void UpdateStatistics()
    {
        lblTotalValue.Text = _log.TotalKeyPresses.ToString();

        lstTopKeys.BeginUpdate();
        lstTopKeys.Items.Clear();
        foreach (KeyValuePair<string, int> pair in _log.GetTopKeys(TopKeyCount))
        {
            lstTopKeys.Items.Add($"{pair.Key}: {pair.Value}");
        }
        lstTopKeys.EndUpdate();
    }
}
