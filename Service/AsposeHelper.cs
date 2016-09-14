using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Aspose.Words;
using Aspose.Words.MailMerging;

namespace Service
{
    public static class AsposeHelper
    {
        public static byte[] ExecuteAsposeMerge(string[] keys, object[] values, string template, DataSet set = null)
        {
            try
            {
                //var license = new Aspose.Words.License();
                //license.SetLicense("Aspose.Total.lic");
                var templateName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);
                var document = new Document(templateName);
                document.MailMerge.Execute(keys, values);

                if (set != null)
                {
                    document.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                    document.MailMerge.ExecuteWithRegions(set);
                    document.MailMerge.DeleteFields();
                }

                var stream = new MemoryStream();
                document.Save(stream, SaveFormat.Pdf);

                return stream.ToArray();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR_DOWNLOADING_PDF_DOCUMENT");
            }
        }

        public static string SerializeObjectAsXml(object obj, XmlAttributeOverrides overrides = null, XmlSerializerNamespaces namespaces = null)
        {
            var serializer = new XmlSerializer(obj.GetType(), overrides);

            var stream = new MemoryStream();
            serializer.Serialize(stream, obj, namespaces);

            stream.Position = 0;
            var reader = new StreamReader(stream, Encoding.UTF8);

            return reader.ReadToEnd();
        }
    }
}
