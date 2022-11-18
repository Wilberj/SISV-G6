namespace QualityManagementApp.ADO;

public class Entity
{
    public List<T> Get<T>(string? condition = "", List<string>? notMapped = null)
    {
        var Data = SQLConnection.QMA.TakeList<T>(this, notMapped, condition);
        return Data;
    }

    public T? FindObject<T>()
    {
        var Data = SQLConnection.QMA.TakeObject<T>(this);
        return Data ?? default;
    }

    /// <summary>
    /// Retorna la tabla de un procedimiento almacenado
    /// </summary>
    /// <param name="procedure"></param>
    /// <param name="parameters"></param>
    public List<T> GetProcedure<T>(string procedure, List<object>? parameters = null)
    {
        return SQLConnection.QMA.ExecuteSqlProcedure<T>(procedure, this, parameters);
    }

    public object Save(List<string>? notMapped = null)
    {
        return SQLConnection.QMA.InsertObject(this, notMapped);
    }

    public bool Update(string ID)
    {
        SQLConnection.QMA.UpdateObject(this, ID);
        return true;
    }

    public bool Update(string[] ID)
    {
        SQLConnection.QMA.UpdateObject(this, ID);
        return true;
    }

    public bool Delete()
    {
        SQLConnection.QMA.Delete(this);
        return true;
    }
}
