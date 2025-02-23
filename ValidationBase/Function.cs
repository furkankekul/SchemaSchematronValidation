using System.Xml.Linq;

namespace C_BitirmeOdevi.ValidationBase
{
    public static class Function
    {
        public static string GetXsdFilePathByDocumentType(DocumentType documentType)
        {
            string filePath = null;
            switch (documentType)
            {
                case DocumentType.Invoice:
                    filePath = "C:\\BitirmeÖdevi\\SchemaValidation\\packges\\xsdrt\\maindoc\\UBL-Invoice-2.1.xsd";
                    break;
                case DocumentType.DespatchAdvice:
                    filePath = @"C:\\BitirmeÖdevi\\SchemaValidation\\packges\\xsdrt\\maindoc\\UBL-DespatchAdvice-2.1.xsd";
                    break;
                case DocumentType.ApplicationResponse:
                    filePath = @"C:\\BitirmeÖdevi\\SchemaValidation\\packges\\xsdrt\\maindoc\\UBL-ApplicationResponse-2.1.xsd";
                    break;
                case DocumentType.ReceiptAdvice:
                    filePath = @"C:\\BitirmeÖdevi\\SchemaValidation\\packges\\xsdrt\\maindoc\\UBL-ReceiptAdvice-2.1.xsd";
                    break;
            }

            return filePath;
        }


        public static string GetXsltFilePathByDocumentType(DocumentType documentType)
        {
            string filePath = null;
            switch (documentType)
            {
                case DocumentType.Invoice:
                case DocumentType.DespatchAdvice:
                case DocumentType.ApplicationResponse:
                case DocumentType.ReceiptAdvice:
                    filePath = "C:\\BitirmeÖdevi\\SchemaValidation\\packges\\schematron\\UBL-TR_Main_Schematron.xslt";
                    break;
                default:
                    break;
            }

            return filePath;
        }
    }
}
