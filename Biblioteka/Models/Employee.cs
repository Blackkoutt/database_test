using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Biblioteka.Models
{
	public class Employee
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id pracownika"),
			Range(0, 99)]
		public int employeeId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Imię"),
			MaxLength(20, ErrorMessage = "Imię nie może zawierać więcej niż 20 znaków")]
		public string name { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwisko"),
			MaxLength(40, ErrorMessage = "Nazwisko nie może zawierać więcej niż 40 znaków")]
		public string surname { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Adres e-mail"),
            EmailAddress,
            MaxLength(40, ErrorMessage = "E-mail nie może zawierać więcej niż 40 znaków")]
        public string? email { get; set; }

        [BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Data zatrudnienia")]
		public DateTime dateOfEmployment { get; set; }

		[BindProperty(SupportsGet = true),
			Display(Name = "Stanowisko")]
		public Position? position { get; set; }
		public EmployeeData? employeeData { get; set; }
		public ICollection<RoomReservation> reservations { get; set; } = new List<RoomReservation>();
		public ICollection<Borrowing> borrowings { get; set; } = new List<Borrowing>();

        public ICollection<EmployeeConfirmingReturn> returnConfirmations { get; set; } = new List<EmployeeConfirmingReturn>();

        public ICollection<EmployeeConfirmingPayment> paymentConfirmation { get; set; } = new List<EmployeeConfirmingPayment>();

        public string FullName
        {
            get
            {
                return $"{name} {surname}";
            }
        }
    }
}
