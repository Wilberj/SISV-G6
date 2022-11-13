namespace QualityManagementApp.Client.Services.Contracts;

public interface ISecurityService
{
    /// <value>Enum de los tipos de usuarios</value>
    public enum UserTypeEnum
    {
        Tecnico_Calidad = 1,
        Director = 2
    }

    /// <value>Estado de la transacción</value>
    bool IsBusy { get; set; }

    /// <value>Mensaje de respuesta</value>
    string? Message { get; set; }

    /// <value>Valores de un Usuario</value>
    UserDetail User { get; set; }

    /// <value>Valores de los logs de un usuario</value>
    List<UserLog>? Logs { get; set; }

    /// <summary>
    /// Iniciar sesión en el sistema
    /// </summary>
    Task Login();

    /// <summary>
    /// Cerrar sesión en el sistema
    /// </summary>
    void Logout();

    /// <summary>
    /// Registrar log de una transacción
    /// </summary>
    Task AddLog(string message);

    /// <summary>
    /// Obtener todos los logs de un usuario
    /// </summary>
    Task GetLogs();

    /// <summary>
    /// Actualizar las credenciales de un usuario
    /// </summary>
    /// <param name="password"></param>
    Task UpdateUser(string password);
}
