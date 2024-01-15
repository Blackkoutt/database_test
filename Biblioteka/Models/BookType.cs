using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class BookType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id rodzaju"),
			Range(0, 9)]
		public int typeId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa"),
			MaxLength(20, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
		public string name { get; set; }
		public ICollection<Book> books { get; set; } = new List<Book>();
	}
}
