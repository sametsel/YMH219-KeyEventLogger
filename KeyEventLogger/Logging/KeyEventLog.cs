namespace KeyEventLogger.Logging;

/// <summary>
/// Kayıt durumunu ve yakalanan olayları tutar; filtreleme ve istatistik hesaplarını yapar.
/// Bu sınıf yalnızca kendisine verilen olayları saklar, klavyeyi kendisi dinlemez:
/// olayları yalnızca formun KeyDown/KeyUp olayları (pencere odaktayken) gönderir.
/// </summary>
public sealed class KeyEventLog
{
    private readonly List<KeyEventRecord> _records = new();
    private readonly HashSet<Keys> _pressedKeys = new();

    public bool IsRecording { get; private set; }
    public IReadOnlyList<KeyEventRecord> Records => _records;

    /// <summary>Basılı tutma tekrarları hariç toplam tuş basışı.</summary>
    public int TotalKeyPresses => _records.Count(IsCountedPress);

    public void Start() => IsRecording = true;

    public void Stop()
    {
        IsRecording = false;
        _pressedKeys.Clear();
    }

    public void Clear()
    {
        _records.Clear();
        _pressedKeys.Clear();
    }

    /// <summary>
    /// Pencere odağı kaybedince çağrılır: tuşun KeyUp olayı başka pencereye gideceği için
    /// bekletilen "basılı" bilgisi silinir, aksi halde sonraki basış tekrar sanılırdı.
    /// </summary>
    public void ForgetPressedKeys() => _pressedKeys.Clear();

    /// <summary>Kayıt açıksa KeyDown olayını ekler; değilse null döner.</summary>
    public KeyEventRecord? AddKeyDown(Keys key, Keys modifiers, DateTime timestamp)
    {
        if (!IsRecording)
        {
            return null;
        }

        // HashSet.Add, tuş zaten varsa false döner: bu, Windows'un otomatik tekrarı demektir.
        bool isRepeat = !_pressedKeys.Add(key);
        return Append(new KeyEventRecord(timestamp, KeyEventKind.KeyDown, key, modifiers, isRepeat));
    }

    /// <summary>Kayıt açıksa KeyUp olayını ekler; değilse null döner.</summary>
    public KeyEventRecord? AddKeyUp(Keys key, Keys modifiers, DateTime timestamp)
    {
        if (!IsRecording)
        {
            return null;
        }

        _pressedKeys.Remove(key);
        return Append(new KeyEventRecord(timestamp, KeyEventKind.KeyUp, key, modifiers, false));
    }

    public IReadOnlyList<KeyEventRecord> GetRange(DateTime from, DateTime to) =>
        _records.Where(r => r.Timestamp >= from && r.Timestamp <= to).ToList();

    /// <summary>En sık basılan tuşları, çoktan aza doğru döndürür.</summary>
    public IReadOnlyList<KeyValuePair<string, int>> GetTopKeys(int count) =>
        _records
            .Where(IsCountedPress)
            .GroupBy(r => r.KeyName)
            .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
            .OrderByDescending(pair => pair.Value)
            .ThenBy(pair => pair.Key, StringComparer.Ordinal)
            .Take(count)
            .ToList();

    private KeyEventRecord Append(KeyEventRecord record)
    {
        _records.Add(record);
        return record;
    }

    private static bool IsCountedPress(KeyEventRecord record) =>
        record.Kind == KeyEventKind.KeyDown && !record.IsRepeat;
}
