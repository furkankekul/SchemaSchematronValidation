namespace C_BitirmeOdevi.ValidationBase
{
    public enum DocumentType : byte
    {
        Invoice = 0,
        DespatchAdvice = 1,
        ApplicationResponse = 2,
        ReceiptAdvice = 3,
    }

    public enum Validation : byte
    {
        Schema = 0,
        Schematron = 1,
        All = 2,
    }
}
