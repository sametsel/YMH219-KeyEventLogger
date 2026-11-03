namespace KeyEventLogger;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblStatus = new Label();
        btnStart = new Button();
        btnStop = new Button();
        btnClear = new Button();
        btnSave = new Button();
        lstRecords = new ListBox();
        grpStats = new GroupBox();
        lblTotalCaption = new Label();
        lblTotalValue = new Label();
        lblTopCaption = new Label();
        lstTopKeys = new ListBox();
        grpFilter = new GroupBox();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnApplyFilter = new Button();
        btnClearFilter = new Button();
        lblScope = new Label();
        lblInfo = new Label();
        saveFileDialog = new SaveFileDialog();
        grpStats.SuspendLayout();
        grpFilter.SuspendLayout();
        SuspendLayout();
        //
        // lblStatus
        //
        lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblStatus.BackColor = Color.FromArgb(75, 85, 99);
        lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblStatus.ForeColor = Color.White;
        lblStatus.Location = new Point(12, 12);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(860, 36);
        lblStatus.Text = "KAYIT KAPALI";
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // btnStart
        //
        btnStart.Location = new Point(12, 58);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(100, 34);
        btnStart.TabIndex = 0;
        btnStart.Text = "Başlat";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += BtnStart_Click;
        //
        // btnStop
        //
        btnStop.Location = new Point(118, 58);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(100, 34);
        btnStop.TabIndex = 1;
        btnStop.Text = "Durdur";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += BtnStop_Click;
        //
        // btnClear
        //
        btnClear.Location = new Point(224, 58);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(100, 34);
        btnClear.TabIndex = 2;
        btnClear.Text = "Temizle";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;
        //
        // btnSave
        //
        btnSave.Location = new Point(330, 58);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(130, 34);
        btnSave.TabIndex = 3;
        btnSave.Text = ".txt Olarak Kaydet";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        //
        // lstRecords
        //
        lstRecords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstRecords.Font = new Font("Consolas", 9.75F);
        lstRecords.FormattingEnabled = true;
        lstRecords.HorizontalScrollbar = true;
        lstRecords.IntegralHeight = false;
        lstRecords.Location = new Point(12, 102);
        lstRecords.Name = "lstRecords";
        lstRecords.Size = new Size(600, 416);
        lstRecords.TabIndex = 4;
        //
        // grpStats
        //
        grpStats.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        grpStats.Controls.Add(lblTotalCaption);
        grpStats.Controls.Add(lblTotalValue);
        grpStats.Controls.Add(lblTopCaption);
        grpStats.Controls.Add(lstTopKeys);
        grpStats.Location = new Point(622, 96);
        grpStats.Name = "grpStats";
        grpStats.Size = new Size(250, 200);
        grpStats.TabIndex = 5;
        grpStats.TabStop = false;
        grpStats.Text = "İstatistik";
        //
        // lblTotalCaption
        //
        lblTotalCaption.AutoSize = true;
        lblTotalCaption.Location = new Point(12, 26);
        lblTotalCaption.Name = "lblTotalCaption";
        lblTotalCaption.Text = "Toplam tuş basışı:";
        //
        // lblTotalValue
        //
        lblTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTotalValue.Location = new Point(130, 22);
        lblTotalValue.Name = "lblTotalValue";
        lblTotalValue.Size = new Size(100, 24);
        lblTotalValue.Text = "0";
        //
        // lblTopCaption
        //
        lblTopCaption.AutoSize = true;
        lblTopCaption.Location = new Point(12, 54);
        lblTopCaption.Name = "lblTopCaption";
        lblTopCaption.Text = "En sık basılan 5 tuş:";
        //
        // lstTopKeys
        //
        lstTopKeys.FormattingEnabled = true;
        lstTopKeys.IntegralHeight = false;
        lstTopKeys.Location = new Point(12, 76);
        lstTopKeys.Name = "lstTopKeys";
        lstTopKeys.SelectionMode = SelectionMode.None;
        lstTopKeys.Size = new Size(226, 110);
        lstTopKeys.TabIndex = 0;
        lstTopKeys.TabStop = false;
        //
        // grpFilter
        //
        grpFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        grpFilter.Controls.Add(lblFrom);
        grpFilter.Controls.Add(dtpFrom);
        grpFilter.Controls.Add(lblTo);
        grpFilter.Controls.Add(dtpTo);
        grpFilter.Controls.Add(btnApplyFilter);
        grpFilter.Controls.Add(btnClearFilter);
        grpFilter.Location = new Point(622, 304);
        grpFilter.Name = "grpFilter";
        grpFilter.Size = new Size(250, 148);
        grpFilter.TabIndex = 6;
        grpFilter.TabStop = false;
        grpFilter.Text = "Tarih/Saat Filtresi";
        //
        // lblFrom
        //
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(12, 28);
        lblFrom.Name = "lblFrom";
        lblFrom.Text = "Başlangıç:";
        //
        // dtpFrom
        //
        dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        dtpFrom.Format = DateTimePickerFormat.Custom;
        dtpFrom.Location = new Point(78, 24);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(160, 23);
        dtpFrom.TabIndex = 0;
        //
        // lblTo
        //
        lblTo.AutoSize = true;
        lblTo.Location = new Point(12, 62);
        lblTo.Name = "lblTo";
        lblTo.Text = "Bitiş:";
        //
        // dtpTo
        //
        dtpTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        dtpTo.Format = DateTimePickerFormat.Custom;
        dtpTo.Location = new Point(78, 58);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(160, 23);
        dtpTo.TabIndex = 1;
        //
        // btnApplyFilter
        //
        btnApplyFilter.Location = new Point(12, 96);
        btnApplyFilter.Name = "btnApplyFilter";
        btnApplyFilter.Size = new Size(108, 34);
        btnApplyFilter.TabIndex = 2;
        btnApplyFilter.Text = "Filtrele";
        btnApplyFilter.UseVisualStyleBackColor = true;
        btnApplyFilter.Click += BtnApplyFilter_Click;
        //
        // btnClearFilter
        //
        btnClearFilter.Location = new Point(130, 96);
        btnClearFilter.Name = "btnClearFilter";
        btnClearFilter.Size = new Size(108, 34);
        btnClearFilter.TabIndex = 3;
        btnClearFilter.Text = "Filtreyi Kaldır";
        btnClearFilter.UseVisualStyleBackColor = true;
        btnClearFilter.Click += BtnClearFilter_Click;
        //
        // lblScope
        //
        lblScope.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblScope.ForeColor = Color.FromArgb(75, 85, 99);
        lblScope.Location = new Point(622, 460);
        lblScope.Name = "lblScope";
        lblScope.Size = new Size(250, 58);
        lblScope.Text = "Yalnızca bu pencere odaktayken tuş yakalanır. Kayıt açıkken düğmeleri fare ile kullanın; tuşlar düğmeleri tetiklemez.";
        //
        // lblInfo
        //
        lblInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblInfo.AutoEllipsis = true;
        lblInfo.Location = new Point(12, 526);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(860, 22);
        lblInfo.TextAlign = ContentAlignment.MiddleLeft;
        //
        // saveFileDialog
        //
        saveFileDialog.DefaultExt = "txt";
        saveFileDialog.Filter = "Metin dosyası (*.txt)|*.txt";
        saveFileDialog.Title = "Kayıtları kaydet";
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(884, 556);
        Controls.Add(lblStatus);
        Controls.Add(btnStart);
        Controls.Add(btnStop);
        Controls.Add(btnClear);
        Controls.Add(btnSave);
        Controls.Add(lstRecords);
        Controls.Add(grpStats);
        Controls.Add(grpFilter);
        Controls.Add(lblScope);
        Controls.Add(lblInfo);
        KeyPreview = true;
        MinimumSize = new Size(900, 595);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Key Event Logger (Eğitsel) - YMH219";
        Activated += MainForm_Activated;
        Deactivate += MainForm_Deactivate;
        KeyDown += MainForm_KeyDown;
        KeyUp += MainForm_KeyUp;
        grpStats.ResumeLayout(false);
        grpStats.PerformLayout();
        grpFilter.ResumeLayout(false);
        grpFilter.PerformLayout();
        ResumeLayout(false);
    }

    private Label lblStatus;
    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Button btnSave;
    private ListBox lstRecords;
    private GroupBox grpStats;
    private Label lblTotalCaption;
    private Label lblTotalValue;
    private Label lblTopCaption;
    private ListBox lstTopKeys;
    private GroupBox grpFilter;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnApplyFilter;
    private Button btnClearFilter;
    private Label lblScope;
    private Label lblInfo;
    private SaveFileDialog saveFileDialog;
}
