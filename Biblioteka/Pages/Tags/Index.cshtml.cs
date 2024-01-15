using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private ITagRepository _tagRepository;

        public IndexModel(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public void OnGet(string? tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
            {
                Tag = new List<Tag> { _tagRepository.searchTag(tagName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Tag = _tagRepository.getAll();
            }
        }
    }
}
