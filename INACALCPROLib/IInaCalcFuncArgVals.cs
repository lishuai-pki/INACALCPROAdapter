using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [ComVisible(true)]
    [Guid("1BE2ED24-891C-421E-86B1-7D8630B3856E")]
    public interface IInaCalcFuncArgVals : IEnumerable
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
        object this[int lIndex] { get; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("1C33CF33-ACF1-47CA-A25F-3CC04D7578C5")]
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
                return _argVals[lIndex - 1];
            }
        }

        public int Count => _argVals.Count;

        public IEnumerator GetEnumerator()
        {
            return _argVals.GetEnumerator();
        }
    }
}
