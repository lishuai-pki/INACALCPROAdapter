using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib.MathEquations
{
    [Guid("4F69FD3B-27BF-4924-8763-927EDE24FE7D")]
    [ComVisible(true)]
    public interface IMathEquation
    {
        /// <summary>
        /// equation name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// result value type
        /// </summary>
        /// <param name="argTypes">paramter types</param>
        /// <returns></returns>
        EInaValueType GetValueType(IInaCalcFuncArgTypes argTypes);

        /// <summary>
        /// result value
        /// </summary>
        /// <param name="argVals">parameter values</param>
        /// <returns></returns>
        object GetResult(IInaCalcFuncArgVals argVals);
    }
}
