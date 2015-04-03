using System.Collections.Generic;
using System.IO;
using RelaxedWinnerDll.Model;

namespace RelaxedWinnerDll
{
    public class Files
    {
        public const string FileName = "Messages.xml";
        public static string Folder = Directory.GetCurrentDirectory() + @"\RelaxedWinner";

        public static void GetData()
        {
            if (true) /*!File.Exists(Path.Combine(Folder, FileName))*/
            {
                RelaxedWinner.MessageData.GameEnd = new List<Information>
                {
                    new Information { Message = "gg" },
                    new Information { Message = "GG" },
                    new Information { Message = "gg all" },
                    new Information { Message = "GG all" },
                    new Information { Message = "good game all" },
                    new Information { Message = "gg wp" }
                };
                RelaxedWinner.MessageData.GameStart = new List<Information>
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
                //Save();
            }
            else
            {
                RelaxedWinner.MessageData =
                    (Messages)
                        SerializeXml.DeserializeFromXml(
                            Path.Combine(Folder, FileName), RelaxedWinner.MessageData.GetType());
            }
        }

        public static void CreateFolder()
        {
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
        }

        internal static void Save()
        {
            SerializeXml.SerializeToXml(RelaxedWinner.MessageData, Path.Combine(Folder, FileName));
        }
    }
}