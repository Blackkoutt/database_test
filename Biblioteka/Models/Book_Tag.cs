using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Book_Tag
	{
        [BindProperty(SupportsGet = true),
            Display(Name = "Id książki")]
        public int bookId { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Id tagu")]
        public int tagId { get; set; }

		public Book book { get; set; }
		public Tag tag { get; set; }
	}
}
