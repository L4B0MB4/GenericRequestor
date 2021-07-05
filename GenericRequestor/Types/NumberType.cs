using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor.Types
{
    class NumberType: ITypeable
    {
       public double Value { get; private set; }

        public string GetStringValue()
        {
            return Value.ToString();
        }

        public void SetValue(string v)
        {
            double outval;
            if(Double.TryParse(v, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out outval)){
                this.Value = outval;
            }
            else
            {
                throw new Exception("Could not parse double '" + v + "'");
            }
        }

        public void SetValue(Func<Dictionary<string, ITypeable>> setValFunc)
        {
            throw new NotImplementedException();
        }
    }
}
