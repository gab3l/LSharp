using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Bot_Stablelizer.CloseByPictureCompare;

namespace Bot_Stablelizer.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private readonly DispatcherTimer dispatcherTimerClosePopUp = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sry for bad style just a quick hack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += DispatcherTimerTick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
            //dispatcherTimer.Start();

            dispatcherTimerClosePopUp.Tick += DispatcherTimerClosePopUpTick;
            dispatcherTimerClosePopUp.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimerClosePopUp.Start();
        }

        private void DispatcherTimerClosePopUpTick(object sender, EventArgs e)
        {
            foreach (var item in Process.GetProcesses().Where(x => x.MainWindowTitle.Equals("Error Report") ||
                x.MainWindowTitle.Equals("Current Connected: -1") ||
               x.MainWindowTitle.ToLower().Equals("failed to connect") ||
                x.MainWindowTitle.ToLower().Contains("network") ||
                x.MainWindowTitle.ToLower().Contains("netzwerkfehler") ||
                x.MainWindowTitle.Equals("VoliBot")))
            {
                try
                {
                    item.Kill();
                }
                catch
                {
                }
            }

          
        }

        private static void DispatcherTimerTick(object sender, EventArgs e)
        {
            foreach (var client in Process.GetProcesses().Where(x => x.ProcessName.Contains("League of Legends")))
            {
                try
                {
                    Closeing.ClickAcceptOnEnd(client);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void RestartClicked(object sender, RoutedEventArgs e)
        {
            foreach (
                var item in
                    Process.GetProcesses()
                        .Where(
                            x =>
                                x.MainWindowTitle.Contains("Current Connected: ") ||
                                x.ProcessName.Contains("League of Legends")))
            {
                try
                {
                    item.Kill();
                }
                catch 
                {
                }
            }

            foreach (
                var bot in
                    Directory.GetFiles(@"C:\Users\Fritz\Desktop\MyVoliBots", "VoliBot.exe", SearchOption.AllDirectories)
                )
            {
                Process.Start(bot);
            }
        }

    }
}