using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INACALCPROLib.MathEquations
{
    public interface IMathEquation
    {
        /// <summary>
        /// equation name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// result value type
        /// </summary>
        /// <returns></returns>
        EInaValueType GetValueType();

        /// <summary>
        /// result value
        /// </summary>
        /// <param name="argVals"></param>
        /// <returns></returns>
        object GetResult(IInaCalcFuncArgVals argVals);
    }
}
