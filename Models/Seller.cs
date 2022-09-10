using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace CSharpUdemy_MVC.Models
{
    public class Seller
    {

        public int Id { get; set; }

        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Insira um {0} entre {2} e {1} caracteres!" )]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} precisa ser um campo válido!")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Range(100.0, 500000, ErrorMessage ="{0} está inválido! insira um valor entre {1} e {2}")]
        public double BaseSalary { get; set; }


        [Display(Name = "Departamento")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);

        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }


    }
}
