using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using static QualityManagementApp.Shared.SurveyInsightModel;

namespace QualityManagementApp.Client.Services.Contracts
{
    public interface ISurveyInsightsService
    {
        /// <value>Estado de la transacción</value>
        bool IsBusy { get; set; }

        /// <value>Valores de una encuesta</value>
        //Survey Survey { get; set; }

        /// <value>Valores de todas las preguntas</value>
        List<Question> Questions { get; set; }

        /// <value>Valores de las estadisticas</value>
        List<SurveyInsightModel> Insight {get; set; }

        /// <summary>
        /// Obtiene todas las preguntas apartir de una encuesta
        /// </summary>
        /// <param name="surveyId"></param>
        Task GetQuestions(string? surveyId);

        /// <summary>
        /// Retorna la estadistica de una pregunta
        /// </summary>
        /// <param name="SurveyId"></param>
        /// <param name="question"></param>
        List<Chart> ReturnInsight(string SurveyId, int? question);
    }
}
