using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Models.BookkeepingModels
{
    public class Account
    {

        public Guid ID { get; set; }


        [Display(Name = "ИНН")]
        public Int64 INN { get; set; }


        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }
        public Guid? OperationTypeID { get; set; }

        public ICollection<Accounting> Accountings { get; set; }
        [Display(Name = "Вид операции")]
        public OperationType OperationType { get; set; }
    }
}
