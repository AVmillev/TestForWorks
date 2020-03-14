using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Models.BookkeepingModels
{
    public enum PaymentType { Cash, Card, CreditCard }
    public class Wallet
    {
        public Guid ID { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        public string UserID { get; set; }
        public Guid? PatternSMSID { get; set; }

        [Display(Name = "Баланс")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }

        [Display(Name = "Вид средств")]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Шаблон СМС")]
        public PatternSMS PatternSMS { get; set; }
        public ICollection<Accounting> Accountings { get; set; }



    }

}
