using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    public interface IInaCalcPro
    {
        [DispId(6)]
        void Clear();
        [DispId(7)]
        void Recalc();
        [DispId(8)]
        object Eval(string strFormula);
        [DispId(9)]
        int ParseFormula(string strFormula);
        [DispId(10)]
        object ParseValue(string strValue, EInaValueType eValType = EInaValueType.inaValEmpty);
        [DispId(11)]
        string PrintValue(object vntValue);
        [DispId(12)]
        int DoFormulaBuilder(ref string strFormula, int bShowRef = 1);
        [DispId(13)]
        void AboutBox();

        [DispId(0)]
        IInaCalcAtoms Atoms { get; }
        [DispId(1)]
        int AutoCalc { get; set; }
        [DispId(2)]
        int RestrictVariables { get; set; }
        [DispId(3)]
        EInaErrorValue LastError { get; }
        [DispId(4)]
        string LastErrorDescr { get; }
        [DispId(5)]
        IInaCalcFuncs Funcs { get; }
        [DispId(14)]
        int LangId { get; set; }
        [DispId(15)]
        int AutoCalcManual { get; set; }
    }
}
