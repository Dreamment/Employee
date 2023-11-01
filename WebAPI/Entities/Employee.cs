using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? ManagerId { get; init; }

        public ICollection<int>? SubordinatesIds { get; init; }


        [ForeignKey("ManagerId")]
        public Employee? Manager { get; init; }

        [ForeignKey("SubordinatesIds")]
        public ICollection<Employee>? Subordinates { get; init; }
    }
}
