using INACALCPROLib.MathEquations;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INACALCPROLib
{
    public class InaCalcProClass : IInaCalcPro, _IInaCalcProEvents_Event
    {
        public event _IInaCalcProEvents_CheckCustomFunctionEventHandler CheckCustomFunction;
        public event _IInaCalcProEvents_EvalCustomFunctionEventHandler EvalCustomFunction;

        #region useless
        [Obsolete("Not Implement.")]
        public int AutoCalc { get; set; }
        [Obsolete("Not Implement.")]
        public int RestrictVariables { get; set; }
        [Obsolete("Not Implement.")]
        public int LangId { get; set; }
        [Obsolete("Not Implement.")]
        public int AutoCalcManual { get; set; }
        [Obsolete("Not Implement.")]
        public void AboutBox()
        {
            throw new NotImplementedException();
        }
        [Obsolete("Not Implement.")]
        public void Clear()
        {
            throw new NotImplementedException();
        }
        [Obsolete("Not Implement.")]
        public int DoFormulaBuilder(ref string strFormula, int bShowRef = 1)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Not Implement.")]
        public int ParseFormula(string strFormula)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Not Implement.")]
        public object ParseValue(string strValue, EInaValueType eValType = EInaValueType.inaValEmpty)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Not Implement.")]
        public string PrintValue(object vntValue)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Not Implement.")]
        public event _IInaCalcProEvents_CheckAtomEventHandler CheckAtom;
        [Obsolete("Not Implement.")]
        public event _IInaCalcProEvents_GetAtomValueEventHandler GetAtomValue;
        [Obsolete("Not Implement.")]
        public event _IInaCalcProEvents_ValueChangedEventHandler ValueChanged;
        [Obsolete("Not Implement.")]
        public event _IInaCalcProEvents_AtomChangedEventHandler AtomChanged;
        #endregion


        public IInaCalcAtoms Atoms { get; }


        private EInaErrorValue _lastError;
        public EInaErrorValue LastError
        {
            get
            {
                return _lastError;
            }
        }

        private string _lastErrorDescr;
        public string LastErrorDescr
        {
            get
            {
                return _lastErrorDescr;
            }
        }

        public IInaCalcFuncs Funcs { get; }

        public InaCalcProClass()
        {
            Atoms = new InaCalcAtoms(this);
            Funcs = new InaCalcFuncs();
        }

        public void SetErrorInfo(EInaErrorValue error, string errorDesc)
        {
            _lastError = error;
            _lastErrorDescr = errorDesc;
        }

        public object Eval(string strFormula)
        {
            ClearError();

            strFormula = CustomFunctionCheckHelper.RemoveLogicalExpression(strFormula);
            Expression expression = new Expression(strFormula);
            if (expression.HasErrors())
            {
                this._lastError = EInaErrorValue.inaErrSyntax;
                this._lastErrorDescr = expression.Error;
                return null;
            }

            for (int i = 0; i < Atoms.Count; i++)
            {
                var atom = Atoms[i];
                if (string.IsNullOrWhiteSpace(atom.Formula))
                {
                    expression.Parameters.Add(atom.Name, atom.Value);
                }
                else
                {
                    var subExpression = new Expression(atom.Formula);
                    expression.Parameters[atom.Name] = subExpression;
                }
            }

            expression.EvaluateFunction += E_EvaluateFunction;

            var result = expression.Evaluate();
            return result;
        }

        private void E_EvaluateFunction(string name, FunctionArgs args)
        {
            List<object> vals = new List<object>();

            foreach (var para in args.Parameters)
            {
                vals.Add(para.Evaluate());
            }
            IInaCalcFuncArgVals argVals = new InaCalcFuncArgVals(vals);

            var equation = MathEquationHelper.mathEquations.Find(m => string.Compare(m.Name, name, true) == 0);
            if (equation != null)
            {
                var vntValue = equation.GetResult(argVals);
                args.Result = vntValue;
                return;
            }

            if (Funcs[name] != null)
            {
                //在这里验证函数并设置valueType ?????
                EvalCustomFunction(name, argVals, out object vntValue);
                if (vntValue != null)
                {
                    args.Result = vntValue;
                }
            }
        }


        public void Recalc()
        {
            ClearError();
            for (int i = 0; i < Atoms.Count; i++)
            {
                var atom = Atoms[i];
                if (!string.IsNullOrWhiteSpace(atom.Formula))
                {
                    //check custom functions
                    atom.Formula = atom.Formula;

                    //eval formual
                    var result = Eval(atom.Formula);
                    atom.Value = result;
                }
            }
        }

        public void OnCheckCustomFunction(string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType)
        {
            if (this.CheckCustomFunction != null)
            {
                this.CheckCustomFunction(strFunc, argTypes, out valType);
            }
            else
            {
                valType = EInaValueType.inaValEmpty;
            }
        }

        private void ClearError()
        {
            _lastError = EInaErrorValue.inaErrNone;
            _lastErrorDescr = string.Empty;
        }
    }
}
