namespace QualityManagementApp.Client.Services.Contracts;

public interface IPlanningService
{
    /// <value>Enum de los estados</value>
    public enum Status
    {
        En_Pausa = 1,
        En_Progreso = 2,
        Completada = 3,
        Incompleta = 4,
        Indefinido = 5
    }

    /// <value>Estado de la transacción</value>
    bool IsBusy { get; set; }

    /// <value>Valores de todas las planificaciones</value>
    List<Planning>? Plannings { get; set; }

    /// <value>Valores de una planificación</value>
    Planning Planning { get; set; }

    /// <value>Valores de una actividad</value>
    Activity Activity { get; set; }

    /// <value>Valores de los estados</value>
    List<QualityManagementApp.Shared.Model.Status> Statuses { get; set; }

    /// <summary>
    /// Obtiene todas las planificaciones
    /// </summary>
    Task GetPlannings();

    /// <summary>
    /// Obtiene una planificación
    /// </summary>
    Task GetPlanning(int planningId);

    /// <summary>
    /// Obtiene una actividad
    /// </summary>
    Task GetActivity(int activityId);

    /// <summary>
    /// Obtiene todos los estados
    /// </summary>
    Task GetStatuses();

    /// <summary>
    /// agrega una planificación
    /// </summary>
    Task AddPlanning();

    /// <summary>
    /// agrega una nueva actividad a la planificación
    /// </summary>
    void AddActivity();

    /// <summary>
    /// actualiza una actividad de una planificación
    /// </summary>
    Task UpdateActivity();

    /// <summary>
    /// actualiza el estado de una actividad
    /// </summary>
    Task UpdateActivityStatus();

    /// <summary>
    /// actualiza el estado de una planificación
    /// </summary>
    Task UpdatePlanningStatus(bool? check);
}
