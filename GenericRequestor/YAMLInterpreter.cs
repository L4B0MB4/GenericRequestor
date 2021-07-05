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
    }
}
