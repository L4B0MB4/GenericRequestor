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
    public class YAMLInterpreter
    {
        public Dictionary<string, object> interpreterPairs;

        public void DeSerialize(string yml)
        {


            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            //yml contains a string containing your YAML
            this.interpreterPairs = deserializer.Deserialize<Dictionary<string, object>>(yml);
        }

        public ITypeable GetTypeableObject(object type)
        {
            if (type is Dictionary<string, object> || type is Dictionary<object, object>)
            {
                return new ObjectType();
            }
            switch (type)
            {
                case "number":
                    return new NumberType();
                case "string":
                    return new StringType();
                default:
                    return null;
            }
        }
        ///<summary>
        ///Returns Type if is a dictionary or an expected string literal otherwise returns null
        ///</summary>
        public Type GetTypeable(object type)
        {
            if (type is Dictionary<string, object> || type is Dictionary<object, object>)
            {
                return typeof(ObjectType);
            }
            switch (type)
            {
                case "number":
                    return typeof(NumberType);
                case "string":
                    return typeof(StringType);
                default:
                    return null;
            }
        }


        protected void Flatten(Dictionary<string, ITypeable> valuePairs, string currentKey, Dictionary<string, ITypeable> flattend)
        {
            foreach (var key in valuePairs.Keys)
            {
                var newKey = currentKey == "" ? key : currentKey + "." + key;
                if (valuePairs[key] is ObjectType)
                {
                    Flatten((valuePairs[key] as ObjectType).Value, newKey, flattend);
                }
                else
                {
                    flattend.Add(newKey, valuePairs[key]);
                }
            }
        }
        protected void Flatten(Dictionary<string, object> valuePairs, string currentKey, Dictionary<string, ITypeable> flattend)
        {
            foreach (var key in valuePairs.Keys)
            {
                var newKey = currentKey == "" ? key : currentKey + "." + key;
                var currentValue = valuePairs[key];
                var currentType = GetTypeable(currentValue);
                if (currentType == typeof(ObjectType))
                {
                    Flatten(Helper.toDictionary(valuePairs[key]), newKey, flattend);
                }
                else
                {
                    flattend.Add(newKey, valuePairs[key] as ITypeable);
                }
            }
        }

        public Dictionary<string, ITypeable> Flatten(Dictionary<string, object> valuePairs)
        {
            var flattend = new Dictionary<string, ITypeable>();
            Flatten(valuePairs, "", flattend);
            return flattend;
        }

        public Dictionary<string, ITypeable> Flatten(Dictionary<string, ITypeable> valuePairs)
        {
            var flattend = new Dictionary<string, ITypeable>();
            Flatten(valuePairs, "", flattend);
            return flattend;
        }
    }
}
