using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Models.BookkeepingModels
{
    public class PatternSMS
    {
        public Guid ID { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Текст")]
        public string Value { get; set; }

        public ICollection<Wallet> Wallets { get; set; }
    }
}
