using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Vooban.FreshBooks.Invoices;
using Vooban.FreshBooks.Invoices.Models;
using Xunit;

namespace Vooban.FreshBooks.Tests.Invoices
{    
    public class InvoiceApiTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        public class CallSearch
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new InvoiceApi(freshbooks);

                var result = testedClass.Search(new InvoiceFilter { From = new DateTime(2015,08,01), To = new DateTime(2016,07,31) });

                var builder = new StringBuilder();
                var lineBuilder = new StringBuilder();

                builder.AppendLine("Date facture;Numéro de facture;Client;Montant avant taxes;Montant TPS;Montant TVQ;Montant total;Currency;Status;");
                lineBuilder.AppendLine("Date facture;Numéro de facture;Client;Nom tâche;Description tâche;Montant tâche avant taxe;Montant taxe 1;Montant taxe 2;Montant total;");
                foreach (var invoice in result.Result)
                {
                    builder.AppendFormat("{0};", invoice.Date?.ToShortDateString() ?? string.Empty);
                    builder.AppendFormat("{0};", invoice.Number);
                    builder.AppendFormat("{0};", invoice.Organization);
                    if (invoice.Amount != null)
                    {
                        var avantTaxes = invoice.AmountBeforeTaxes ?? 0;
                        var tps = invoice.TotalTax1Amount;
                        var tvq = invoice.TotalTax2Amount;
                        builder.AppendFormat("{0};", avantTaxes);
                        builder.AppendFormat("{0};", tps);
                        builder.AppendFormat("{0};", tvq);
                        builder.AppendFormat("{0};", invoice.Amount);
                    }
                    builder.AppendFormat("{0};", invoice.CurrencyCode);
                    builder.AppendFormat("{0};", InvoiceModel.DeparseStatus(invoice.Status));
                    builder.AppendLine();

                    foreach (var line in invoice.Lines.Where(x=>x.Amount>0 && x.Type != "Expense"))
                    {
                        lineBuilder.AppendFormat("{0};", invoice.Date?.ToShortDateString() ?? string.Empty);
                        lineBuilder.AppendFormat("{0};", invoice.Number);
                        lineBuilder.AppendFormat("{0};", invoice.Organization);
                        lineBuilder.AppendFormat("{0};", line.Name);
                        lineBuilder.AppendFormat("{0};", line.Description);
                        lineBuilder.AppendFormat("{0};", line.Amount);
                        lineBuilder.AppendFormat("{0};", line.Tax1Amount);
                        lineBuilder.AppendFormat("{0};", line.Tax2Amount);
                        lineBuilder.AppendFormat("{0};", line.Amount + line.Tax1Amount + line.Tax2Amount);
                        lineBuilder.AppendLine();
                    }

                }

                var output = builder.ToString();
                var lineOutput = lineBuilder.ToString();
                Console.Write(output);
                Console.Write(lineOutput);

                Assert.NotNull(result);
                Assert.True(result.Success, "The Freshbooks response indicated a fail");
            }
        }

        public class CallGetList
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new InvoiceApi(freshbooks);

                var result = testedClass.GetList();
                Assert.NotNull(result);
                Assert.True(result.Success);
            }
        }

        public class CallGetMethod
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new InvoiceApi(freshbooks);

                var allInvoices = testedClass.GetList();
                foreach (var invoice in allInvoices.Result)
                {
                    Assert.NotNull(testedClass.Get(invoice.Id.Value));
                }
            }
        }


    }
}
