using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    public interface IInaCalcAtoms
    {
        [DispId(-4)]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int Count { get; }

        [DispId(0)]
        IInaCalcAtom this[object vntAtom] { get; }
    }

    public class InaCalcAtoms : IInaCalcAtoms
    {
        private List<IInaCalcAtom> _atomList = new List<IInaCalcAtom>();
        private InaCalcProClass _inaCalcProClass;

        public InaCalcAtoms(InaCalcProClass inaCalcProClass)
        {
            _inaCalcProClass = inaCalcProClass;
        }

        public IInaCalcAtom this[object vntAtom]
        {
            get
            {
                if (vntAtom == null)
                {
                    throw new System.ArgumentNullException("vntAtom can't be null.");
                }

                if (int.TryParse(vntAtom.ToString(), out int index))
                {
                    return _atomList[index];
                }

                var atom = _atomList.Find(a => string.Compare(a.Name, vntAtom.ToString(), true) == 0);
                if (atom == null)
                {
                    atom = new InaCalcAtom(vntAtom.ToString(), _inaCalcProClass);
                    _atomList.Add(atom);
                }
                return atom;
            }
        }

        public int Count => _atomList.Count;

        public IEnumerator GetEnumerator()
        {
            return _atomList.GetEnumerator();
        }
    }
}
