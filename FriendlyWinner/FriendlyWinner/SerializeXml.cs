using System;
using System.IO;
using System.Xml.Serialization;

namespace FriendlyWinner
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class SerializeXml
    {
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
    }
}