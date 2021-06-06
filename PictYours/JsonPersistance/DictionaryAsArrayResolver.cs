using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPersistance
{
    public class DictionaryAsArrayResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (IsDictionary(objectType))
            {
                JsonArrayContract contract = base.CreateArrayContract(objectType);
                contract.OverrideCreator = (args) => CreateInstance(objectType);
                return contract;
            }

            return base.CreateContract(objectType);
        }

        internal static bool IsDictionary(Type objectType)
        {
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                return true;
            }

            if (objectType.GetInterface(typeof(IDictionary<,>).Name) != null)
            {
                return true;
            }

            return false;
        }

        private object CreateInstance(Type objectType)
        {
            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(objectType.GetGenericArguments());
            return Activator.CreateInstance(dictionaryType);
        }
    }
}
