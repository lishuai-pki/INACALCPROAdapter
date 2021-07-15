using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_CheckAtomEventHandler(string strAtom, out EInaValueType valType);
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_GetAtomValueEventHandler(string strAtom, out object vntValue);
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_ValueChangedEventHandler(string strAtom);
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_CheckCustomFunctionEventHandler(string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType);
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_EvalCustomFunctionEventHandler(string strFunc, IInaCalcFuncArgVals argVals, out object vntValue);
    [ComVisible(false)]
    public delegate void _IInaCalcProEvents_AtomChangedEventHandler(IInaCalcAtom pAtom);

    [Guid("0DF0A4C4-D708-4504-9432-27D2AEED9FD4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _IInaCalcProEvents
    {
        [DispId(1)]
        void CheckAtom(string strAtom, out EInaValueType valType);
        [DispId(2)]
        void GetAtomValue(string strAtom, out object vntValue);
        [DispId(3)]
        void ValueChanged(string strAtom);
        [DispId(4)]
        void CheckCustomFunction(string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType);
        [DispId(5)]
        void EvalCustomFunction(string strFunc, IInaCalcFuncArgVals argVals, out object vntValue);
        [DispId(6)]
        void AtomChanged(IInaCalcAtom pAtom);
    }
}
