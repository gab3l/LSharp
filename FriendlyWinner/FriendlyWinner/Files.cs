using System.Collections.Generic;
using System.IO;
using FriendlyWinnerDll.Data;
using LeagueSharp.Common;

namespace FriendlyWinner
{
    public class Files
    {
        private const string FileName = "Messages.xml";
        public static string Folder = Config.LeagueSharpDirectory + @"\FriendlyWinner\";

        internal static void GetData()
        {
            if (!File.Exists(FileName))
            {
                FriendlyWinnerDll.FriendlyWinner.Motivation.GameEnd = new List<MyMessage>
                {
                    new MyMessage { Message = "gg" },
                    new MyMessage { Message = "GG" },
                    new MyMessage { Message = "gg all" },
                    new MyMessage { Message = "GG all" },
                    new MyMessage { Message = "good game all" },
                    new MyMessage { Message = "gg wp" }
                };
                FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart = new List<MyMessage>
                {
                    new MyMessage { Message = "gl & hf" },
                    new MyMessage { Message = "GL HF" },
                    new MyMessage { Message = "GL && HF" }
                };
                Save();
            }
            else
            {
                FriendlyWinnerDll.FriendlyWinner.Motivation =
                    (Messages)
                        SerializeXml.DeserializeFromXml(
                            Path.Combine(Folder, FileName), FriendlyWinnerDll.FriendlyWinner.Motivation.GetType());
            }
        }

        internal static void CreateFolder()
        {
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
        }

        internal static void Save()
        {
            SerializeXml.SerializeToXml(FriendlyWinnerDll.FriendlyWinner.Motivation, Path.Combine(Folder, FileName));
        }
    }
}