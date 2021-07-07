using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INACALCPROLib.MathEquations
{
    public class LogEquation : IMathEquation
    {
        public string Name { get; set; } = "Log";

        public object GetResult(IInaCalcFuncArgVals argVals)
        {
            if (argVals == null || argVals.Count != 2)
            {
                throw new ArgumentException($"{nameof(Name)} must has two parameters");
            }
            double[] paras = new double[2];
            for (int i = 0; i < argVals.Count; i++)
            {
                try
                {
                    paras[i] = Convert.ToDouble(argVals[i]);
                }
                catch (Exception ex)
                {
                    throw new Exception($"parameters of {nameof(Name)} must be numbers");
                }
            }

            return Math.Log(paras[0], paras[1]);
        }

        public EInaValueType GetValueType()
        {
            return EInaValueType.inaValNumber;
        }
    }
}
