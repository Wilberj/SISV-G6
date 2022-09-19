using Microsoft.AspNetCore.Components.Web;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts
{
    public interface ISurveyService
    {
        /// <value>Enum de los tipos de respuestas</value>
        public enum TypeAnswer
        {
            Cerradas = 1,
            Abiertas = 2,
            Calificativa_cuantitativa = 3,
            Calificativa_cualitativa = 4
        }

        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        Snackbar Snackbar { get; set; }

        /// <value>Valores de una encuesta</value>
        Survey Survey { get; set; }

        /// <value>Valores de todas las encuestas</value>
        Survey[]? Surveys { get; set; }

        /// <value>Valores de todas las categorias de la encuesta</value>
        SurveyCategory[] SurveyCategories { get; set; }

        /// <value>Valores de todos los tipos de preguntas</value>
        TypeQA[] TypesQA { get; set; }

        /// <value>Valores de una pregunta</value>
        Question Question { get; set; }

        /// <value>Valores de todas las preguntas</value>
        List<Question> Questions { get; set; }

        /// <value>Valores de todas las posibles respuestas</value>
        Answer[]? AnswersByTypeQA { get; set; }

        Interviewed Interviewed { get; set; }

        /// <value>Valores de todas las respuestas seleccionadas</value>
        List<SelectedAnswer> SelectedAnswers { get; set; }

        /// <summary>
        /// Método para agregar una respuesta seleccionada
        /// </summary>
        void AddSelectedAnswer(int? questionId, int? answerId);

        /// <summary>
        /// Método que agrega todas las respuestas seleccionadas por un invitado
        /// </summary>
        Task AddSelectedAnswers();

        /// <summary>
        /// Método que agrega los datos de un invitado
        /// </summary>
        Task<int> AddInterviewed();

        /// <summary>
        /// Método que agrega todas las respuestas seleccionadas por un invitado
        /// </summary>
        /// <returns>Array de las respuestas por el tipo: Answer[]</returns>
        Task<Answer[]> ReturnAnswersByTypeQA(int? typeQAId);

        /// <summary>
        /// Método que obtiene todas las posibles respuestas
        /// </summary>
        Task GetAnswersByTypeQA(int? typeQAId);

        /// <summary>
        /// Método para agregar una nueva pregunta
        /// </summary>
        void AddQuestion();

        /// <summary>
        /// Método que obtiene todos los tipos de preguntas
        /// </summary>
        Task GetTypesQA();

        /// <summary>
        /// Método para guardar una encuesta y sus preguntas
        /// </summary>
        Task AddSurvey();

        /// <summary>
        /// Método que obtiene una encuesta
        /// </summary>
        Task GetSurvey(string surveyId);

        /// <summary>
        /// Método que obtiene todas las encuestas
        /// </summary>
        Task GetSurveys();

        /// <summary>
        /// Método que obtiene todas las categorias de la encuesta
        /// </summary>
        Task GetSurveyCategories();

        /// <summary>
        /// Método para generar el código de la encuesta a partir del título y la fecha actual
        /// </summary>
        /// <param name="focusEvent"></param>
        void GenerateCode(FocusEventArgs focusEvent);
    }
}
