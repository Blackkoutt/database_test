using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private ITagRepository _tagRepository;

        public DeleteModel(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [BindProperty]
      public Tag Tag { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var tag = _tagRepository.getOne(id);

            if (tag == null)
            {
                return NotFound();
            }
            else 
            {
                Tag = tag;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _tagRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
