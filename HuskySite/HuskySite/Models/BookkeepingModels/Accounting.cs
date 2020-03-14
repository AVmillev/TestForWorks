using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Models.BookkeepingModels
{
    public enum AccountingTypes { Incoming, Expense }
    public enum Currencyes { rub, eur, usd}
    public class Accounting
    {
        public Guid ID { get; set; }
        public Guid? WalletID { get; set; }


        [Display(Name = "Валюта")]
        public Currencyes Currency { get; set; }

        [Display(Name = "Дата и время операции")]
        public DateTime? DateTimeOperation { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        //[Required]
        [Display(Name = "Цена")]
        [Range(typeof(decimal),"0,0","9999999,0",ErrorMessage ="Цена не может быть меньше 0,01")]
        public decimal? Summ { get; set; }

        [Display(Name = "ФН№")]
        public string FN { get; set; }

        [Display(Name = "ФД№")]
        public string FD { get; set; }

        [Display(Name = "ФП#")]
        public string FP { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? OperationTypeID { get; set; }
        public string RecieptJSON { get; set; }

        [Display(Name = "Кошелек")]
        public Wallet Wallet { get; set; }

        [Display(Name = "Контрагент")]
        public Account Account { get; set; }        
        public AccountingTypes AccountingType { get; set; }
        [Display(Name ="Вид операции")]
        public OperationType OperationType { get; set; }

    }
}
