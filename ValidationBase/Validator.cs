using System.Text;
using System.Xml.Xsl;
using System.Xml;

namespace C_BitirmeOdevi.ValidationBase
{
    public class Validator
    {
        List<string> message = new List<string>();
        public KeyValuePair<bool, List<string>> SchemaControl(string XmlString, DocumentType documentType)
        {
            string xsdPath = Function.GetXsdFilePathByDocumentType(documentType);
            bool hasErrors = false;
            string commonBasicComponent = Environment.GetEnvironmentVariable("Common_Basic_Component_FilePath");
            string commonExtensionComponent = Environment.GetEnvironmentVariable("Common_Extension_Component_FilePath");
            string commonAggregateComponent = Environment.GetEnvironmentVariable("Common_Aggregate_Component_FilePath"); ;
            string uqdt = Environment.GetEnvironmentVariable("Uqdt_FilePath");
            string qd = Environment.GetEnvironmentVariable("Qd_File_Path");
            string extensionContentDataType = Environment.GetEnvironmentVariable("Extension_Content_Data_Type_FilePath");
            string commonSignatureComponent = Environment.GetEnvironmentVariable("Common_Signature_Component_FilePath");
            string CCTS_CCT = Environment.GetEnvironmentVariable("CCTS_CCT_FilePath");
            string signatureAggregateComponents = Environment.GetEnvironmentVariable("Signature_Aggregate_Components_FilePath");
            string XAdesV141 = Environment.GetEnvironmentVariable("XAdesV141_FilePath");
            string signatureBasicComponents = Environment.GetEnvironmentVariable("Signature_Basic_Components_FilePath");
            string xmldsig = Environment.GetEnvironmentVariable("Xml_Dsig_FilePath");
            string XAdESv132 = Environment.GetEnvironmentVariable("XAdES_v132_FilePath");

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                ValidationType = ValidationType.Schema,
                DtdProcessing = DtdProcessing.Parse
            };

            Console.WriteLine(commonBasicComponent);
            Console.WriteLine(commonExtensionComponent);
            Console.WriteLine(commonAggregateComponent);
            Console.WriteLine(uqdt);
            Console.WriteLine(qd);
            Console.WriteLine(extensionContentDataType);
            Console.WriteLine(commonSignatureComponent);
            Console.WriteLine(CCTS_CCT);
            Console.WriteLine(signatureAggregateComponents);
            Console.WriteLine(XAdesV141);
            Console.WriteLine(signatureBasicComponents);
            Console.WriteLine(xmldsig);
            Console.WriteLine(XAdESv132);

            settings.ValidationEventHandler += (sender, e) =>
            {
                hasErrors = true;
                message.Add(e.Message);
                Console.WriteLine($"Doğrulama Hatası: {e.Message}");
            };

            try
            {
                using (StringReader stringReader = new StringReader(XmlString))
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    while (reader.Read()) { }
                }

                if (!hasErrors)
                {
                    message.Add("Şema Kontrolü Başarılı");
                    return new(true, message);
                }
                else
                {
                    return new(false, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                message.Add(ex.Message);
                return new(false, message);
            }
        }


        public KeyValuePair<bool, string> SchematronControl(string xmlString)
        {
            string xsltFilePath = Function.GetXsltFilePathByDocumentType();
            string outputFilePath = Environment.GetEnvironmentVariable("Output_FilePath");

            // XSLT dönüşümünü başlat
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath);

            // XML String'i StringReader ile oku
            using (StringReader stringReader = new StringReader(xmlString))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    using (XmlWriter writer = XmlWriter.Create(outputFilePath))
                    {
                        // XSLT dönüşümünü gerçekleştir
                        xslt.Transform(xmlReader, writer);
                    }
                }

                var byteFile = File.ReadAllBytes(outputFilePath);
                string stringhtml = Encoding.UTF8.GetString(byteFile);
                if (stringhtml != null)
                {
                    Console.WriteLine("XSLT dönüşümü başarıyla tamamlandı. Şematron kontrolü başarılı.");
                    if (File.Exists(outputFilePath))
                    {
                        File.Delete(outputFilePath);
                    }
                    return new(true, stringhtml);
                }

                Console.WriteLine("XSLT dönüşümü yapılamadı. Şematron Kontrolü Başarısız.");
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                return new(false, null);
            }
        }
    }
}
