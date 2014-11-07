using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastyAPI;

namespace Vooban.FreshBooks
{
    /// <summary>
    /// Helpers methods used to facilitate the development of the API
    /// </summary>
    static class Helpers
    {
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }

        public static dynamic ToDynamic(this KeyValuePair<string, object> value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            if (value.Value.GetType().IsValueType || value.Value is string)
                expando.Add(value.Key, value.Value);
            else if (value.Value is FriendlyDynamic)
                expando.Add(value.Key, ((FriendlyDynamic)value.Value).ToDynamic());
            else if (value.Value is IEnumerable<KeyValuePair<string, object>>)
                expando.Add(value.Key, ((IEnumerable<KeyValuePair<string, object>>)value.Value).ToDynamic());
            else if (value.Value is KeyValuePair<string, object>)
                expando.Add(value.Key, ((KeyValuePair<string, object>) value.Value).ToDynamic());

            return expando as ExpandoObject;
        }

        public static dynamic ToDynamic(this FriendlyDynamic value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var r in value)
            {
                if (r.Value.GetType().IsValueType || r.Value is string)
                    expando.Add(r.Key, r.Value);
                else if(r.Value is FriendlyDynamic)
                    expando.Add(r.Key, ((FriendlyDynamic)r.Value).ToDynamic());
                else if (r.Value is IEnumerable<KeyValuePair<string, object>>)
                    expando.Add(r.Key, ((IEnumerable<KeyValuePair<string, object>>)r.Value).ToDynamic());
                else if (r.Value is KeyValuePair<string, object>)
                    expando.Add(r.Key, ((KeyValuePair<string, object>)r.Value).ToDynamic());
            }

            return expando as ExpandoObject;
        }
    }
}
