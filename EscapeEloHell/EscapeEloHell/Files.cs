using System;
using System.Collections.Generic;
using System.IO;
using RelaxedWinnerDll.Model;

namespace RelaxedWinner
{
    public class Files
    {
        public const string FileName = "Messages.xml";

        public static string Folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      @"\RelaxedWinner";

        internal static void GetData()
        {
            if (!File.Exists(Path.Combine(Folder, FileName)))
            {
                RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd = new List<Information>
                {
                    new Information { Message = "gg" },
                    new Information { Message = "GG" },
                    new Information { Message = "gg all" },
                    new Information { Message = "GG all" },
                    new Information { Message = "good game all" },
                    new Information { Message = "gg wp" }
                };
                RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart = new List<Information>
                {
                    new Information { Message = "gl & hf" },
                    new Information { Message = "GL & HF" },
                    new Information { Message = "gl && hf" },
                    new Information { Message = "GL && HF" },
                    new Information { Message = "GL HF" },
                    new Information { Message = "gl hf" },
                    new Information { Message = "gl" },
                    new Information { Message = "GL" },
                    new Information { Message = "HF" },
                    new Information { Message = "hf" }
                };
                Save();
            }
            else
            {
                RelaxedWinnerDll.RelaxedWinner.MessageData =
                    (Messages)
                        SerializeXml.DeserializeFromXml(
                            Path.Combine(Folder, FileName), RelaxedWinnerDll.RelaxedWinner.MessageData.GetType());
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
            SerializeXml.SerializeToXml(RelaxedWinnerDll.RelaxedWinner.MessageData, Path.Combine(Folder, FileName));
        }
    }
}