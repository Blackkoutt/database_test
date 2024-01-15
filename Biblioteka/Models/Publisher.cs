using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Models
{
	public class Publisher
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id wydawnictwa"),
			Range(0, 999)]
		public int publisherId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa"),
			MaxLength(30, ErrorMessage = "Nazwa nie może zawierać więcej niż 30 znaków")]
		public string name { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Opis"),
			MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
		public string description { get; set; }
		public ICollection<Book> books { get; set; } = new List<Book>();
	}
}
