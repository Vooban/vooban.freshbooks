using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using Vooban.FreshBooks.Models;
using Vooban.FreshBooks.Task.Models;

namespace Vooban.FreshBooks.Invoices.Models
{
    /// <summary>
    /// Represents a Freshbooks TimeEntry (time_entry)
    /// </summary>
    /// <remarks>
    ///  <?xml version="1.0" encoding="utf-8"?>
    /// <response xmlns = "https://www.freshbooks.com/api/" status="ok">
    ///   <invoice>
    ///     <invoice_id>344</invoice_id>
    ///     <client_id>3</client_id>
    ///     <contacts>
    ///         <contact>
    ///             <contact_id>0</contact_id>
    ///         </contact>
    ///     </contacts>
    ///     <number>FB00004</number>
    ///     <!-- Total invoice amount, taxes inc. (Read Only) -->
    ///     <amount>45.6</amount>
    ///     <!-- Outstanding amount on invoice from partial payment, etc. (Read Only) -->
    ///     <amount_outstanding>0</amount_outstanding>
    ///     <status>paid</status>
    ///     <date>2007-06-23</date>
    ///     <po_number></po_number>
    ///     <discount>0</discount>
    ///     <notes>Due upon receipt.</notes>
    ///     <terms>Payment due in 30 days.</terms>
    ///     <currency_code>CAD</currency_code>
    ///     <folder>active</folder>
    ///     <language>en</language>
    ///     <url deprecated = "true" > https://2ndsite.freshbooks.com/view/St2gThi6rA2t7RQ</url> <!-- (Read-only) -->
    ///     <auth_url deprecated = "true" > https://2ndsite.freshbooks.com/invoices/344</auth_url> <!-- (Read-only) -->
    ///     <links>
    ///       <client_view>https://2ndsite.freshbooks.com/view/St2gThi6rA2t7RQ</client_view> <!-- (Read-only) -->
    ///       <view>https://2ndsite.freshbooks.com/invoices/344</view> <!-- (Read-only) -->
    ///       <edit>https://2ndsite.freshbooks.com/invoices/344/edit</edit> <!-- (Read-only) -->
    ///     </links>
    ///     <return_uri>http://www.example.com/callback</return_uri> <!-- (Optional) -->
    ///     <updated>2009-08-12 00:00:00</updated>  <!-- (Read-only) -->
    ///     <recurring_id>15</recurring_id> <!-- (Read-only) -->
    ///     <organization>ABC Corp</organization>
    ///     <first_name>John</first_name>
    ///     <last_name>Doe</last_name>
    ///     <p_street1>123 Fake St.</p_street1>
    ///     <p_street2>Unit 555</p_street2>
    ///     <p_city>New York</p_city>
    ///     <p_state>New York</p_state>
    ///     <p_country>United States</p_country>
    ///     <p_code>553132</p_code>
    ///     <vat_name></vat_name>
    ///     <vat_number></vat_number>
    ///     <staff_id>1</staff_id>
    ///     <lines>
    ///       <line>
    ///         <line_id>1</line_id>  <!-- (Read Only) line id -->
    ///         <amount>40</amount>
    ///         <!-- Line amount, taxes/discount excluding. (Read Only) -->
    ///         <name>Yard work</name>
    ///         <description>Mowed the lawn</description>
    ///         <unit_cost>10</unit_cost>
    ///         <quantity>4</quantity>
    ///         <tax1_name>GST</tax1_name>
    ///         <tax2_name>PST</tax2_name>
    ///         <tax1_percent>5</tax1_percent>
    ///         <tax2_percent>8</tax2_percent>
    ///         <type>Item</type>
    ///       </line>
    ///     </lines>
    ///   </invoice>
    /// </response>
    /// </remarks>
    [DebuggerDisplay("{Organization}-{Id}")]
    public class InvoiceModel : FreshbooksModel
    {
        public enum InvoiceStatus
        {
            Disputed,
            Draft,
            Sent,
            Viewed,
            Paid,
            AutoPaid,
            Retry,
            Failed,
            Partial
        }

        public class InvoiceLineModel
        {
            public double? Amount { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public double? UnitCost { get; set; }

            public double? Quantity { get; set; }

            public string Type { get; set; }

            public string Tax1Name { get; set; }

            public double? Tax1Percent { get; set; }

            public string Tax2Name { get; set; }

            public double? Tax2Percent { get; set; }

            public double? Tax1Amount { get; set; }

            public double? Tax2Amount { get; set; }
        }

        #region Properties

        public int? ClientId { get; set; }

        public int? StaffId { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public InvoiceStatus? Status { get; set; }

        public string PONumber { get; set; }

        public string Notes { get; set; }

        public string Organization { get; set; }

        public double? Amount { get; set; }

        public double? AmountBeforeTaxes { get; set; }

        public double? TotalTax1Amount { get; set; }

        public double? TotalTax2Amount { get; set; }

        public double? AmountOutstanding { get; set; }

        public double? Discount { get; set; }

        public string Terms { get; set; }

        public string CurrencyCode { get; set; }

        public IEnumerable<InvoiceLineModel> Lines { get; set; } = new List<InvoiceLineModel>();

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new ExpandoObject();

            result.invoice_id = Id;
            result.staff_id = StaffId;
            result.client_id = ClientId;
            if (!string.IsNullOrEmpty(Number)) result.number = Number;
            if (Status != null) result.status = DeparseStatus(Status);
            if (!string.IsNullOrEmpty(PONumber)) result.po_number = PONumber;
            if (!string.IsNullOrEmpty(Notes)) result.notes = Notes;
            if (!string.IsNullOrEmpty(Organization)) result.organization = Organization;

            if (Amount.HasValue) result.amount = FreshbooksConvert.FromDouble(Amount);
            if (AmountOutstanding.HasValue) result.amount_outstanding = FreshbooksConvert.FromDouble(AmountOutstanding);
            if (Discount.HasValue) result.discount = FreshbooksConvert.FromDouble(Discount);

            if (!string.IsNullOrEmpty(Terms)) result.terms = Terms;
            if (!string.IsNullOrEmpty(CurrencyCode)) result.currency_code = CurrencyCode;


            return result;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an instance of a <see cref="TaskModel"/> from a dynamic Freshbooks object
        /// </summary>
        /// <param name="entry">The xml received from Freshbooks as a dynamic object.</param>
        /// <returns>The corresponding <see cref="Task"/></returns>
        public static InvoiceModel FromFreshbooksDynamic(dynamic entry)
        {            
            var result = new InvoiceModel
            {
                ClientId = FreshbooksConvert.ToInt32(entry.client_id),
                StaffId = FreshbooksConvert.ToInt32(entry.staff_id),
                Id = FreshbooksConvert.ToInt32(entry.invoice_id),
                Number = entry.number,
                Date = FreshbooksConvert.ToDateTime(entry.date),
                Status = ParseStatus(entry.status),
                PONumber = entry.po_number,
                Notes = entry.notes,
                Organization = entry.organization,
                Amount = FreshbooksConvert.ToDouble(entry.amount),
                AmountOutstanding = FreshbooksConvert.ToDouble(entry.amount_outstanding),                
                Discount = FreshbooksConvert.ToDouble(entry.discount),
                Terms = entry.terms,
                CurrencyCode = entry.currency_code,
            };


            if (entry.lines != null)
            {
                if (entry.lines.line is List<object>)
                {
                    foreach (var item in entry.lines.line)
                        ((IList)result.Lines).Add(ConvertInvoiceLineModel(item));
                }
                else
                {
                    foreach (var item in entry.lines)
                        ((IList)result.Lines).Add(ConvertInvoiceLineModel(Helpers.ToDynamic(item.Value)));
                }
            }

            result.AmountBeforeTaxes = result.Lines.Sum(x => x.Amount ?? 0);
            result.TotalTax1Amount = result.Lines.Sum(x => x.Tax1Amount ?? 0);
            result.TotalTax2Amount = result.Lines.Sum(x => x.Tax2Amount ?? 0);

            return result;
        }

        private static InvoiceLineModel ConvertInvoiceLineModel(dynamic dynamicItem)
        {
            //amount, name, description, unit_cost, quantity, type, tax1_name, tax1_percent, tax2_name, tax2_percent

            var lineItem = new InvoiceLineModel()
            {
                Amount = FreshbooksConvert.ToDouble(dynamicItem.amount),
                Name = dynamicItem.name,
                Description = dynamicItem.description,
                UnitCost = FreshbooksConvert.ToDouble(dynamicItem.unit_cost),
                Quantity = FreshbooksConvert.ToDouble(dynamicItem.quantity),
                Type = dynamicItem.type,
                Tax1Name = dynamicItem.tax1_name,
                Tax1Percent = FreshbooksConvert.ToDouble(dynamicItem.tax1_percent),
                Tax2Name = dynamicItem.tax2_name,
                Tax2Percent = FreshbooksConvert.ToDouble(dynamicItem.tax2_percent),
            };

            lineItem.Tax1Amount = Math.Round((lineItem.Amount ?? 0)*((lineItem.Tax1Percent ?? 0d)/100), 2, MidpointRounding.AwayFromZero);
            lineItem.Tax2Amount = Math.Round((lineItem.Amount ?? 0)*((lineItem.Tax2Percent ?? 0d)/100), 2, MidpointRounding.AwayFromZero);            

            return lineItem;
        }

        public static InvoiceStatus? ParseStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return null;

            switch (status.ToLower())
            {
                case "disputed": return InvoiceStatus.Disputed;
                case "draft": return InvoiceStatus.Draft;
                case "sent": return InvoiceStatus.Sent;
                case "viewed": return InvoiceStatus.Viewed;
                case "paid": return InvoiceStatus.Paid;
                case "auto-paid": return InvoiceStatus.AutoPaid;
                case "retry": return InvoiceStatus.Retry;
                case "failed": return InvoiceStatus.Failed;
                case "partial": return InvoiceStatus.Partial;
                default: return null;
            }
        }

        public static string DeparseStatus(InvoiceStatus? status)
        {
            switch (status)
            {
                case InvoiceStatus.Disputed: return "disputed";
                case InvoiceStatus.Draft: return "draft";
                case InvoiceStatus.Sent: return "sent";
                case InvoiceStatus.Viewed: return "viewed";
                case InvoiceStatus.Paid: return "paid";
                case InvoiceStatus.AutoPaid: return "auto-paid";
                case InvoiceStatus.Retry: return "retry";
                case InvoiceStatus.Failed: return "failed";
                case InvoiceStatus.Partial: return "partial";
                default: return null;
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the name of the Freshbooks entity related to this model
        /// </summary>
        public override string FreshbooksEntityName
        {
            get { return "invoice"; }
        }

        #endregion
    }
}
