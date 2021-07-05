using GenericRequestor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GenericRequestor
{
    public class YAMLInputInterpreter:YAMLInterpreter
    {
        

        public Dictionary<string, ITypeable> Interpret(Dictionary<string, object> apiValue)
        {
            return Interpret(apiValue, this.interpreterPairs);
        }

        private Dictionary<string, ITypeable> Interpret(Dictionary<string, object> apiValue, Dictionary<string, object> interpreterPairs)
        {
            var retVal = new Dictionary<string, ITypeable>();
            foreach (var key in apiValue.Keys)
            {
                if (!interpreterPairs.ContainsKey(key))
                    continue;
                var type = GetTypeableObject(interpreterPairs[key]);
                if (type != null)
                {
                    if (type is ObjectType)
                    {
                        type.SetValue(() => this.Interpret(Helper.toDictionary(apiValue[key]), Helper.toDictionary(interpreterPairs[key])));
                    }
                    else
                    {
                        type.SetValue(apiValue[key].ToString());
                    }
                    retVal.Add(key, type);
                }
            }

            return retVal;
        }

        
       


    }
}
