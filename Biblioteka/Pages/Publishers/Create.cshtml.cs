using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public CreateModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid ||Publisher == null)
            {
                return Page();
            }
            _publisherRepository.Add(Publisher);
            return RedirectToPage("./Index");
        }
    }
}
