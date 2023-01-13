using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeData.DB.Entites
{
    [Table("Employee")]
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int _employeeNo { get; set; }

        [ConcurrencyCheck]
        public string _name { get; set; }

        [ConcurrencyCheck]
        public string _email { get; set; }

        [ConcurrencyCheck]
        public string _tel { get; set; }

        [ConcurrencyCheck]
        public DateTime _joined { get; set; }
    }
}
