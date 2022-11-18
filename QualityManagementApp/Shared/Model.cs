using QualityManagementApp.ADO;
using System.ComponentModel.DataAnnotations;

namespace QualityManagementApp.Shared;

public class Model
{
    public class PresetActivity : Entity
    {
        public int? PkPresetActivity { get; set; }

        [Required(ErrorMessage = "El título es Requerido")]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public int? QualityExpected { get; set; }
    }

    public class Status : Entity
    {
        public int? PkStatus { get; set; }
        public string Title { get; set; } = null!;
    }

    public class SurveyCategory : Entity
    {
        public int? PkSurveyCategory { get; set; }
        public string Title { get; set; } = null!;
    }

    public class TypeQA : Entity
    {
        public int? PkTypeQA { get; set; }
        public string Title { get; set; } = null!;
    }

    public class UserType : Entity
    {
        public int? PkUserType { get; set; }
        public string Title { get; set; } = null!;
    }

    public class City : Entity
    {
        public int? PkCity { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es Requerido")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El Departamento es Requerido")]
        public int? FkDepartment { get; set; }

        public Department? Department { get; set; }
    }

    public class Department : Entity
    {
        public int? PkDepartment { get; set; }

        [Required(ErrorMessage = "El nombre es Requerido")]
        public string Name { get; set; } = null!;
    }

    public class SelectedAnswer : Entity
    {
        public int? PkSelectedAnswer { get; set; }
        public int? FkInterviewed { get; set; }
        public int? FkQuestion { get; set; }
        public int? FkAnswer { get; set; }
    }

    public class Activity : Entity
    {
        public int? PkActivity { get; set; }
        public int? ActivityId { get; set; }

        [Required(ErrorMessage = "El título es Requerido")]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es Requerida")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin es Requerida")]
        public DateTime? FinishDate { get; set; }

        public int? QualityExpected { get; set; }
        public int? QualityObtained { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string? Observation { get; set; }
        public int? PredecessorId { get; set; }
        public int? FkPlanning { get; set; }
        public int? FkStatus { get; set; }
        public Status? Status { get; set; }
        public List<ActivityDetail>? Details { get; set; }
    }

    public class ActivityDetail : Entity
    {
        public int? PkActivityDetail { get; set; }
        public int? FkActivity { get; set; } = null!;
        public int? FkEmployee { get; set; } = null!;
        public Employee? Employee { get; set; }
    }

    public class Employee : Entity
    {
        public int? PkEmployee { get; set; } = null!;

        [Required(ErrorMessage = "La Cédula es Requerida")]
        public string IdentificationCard { get; set; } = null!;

        [Required(ErrorMessage = "El Primer nombre es Requerido")]
        public string FirstName { get; set; } = null!;

        public string? SecondName { get; set; }

        [Required(ErrorMessage = "El Primer apellido es Requerido")]
        public string FirstSurname { get; set; } = null!;
        public string? SecondSurname { get; set; }
        public string? HomeAddress { get; set; }
        public string? Telephone { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        [Required(ErrorMessage = "La ciudad es Requerida")]
        public int? FkCity { get; set; }
        public City? City { get; set; }

        [Required(ErrorMessage = "El cargo es Requerido")]
        public int? FkPosition { get; set; }
        public Position? Position { get; set; }
    }

    public class Interviewed : Entity
    {
        public int? PkInterviewed { get; set; }

        [Required(ErrorMessage = "Su nombre es Requerido")]
        public string Name { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public string? Observation { get; set; }
    }

    public class Observation : Entity
    {
        public string Name { get; set; } = null!;
        public string ObservationText { get; set; } = null!;
    }

    public class Planning : Entity
    {
        public int? PkPlanning { get; set; }

        [Required(ErrorMessage = "El título es Requerido")]
        public string? Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "El mes es Requerido")]
        public int? Month { get; set; }

        [Required(ErrorMessage = "El año es Requerido")]
        public int? Year { get; set; }

        public int? QualityExpected { get; set; }
        public float? QualityObtained { get; set; }
        public float? PercentageCompleted { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string? Observation { get; set; }
        public int? FkStatus { get; set; }
        public Status? Status { get; set; }
        public List<Activity>? Activities { get; set; }
    }

    public class PlanningMonitoring : Entity
    {
        public int? PkPlanningMonitoring { get; set; }
        public string? Efficiency { get; set; }
        public string? Observation { get; set; }
        public DateTime? MonitoringDate { get; set; }
        public int FkPlanning { get; set; }
    }

    public class Answer : Entity
    {
        public int? PkAnswer { get; set; }
        public string AnswerName { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public int? FkTypeQA { get; set; }
    }

    public class Question : Entity
    {
        public int? PkQuestion { get; set; }

        [Required(ErrorMessage = "La pregunta es Requerida")]
        public string Title { get; set; } = null!;

        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? FkTypeQA { get; set; }

        public string FkSurvey { get; set; } = null!;
    }

    public class SurveyToUser : Entity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TotalAnswered { get; set; }
        public List<QuestionToUser>? Questions { get; set; }
    }

    public class QuestionToUser : Entity
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public int Type { get; set; }
        public List<ChartToUser>? Charts { get; set; }
    }

    public class ChartToUser : Entity
    {
        public string Answer { get; set; } = null!;
        public int Total { get; set; }
    }

    public class SurveyToInterviewed : Entity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public List<QuestionToInterviewed>? Questions { get; set; }
    }

    public class QuestionToInterviewed : Entity
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public int Type { get; set; }
        public List<AnswerToInterviewed>? Answers { get; set; }
    }

    public class AnswerToInterviewed : Entity
    {
        public int Id { get; set; }
        public string Answer { get; set; } = null!;
    }

    public class Survey : Entity
    {
        public string PkSurvey { get; set; } = null!;

        [Required(ErrorMessage = "El título es Requerido")]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModificationDate { get; set; }

        public bool? Status { get; set; } = null;

        [Required(ErrorMessage = "La categoría es Requerida")]
        public int? FkSurveyCategory { get; set; }

        public SurveyCategory? SurveyCategory { get; set; }

        public List<Question>? Questions { get; set; }
    }

    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(UserDetail tableuser)
        {
            this.Id = tableuser.PkUser;
            this.Username = tableuser.Username;
            this.TypeUser = tableuser.FkUserType;
            this.CreationDate = tableuser.CreationDate;
            this.LastModificationDate = tableuser.LastModificationDate;
        }

        public int? Id { get; set; }
        public string? Username { get; set; }
        public int? TypeUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
    }

    public class UserDetail : Entity
    {
        public int? PkUser { get; set; }

        [Required(ErrorMessage = "El usuario es Requerido")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es Requerida")]
        public string Password { get; set; } = null!;

        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public int? FkUserType { get; set; } = null!;
    }

    public class UserLog : Entity
    {
        public int? PkUserLog { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? FkUser { get; set; }
    }

    public class Position : Entity
    {
        public int? PkPosition { get; set; }

        [Required(ErrorMessage = "El título es Requerido")]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
