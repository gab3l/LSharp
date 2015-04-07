using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Stablelizer.CloseByPictureCompare
{
    public class CreateAccount
    {
        internal static void Create()
        {
            Thread.Sleep(1000);
            var name = GetName();
            var pw = "";

            Send(name, pw);

            SaveFile(name);
        }

        private static void SaveFile(string name)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Accounts.txt");
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            string readText = File.ReadAllText(path);

            File.WriteAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Accounts.txt"), readText + Environment.NewLine + name);

        }

        private static void Send(string name, string pw)
        {
            var delay = 100;
            SendKeys.SendWait("^a");
            Thread.Sleep(delay);
            SendKeys.SendWait(name);
            Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            SendKeys.SendWait(pw);
            Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            SendKeys.SendWait(pw);
            Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            SendKeys.SendWait("^a");
            Thread.Sleep(delay);
            SendKeys.SendWait(GetMail());
            Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            //SendKeys.SendWait("02");
            //Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            //Thread.Sleep(delay);
            //SendKeys.SendWait("02");
            //Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            //SendKeys.SendWait("1982");
            //Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(delay);
            //Thread.Sleep(delay);

            SendKeys.SendWait("{TAB}");
            //Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");
            //Thread.Sleep(delay);
            SendKeys.SendWait("{TAB}");

        }
        private static Random rnd = new Random();
        private static string GetMail()
        {
            return  rnd.Next(0, 20000).ToString() + "rzhtr" + rnd.Next(0, 20000).ToString() + @"@" + "web.com";
        }

        private static string GetName()
        {
            return Names.PreName[rnd.Next(0, Names.PreName.Count() - 1)] + Names.FantasyName[rnd.Next(0, Names.FantasyName.Count() - 1)];
        }
    }
}
