using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class EmployeeConfirmingPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeConfirmingPaymentId { get; set; }

        [Display(Name = "Pracownik potwierdzający opłatę"),]
        public Employee? employee { get; set; }
    }
}