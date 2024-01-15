using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Models
{
	public class Position
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id stanowiska"),
			Range(0, 9)]
		public int positionId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa stanowiska"),
			MaxLength(20, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
		public string name { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Pensja stanowiska"),
			Range(0, 9999, ErrorMessage = "Pensja nie może być ujemna ani większa niż 9999")]
		public int salary { get; set; }

		public ICollection<Employee> employees { get; set; } = new List<Employee>();
	}
}
