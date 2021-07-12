using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [ComVisible(true)]
    [Guid("13E06C43-CEFF-4728-B626-DA2EEEA1E651")]
    public interface IInaCalcFunc
    {
        [DispId(0)]
        string Name { get; set; }
        [DispId(2)]
        string Format { get; set; }
        [DispId(3)]
        string Descr { get; set; }
        [DispId(4)]
        EInaFuncCategory Category { get; set; }
        [DispId(5)]
        List<EInaValueType> ParamValueTypes { get; set; }
        [DispId(6)]
        EInaValueType OutputValueType { get; set; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("E7DAC245-B6B7-4F11-A864-2353A2A625D3")]
    public class InaCalcFunc : IInaCalcFunc
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public string Descr { get; set; }
        public EInaFuncCategory Category { get; set; }
        public List<EInaValueType> ParamValueTypes { get; set; } = new List<EInaValueType>();
        public EInaValueType OutputValueType { get; set; }
    }
}
