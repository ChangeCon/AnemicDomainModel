using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ticketing.Infrastructure.Helpers
{
    public static class Serializer
    {
        private static string Utf8ByteArrayToString(Byte[] characters)
        {
            try
            {
                var encoding = new UTF8Encoding();
                var constructedString = encoding.GetString(characters);
                return (constructedString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static XmlDocument SerializeToXmlDocument<T>(this T classInstance, XmlSerializerNamespaces namespaces = null)
        {
            XmlDocument result = new XmlDocument();

            result.LoadXml(SerializeToString(classInstance, namespaces));
            
            return result;
        }

        public static XDocument SerializeToXDocument<T>(this T instance)
        {
            var memoryStream = new MemoryStream();
            var xs = new XmlSerializer(typeof(T));
            var xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding(false));
            XDocument result = null;

            try
            {
                xs.Serialize(xmlTextWriter, instance);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                result = XDocument.Parse(Utf8ByteArrayToString(memoryStream.ToArray()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (xmlTextWriter != null)
                    xmlTextWriter.Close();

                if (memoryStream != null)
                {
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }

            return result;

        }

        public static string SerializeToString<T>(this T instance, XmlSerializerNamespaces namespaces = null, bool removeVersionHeader = false)
        {
            string result = null;

            var memoryStream = new MemoryStream();
            var xs = new XmlSerializer(typeof(T));
            var xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding(false));


            try
            {
                if (namespaces != null)
                    xs.Serialize(xmlTextWriter, instance, namespaces);
                else
                    xs.Serialize(xmlTextWriter, instance);

                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                result = Utf8ByteArrayToString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (xmlTextWriter != null)
                    xmlTextWriter.Close();

                if (memoryStream != null)
                {
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
            if (!removeVersionHeader)
                return result;
            else
                return result.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

        }

        public static string SerializeWithDataContract<T>(this T instance)
        {
            string result = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memoryStream, instance);
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return result;
        }
    }

}