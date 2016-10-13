using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ticketing.Infrastructure.Helpers
{
    public static class Deserializer
    {
        public static T Deserialize<T>(this string xml)
        {
            T result = default(T);
            XmlSerializer ser = null;
            StringReader sr = null;
            XmlTextReader reader = null;
            
            try
            {
                ser = new XmlSerializer(typeof(T));
                sr = new StringReader(xml);
                reader = new XmlTextReader(sr);

                result = (T)ser.Deserialize(reader);

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            
            return result;
        }

        public static T Deserialize<T>(this XmlDocument xml, string defaultNamespace = null)
        {
            T result = default(T);
            XmlSerializer ser = null;
            
            string rootElementName = String.Format("tns:{0}", typeof(T).Name);

            ser = new XmlSerializer(typeof(T), defaultNamespace);
            
            result = (T)ser.Deserialize(xml.GetElementsByTagName(rootElementName)[0].CreateNavigator().ReadSubtree());
            
            return result;

        }

        public static T DeserializeWithDataContract<T>(this string xml)
        {
            T result = default(T);
            DataContractSerializer serializer;

            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                XmlDictionaryReaderQuotas quota = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = 2147483647,
                    MaxBytesPerRead = 2147483647,
                    MaxDepth = 2147483647,
                    MaxNameTableCharCount = 2147483647,
                    MaxStringContentLength = 2147483647
                };
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.UTF8, quota, null);
                serializer = new DataContractSerializer(typeof(T));
                result = (T)serializer.ReadObject(reader);
            }

            return result;
        }

    }
}