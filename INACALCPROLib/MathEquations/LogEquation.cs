using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib.MathEquations
{
    [Guid("3958699A-13C2-46DB-BCD6-60EEFE36A453")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
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
                    paras[i] = Convert.ToDouble(argVals[i + 1]);
                }
                catch (Exception ex)
                {
                    throw new Exception($"parameters of {nameof(Name)} must be numbers");
                }
            }

            return Math.Log(paras[0], paras[1]);
        }

        public EInaValueType GetValueType(IInaCalcFuncArgTypes argTypes)
        {
            if (argTypes == null || argTypes.Count != 2)
            {
                throw new ArgumentException($"{nameof(Name)} must has two parameters");
            }
            return EInaValueType.inaValNumber;
        }
    }
}
