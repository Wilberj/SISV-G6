using Position = QualityManagementApp.Shared.Model.Position;
namespace QualityManagementApp.Client.Services.Contracts.Catalog;

public interface IPositionService
{
    /// <value>Boolean</value>
    bool IsBusy { get; set; }

    /// <value>Valores de un cargo</value>
    Position Position { get; set; }

    /// <value>Valores de todos los cargos</value>
    List<Position>? Positions { get; set; }


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

    /// <summary>
    /// Método que elimina un departamento
    /// </summary>
    Task DeletePosition();

    /// <summary>
    /// Método que actualiza un departamento
    /// </summary>
    Task<bool> UpdatePosition();
}
