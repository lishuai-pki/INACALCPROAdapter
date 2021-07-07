using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{

    public interface IInaCalcAtom
    {
        [DispId(6)]
        void Clear();

        [DispId(0)]
        object Value { get; set; }
        [DispId(1)]
        string Name { get; }
        [DispId(2)]
        string Formula { get; set; }
        [DispId(3)]
        EInaValueType ReturnType { get; }
        [DispId(4)]
        IInaCalcStringEnum FormulaRefs { get; }
        [DispId(5)]
        IInaCalcStringEnum FormulaDeps { get; }
    }

    public class InaCalcAtom : IInaCalcAtom
    {
        public InaCalcAtom(string name, InaCalcProClass inaCalcProClass)
        {
            Name = name;
            _inaCalcProClass = inaCalcProClass;
        }

        private InaCalcProClass _inaCalcProClass;
        public object Value { get; set; }

        public string Name { get; }

        private string _formmula;
        public string Formula
        {
            get
            {
                var result = CustomFunctionCheckHelper.RemoveLogicalExpression(_formmula);
                return result;
            }

            set
            {
                //check if expression illegal before set value                
                CustomFunctionCheckHelper.Check(_inaCalcProClass, value, ref _returnType);
                _formmula = value;
            }
        }

        private EInaValueType _returnType;
        public EInaValueType ReturnType
        {
            get
            {
                return _returnType;
            }
        }

        public IInaCalcStringEnum FormulaRefs { get; }

        public IInaCalcStringEnum FormulaDeps { get; }

        public void Clear()
        {

        }
    }
}
