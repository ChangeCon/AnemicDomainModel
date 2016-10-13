using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Ticketing.Infrastructure.Helpers
{
	public static class DeepCopy
	{
		public static object DeepClone(this object source)
		{
			MemoryStream stream = new MemoryStream();
			DataContractSerializer ser =
				new DataContractSerializer(source.GetType());
			ser.WriteObject(stream, source);
			stream.Position = 0;
			XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas() { MaxArrayLength = 2147483647 });

			return ser.ReadObject(reader, true);
		}
	}
}
