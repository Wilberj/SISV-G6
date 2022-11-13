using QualityManagementApp.ADO;
namespace QualityManagementApp.Shared;

public static class Auth
{
    public static bool VerifyConnection()
    {
        if (SQLConnection.QMA == null)
        {
            SQLConnection.QMA = null!;
            return false;
        }
        return true;
    }

    public static bool StartConnection()
    {
        try
        {
            SQLConnection.StartConnection();
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;               
        }           
    }
}
