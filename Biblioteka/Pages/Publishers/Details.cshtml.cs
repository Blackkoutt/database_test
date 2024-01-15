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
    public class DetailsModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public DetailsModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public Publisher Publisher { get; set; } = default!; 

        public IActionResult OnGet(int id)
        {

            var publisher = _publisherRepository.getOne(id);
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
    }
}
