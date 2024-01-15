using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Author
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id autora"),
			Range(0, 999)]
		public int authorId { get; set; }

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
			Required,
			Display(Name = "Data urodzenia")]
		public DateTime birthDate { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Kraj pochodzenia"),
			MaxLength(30, ErrorMessage = "Kraj nie może zawierać więcej niż 30 znaków")]
		public string country { get; set; }

		[BindProperty(SupportsGet = true),
			Display(Name = "Pseudonim"),
			MaxLength(35, ErrorMessage = "Pseudonim nie może zawierać więcej niz 35 znaków")]
		public string? nickname { get; set; }

		[BindProperty(SupportsGet = true),
			Display(Name = "Opis"),
			MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
		public string? description { get; set; }
		public ICollection<Book_Author> books { get; set; } = new List<Book_Author>();
    }
}
