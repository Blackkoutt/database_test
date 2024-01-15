using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public EditModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGet(int id)
        {

            var publisher =  _publisherRepository.getOne(id);
            if (publisher == null)
            {
                return NotFound();
            }
            Publisher = publisher;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            try
            {
                _publisherRepository.Update(Publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(Publisher.publisherId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PublisherExists(int id)
        {
            var isExisted = _publisherRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
