using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Book_Author
	{
		[BindProperty(SupportsGet = true),
			Display(Name = "Id książki")]
		public int bookId { get; set; }
        [BindProperty(SupportsGet = true),
            Display(Name = "Id autora")]
        public int authorId { get; set; }
        public Book book { get; set; }	
		public Author author { get; set; }
	}
}
