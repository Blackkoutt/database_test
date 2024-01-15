using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace Biblioteka.Models
{
	public class RoomReservation
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id rezerwacji"),
			Range(0, 9999)]
		public int reservationId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Wynajmujący czytelnik")]
		public Reader reader { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Wynajmowana sala")]
		public Room room { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Data początku rezerwacji")]
		public DateTime begginingOfReservationDate { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Data końca rezerwacji")]
		public DateTime endOfReservationDate { get; set; }

		[BindProperty(SupportsGet = true),
		Display(Name = "Pracownik nadzorujący wynajęcie")]
		public Employee? employee { get; set; }


        [BindProperty(SupportsGet = true),
        Display(Name = "Potwierdzenie wydania klucza")]
        public bool Confirmation { get; set; }
    }
}
