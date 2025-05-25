using Syncfusion.UI.Xaml.Charts;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WhealthDistributionSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumericalAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            if (double.TryParse(e.AxisLabel.LabelContent?.ToString(), out double value))
            {
                double trillionValue = value / 1_000_000;
                if (trillionValue <= 100)
                {
                    e.AxisLabel.LabelContent = $"${trillionValue:0.#}T";
                }
                else
                {
                    e.AxisLabel.LabelContent = string.Empty;
                    e.AxisLabel.Position = 0;
                }
            }
        }

        private void NumericalAxis_ActualRangeChanged(object sender, ActualRangeChangedEventArgs e)
        {
            if (customAxis != null)
            {
                customAxis.Maximum = (double?)e.ActualMaximum;
                customAxis.Minimum = (double?)e.ActualMinimum;
            }
        }
    }

    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string content = value?.ToString() ?? string.Empty;

            string colorHex = content switch
            {
                "Bottom 50%" => "#A94438",
                "50 - 90%" => "#CD5C08",
                "90 - 99%" => "#E8C872",
                "Top 0.9%" => "#BBE2EC",
                "Top 0.1%" => "#DFF5FF",
                _ => "#888888" // fallback neutral gray
            };

            return (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex) ?? Brushes.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
