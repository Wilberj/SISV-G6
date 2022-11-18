namespace QualityManagementApp.Client.Services.Contracts.Catalog;

public interface IPresetActivityService
{
    /// <value>Estado de la transacción</value>
    bool IsBusy { get; set; }

    /// <value>Valores de una actividad preestablecida</value>
    PresetActivity PresetActivity { get; set; }

    /// <value>Valores de todas las actividades preestablecidas</value>
    List<PresetActivity>? PresetActivities { get; set; }

    /// <summary>
    /// Método para guardar una actividad predefinida
    /// </summary>
    Task AddPresetActivity();

    /// <summary>
    /// Método que obtiene una actividad predefinida
    /// </summary>
    Task GetPresetActivity(int? presetActivityId);

    /// <summary>
    /// Método que obtiene todas las actividades predefinidas
    /// </summary>
    Task GetPresetActivities();

    /// <summary>
    /// Método que elimina una actividad predefinida
    /// </summary>
    Task DeletePresetActivity();

    /// <summary>
    /// Método que actualiza una actividad predefinida
    /// </summary>
    Task<bool> UpdatePresetActivity();
}
