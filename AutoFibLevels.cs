using cAlgo.API;
using cAlgo.API.Internals;
using System.Linq;

namespace cAlgo.Indicators
{
    [Indicator(IsOverlay = true, AccessRights = AccessRights.None)]
    public class AutoFibLockedWithHotkey : Indicator
    {
        // Static untuk mempertahankan nilai Low/High
        private static double _savedLow = double.NaN;
        private static double _savedHigh = double.NaN;

        [Parameter("High Level Color (Unlocked)", Group = "Colors", DefaultValue = "Red")]
        public Color HighColorUnlocked { get; set; }

        [Parameter("50% Level Color (Unlocked)", Group = "Colors", DefaultValue = "Yellow")]
        public Color MidColorUnlocked { get; set; }

        [Parameter("Low Level Color (Unlocked)", Group = "Colors", DefaultValue = "Blue")]
        public Color LowColorUnlocked { get; set; }

        [Parameter("Line Style", Group = "Appearance", DefaultValue = LineStyle.Dots)]
        public LineStyle FibLineStyle { get; set; }

        [Parameter("Line Thickness", Group = "Appearance", DefaultValue = 1, MinValue = 1, MaxValue = 5)]
        public int LineThickness { get; set; }

        [Parameter("Show Level Labels", Group = "Labels", DefaultValue = true)]
        public bool ShowLabels { get; set; }

        [Parameter("Label Font Size", Group = "Labels", DefaultValue = 9, MinValue = 6, MaxValue = 14)]
        public int LabelFontSize { get; set; }

        // Custom Levels (maksimal 5)
        [Parameter("Enable Level 1", Group = "Custom Levels", DefaultValue = true)]
        public bool EnableLevel1 { get; set; }

        [Parameter("Level 1 Value", Group = "Custom Levels", DefaultValue = 0.0)]
        public double Level1Value { get; set; }

        [Parameter("Level 1 Color (Unlocked)", Group = "Custom Levels", DefaultValue = "White")]
        public Color Level1ColorUnlocked { get; set; }

        [Parameter("Enable Level 2", Group = "Custom Levels", DefaultValue = true)]
        public bool EnableLevel2 { get; set; }

        [Parameter("Level 2 Value", Group = "Custom Levels", DefaultValue = 0.236)]
        public double Level2Value { get; set; }

        [Parameter("Level 2 Color (Unlocked)", Group = "Custom Levels", DefaultValue = "Orange")]
        public Color Level2ColorUnlocked { get; set; }

        [Parameter("Enable Level 3", Group = "Custom Levels", DefaultValue = true)]
        public bool EnableLevel3 { get; set; }

        [Parameter("Level 3 Value", Group = "Custom Levels", DefaultValue = 0.382)]
        public double Level3Value { get; set; }

        [Parameter("Level 3 Color (Unlocked)", Group = "Custom Levels", DefaultValue = "Lime")]
        public Color Level3ColorUnlocked { get; set; }

        [Parameter("Enable Level 4", Group = "Custom Levels", DefaultValue = true)]
        public bool EnableLevel4 { get; set; }

        [Parameter("Level 4 Value", Group = "Custom Levels", DefaultValue = 0.5)]
        public double Level4Value { get; set; }

        [Parameter("Level 4 Color (Unlocked)", Group = "Custom Levels", DefaultValue = "Yellow")]
        public Color Level4ColorUnlocked { get; set; }

        [Parameter("Enable Level 5", Group = "Custom Levels", DefaultValue = true)]
        public bool EnableLevel5 { get; set; }

        [Parameter("Level 5 Value", Group = "Custom Levels", DefaultValue = 0.618)]
        public double Level5Value { get; set; }

        [Parameter("Level 5 Color (Unlocked)", Group = "Custom Levels", DefaultValue = "Magenta")]
        public Color Level5ColorUnlocked { get; set; }

        private double currentLow;
        private double currentHigh;
        private bool isLocked = false;

        protected override void Initialize()
        {
            Chart.MouseDown += Chart_MouseDown;

            bool added = Chart.AddHotkey(ToggleLock, Key.K, ModifierKeys.None);
            Print(added ? "Hotkey 'K' aktif." : "Hotkey 'K' gagal.");

            currentLow = _savedLow;
            currentHigh = _savedHigh;

            Print("AutoFib siap. Status lock: " + (isLocked ? "TERKUNCI" : "TIDAK TERKUNCI"));

            if (!double.IsNaN(currentLow) && !double.IsNaN(currentHigh) && currentHigh > currentLow)
            {
                DrawOrUpdateLevels();
            }
        }

        private void ToggleLock(ChartKeyboardEventArgs args)
        {
            isLocked = !isLocked;
            Print("Lock: " + (isLocked ? "TERKUNCI" : "TIDAK TERKUNCI"));

            if (!double.IsNaN(currentLow) && !double.IsNaN(currentHigh) && currentHigh > currentLow)
            {
                DrawOrUpdateLevels();
            }
        }

        private void Chart_MouseDown(ChartMouseEventArgs obj)
        {
            if (isLocked) return;

            if (obj.ChartArea == null) return;

            double price = obj.YValue;

            if (obj.CtrlKey)
            {
                currentHigh = price;
                _savedHigh = currentHigh;
                if (!double.IsNaN(currentLow) && currentHigh > currentLow)
                    DrawOrUpdateLevels();
            }
            else
            {
                currentLow = price;
                _savedLow = currentLow;
                if (!double.IsNaN(currentHigh) && currentHigh > currentLow)
                    DrawOrUpdateLevels();
            }
        }

        public override void Calculate(int index)
        {
            if (double.IsNaN(currentLow) || double.IsNaN(currentHigh)) return;
            if (index != Bars.Count - 1) return;

            bool changed = false;

            if (Bars.HighPrices[index] > currentHigh)
            {
                currentHigh = Bars.HighPrices[index];
                _savedHigh = currentHigh;
                changed = true;
            }

            if (Bars.LowPrices[index] < currentLow)
            {
                currentLow = Bars.LowPrices[index];
                _savedLow = currentLow;
                changed = true;
            }

            if (changed)
            {
                DrawOrUpdateLevels();
            }
        }

        private void DrawOrUpdateLevels()
        {
            double diff = currentHigh - currentLow;
            if (diff <= 0) return;

            // Hapus label lama
            RemoveAllLabels();

            // High (100%)
            Color highColor = isLocked ? DarkenColor(HighColorUnlocked, 0.5) : HighColorUnlocked;
            DrawOrUpdateHLine("FibHigh", currentHigh, highColor, FibLineStyle, LineThickness);
            if (ShowLabels) DrawLevelLabel("FibHighLabel", "100.0%", currentHigh, highColor);

            // Mid (50%) - jika tidak di-custom
            if (!(EnableLevel4 && Level4Value == 0.5))
            {
                Color midColor = isLocked ? DarkenColor(MidColorUnlocked, 0.5) : MidColorUnlocked;
                DrawOrUpdateHLine("FibMid", currentLow + diff * 0.5, midColor, FibLineStyle, LineThickness);
                if (ShowLabels) DrawLevelLabel("FibMidLabel", "50.0%", currentLow + diff * 0.5, midColor);
            }

            // Low (0%)
            Color lowColor = isLocked ? DarkenColor(LowColorUnlocked, 0.5) : LowColorUnlocked;
            DrawOrUpdateHLine("FibLow", currentLow, lowColor, FibLineStyle, LineThickness);
            if (ShowLabels) DrawLevelLabel("FibLowLabel", "0.0%", currentLow, lowColor);

            // Custom Levels
            DrawCustomLevel(1, EnableLevel1, Level1Value, Level1ColorUnlocked, diff);
            DrawCustomLevel(2, EnableLevel2, Level2Value, Level2ColorUnlocked, diff);
            DrawCustomLevel(3, EnableLevel3, Level3Value, Level3ColorUnlocked, diff);
            DrawCustomLevel(4, EnableLevel4, Level4Value, Level4ColorUnlocked, diff);
            DrawCustomLevel(5, EnableLevel5, Level5Value, Level5ColorUnlocked, diff);
        }

        private void DrawCustomLevel(int index, bool enabled, double fibLevel, Color unlockedColor, double diff)
        {
            if (!enabled) return;

            double price = currentLow + diff * fibLevel;
            string name = $"FibCustom{index}";
            Color color = isLocked ? DarkenColor(unlockedColor, 0.5) : unlockedColor;

            DrawOrUpdateHLine(name, price, color, FibLineStyle, LineThickness);

            if (ShowLabels)
            {
                string labelText = $"{(fibLevel * 100):F1}%";
                DrawLevelLabel($"{name}Label", labelText, price, color);
            }
        }

        private void DrawOrUpdateHLine(string name, double price, Color color, LineStyle style, int thickness)
        {
            var line = Chart.Objects.OfType<ChartHorizontalLine>().FirstOrDefault(o => o.Name == name);

            if (line == null)
            {
                Chart.DrawHorizontalLine(name, price, color, thickness, style);
            }
            else
            {
                line.Y = price;
                line.Color = color;
                line.LineStyle = style;
                line.Thickness = thickness;
            }
        }

        private void DrawLevelLabel(string name, string text, double price, Color color)
        {
            Chart.DrawText(name, text, Bars.Count - 5, price, color);
        }

        private void RemoveAllLabels()
        {
            Chart.RemoveObject("FibHighLabel");
            Chart.RemoveObject("FibMidLabel");
            Chart.RemoveObject("FibLowLabel");

            for (int i = 1; i <= 5; i++)
            {
                Chart.RemoveObject($"FibCustom{i}Label");
            }
        }

        private Color DarkenColor(Color original, double factor)
        {
            int r = (int)(original.R * (1 - factor));
            int g = (int)(original.G * (1 - factor));
            int b = (int)(original.B * (1 - factor));
            return Color.FromArgb(original.A, r, g, b);
        }

        protected override void OnDestroy()
        {
            RemoveLevels();
        }

        private void RemoveLevels()
        {
            Chart.RemoveObject("FibHigh");
            Chart.RemoveObject("FibMid");
            Chart.RemoveObject("FibLow");

            for (int i = 1; i <= 5; i++)
            {
                Chart.RemoveObject($"FibCustom{i}");
            }

            RemoveAllLabels();
        }
    }
}
