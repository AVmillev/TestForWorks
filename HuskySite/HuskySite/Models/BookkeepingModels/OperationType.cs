using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Models.BookkeepingModels
{
    public class OperationType
    {
        public Guid ID { get; set; }
        [Display(Name = "Название")]
        public string  Name { get; set; }
        [Display(Name = "Цвет")]
        public string Color { get; set; }
        [Display(Name = "Иконка")]
        public string Icon { get; set; }

        public ICollection<Accounting> Accountings { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
