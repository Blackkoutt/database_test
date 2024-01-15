using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Models
{
	public class Genre
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id gatunku"),
			Range(0, 99)]
		public int genreId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa"),
			MaxLength(30, ErrorMessage = "Nazwa nie może zawierać więcej niż 30 znaków")]
		public string name { get; set; }
		public ICollection<Book> books { get; set; } = new List<Book>();
	}
}
