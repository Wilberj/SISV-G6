using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface IPositionService
    {
        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        //Snackbar Snackbar { get; set; }

        /// <value>Valores de un cargo</value>
        Position Position { get; set; }

        /// <value>Valores de todos los cargos</value>
        Position[]? Positions { get; set; }


        /// <summary>
        /// Método para guardar un cargo
        /// </summary>
        Task AddPosition();

        /// <summary>
        /// Método que obtiene un cargo
        /// </summary>
        Task GetPosition(int? positionId);

        /// <summary>
        /// Método que obtiene todos los cargos
        /// </summary>
        Task GetPositions();
    }
}
