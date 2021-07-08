using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{

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
