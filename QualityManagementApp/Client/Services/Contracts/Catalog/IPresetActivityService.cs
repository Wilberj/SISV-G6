using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface IPresetActivityService
    {
        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        //Snackbar Snackbar { get; set; }

        /// <value>Valores de un departamento</value>
        PresetActivity PresetActivity { get; set; }

        /// <value>Valores de todos los departamentos</value>
        PresetActivity[]? PresetActivities { get; set; }


        /// <summary>
        /// Método para guardar una actividad predefinida
        /// </summary>
        Task AddPresetActivity();

        /// <summary>
        /// Método que obtiene una actividad predefinida
        /// </summary>
        Task GetPresetActivity(string presetActivityId);

        /// <summary>
        /// Método que obtiene todas las actividades predefinidas
        /// </summary>
        Task GetPresetActivities();
    }
}
