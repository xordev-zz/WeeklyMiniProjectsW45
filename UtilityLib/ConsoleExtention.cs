namespace UtilityLib;

public static class ConsoleExtention
{
    public enum TextFormatting
    {
        Default = 0,
        Negative = 7,
        ForeGroundBlack = 30,
        ForeGroundRed = 31,
        ForeGroundGreen = 32,
        ForeGroundYellow = 33,
        ForeGroundBlue = 34,
        ForeGroundMagenta = 35,
        ForeGroundCyan = 36,
        ForeGroundWhite = 37,
        ForeGroundDefault = 39,
        ForeGroundBrightBlack = 90,
        ForeGroundBrightGreen = 92,
        ForeGroundBrightYellow = 93,
        ForeGroundBrightBlue = 94,
        ForeGroundBrightWhite = 97,
        BackGroundDefault = 49
    }

    const string ESC = "\u001b";
    const string CSI = "\u001b[";
    
    public static string StringReadValidation(string text, TextFormatting foreGroundColorText,
        string helpString, TextFormatting foreGroundColorValidationString,
        string[] validationStrings, string[] commands)
    {
        string inputString;
        int index;
        while (true)
        {
            WriteLineColor(helpString, foreGroundColorValidationString);
            WriteColor(text, foreGroundColorText);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                inputString = input.Trim();
                index = Array.FindIndex(commands, t => t.Equals(inputString, StringComparison.OrdinalIgnoreCase));
                if (index != -1)
                    return commands[index];

                index = Array.FindIndex(validationStrings, t => t.Equals(inputString, StringComparison.OrdinalIgnoreCase));
                if (index != -1)
                {
                    inputString = validationStrings[index];
                    break;
                }
            }
            WriteLineColor($"{input} Is unknown! Try again!", TextFormatting.ForeGroundRed);
        }
        return inputString;
    }

    public static string StringReadNotEmpty(string text)
    {
        string inputString;
        while (true)
        {
            Console.Write($"{text}");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                WriteLineColor("String can't be empty! Try again!", TextFormatting.ForeGroundRed);
                continue;
            }
            inputString = input;
            break;
        }
        return inputString;
    }
    public static string StringRead(string? text = null)
    {
        if (text != null)
        {
            Console.Write($"{text}");
        }
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        return (string)input;
    }
    public static int NumberReadPositive(string text)
    {
        UInt32 inputNumber;

        while (true)
        {
            Console.Write($"{text}");
            var inputString = Console.ReadLine();
            if (!UInt32.TryParse(inputString, out inputNumber))
            {
                WriteLineColor("Must be a positive number! Try again!", TextFormatting.ForeGroundRed);
                continue;
            }
            break;
        }
        return (int)inputNumber;
    }

    public static double DoubleReadPositive(string text)
    {
        double inputNumber;

        while (true)
        {
            Console.Write($"{text}");
            var inputString = Console.ReadLine();
            if (!Double.TryParse(inputString, out inputNumber))
            {
                WriteLineColor("Wrong number format! Try again!", TextFormatting.ForeGroundRed);
                continue;
            }
            break;
        }
        return inputNumber;
    }
    public static string DateTimeOffsetValid(string text)
    {
        DateTimeOffset resultString;

        while (true)
        {
            Console.Write($"{text}");
            var inputString = Console.ReadLine();
            if (!DateTimeOffset.TryParse(inputString, out resultString))
            {
                WriteLineColor("Wrong date format! Try again!", TextFormatting.ForeGroundRed);
                continue;
            }
            break;
        }
        return resultString.ToString();
    }

    public static void WriteLineColor(string text, TextFormatting foreGroundColor = TextFormatting.BackGroundDefault, TextFormatting backGroundColor = TextFormatting.BackGroundDefault)
    {
        Console.WriteLine($"{CSI}{(int)foreGroundColor};{(int)backGroundColor}m{text}{CSI}0m");
    }

    public static void WriteColor(string text, TextFormatting foreGroundColor = TextFormatting.BackGroundDefault, TextFormatting backGroundColor = TextFormatting.BackGroundDefault)
    {
        Console.Write($"{CSI}{(int)foreGroundColor};{(int)backGroundColor}m{text}{CSI}0m");
    }

    public static void WriteLineCenterColor(string text, int width, TextFormatting foreGroundColor = TextFormatting.BackGroundDefault, TextFormatting backGroundColor = TextFormatting.BackGroundDefault)
    {
        int x = 0; 
        if (text.Length < width)
        {
            x = (width/2) - (text.Length / 2);
        }
        Console.Write($"{CSI}{x}G");
        Console.WriteLine($"{CSI}{(int)foreGroundColor};{(int)backGroundColor}m{text}{CSI}0m");
    }

    public static void WriteLineRepeatCharColor(string text, int len, TextFormatting foreGroundColor = TextFormatting.BackGroundDefault, TextFormatting backGroundColor = TextFormatting.BackGroundDefault)
    {
        Console.Write($"{CSI}{(int)foreGroundColor};{(int)backGroundColor}m");

        for (int i = 0; i < len; i++)
        {
            Console.Write($"{text}");
        }
        Console.WriteLine($"{CSI}0m");
    }
}
