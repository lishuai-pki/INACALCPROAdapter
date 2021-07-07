using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    public interface IInaCalcFuncArgVals : IEnumerable
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        object this[int lIndex] { get; }
    }

    public class InaCalcFuncArgVals : IInaCalcFuncArgVals
    {
        private List<object> _argVals;

        public InaCalcFuncArgVals(List<object> argVals)
        {
            _argVals = new List<object>();
            if (argVals != null)
            {
                _argVals.AddRange(argVals);
            }
        }

        public object this[int lIndex]
        {
            get
            {
                if (lIndex < 0 || lIndex >= _argVals.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _argVals[lIndex];
            }
        }

        public int Count => _argVals.Count;

        public IEnumerator GetEnumerator()
        {
            return _argVals.GetEnumerator();
        }
    }
}
