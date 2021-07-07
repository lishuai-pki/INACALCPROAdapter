namespace INACALCPROLib
{

    public delegate void _IInaCalcProEvents_CheckAtomEventHandler(string strAtom, out EInaValueType valType);
    public delegate void _IInaCalcProEvents_GetAtomValueEventHandler(string strAtom, out object vntValue);
    public delegate void _IInaCalcProEvents_ValueChangedEventHandler(string strAtom);
    public delegate void _IInaCalcProEvents_CheckCustomFunctionEventHandler(string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType);
    public delegate void _IInaCalcProEvents_EvalCustomFunctionEventHandler(string strFunc, IInaCalcFuncArgVals argVals, out object vntValue);
    public delegate void _IInaCalcProEvents_AtomChangedEventHandler(IInaCalcAtom pAtom);

    public interface _IInaCalcProEvents_Event
    {
        event _IInaCalcProEvents_CheckAtomEventHandler CheckAtom;
        event _IInaCalcProEvents_GetAtomValueEventHandler GetAtomValue;
        event _IInaCalcProEvents_ValueChangedEventHandler ValueChanged;
        event _IInaCalcProEvents_CheckCustomFunctionEventHandler CheckCustomFunction;
        event _IInaCalcProEvents_EvalCustomFunctionEventHandler EvalCustomFunction;
        event _IInaCalcProEvents_AtomChangedEventHandler AtomChanged;
    }
}
