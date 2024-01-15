using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace Biblioteka.Models
{
    public class Borrowing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Display(Name = "Id wypożyczenia"),
            Range(0, 99999)]
        public int borrowId { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Data wypożyczenia")]
        public DateTime borrowDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Planowana data zwrotu")]
        public DateTime plannedReturnDate { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Potwierdzenie odbioru")]
        public bool Confirmation { get; set; }
        [BindProperty(SupportsGet = true),
        Display(Name = "Potwierdzenie zwrotu")]
        public bool IsReturned { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Potwierdzenie opłaty")]
        public bool IsPaid { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Data zwrotu")]
        public DateTime? returnDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Książka")]
        public Book book { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Pracownik potwierdzający odbiór")]
        public Employee? employee { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Pracownik potwierdzający zwrot")]
        public EmployeeConfirmingReturn? employeeConfirmingReturn { get; set; }


        [BindProperty(SupportsGet = true),
        Display(Name = "Pracownik potwierdzający opłatę")]
        public EmployeeConfirmingPayment? employeeConfirmingPayment { get; set; }

        public ICollection<Reader_Borrowings> readers { get; set; } = new List<Reader_Borrowings>();
    }
}
