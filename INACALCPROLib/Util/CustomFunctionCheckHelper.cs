using NCalc;
using NCalc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INACALCPROLib
{
    public class CustomFunctionCheckHelper
    {
        public static void Check(InaCalcPro _inaCalcProClass, string formula, ref EInaValueType _returnType)
        {
            try
            {
                _inaCalcProClass.ClearError();
                Expression expression = new Expression(formula);
                if (expression.HasErrors())
                {
                    _inaCalcProClass.SetErrorInfo(EInaErrorValue.inaErrSyntax, expression.Error);
                    return;
                }
                var parsedExpression = expression.ParsedExpression;
                if (!(parsedExpression is NCalc.Domain.Function))
                {
                    return;
                }

                // try to solve the function executing sequence
                var funcLevelList = new List<FunctionWithLevel>();
                GetAllFunctionExpressions(parsedExpression, funcLevelList, 0);
                var funcList = new List<Function>();
                funcLevelList.OrderByDescending(f => f.Level).ToList().ForEach(fv =>
                {
                    if (!funcList.Exists(f => f.Identifier.Name == fv.Func.Identifier.Name))
                    {
                        funcList.Add(fv.Func);
                    }
                });

                var funcTypes = new List<IInaCalcFunc>();

                foreach (var fun in funcList)
                {
                    var functionName = fun.Identifier.Name;

                    var customFunc = _inaCalcProClass.Funcs[functionName];
                    if (customFunc != null)
                    {
                        var calcValueTypes = ConvertToInaValueType(fun.Expressions, funcTypes);

                        IInaCalcFuncArgTypes inaCalcFuncArgTypes = new InaCalcFuncArgTypes(calcValueTypes);
                        _inaCalcProClass.OnCheckCustomFunction(functionName, inaCalcFuncArgTypes, out EInaValueType returnType);

                        funcTypes.Add(new InaCalcFunc
                        {
                            Name = functionName,
                            ParamValueTypes = calcValueTypes,
                            OutputValueType = returnType,
                            Category = EInaFuncCategory.inaFuncCustom
                        });
                        continue;
                    }

                    var mathFunc = MathEquationHelper.mathEquations.Find(m => string.Compare(m.Name, functionName, true) == 0);
                    if (mathFunc != null)
                    {
                        var calcValueTypes = ConvertToInaValueType(fun.Expressions, funcTypes);
                        IInaCalcFuncArgTypes inaCalcFuncArgTypes = new InaCalcFuncArgTypes(calcValueTypes);
                        var returnType = mathFunc.GetValueType(inaCalcFuncArgTypes);

                        funcTypes.Add(new InaCalcFunc
                        {
                            Name = functionName,
                            ParamValueTypes = calcValueTypes,
                            OutputValueType = returnType,
                            Category = EInaFuncCategory.inaFuncCustom
                        });

                        continue;
                    }
                }

                var targetFuncType = funcTypes.Find(f => f.Name == ((Function)parsedExpression).Identifier.Name);
                if (targetFuncType != null)
                {
                    _returnType = targetFuncType.OutputValueType;
                }
            }
            catch (Exception ex)
            {
                _inaCalcProClass.SetErrorInfo(EInaErrorValue.inaErrCustFunc, ex.Message);
            }

        }

        /// <summary>
        /// convert naclc value type to inacal value type
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        static List<EInaValueType> ConvertToInaValueType(LogicalExpression[] expressions, List<IInaCalcFunc> inaCalcFuncs = null)
        {
            List<EInaValueType> calcValueTypes = new List<EInaValueType>();

            foreach (var item in expressions)
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
                else if (item is Function function)
                {

                    var targetFunc = inaCalcFuncs?.Find(f => f.Name == function.Identifier.Name);
                    if (targetFunc != null)
                    {
                        calcValueTypes.Add(targetFunc.OutputValueType);
                    }
                }
                else
                {
                    calcValueTypes.Add(EInaValueType.inaValEmpty);
                }
            }
            return calcValueTypes;
        }

        static void GetAllFunctionExpressions(LogicalExpression logicalExpression, List<FunctionWithLevel> functions, int level)
        {
            if (logicalExpression == null)
            {
                return;
            }
            int nextLevel = level + 1;

            if (logicalExpression is BinaryExpression binaryExpression)
            {
                GetAllFunctionExpressions(binaryExpression.LeftExpression, functions, nextLevel);
                GetAllFunctionExpressions(binaryExpression.RightExpression, functions, nextLevel);
            }
            else if (logicalExpression is Function functionExpression)
            {
                if (!functions.Exists(f => string.Compare(f.Func.Identifier.Name, functionExpression.Identifier.Name, true) == 0 && f.Level == level))
                {
                    functions.Add(new FunctionWithLevel { Func = functionExpression, Level = level });
                    foreach (var subExp in functionExpression.Expressions)
                    {
                        GetAllFunctionExpressions(subExp, functions, nextLevel);
                    }
                    GetAllFunctionExpressions(functionExpression.Identifier, functions, nextLevel);
                }
            }
            else if (logicalExpression is Identifier identifier)
            {
                //GetAllFunctionExpressions(identifier, functions);
            }
            else if (logicalExpression is TernaryExpression ternaryExpression)
            {
                GetAllFunctionExpressions(ternaryExpression.LeftExpression, functions, nextLevel);
                GetAllFunctionExpressions(ternaryExpression.MiddleExpression, functions, nextLevel);
                GetAllFunctionExpressions(ternaryExpression.RightExpression, functions, nextLevel);
            }
            else if (logicalExpression is UnaryExpression unaryExpression)
            {
                GetAllFunctionExpressions(unaryExpression.Expression, functions, nextLevel);
            }
            else if (logicalExpression is ValueExpression valueExpression)
            {

            }
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

        class FunctionWithLevel
        {
            public Function Func { get; set; }
            public int Level { get; set; }
        }
    }
}
