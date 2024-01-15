using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Reader
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Key,
			Display(Name = "ID czytelnika"),
			Range(0, 9999)]
		public int readerId { get; set; }

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
			Display(Name = "Data urodzenia")]
		public DateTime? birthDate { get; set; }

		public ICollection<Reader_Borrowings> borrowings { get; set; } = new List<Reader_Borrowings>();
		public ICollection<RoomReservation> reservations { get; set; } = new List<RoomReservation>();
        public ICollection<Book_Opinions> bookOpinions { get; set; } = new List<Book_Opinions>();

        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                return $"{name} {surname}";
            }
        }
    }
}
