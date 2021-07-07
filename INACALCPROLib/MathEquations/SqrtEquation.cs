﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INACALCPROLib.MathEquations
{
    public class SqrtEquation : IMathEquation
    {
        public string Name { get; set; } = "Sqrt";

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

            return Math.Sqrt(para);
        }

        public EInaValueType GetValueType()
        {
            return EInaValueType.inaValNumber;
        }
    }
}
