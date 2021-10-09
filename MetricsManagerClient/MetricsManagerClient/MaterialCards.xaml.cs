using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManagerClient
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class MaterialCards : UserControl, INotifyPropertyChanged
    {
        
        public MaterialCards()
        {

            InitializeComponent();

            CpuRequest = new CpuRequest();

            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            TimeSpan fromTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.AddHours(-12).ToUnixTimeSeconds());

            CpuMetricResponce cpuMetrics = CpuRequest.GetAllCpuMetrics(fromTime, toTime);

            AverageTwelveHours = (int)cpuMetrics.Metrics.Average(c => c.Value);

            List<int> list = new List<int>();

            for (int i = 0; i < cpuMetrics.Metrics.Count; i++)
            {
                list.Add(cpuMetrics.Metrics[i].Value);
            }

            ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>(list)
                }
            };

            DataContext = this;
        }

        public SeriesCollection ColumnSeriesValues { get; set; }
        public int AverageTwelveHours { get; set; } = 0;

        public CpuRequest CpuRequest;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            TimePowerChart.Update(true);
        }

        
    }
}
