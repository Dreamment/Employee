using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.Entities
{
    public record Employee
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Surname { get; init; }

        [Required]
        [MaxLength(11),MinLength(11)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string RegistrationNumber { get; init; }

        [AllowNull]
        public int? ManagerId { get; init; }
    }
}
