using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor.Types
{
    class ObjectType : ITypeable
    {
        public Dictionary<string,ITypeable> Value { get; private set; }

        public string GetStringValue()
        {
            throw new NotImplementedException();
        }

        public void SetValue(Func<Dictionary<string, ITypeable>> setValFunc)
        {
            Value = setValFunc();
        }

        public void SetValue(string v)
        {
            throw new NotImplementedException();
        }

    }
}
