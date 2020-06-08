using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBuild.CLBuild
{
    public struct ResultLog
    {
        public string Value { get; set; }

        public ResultLog(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
