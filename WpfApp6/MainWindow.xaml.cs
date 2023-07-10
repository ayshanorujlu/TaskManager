using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace WpfApp6
{
    
    public partial class MainWindow : Window
    {
        public string NameProcess { get;set; }

        public Process SelectedProcess { get; set; }

        public ObservableCollection<Process> Processes
        {
            get { return (ObservableCollection<Process>)GetValue(ProcessesProperty); }
            set { SetValue(ProcessesProperty, value); }
        }

        public static readonly DependencyProperty ProcessesProperty =
            DependencyProperty.Register("Processes", typeof(ObservableCollection<Process>), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var process = Process.Start($"{NameProcess}");
                Processes.Add(process);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid process name");
            }
        }

        private void End_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedProcess.Kill();
                Processes.Remove(SelectedProcess);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The process could not be stopped.");
            }
        }
    }
}
