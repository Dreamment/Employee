namespace WebAPI.DataTransferObjects
{
    public class EmployeeDtoForGet
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string RegistrationNumber { get; init; }

        public int? ManagerId { get; init; }

        public List<int>? SubordinatesIds { get; set; }
    }
}
