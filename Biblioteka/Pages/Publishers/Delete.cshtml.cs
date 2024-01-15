using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public DeleteModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
      public Publisher Publisher { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var publisher =  _publisherRepository.getOne(id);

            if (publisher == null)
            {
                return NotFound();
            }
            else 
            {
                Publisher = publisher;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {

            _publisherRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
