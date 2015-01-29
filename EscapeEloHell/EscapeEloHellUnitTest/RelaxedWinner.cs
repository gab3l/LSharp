using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using RelaxedWinnerDll.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendlyWinnerUnitTest
{
    [TestClass]
    public class UnitTest
    {
        public static Messages Winner { get; set; }

        [TestMethod]
        public void GetMessageFromData()
        {
            var myMessage =
                RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart);
            Assert.IsTrue(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart.Any(x => x.Equals(myMessage)));
        }

        [TestMethod]
        public void ShowDifferentMessageEachRequest()
        {
            var list = SayAllMessagesOnce();

            ResetAfterAllMessagesUsed(list);
        }

        public static object DeserializeFromXml(string fileFullName, Type type)
        {
            var deserializer = new XmlSerializer(type);
            TextReader textReader = new StreamReader(fileFullName);

            var result = deserializer.Deserialize(textReader);
            textReader.Close();

            return result;
        }

        public static void SerializeToXml<T>(T target, string fileFullName)
        {
            var serializer = new XmlSerializer(target.GetType());
            var textWriter = new StreamWriter(fileFullName);
            serializer.Serialize(textWriter, target);
            textWriter.Close();
        }

        [TestMethod]
        public void DontRepeatTooFast()
        {
            for (var i = 0; i < RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart.Count; i++)
            {
                RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart);
            }
            var file = Environment.CurrentDirectory + @"bla.xml";
            if (!File.Exists(file))
            {
                File.Delete(file);
            }
            SerializeToXml(RelaxedWinnerDll.RelaxedWinner.MessageData, file);

            RelaxedWinnerDll.RelaxedWinner.MessageData =
                (Messages) DeserializeFromXml(file, RelaxedWinnerDll.RelaxedWinner.MessageData.GetType());
            Assert.AreEqual(
                false,
                RelaxedWinnerDll.RelaxedWinner.RepeatMaximum(
                    20, RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd = new List<Information>
            {
                new Information { Message = "gg" },
                new Information { Message = "GG" },
                new Information { Message = "wp" },
                new Information { Message = "WP" },
                new Information { Message = "gg all" },
                new Information { Message = "GG all" },
                new Information { Message = "good game all" },
                new Information { Message = "good game" },
                new Information { Message = "gg wp" },
                new Information { Message = "gg wp" },
            };

            RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart = new List<Information>
            {
                new Information { Message = "GL HF" },
                new Information { Message = "gl hf" },
                new Information { Message = "GL & HF" },
                new Information { Message = "gl & hf" },
                new Information { Message = "GL && HF" },
                new Information { Message = "gl && hf" },
            };
        }

        private static IEnumerable<string> SayAllMessagesOnce()
        {
            var list = new List<string>();
            for (var i = 0; i < RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart.Count; i++)
            {
                list.Add(
                    RelaxedWinnerDll.RelaxedWinner.GetNewMessage(
                        RelaxedWinnerDll.RelaxedWinner.GetMessage,
                        RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart));
            }

            Assert.AreEqual(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart.Count, list.Count);
            return list;
        }

        private static void ResetAfterAllMessagesUsed(IEnumerable<string> list)
        {
            foreach (var result in list)
            {
                var match =
                    RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart.FirstOrDefault(x => x.Message.Equals(result));
                Assert.IsTrue(match != null);
            }

            Assert.IsTrue(
                !string.IsNullOrEmpty(
                    RelaxedWinnerDll.RelaxedWinner.GetNewMessage(
                        RelaxedWinnerDll.RelaxedWinner.GetMessage,
                        RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)));
        }
    }
}