using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor.Types
{
    public interface ITypeable
    {
        void SetValue(string v);
        void SetValue(Func<Dictionary<string, ITypeable>> setValFunc);

        string GetStringValue();
    }
}
