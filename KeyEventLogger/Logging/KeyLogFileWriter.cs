using System.Text;

namespace KeyEventLogger.Logging;

/// <summary>Kayıtları kullanıcının seçtiği yerel .txt dosyasına yazar.</summary>
public static class KeyLogFileWriter
{
    public static void Write(string path, IEnumerable<KeyEventRecord> records)
    {
        // using bloğu, hata olsa bile dosyanın kapatılmasını garanti eder.
        using var writer = new StreamWriter(path, append: false, Encoding.UTF8);

        writer.WriteLine("YMH219 Key Event Logger - dışa aktarılan kayıtlar");
        writer.WriteLine("Tarih-Saat              | Olay    | Tuş | Değiştirici");
        writer.WriteLine(new string('-', 60));

        foreach (KeyEventRecord record in records)
        {
            writer.WriteLine(record.ToLogLine());
        }
    }
}
