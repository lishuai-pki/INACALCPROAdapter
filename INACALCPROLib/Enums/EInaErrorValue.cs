using System.Runtime.InteropServices;

namespace INACALCPROLib
{
    [Guid("9A0CE312-A250-4BF0-B20C-AEDAB5F97A3C")]
    [ComVisible(true)]
    public enum EInaErrorValue
    {
        inaErrNone = 0,
        inaErrSyntax = 1,
        inaErrCircRef = 2,
        inaErrTypeMismatch = 3,
        inaErrDiv0 = 4,
        inaErrIndefinite = 5,
        inaErrInfinite = 6,
        inaErrInvalidDate = 7,
        inaErrFunc = 8,
        inaErrValue = 9,
        inaErrCustFunc = 10,
        inaErrRef = 11,
        inaErrCount = 12
    }
}
