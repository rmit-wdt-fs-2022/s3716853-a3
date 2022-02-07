namespace Assessment03.Utilities;

public enum State
{
    VIC,
    TAS,
    WA,
    SA,
    NT,
    QLD,
    NSW,
    ACT
}

public static class Constants
{
    public static readonly string NameRegex = "^[A-Z][a-zA-Z]*$";
    public static readonly string PhoneNumberRegex = "^04\\d{8}$";
    
    // This regex only checks for [{anything BUT @}@{anything BUT @}] so it only ensures the x@x format is followed for email
    // Email regex is annoying and difficult to get one that actually works all the time, so it shouldn't be used as the only means
    // of email validation (generally)
    public static readonly string EmailRegex = "^[^@]+@[^@]+$";
    public static readonly string PostCodeRegex = "^\\d+";
    public static readonly string SuburbRegex = "^[A-Z](?:[a-zA-Z]| )*$"; // same as NameRegex but now spaces are allowed too
}


