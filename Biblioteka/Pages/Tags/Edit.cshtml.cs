using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Tags
{
    public class EditModel : PageModel
    {
        private ITagRepository _tagRepository;

        public EditModel(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var tag =  _tagRepository.getOne(id);
            if (tag == null)
            {
                return NotFound();
            }
            Tag = tag;
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
                _tagRepository.Update(Tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(Tag.tagId))
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

        private bool TagExists(int id)
        {
            var isExisted = _tagRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
