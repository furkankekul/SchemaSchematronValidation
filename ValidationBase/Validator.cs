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
            string commonBasicComponent = Environment.GetEnvironmentVariable("commonBasicComponentFilePath");
            string commonExtensionComponent = Environment.GetEnvironmentVariable("commonExtensionComponentFilePath");
            string commonAggregateComponent = Environment.GetEnvironmentVariable("commonAggregateComponentFilePath"); ;
            string uqdt = Environment.GetEnvironmentVariable("uqdtFilePath");
            string qd = Environment.GetEnvironmentVariable("qdFilePath");
            string extensionContentDataType = Environment.GetEnvironmentVariable("extensionContentDataTypeFilePath");
            string commonSignatureComponent = Environment.GetEnvironmentVariable("commonSignatureComponentFilePath");
            string CCTS_CCT = Environment.GetEnvironmentVariable("CCTS_CCT_FilePath");
            string signatureAggregateComponents = Environment.GetEnvironmentVariable("signatureAggregateComponentsFilePath");
            string XAdesV141 = Environment.GetEnvironmentVariable("XAdesV141FilePath");
            string signatureBasicComponents = Environment.GetEnvironmentVariable("signatureBasicComponentsFilePath");
            string xmldsig = Environment.GetEnvironmentVariable("xmldsigFilePath");
            string XAdESv132 = Environment.GetEnvironmentVariable("XAdESv132FilePath");

            bool status = File.Exists(commonExtensionComponent);

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                ValidationType = ValidationType.Schema,
                DtdProcessing = DtdProcessing.Parse
            };

            settings.Schemas.Add(null, xsdPath);
            settings.Schemas.Add(null, commonBasicComponent);
            settings.Schemas.Add(null, commonExtensionComponent);
            settings.Schemas.Add(null, commonAggregateComponent);
            settings.Schemas.Add(null, uqdt);
            settings.Schemas.Add(null, qd);
            settings.Schemas.Add(null, extensionContentDataType);
            settings.Schemas.Add(null, commonSignatureComponent);
            settings.Schemas.Add(null, CCTS_CCT);
            settings.Schemas.Add(null, signatureAggregateComponents);
            settings.Schemas.Add(null, XAdesV141);
            settings.Schemas.Add(null, signatureBasicComponents);
            settings.Schemas.Add(null, XAdESv132);
            settings.Schemas.Add(null, xmldsig);


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
            string outputFilePath = Environment.GetEnvironmentVariable("outputFilePath");

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
