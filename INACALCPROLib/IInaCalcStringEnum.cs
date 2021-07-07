using System.Collections;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{

    public interface IInaCalcStringEnum : IEnumerable
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        string this[int lIndex] { get; }
    }
}
