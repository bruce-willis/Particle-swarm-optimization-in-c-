using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace dotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var pso = new PSO.PSO();
            Values = pso.GetBestCosts();
            Time = $"Elapsed time is {pso.ElapsedTime}.\nMinimum cost is {pso.GlobalBest.Cost}";

            DataContext = this;
        }

        public ChartValues<ObservableValue> Values { get; set; }

        public string Time { get; set; }

        public SeriesCollection Series { get; set; }
    }
}
