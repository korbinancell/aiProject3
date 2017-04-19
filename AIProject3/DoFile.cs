using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AIProject3
{
    public class item
    {
        [XmlAttribute]
        public string key;
        [XmlAttribute]
        public double[] value;
    }

    class DoFile
    {
        public void WriteStatetionary(string filename, Dictionary<string, double[]> writeMe)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(typeof(item[])/*, new XmlRootAttribute() { ElementName = "items" }*/);
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, writeMe.Select(kv => new item() { key = kv.Key, value = kv.Value }).ToArray());
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(filename);
                stream.Close();
            }
        }

        public Dictionary<string, double[]> ReadStatetionary(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(item[]));
                Dictionary<string, double[]> returnMe;
                using (XmlReader reader = new XmlTextReader(read))
                {
                    returnMe = ((item[])serializer.Deserialize(reader)).ToDictionary(i => i.key, i => i.value);
                    reader.Close();
                }

                read.Close();

                return returnMe;
            }
        }
    }
}
