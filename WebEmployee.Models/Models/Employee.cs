using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEmployee.Models.Models
{
    public class Employee
    {

        public int Id { get; set; }

        [DisplayName("Numero de Empleado")]
        public string? EmployeeBadge { get; set; }

        [DisplayName("Carnet de Identidad")]
        public string? NationalId { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Apellido")]
        public string Lastname { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        public DateOnly DateofBirth { get; set; }


        [DisplayName("Ciudad de Nacimiento")]
        public string? BirthPlaceCity { get; set; }

        [DisplayName("Estado de Nacimiento")]
        public string? BirthPlaceState { get; set; }

        [DisplayName("Pais de Nacimiento")]
        public string? BirthPlaceCountry { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public DateTime? CreationTime { get; set; }


        [ValidateNever]
        public List<EmployeeImage> EmployeeImages { get; set; }

        public int? EmployeeTypeId { get; set; }
        [ForeignKey("EmployeeTypeId")]
        public EmployeeType? EmployeeType { get; set; }

        public int? BossId { get; set; }
        [ForeignKey("BossId")]
        public Employee? Boss { get; set; }

    }
}
