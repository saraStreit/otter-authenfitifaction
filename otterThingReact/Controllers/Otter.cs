using System.Globalization;

namespace otterThingReact.Controllers
{
    public class Otter
    {
        public string name { get; set; } = "Evelines Otter";
        public OtterColour colour = OtterColour.dark;
        public bool bracelet = false;
    }

    public enum OtterColour
    {
        dark,
        light
    }
}
