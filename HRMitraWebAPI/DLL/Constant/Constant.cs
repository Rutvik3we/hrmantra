using System;
using System.Runtime.Serialization;
/// <summary>
/// System global variable / Constatnt / Enum
/// </summary>
namespace NConstant
{
    public class Constant
    {
        public enum APIResponseStatus : int
        {
            Success = 0,
            NoDataFound = 1,
            ExceptionOrFailed = 2,
            Conflict = 3,
            Unauthorized = 4
        }

        public enum QRCodeType
        {
            ItemPalletization = 0,
            PalletMaster = 1,
            SamplingMaterial = 2
        }

        public enum VerifyQRCode
        {
            Success = 0,
            QRCodeNotValid = 1,
            QRCodeNotForItem = 2,
            QRCodeNotForPallet = 3,
            QRCodeNotForSample = 4
        }

        public enum QCStatus : int
        {
            QUARANTINED = 0,
            APPROVED = 1,
            REJECTED = 2,
            BLOCKED = 3
        }

        public enum LastActionList
        {
            AssignedPallet = 1,
            ApprovedPallet = 2,
        }

        public enum AttendanceResponse
        {
            NotRange = 0,
            PunchIn = 1,
            PunchOut = 2,
            ErrorPunchIn = 3,
            ErrorPunchOut = 4,

        }

        public enum BreakTimeResponse
        {
            BreakStartTime = 0,
            BreakEndTime = 1
        }
    }
}
