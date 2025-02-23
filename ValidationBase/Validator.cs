using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Xml.Linq;
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
            string commonBasicComponent = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-CommonBasicComponents-2.1.xsd";
            string commonExtensionComponent = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-CommonExtensionComponents-2.1.xsd";
            string commonAggregateComponent = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-CommonAggregateComponents-2.1.xsd";
            string uqdt = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-UnqualifiedDataTypes-2.1.xsd";
            string qd = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-QualifiedDataTypes-2.1.xsd";
            string extensionContentDataType = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-ExtensionContentDataType-2.1.xsd";
            string commonSignatureComponent = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-CommonSignatureComponents-2.1.xsd";
            string CCTS_CCT = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\CCTS_CCT_SchemaModule-2.1.xsd";
            string signatureAggregateComponents = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-SignatureAggregateComponents-2.1.xsd";
            string XAdesV141 = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-XAdESv141-2.1.xsd";
            string signatureBasicComponents = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-SignatureBasicComponents-2.1.xsd";
            string xmldsig = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-xmldsig-core-schema-2.1.xsd";
            string XAdESv132 = "C:\\C#BitirmeOdevi\\packges\\xsdrt\\common\\UBL-XAdESv132-2.1.xsd";

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
            string xsltFilePath = "C:\\BitirmeÖdevi\\SchemaValidation\\packges\\schematron\\BasicSchematron.xslt";
            string outputFilePath = "C:\\BitirmeÖdevi\\SchemaValidation\\packges\\schematron\\output.html";

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
