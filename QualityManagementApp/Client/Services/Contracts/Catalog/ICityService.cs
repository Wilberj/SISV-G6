namespace QualityManagementApp.Client.Services.Contracts.Catalog;

public interface ICityService
{
    /// <value>Boolean</value>
    bool IsBusy { get; set; }

    /// <value>Valores de una ciudad</value>
    City City { get; set; }

    /// <value>Valores de todas las ciudades</value>
    List<City>? Cities { get; set; }


    /// <summary>
    /// Método para guardar una ciudad
    /// </summary>
    Task AddCity();

    /// <summary>
    /// Método que obtiene una ciduad
    /// </summary>
    Task GetCity(int? cityId);

    /// <summary>
    /// Método que obtiene todas las ciudades
    /// </summary>
    Task GetCities();

    /// <summary>
    /// Método que elimina una ciudad
    /// </summary>
    Task DeleteCity();

    /// <summary>
    /// Método que actualiza una ciudad
    /// </summary>
    Task<bool> UpdateCity();
}
