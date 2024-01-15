using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class EmployeeConfirmingReturn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeConfirmingReturnId { get; set; }

        [Display(Name = "Pracownik potwierdzający odbiór"),]
        public Employee? employee { get; set; }
    }
}