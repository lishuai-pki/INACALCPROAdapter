using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib.MathEquations
{
    [Guid("FF4F337F-A269-40A6-9814-CC1E7943EB69")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class LnEquation : IMathEquation
    {
        public string Name { get; set; } = "Ln";

        public object GetResult(IInaCalcFuncArgVals argVals)
        {
            if (argVals == null || argVals.Count != 1)
            {
                throw new ArgumentException($"{nameof(Name)} must has a parameter");
            }

            double para;
            try
            {
                para = Convert.ToDouble(argVals[1]);
            }
            catch (Exception ex)
            {
                throw new Exception($"parameter of {nameof(Name)} must be a number");
            }

            return Math.Log(para);
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
