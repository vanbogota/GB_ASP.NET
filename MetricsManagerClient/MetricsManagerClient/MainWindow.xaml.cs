using MetricsManagerClient.Services;
using System.Windows;

namespace MetricsManagerClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CpuRequest cpuRequest = new CpuRequest();

            var allCpuMetrics = cpuRequest.GetAllCpuMetrics();

            int step = allCpuMetrics.Metrics[allCpuMetrics.Metrics.Count - 1].Value;

            CpuChart.ColumnSeriesValues[0].Values.Add(step);            
        }
    }
}
