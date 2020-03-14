using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HuskySite.Helpers
{
    public partial class ReceiptToClass
    {
        [JsonProperty("document")]
        public Document Document { get; set; }
    }

    public partial class Document
    {
        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }
    }

    public partial class Receipt
    {
        [JsonProperty("ecashTotalSum")]
        public long EcashTotalSum { get; set; }

        [JsonProperty("requestNumber")]
        public long RequestNumber { get; set; }

        [JsonProperty("dateTime")]
        public System.DateTimeOffset DateTime { get; set; }

        [JsonProperty("fiscalDocumentNumber")]
        public long FiscalDocumentNumber { get; set; }

        [JsonProperty("shiftNumber")]
        public long ShiftNumber { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("totalSum")]
        public long TotalSum { get; set; }

        [JsonProperty("kktRegId")]
        public string KktRegId { get; set; }

        [JsonProperty("nds10")]
        public long Nds10 { get; set; }

        [JsonProperty("userInn")]
        public string UserInn { get; set; }

        [JsonProperty("taxationType")]
        public long TaxationType { get; set; }

        [JsonProperty("operationType")]
        public long OperationType { get; set; }

        [JsonProperty("fiscalDriveNumber")]
        public string FiscalDriveNumber { get; set; }

        [JsonProperty("receiptCode")]
        public long ReceiptCode { get; set; }

        [JsonProperty("retailPlaceAddress")]
        public string RetailPlaceAddress { get; set; }

        [JsonProperty("nds18")]
        public long Nds18 { get; set; }

        [JsonProperty("cashTotalSum")]
        public long CashTotalSum { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("fiscalSign")]
        public long FiscalSign { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("nds10")]
        public long? Nds10 { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("sum")]
        public long Sum { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nds18")]
        public long? Nds18 { get; set; }
    }

    public partial class ReceiptToClass
    {
        public static ReceiptToClass FromJson(string json) => JsonConvert.DeserializeObject<ReceiptToClass>(json, HuskySite.Helpers.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ReceiptToClass self) => JsonConvert.SerializeObject(self, HuskySite.Helpers.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
