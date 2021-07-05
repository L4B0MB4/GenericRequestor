using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor
{
    public static class Helper
    {
        public static Dictionary<string, object> toDictionary(object dictObj)
        {
            if (dictObj is Dictionary<string, object>)
            {
                return dictObj as Dictionary<string, object>;
            }
            var res = new Dictionary<string, object>();
            if (dictObj is Dictionary<object, object>)
            {
                var dict = dictObj as Dictionary<object, object>;
                foreach (var key in dict.Keys)
                {
                    res.Add(key.ToString(), dict[key]);
                }
            }
            return res;
        }
    }
}
