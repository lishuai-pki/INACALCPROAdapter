using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
namespace INACALCPROLib
{

    [ComVisible(true)]
    [Guid("5A8B067E-39FF-419A-A3FD-67CBEF59C4BE")]
    public interface IInaCalcStringEnum : IEnumerable
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        string this[int lIndex] { get; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("8E36AD20-23C7-4A8F-8E6E-397D83C155A1")]
    public class InaCalcStringEnum : IInaCalcStringEnum
    {
        private List<string> _enumList = new List<string>();
        public string this[int lIndex]
        {
            get
            {
                if (lIndex < 0 || lIndex >= _enumList.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _enumList[lIndex];
            }
        }

        public int Count => _enumList.Count;

        public IEnumerator GetEnumerator()
        {
            return _enumList.GetEnumerator();
        }
    }
}
