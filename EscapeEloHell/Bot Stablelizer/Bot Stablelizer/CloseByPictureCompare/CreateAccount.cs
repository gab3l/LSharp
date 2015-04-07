using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Stablelizer.CloseByPictureCompare
{
    public class CreateAccount
    {
        internal static void Create()
        {
            var name = GetName();
            var pw = "";

            Send(name, pw);
        }

        private static void Send(string name, string pw)
        {
            SendKeys.SendWait(name);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(pw);
            SendKeys.SendWait("{TAB}");
        }

        private static string GetName()
        {
            return Names.PreName[new Random().Next(0, Names.PreName.Count() - 1)] + Names.FantasyName[new Random().Next(0, Names.FantasyName.Count() - 1)];
        }
    }
}
