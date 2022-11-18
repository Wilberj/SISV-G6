namespace QualityManagementApp.Client.Services.Contracts;

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

    /// <value>Estado de la transacción</value>
    bool IsBusy { get; set; }

    /// <value>Valores de una encuesta</value>
    Survey Survey { get; set; }

    /// <value>Valores de una encuesta mostrada al invitado</value>
    SurveyToInterviewed SurveyToInterviewed { get; set; }

    /// <value>Valores de una encuesta mostrada a un usario del sistema</value>
    SurveyToUser SurveyToUser { get; set; }

    /// <value>Valores de todas las encuestas</value>
    List<Survey>? Surveys { get; set; }

    /// <value>Valores de todas las categorias de la encuesta</value>
    SurveyCategory[] SurveyCategories { get; set; }

    /// <value>Valores de todos los tipos de preguntas</value>
    TypeQA[] TypesQA { get; set; }

    /// <value>Valores de una pregunta</value>
    Question Question { get; set; }

    /// <value>Valores de todas las posibles respuestas</value>
    Answer[]? AnswersByTypeQA { get; set; }

    /// <value>Valores de un invitado</value>
    Interviewed Interviewed { get; set; }

    /// <value>Valores de todas las respuestas seleccionadas</value>
    List<SelectedAnswer> SelectedAnswers { get; set; }

    /// <value>Valores de todos los comentarios de una encuesta</value>
    List<Observation>? Observations { get; set; }

    /// <summary>
    /// Agrega una respuesta seleccionada
    /// </summary>
    /// /// <param name="questionId"></param>
    /// /// <param name="answerId"></param>
    void AddSelectedAnswer(int? questionId, int? answerId);

    /// <summary>
    /// Agrega al invitado y todas las respuestas seleccionadas por un invitado
    /// </summary>
    Task AddSelectedAnswersInterviewed();

    /// <summary>
    /// Obtiene todas las posibles respuestas apartir del Id de un tipo de QA
    /// </summary>
    Task GetAnswersByTypeQA(int? typeQAId);

    /// <summary>
    /// Agrega una nueva pregunta a la encuesta
    /// </summary>
    void AddQuestion();

    /// <summary>
    /// Obtiene todos los tipos de preguntas
    /// </summary>
    Task GetTypesQA();

    /// <summary>
    /// Guardar una encuesta y sus preguntas
    /// </summary>
    Task AddSurvey();

    /// <summary>
    /// Obtiene una encuesta apartir de su Id
    /// </summary>
    /// <param name="surveyId"></param>
    /// <param name="isTest"></param>
    Task GetSurvey(string surveyId, bool? isTest);

    /// <summary>
    /// Obtiene las estadisticas de una encuesta
    /// </summary>
    /// <param name="surveyId"></param>
    Task GetSurveyInsights(string surveyId);

    /// <summary>
    /// Obtiene todas las encuestas
    /// </summary>
    Task GetSurveys();

    /// <summary>
    /// Obtiene todas las categorias de la encuesta
    /// </summary>
    Task GetSurveyCategories();

    /// <summary>
    /// Generar el código de la encuesta a partir del título y la fecha actual
    /// </summary>
    /// <param name="callbackString"></param>
    void GenerateCode(string callbackString);

    /// <summary>
    /// Obtiene todos los comentarios de una encuesta
    /// </summary>
    /// <param name="surveyId"></param>
    Task GetAllObservations(string surveyId);
}
