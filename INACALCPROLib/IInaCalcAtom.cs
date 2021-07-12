using System;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [Guid("E94F4143-E6CE-477D-B154-705F7AC59972")]
    [ComVisible(true)]
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

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("31BCC53B-A32F-49CB-B056-B35AB5FBC3A8")]
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
