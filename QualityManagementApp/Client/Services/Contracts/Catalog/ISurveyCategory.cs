namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface ISurveyCategory
    {
        /// <value>Estado de la transacción</value>
        bool IsBusy { get; set; }

        /// <value>Valores de una Categoria de encuesta</value>
        SurveyCategory SurveyCategory { get; set; }

        /// <value>Valores de todas las categorias de encuestas</value>
        List<SurveyCategory>? SurveyCategories { get; set; }

        /// <summary>
        /// Método para guardar una categoria de encuesta
        /// </summary>
        Task AddSurveyCategory();

        /// <summary>
        /// Método que obtiene una categoria de encuesta
        /// </summary>
        Task GetSurveyCategory(int? surveyCategoryId);

        /// <summary>
        /// Método que obtiene todas las categorias de encuesta
        /// </summary>
        Task GetSurveyCategories();

        /// <summary>
        /// Método que elimina una categoria de encuesta
        /// </summary>
        Task DeleteSurveyCategory();

        /// <summary>
        /// Método que actualiza una categoria de encuesta
        /// </summary>
        Task<bool> UpdateSurveyCategory();
    }
}
