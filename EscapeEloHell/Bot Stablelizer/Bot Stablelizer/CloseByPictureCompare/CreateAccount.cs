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
            var pw = "test";

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
            SendKeys.SendWait(name);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(pw);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(pw);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(GetMail());
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("2");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("2");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("1982");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(" ");
            
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");

        }

        private static string GetMail()
        {
            return  new Random().Next(0, 20000).ToString() + "rzhtr" + new Random().Next(0, 20000).ToString() + @"@" + "web.com";
        }

        private static string GetName()
        {
            return Names.PreName[new Random().Next(0, Names.PreName.Count() - 1)] + Names.FantasyName[new Random().Next(0, Names.FantasyName.Count() - 1)];
        }
    }
}
