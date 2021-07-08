using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [Guid("38FD8E5B-016B-4C0F-87B0-2F0C27562837")]
    [ComVisible(true)]
    public enum EInaValueType
    {
        inaValEmpty = 0,
        inaValText = 1,
        inaValNumber = 2,
        inaValDate = 4,
        inaValBool = 8,
        inaValError = 16
    }
}
