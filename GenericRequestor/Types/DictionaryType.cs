using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor.Types
{
    public class DictionaryType : ITypeable
    {
        public Dictionary<object, object> Value {get; private set;}
        public DictionaryType()
        {
            
        }
        public string GetStringValue()
        {
            throw new NotImplementedException();
        }

        public void SetValue(string v)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Func<Dictionary<string, ITypeable>> setValFunc)
        {
            throw new NotImplementedException();
        }
    }
}
