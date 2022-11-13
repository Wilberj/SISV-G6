namespace QualityManagementApp.ADO;

public class SQLConnection
{
    private static string UserSQLConection = "";
    #pragma warning disable CA2211 // Los campos no constantes no deben ser visibles
    public static GData QMA = null!;
    #pragma warning restore CA2211 // Los campos no constantes no deben ser visibles
    static readonly string DataBaseName = "QMA";
    static readonly string Server = ".";

    static public bool StartConnection()
    {
        try
        {
            UserSQLConection = "Data Source=" + Server + "; Initial Catalog=" + DataBaseName + ";Trusted_Connection=True;";
            QMA = new GData(UserSQLConection);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
            throw;
        }
    }
}
