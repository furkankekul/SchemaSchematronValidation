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
                    filePath = Environment.GetEnvironmentVariable("invoiceXsdFilePath");
                    break;
                case DocumentType.DespatchAdvice:
                    filePath = Environment.GetEnvironmentVariable("despatchXsdFilePath");
                    break;
                case DocumentType.ApplicationResponse:
                    filePath = Environment.GetEnvironmentVariable("applicationResponseXsdFilePath");
                    break;
                case DocumentType.ReceiptAdvice:
                    filePath = Environment.GetEnvironmentVariable("receiptAdviceFilePath");
                    break;
            }

            return filePath;
        }


        public static string GetXsltFilePathByDocumentType()
        {
            return Environment.GetEnvironmentVariable("xsltFilePath");
        }
    }
}
