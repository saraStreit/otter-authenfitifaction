
using System.Globalization;

public static class Otter
    {
        public static string name { get; set; } = "Evelines Otter";
        public static OtterColour colour = OtterColour.dark;
        public static bool bracelet = false;
    }

    public enum OtterColour
    {
        dark,
        light
    }