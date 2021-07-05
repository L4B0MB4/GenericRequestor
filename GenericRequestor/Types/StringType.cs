using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor.Types
{
    class StringType : ITypeable
    {
        public string Value { get; private set; }

        public string GetStringValue()
        {
            return Value;
        }

        public void SetValue(string v)
        {
            this.Value = v;
        }

        public void SetValue(Func<Dictionary<string, ITypeable>> setValFunc)
        {
            throw new NotImplementedException();
        }
    }
}
