using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public string? Name { get; set; }
        public string? Contents { get; set; }
        public string? StartDate { get; set; }
        public string? FinishDate { get; set; }
        public int RemainingDays { get; set; }
        public bool IsImportant { get; set; }
        public bool IsUrgent { get; set; }
        public string? Status { get; set; }
    }
}
