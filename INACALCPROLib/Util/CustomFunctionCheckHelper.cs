using NCalc;
using System;
using System.Collections.Generic;

namespace INACALCPROLib
{
    public class CustomFunctionCheckHelper
    {
        public static void Check(InaCalcProClass _inaCalcProClass, string formula, ref EInaValueType _returnType)
        {
            //check if expression illegal before set value

            List<EInaValueType> calcValueTypes = new List<EInaValueType>();

            Expression expression = new Expression(formula);
            if (expression.HasErrors())
            {
                throw new Exception(expression.Error);
            }
            var parsedExpression = expression.ParsedExpression;
            if (!(parsedExpression is NCalc.Domain.Function))
            {
                return;
            }

            var functionExpressions = (NCalc.Domain.Function)expression.ParsedExpression;

            var functionName = functionExpressions.Identifier.Name;

            //var mathEquation = MathEquationHelper.mathEquations.Find(m => string.Compare(m.Name, functionName, true) == 0);
            //if (mathEquation != null)
            //{

            //}

            if (_inaCalcProClass.Funcs[functionName] == null)
            {
                return;
            }

            foreach (var item in functionExpressions.Expressions)
            {
                if (item is NCalc.Domain.ValueExpression valPara)
                {
                    var paraType = valPara.Type;
                    switch (paraType)
                    {
                        case NCalc.Domain.ValueType.String:
                            calcValueTypes.Add(EInaValueType.inaValText);
                            break;
                        case NCalc.Domain.ValueType.Integer:
                        case NCalc.Domain.ValueType.Float:
                            calcValueTypes.Add(EInaValueType.inaValNumber);
                            break;
                        case NCalc.Domain.ValueType.Boolean:
                            calcValueTypes.Add(EInaValueType.inaValBool);
                            break;
                        case NCalc.Domain.ValueType.DateTime:
                            calcValueTypes.Add(EInaValueType.inaValDate);
                            break;
                        default:
                            calcValueTypes.Add(EInaValueType.inaValEmpty);
                            break;
                    }
                }
                else if (item is NCalc.Domain.Identifier idenPara)
                {
                    calcValueTypes.Add(EInaValueType.inaValText);
                }
                else
                {
                    calcValueTypes.Add(EInaValueType.inaValEmpty);
                }
            }

            IInaCalcFuncArgTypes inaCalcFuncArgTypes = new InaCalcFuncArgTypes(calcValueTypes);
            _inaCalcProClass.OnCheckCustomFunction(formula, inaCalcFuncArgTypes, out _returnType);
        }

        /// <summary>
        /// Ncalc can't resolve expression logical
        /// </summary>
        /// <param name="strFormula"></param>
        /// <returns></returns>
        public static string RemoveLogicalExpression(string strFormula)
        {
            if (!string.IsNullOrWhiteSpace(strFormula))
            {
                int index = strFormula.IndexOf("logical(", StringComparison.OrdinalIgnoreCase);
                if (index == 0)
                {
                    var lastIndex = strFormula.LastIndexOf(")");
                    if (lastIndex == strFormula.Length - 1)
                    {
                        var result = strFormula.Substring(8, strFormula.Length - 8 - 1);
                        return result;
                    }
                }


            }
            return strFormula;
        }
    }
}
