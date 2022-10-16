using QualityManagementApp.ADO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityManagementApp.Shared
{
    public class Model
    {
        public class PresetActivity : Entity
        {
            public int? PkPresetActivity { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? QualityExpected { get; set; }
        }

        public class Status : Entity
        {
            public int? PkStatus { get; set; }
            public string Title { get; set; } = null!;
        }

        public class Answer : Entity
        {
            public int? PkAnswer { get; set; }
            public string AnswerName { get; set; } = null!;
            public DateTime? CreationDate { get; set; }
            public int? FkTypeQA { get; set; }
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
            public string Name { get; set; } = null!;
            public int? FkDepartment { get; set; }
        }

        public class Department : Entity
        {
            public int? PkDepartment { get; set; }
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
            public string PkActivity { get; set; } = null!;
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? FinishDate { get; set; }
            public string? QualityExpected { get; set; }
            public string? QualityObtained { get; set; }
            public DateTime? CompletedDate { get; set; }
            public DateTime? CreationDate { get; set; }
            public DateTime? LastModificacionDate { get; set; }
            public string? Observation { get; set; }
            public string FkPlanning { get; set; } = null!;
            public int? FkStatus { get; set; }
        }

        public class ActivityDetail : Entity
        {
            public int? PkActivityDetail { get; set; }
            public string FkActivity { get; set; } = null!;
            public int? FkEmployee { get; set; } = null!;
        }

        public class Employee : Entity
        {
            public int? PkEmployee { get; set; } = null!;
            public string IdentificationCard { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string SecondName { get; set; } = null!;
            public string FirstSurname { get; set; } = null!;
            public string SecondSurname { get; set; } = null!;
            public string HomeAddress { get; set; } = null!;
            public string Telephone { get; set; } = null!;
            public DateTime? CreationDate { get; set; }
            public DateTime? LastModificationDate { get; set; }
            public int? FkCity { get; set; }
            public int? FkPosition { get; set; }
        }

        public class Interviewed : Entity
        {
            public int? PkInterviewed { get; set; }
            public string Name { get; set; } = null!;
            public DateTime? CreationDate { get; set; }
            public string? Observation { get; set; }
        }

        public class Planning : Entity
        {
            public string PkPlanning { get; set; } = null!;
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public int? Month { get; set; }
            public int? Year { get; set; }
            public string? QualityExpected { get; set; }
            public string? QualityObtained { get; set; }
            public string? PercentageCompleted { get; set; }
            public bool? IsVerified { get; set; }
            public DateTime? CreationDate { get; set; }
            public DateTime? LastModificationDate { get; set; }
            public string? Observation { get; set; }
            public int? FkStatus { get; set; }
        }

        public class PlanningMonitoring : Entity
        {
            public int? PkPlanningMonitoring { get; set; }
            public string? Efficiency { get; set; }
            public string? Observation { get; set; }
            public DateTime? MonitoringDate { get; set; }
            public string FkPlanning { get; set; } = null!;
        }

        public class Question : Entity
        {
            public int? PkQuestion { get; set; }

            [Required(ErrorMessage = "Requerido")]
            public string Title { get; set; } = null!;

            public DateTime? CreationDate { get; set; }

            [Required(ErrorMessage = "Requerido")]
            public int? FkTypeQA { get; set; }

            public string FkSurvey { get; set; } = null!;
        }
 
        public class Survey : Entity
        {
            public string PkSurvey { get; set; } = null!;

            [Display(Name="Título")]
            [Required(ErrorMessage = "Requerido")]
            public string Title { get; set; } = null!;

            public string? Description { get; set; }

            public DateTime? CreationDate { get; set; }

            public DateTime? LastModificationDate { get; set; }

            [Required]
            public bool Status { get; set; }

            [Required(ErrorMessage = "Requerido")]
            public int? FkSurveyCategory { get; set; }
        }

        public class UserModel
        {
            public UserModel()
            {

            }

            public UserModel(UserDetail tableuser)
            {
                this.Username = tableuser.Username;
                this.TypeUser = tableuser.FkUserType;
                this.CreationDate = tableuser.CreationDate;
                this.LastModificationDate = tableuser.LastModificationDate;
            }

            public string? Username { get; set; }
            public int? TypeUser { get; set; }
            public DateTime? CreationDate { get; set; }
            public DateTime? LastModificationDate { get; set; }
        }

        public class UserDetail : Entity
        {
            public int? PkUser { get; set; }

            [Required(ErrorMessage = "Requerido")]
            public string Username { get; set; } = null!;

            [Required(ErrorMessage = "Requerido")]
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
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
    }
}
