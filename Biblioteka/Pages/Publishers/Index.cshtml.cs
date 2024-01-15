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
    public class IndexModel : PageModel
    {
        private IPublisherRepository _publisherRepository;

        public IndexModel(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public void OnGet  (string? publisherName)
        {
            if (!string.IsNullOrEmpty(publisherName))
            {
                Publisher = new List<Publisher> { _publisherRepository.searchPublisher(publisherName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Publisher = _publisherRepository.getAll();
            }
        }
    }
}
