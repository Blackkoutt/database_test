using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Tag
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id gatunku"),
			Range(0, 99)]
		public int tagId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa"),
			MaxLength(25, ErrorMessage = "Nazwa nie może zawierać więcej niż 25 znaków")]
		public string name { get; set; }

		public ICollection<Book_Tag> books { get; set; } = new List<Book_Tag>();
	}
}
