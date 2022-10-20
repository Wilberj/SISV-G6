using QualityManagementApp.ADO;

namespace QualityManagementApp.Shared;

public class SurveyInsightModel
{
    public class Chart : Entity
    {
        public int Total { get; set; }
        public string Answer { get; set; } = null!;
    }

    public record class ToDo(string survey, string? question);
}
