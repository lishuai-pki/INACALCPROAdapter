using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [Guid("A32D4401-CF3B-420A-A95C-DCDC1E182A05")]
    [ComVisible(true)]
    public enum EInaFuncCategory
    {
        inaFuncArithmetical = 1,
        inaFuncMathematical = 2,
        inaFuncText = 3,
        inaFuncStatistical = 4,
        inaFuncFinancial = 5,
        inaFuncLogical = 6,
        inaFuncDateTime = 7,
        inaFuncCustom = 8
    }
}
