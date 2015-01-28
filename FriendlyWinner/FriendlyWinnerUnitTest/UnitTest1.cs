using System;
using System.IO;
using System.Xml.Serialization;
using FriendlyWinnerDll;

namespace FriendlyWinnerUnitTest
{
    using FriendlyWinnerDll.Data;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class UnitTest
    {
          
        public static Messages Winner { get; set; }

        [TestMethod]
        public void MotivationMessage()
        {
            var myMessage = FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart);
            Assert.IsTrue(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart.Any(x => x.Equals(myMessage)));
        }

        [TestMethod]
        public void ShowDifferentMessageEachRequest()
        {
            var list = AllMessagesOnce();

            AllMessagesUsedGetNext(list);
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
        public void DontRepeatToFast()
        {
            for (var i = 0; i < FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart.Count; i++)
            {
                FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart); 
            }
            var file = Environment.CurrentDirectory + @"bla.xml";
            if (!File.Exists(file))
            {
                File.Delete(file);
            }
            SerializeToXml(FriendlyWinnerDll.FriendlyWinner.Motivation, file);

            FriendlyWinnerDll.FriendlyWinner.Motivation =
                (Messages)DeserializeFromXml(file, FriendlyWinnerDll.FriendlyWinner.Motivation.GetType());
            Assert.AreEqual(false, FriendlyWinnerDll.FriendlyWinner.RepeatMaximum(20, FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart));

        }

        [TestInitialize]
        public void TestInitialize()
        {
            FriendlyWinnerDll.FriendlyWinner.Motivation.GameEnd = new List<MyMessage>
                                   {
                                        new MyMessage { Message = "gg" },
                                        new MyMessage { Message = "GG" },
                                        new MyMessage { Message = "gg all" },
                                        new MyMessage { Message = "GG all" },
                                        new MyMessage { Message = "good game all" },
                                        new MyMessage { Message = "gg wp" },
                                    };
            FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart = new List<MyMessage>
                                   {
                                        new MyMessage { Message = "gl & hf" },
                                        new MyMessage { Message = "GL HF" },
                                        new MyMessage { Message = "GL && HF" },
                                    };
        }


        private static IEnumerable<string> AllMessagesOnce()
        {
            var list = new List<string>();
            for (var i = 0; i < FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart.Count; i++)
            {
                list.Add(
                    FriendlyWinnerDll.FriendlyWinner.GetNewMessage(
                        FriendlyWinnerDll.FriendlyWinner.GetMessage, FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart));
            }

            Assert.AreEqual(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart.Count, list.Count);
            return list;
        }

        private static void AllMessagesUsedGetNext(IEnumerable<string> list)
        {
            foreach (var result in list)
            {
                var match =
                    FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart.FirstOrDefault(
                        x => x.Message.Equals(result));
                Assert.IsTrue(match != null);
            }

            Assert.IsTrue(
                !string.IsNullOrEmpty(
                    FriendlyWinnerDll.FriendlyWinner.GetNewMessage(
                        FriendlyWinnerDll.FriendlyWinner.GetMessage, FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart)));
        }
    }
}