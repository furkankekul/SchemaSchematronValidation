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
                    filePath = Environment.GetEnvironmentVariable("Invoice_Xsd_FilePath");
                    break;
                case DocumentType.DespatchAdvice:
                    filePath = Environment.GetEnvironmentVariable("Despatch_Xsd_FilePath");
                    break;
                case DocumentType.ApplicationResponse:
                    filePath = Environment.GetEnvironmentVariable("ApplicationResponse_Xsd_FilePath");
                    break;
                case DocumentType.ReceiptAdvice:
                    filePath = Environment.GetEnvironmentVariable("ReceiptAdvice_FilePath");
                    break;
            }

            return filePath;
        }


        public static string GetXsltFilePathByDocumentType()
        {
            return Environment.GetEnvironmentVariable("Xslt_FilePath");
        }
    }
}
