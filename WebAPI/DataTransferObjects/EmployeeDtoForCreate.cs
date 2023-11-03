using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.DataTransferObjects
{
    public class EmployeeDtoForCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string RegistrationNumber { get; set; }

        [AllowNull]
        public int? ManagerId { get; set; }
    }
}
