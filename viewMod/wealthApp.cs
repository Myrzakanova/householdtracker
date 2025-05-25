using Microsoft.VisualBasic;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WhealthDistributionSample
{
    public class HouseHoldWealthData
    {
        public ObservableCollection<WealthData> Bottom50Data { get; set; }

        public ObservableCollection<WealthData> Next40 { get; set; }

        public ObservableCollection<WealthData> Top9 { get; set; }

        public ObservableCollection<WealthData> Top1 { get; set; }

        public ObservableCollection<WealthData> TopPoint1 { get; set; }
        public List<LabelItem> Labels { get; set; }

        public HouseHoldWealthData()
        {

            Bottom50Data = new ObservableCollection<WealthData>();
            Next40 = new ObservableCollection<WealthData>();
            Top9 = new ObservableCollection<WealthData>();
            Top1 = new ObservableCollection<WealthData>();
            TopPoint1 = new ObservableCollection<WealthData>();
            Labels = new List<LabelItem>();

            ReadCSV();

        }
    

        public void ReadCSV()
        {
            Assembly executingAssembly = typeof(App).GetTypeInfo().Assembly;
            Stream inputStream = executingAssembly.GetManifestResourceStream("WhealthDistributionSample.dfanetworthlevels.csv");
            List<string> lines = new List<string>();
            if (inputStream != null)
            {
                string line;
                StreamReader reader = new StreamReader(inputStream);
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                lines.RemoveAt(0);

                double _bottom50 = 0, _next40 = 0, _top9 = 0, _top1 = 0, _topPoint1 = 0;


                foreach (var dataPoint in lines)
                {
                    string[] data = dataPoint.Split(',');

                    string[] date = data[0].Split(':');
                    

                    switch (data[1])
                    {
                        case "Bottom50":
                            var bottom50value = Convert.ToDouble(data[2]);
                            Bottom50Data.Add(new WealthData(date[0], bottom50value));
                            _bottom50 = Math.Max(bottom50value, _bottom50);
                            break;
                        case "Next40":
                            var next40Value = Convert.ToDouble(data[2]);
                            Next40.Add(new WealthData(date[0], next40Value));
                            _next40 = Math.Max(next40Value, _next40);
                            break;
                        case "Next9":
                            var next9Value = Convert.ToDouble(data[2]);
                            Top9.Add(new WealthData(date[0], next9Value));
                            _top9 = Math.Max(next9Value, _top9);
                            break;
                        case "RemainingTop1":
                            var remainingTop1Value = Convert.ToDouble(data[2]);
                            Top1.Add(new WealthData(date[0], remainingTop1Value));
                            _top1 = Math.Max(remainingTop1Value, _top1);
                            break;

                        default:
                            var topPoint1Value = Convert.ToDouble(data[2]);
                            TopPoint1.Add(new WealthData(date[0], topPoint1Value));
                            _topPoint1 = Math.Max(topPoint1Value, _topPoint1);
                            break;
                    }

                }

                AddCustomLabels(_bottom50, _next40, _top9, _top1, _topPoint1);
               
            }

        }

        private void AddCustomLabels(double bottom50, double next40, double top9, double top1, double topPoint1)
        {
            next40 += bottom50;
            top9 += next40;
            top1 += top9;
            topPoint1 += top1;

            Labels.Add(new LabelItem() { Position = bottom50, Content = "Bottom 50%" });
            Labels.Add(new LabelItem() { Position = next40, Content = "50 - 90%" });
            Labels.Add(new LabelItem() { Position = top9, Content = "90 - 99%" });
            Labels.Add(new LabelItem() { Position = top1, Content = "Top 0.9%" });
            Labels.Add(new LabelItem() { Position = topPoint1, Content = "Top 0.1%" });
        }
    }

    public class LabelItem
    {
        public string? Content { get; set; }

        public double Position { get; set; }
    }
}
