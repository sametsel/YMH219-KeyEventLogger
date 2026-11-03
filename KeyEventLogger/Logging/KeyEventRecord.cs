using System.Globalization;

namespace KeyEventLogger.Logging;

public enum KeyEventKind
{
    KeyDown,
    KeyUp,
}

/// <summary>Yakalanan tek bir klavye olayının değişmez kaydı.</summary>
public sealed record KeyEventRecord(
    DateTime Timestamp,
    KeyEventKind Kind,
    Keys Key,
    Keys Modifiers,
    bool IsRepeat)
{
    private const string TimeFormat = "dd.MM.yyyy HH:mm:ss.fff";

    public string KeyName => FormatKeyName(Key);

    /// <summary>Ekranda ve .txt dosyasında kullanılan ortak satır biçimi.</summary>
    public string ToLogLine()
    {
        string line = string.Create(
            CultureInfo.InvariantCulture,
            $"{Timestamp.ToString(TimeFormat, CultureInfo.InvariantCulture)} | {Kind,-7} | {KeyName}");

        string modifiers = FormatModifiers();
        if (modifiers.Length > 0)
        {
            line += $" | {modifiers}";
        }

        return IsRepeat ? line + " | basılı tutuluyor" : line;
    }

    private string FormatModifiers()
    {
        var names = new List<string>();

        // Tuşun kendisi zaten Ctrl/Shift/Alt ise ayrıca değiştirici olarak yazılmaz.
        if (Modifiers.HasFlag(Keys.Control) && Key != Keys.ControlKey)
        {
            names.Add("Ctrl");
        }
        if (Modifiers.HasFlag(Keys.Shift) && Key != Keys.ShiftKey)
        {
            names.Add("Shift");
        }
        if (Modifiers.HasFlag(Keys.Alt) && Key != Keys.Menu)
        {
            names.Add("Alt");
        }

        return string.Join("+", names);
    }

    private static string FormatKeyName(Keys key)
    {
        if (key is >= Keys.D0 and <= Keys.D9)
        {
            return ((int)(key - Keys.D0)).ToString(CultureInfo.InvariantCulture);
        }

        return key switch
        {
            Keys.ShiftKey => "Shift",
            Keys.ControlKey => "Ctrl",
            Keys.Menu => "Alt",
            Keys.Return => "Enter",
            Keys.Back => "Backspace",
            Keys.Capital => "CapsLock",
            Keys.Prior => "PageUp",
            Keys.Next => "PageDown",
            _ => key.ToString(),
        };
    }
}
