using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{

    public interface IInaCalcFuncs : IEnumerable
    {
        [DispId(2)]
        IInaCalcFunc Add(string strName, EInaFuncCategory funcCat = EInaFuncCategory.inaFuncCustom, string strFormat = "0", string strDescr = "0");
        [DispId(3)]
        void Remove(object vntIndex);
        [DispId(4)]
        void Reset();
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        IInaCalcFunc this[object vntFunc] { get; }
    }

    public class InaCalcFuncs : IInaCalcFuncs
    {
        private List<IInaCalcFunc> _funcs;

        public InaCalcFuncs()
        {
            _funcs = new List<IInaCalcFunc>();
        }

        public IInaCalcFunc this[object vntFunc]
        {
            get
            {
                if (vntFunc == null)
                {
                    throw new ArgumentNullException();
                }
                var func = _funcs.Find(f => string.Compare(f.Name, vntFunc.ToString(), true) == 0);
                return func;
            }
        }

        public int Count => _funcs.Count;

        public IInaCalcFunc Add(string strName, EInaFuncCategory funcCat = EInaFuncCategory.inaFuncCustom, string strFormat = "0", string strDescr = "0")
        {
            var func = _funcs.Find(f => string.Compare(f.Name, strName, true) == 0);
            if (func != null)
            {
                func.Category = funcCat;
                func.Format = strFormat;
                func.Descr = strDescr;
            }
            else
            {
                func = new InaCalcFunc
                {
                    Name = strName.ToLower(),
                    Category = funcCat,
                    Format = strFormat,
                    Descr = strDescr
                };
                _funcs.Add(func);
            }
            return func;
        }

        public IEnumerator GetEnumerator()
        {
            return _funcs.GetEnumerator();
        }

        public void Remove(object vntIndex)
        {
            if (vntIndex == null)
            {
                throw new ArgumentNullException();
            }
            if (int.TryParse(vntIndex.ToString(), out int index))
            {
                _funcs.RemoveAt(index);
                return;
            }
            var func = _funcs.Find(f => string.Compare(f.Name, vntIndex.ToString(), true) == 0);
            if (func != null)
            {
                _funcs.Remove(func);
            }
        }

        public void Reset()
        {
            _funcs = new List<IInaCalcFunc>();
        }
    }
}
