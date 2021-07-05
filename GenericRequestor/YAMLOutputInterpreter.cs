using GenericRequestor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor
{
    public class YAMLOutputInterpreter : YAMLInterpreter
    {

        public Dictionary<string, object> Interpret(Dictionary<string, ITypeable> typeablePairs)
        {
            if (interpreterPairs == null) throw new Exception("No interpreted values");
            var flattend = new Dictionary<string, ITypeable>();
            Flatten(typeablePairs, "", flattend);
            return this.Interpret(typeablePairs, this.interpreterPairs, flattend);
        }

        private Dictionary<string, object> Interpret(Dictionary<string, ITypeable> valuePairs, Dictionary<string, object> interpreterPairs, Dictionary<string, ITypeable> flattend)
        {
            var ret = new Dictionary<string, object>();

            foreach (var key in interpreterPairs.Keys)
            {
                var currentVal = interpreterPairs[key];
                var type = GetTypeable(currentVal);
                if (type == typeof(ObjectType))
                {
                    ret.Add(key, Interpret(valuePairs, Helper.toDictionary(currentVal), flattend));
                }
                else if (type == null)
                {
                    var flattendVal = flattend[currentVal as string];
                    ret.Add(key, flattendVal);
                }

            }

            #region old
            /*
            foreach (var key in interpreterPairs.Keys)
            {
                var currentVal = interpreterPairs[key];

                switch (GetTypeable(currentVal))
                {
                    case ObjectType valObj:
                        ret = Interpret(typeablePairs, Helper.toDictionary(currentVal));
                        break;
                    case null:
                        if (currentVal is string)
                        {
                            Console.WriteLine(currentVal);
                            var keys = (currentVal as string).Split('.');
                            var retVal = getValueOfTypeable(typeablePairs, keys);
                            var firstKey = keys.FirstOrDefault();
                            if (!ret.ContainsKey(firstKey))
                            {
                                ret.Add(firstKey, retVal);
                            }
                            else
                            {
                                var retValObj = ret[firstKey] as ObjectType;
                                retValObj.SetValue(() =>
                                {
                                    retValObj.Value.Add(keys[1], retVal);
                                    return retValObj.Value;
                                });
                            }
                        }
                        else
                        {
                            throw new Exception("No such type known");
                        }
                        break;

                }

            }*/
            #endregion

            return ret;
        }

       
        public ITypeable getValueOfTypeable(Dictionary<string, ITypeable> valuePairs, string[] keys)
        {
            ITypeable val = null;
            var currentKey = keys.First();
            if (keys.Length > 1)
            {
                keys = keys.Skip(1).ToArray();
                var ret = getValueOfTypeable((valuePairs[currentKey] as ObjectType).Value, keys);
                val = new ObjectType();
                val.SetValue(() =>
                {
                    var dict = new Dictionary<string, ITypeable>();
                    dict.Add(keys.First(), ret);
                    return dict;
                }
                );
            }
            else
            {
                switch (valuePairs[currentKey])
                {
                    case StringType s:
                        Console.WriteLine(s.Value);
                        val = new StringType();
                        val.SetValue(s.Value);
                        break;
                    case NumberType i:
                        Console.WriteLine(i.Value);
                        val = new NumberType();
                        val.SetValue(i.Value.ToString());
                        break;

                }
            }
            return val;
        }
    }
}
