using INACALCPROLib.MathEquations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INACALCPROLib
{
    public static class MathEquationHelper
    {
        /// <summary>
        /// get adapter math equtaion list
        /// </summary>
        public static List<IMathEquation> mathEquations { get; }

        static MathEquationHelper()
        {
            mathEquations = new List<IMathEquation>
            {
                new LogEquation(),
                new ExpEquation(),
                new LnEquation(),
                new SqrEquation(),
                new SqrtEquation()
            };
        }
    }
}
