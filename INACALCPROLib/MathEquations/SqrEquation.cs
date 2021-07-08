using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib.MathEquations
{
    [Guid("B8425AB1-8DD0-4A77-8FD7-C11BEEC8A43D")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class SqrEquation : IMathEquation
    {
        public string Name { get; set; } = "Sqr";

        public object GetResult(IInaCalcFuncArgVals argVals)
        {
            if (argVals == null || argVals.Count != 1)
            {
                throw new ArgumentException($"{nameof(Name)} must has a parameter");
            }

            double para;
            try
            {
                para = Convert.ToDouble(argVals[0]);
            }
            catch (Exception ex)
            {
                throw new Exception($"parameter of {nameof(Name)} must be a number");
            }

            return Math.Pow(para, 2);
        }

        public EInaValueType GetValueType(IInaCalcFuncArgTypes argTypes)
        {
            if (argTypes == null || argTypes.Count != 1)
            {
                throw new ArgumentException($"{nameof(Name)} must has a parameter");
            }
            return EInaValueType.inaValNumber;
        }
    }
}
