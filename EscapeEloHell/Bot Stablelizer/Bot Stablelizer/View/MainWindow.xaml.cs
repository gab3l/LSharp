using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Bot_Stablelizer.CloseByPictureCompare;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Bot_Stablelizer.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private readonly DispatcherTimer dispatcherTimerClosePopUp = new DispatcherTimer();
        private readonly DispatcherTimer dispatcherTimerErrorPopUp = new DispatcherTimer();

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

            DispatcherTimerErrorPopUpTick(null, null);
            dispatcherTimerErrorPopUp.Tick += DispatcherTimerErrorPopUpTick;
            dispatcherTimerErrorPopUp.Interval = TimeSpan.FromSeconds(120);
            dispatcherTimerErrorPopUp.Start();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        private void DispatcherTimerErrorPopUpTick(object sender, EventArgs e)
        {

            foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
            {
                IntPtr handle = window.Key;
                string title = window.Value;

                if(title.ToLower().Equals("failed to connect") ||
                title.ToLower().Contains("network") ||
                title.ToLower().Contains("netzwerkfehler") ||
                title.Equals("VoliBot"))
                {
                    SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }
          
        }

        private void DispatcherTimerClosePopUpTick(object sender, EventArgs e)
        {
            foreach (var item in Process.GetProcesses().Where(x => x.MainWindowTitle.Equals("Error Report") ||
                x.MainWindowTitle.Equals("Current Connected: -1")))
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