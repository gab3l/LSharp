using System.Collections.Generic;
using LeagueSharp;
using LeagueSharp.Common;

namespace FriendlyWinner
{
    using FriendlyWinnerDll;
    using System.IO;
    using FriendlyWinnerDll.Data;

    public class Files
    {
        public static string Folder = Config.LeagueSharpDirectory + @"\FriendlyWinner\";
        private const string FileName = "Messages.xml";
        internal static void GetData()
        {
            if (!File.Exists(FileName))
            {
                FriendlyWinner.Motivation.GameEnd = new List<MyMessage>
                                   {
                                        new MyMessage { Message = "gg" },
                                        new MyMessage { Message = "GG" },
                                        new MyMessage { Message = "gg all" },
                                        new MyMessage { Message = "GG all" },
                                        new MyMessage { Message = "good game all" },
                                        new MyMessage { Message = "gg wp" },
                                    };
                FriendlyWinner.Motivation.GameStart = new List<MyMessage>
                                   {
                                        new MyMessage { Message = "gl & hf" },
                                        new MyMessage { Message = "GL HF" },
                                        new MyMessage { Message = "GL && HF" },
                                    };
                Save();
            }
            else
                FriendlyWinner.Motivation =
                    (Messages)
                        SerializeXml.DeserializeFromXml(Path.Combine(Folder, FileName),
                            FriendlyWinner.Motivation.GetType());
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
            SerializeXml.SerializeToXml(FriendlyWinner.Motivation, Path.Combine(Folder, FileName));
        }
    }
}