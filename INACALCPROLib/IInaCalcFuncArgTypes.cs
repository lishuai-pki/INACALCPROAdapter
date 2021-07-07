using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    public interface IInaCalcFuncArgTypes : IEnumerable
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        EInaValueType this[int lIndex] { get; }
    }

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
                if (lIndex < 0 || lIndex >= _args.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _args[lIndex];
            }
        }

        public int Count => _args.Count;

        public IEnumerator GetEnumerator()
        {
            return _args.GetEnumerator();
        }
    }
}
