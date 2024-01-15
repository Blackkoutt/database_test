using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Reader_Borrowings
	{
        [BindProperty(SupportsGet = true),
            Display(Name = "Id czytelnika")]
        public int readerId { get; set; }
        [BindProperty(SupportsGet = true),
            Display(Name = "Id wypożyczenia")]
        public int borrowId { get; set; }
        
		public Reader reader { get; set; }

		public Borrowing borrow { get; set; }
	}
}
