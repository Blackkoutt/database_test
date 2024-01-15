using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages
{
    public class alelatwoModel : PageModel
    {

        public alelatwoModel()
        {
            
        }

        public void OnGet()
        {
        }

        // do sprawdzenia
        public IActionResult OnPostAdd()
        {
            return RedirectToPage();
        }

        // dziala
        public IActionResult OnPostUpdate()
        {
            return RedirectToPage();
        }

        // dziala
        public IActionResult OnPostDelete(int employeeId)
        {
            return RedirectToPage();
        }

        public void OnPostListAll()
        {
        }

    }
}