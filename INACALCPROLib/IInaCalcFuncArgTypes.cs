using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [ComVisible(true)]
    [Guid("A92F6C30-2379-4154-9527-042C99D0BA60")]
    public interface IInaCalcFuncArgTypes : IEnumerable
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        /// <summary>
        /// index starts at 1
        /// </summary>
        /// <param name="lIndex"></param>
        /// <returns></returns>
        [DispId(0)]
        EInaValueType this[int lIndex] { get; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("EBC703F5-C3F0-4878-A589-264A38519D6A")]
    public class InaCalcFuncArgTypes : IInaCalcFuncArgTypes
    {
        private List<EInaValueType> _args;

        public InaCalcFuncArgTypes(List<EInaValueType> args)
        {
            _args = new List<EInaValueType>();

            if (args != null)
            {
                _args.AddRange(args);
            }
        }

        public EInaValueType this[int lIndex]
        {
            get
            {
                return _args[lIndex - 1];
            }
        }

        public int Count => _args.Count;

        public IEnumerator GetEnumerator()
        {
            return _args.GetEnumerator();
        }
    }
}
