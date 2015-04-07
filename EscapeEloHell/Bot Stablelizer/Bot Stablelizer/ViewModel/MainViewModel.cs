using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Bot_Stablelizer.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CloseCommand = new RelayCommand(CloseBots);
            StartAllCommand = new RelayCommand(StartAll);
            StartDelayedCommand = new RelayCommand(StartDelayed);
        }

        private static void StartDelayed()
        {
            foreach (
               var bot in
                   Directory.GetFiles(@"C:\Users\Fritz\Desktop\MyVoliBots", "VoliBot.exe", SearchOption.AllDirectories)
               )
            {
                Process.Start(bot);
                Thread.Sleep(60000);
            }
        }

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand StartAllCommand { get; set; }
        public RelayCommand StartDelayedCommand { get; set; }

        private static void CloseBots()
        {
            foreach (var item in Process.GetProcesses().Where(x => x.MainWindowTitle.Contains("Current Connected: ")))
            {
                item.Kill();
            }
        }
           private static void StartAll()
        {
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