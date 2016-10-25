using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Invoices.Models;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks.Invoices
{
    public class InvoiceApi : IInvoiceApi
    {
        #region Private Classes

        public class InvoiceApiOptions : GenericApiOptions<InvoiceModel>
        {
            #region Constantes

            public const string COMMAND_LIST = "invoice.list";
            public const string COMMAND_GET = "invoice.get";

            #endregion

            #region Properties

            /// <summary>
            /// Gets the name of the id property used with this entity
            /// </summary>
            public override string IdProperty
            {
                get { return "invoice_id"; }
            }
            
            /// <summary>
            /// Gets the Freshbooks get command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string GetCommand
            {
                get { return COMMAND_GET; }
            }

            /// <summary>
            /// Gets the Freshbooks list command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string ListCommand
            {
                get { return COMMAND_LIST; }
            }

            /// <summary>
            /// Creates an entity of type <see cref="InvoiceModel"/> from the Freshbooks dynamic response
            /// </summary>
            /// <returns>The fully loaded entity</returns>
            public override Func<dynamic, InvoiceModel> FromDynamicModel
            {
                get
                {
                    return d => InvoiceModel.FromFreshbooksDynamic(d.response.project);
                }
            }

            /// <summary>
            /// Enumerates over the list response of the Freshbooks API
            /// </summary>
            /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
            public override Func<dynamic, IEnumerable<InvoiceModel>> BuildEnumerableFromDynamicResult
            {
                get
                {
                    return resultList =>
                    {
                        var result = new List<InvoiceModel>();

                        foreach (var item in resultList.response.invoices.invoice)
                            result.Add(InvoiceModel.FromFreshbooksDynamic(item));

                        return result;
                    };
                }
            }

            public static InvoiceApiOptions Options => new InvoiceApiOptions();

            #endregion
        }

        #endregion


        #region Private Members

        private readonly GenericApi<InvoiceModel, InvoiceFilter> _api;

        #endregion

        #region Constructors

        public InvoiceApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _api = new GenericApi<InvoiceModel, InvoiceFilter>(freshbooks, InvoiceApiOptions.Options);
        }

        public InvoiceApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _api = new GenericApi<InvoiceModel, InvoiceFilter>(freshbooks, InvoiceApiOptions.Options);
        }

        #endregion

        public InvoiceModel Get(int id)
        {
            return _api.Get(id);
        }

        public IEnumerable<InvoiceModel> GetAllPages()
        {
            return _api.GetAllPages();
        }

        public FreshbooksPagedResponse<InvoiceModel> GetList(int page = 1, int itemPerPage = 100)
        {
            return _api.GetList(page, itemPerPage);
        }

        public FreshbooksPagedResponse<InvoiceModel> Search(InvoiceFilter template, int page = 1, int itemPerPage = 100)
        {
            return _api.Search(template, page, itemPerPage);

        }

        public IEnumerable<InvoiceModel> SearchAll(InvoiceFilter template, int page = 1, int itemPerPage = 100)
        {
            return _api.SearchAll(template, page, itemPerPage);

        }
    }
}
