using System;
using NDataMapper.Attributes;
namespace NDataModel
{
    public class UserModel
    {
        [DataNames("Id", "Id")]
        public long Id { get; set; }

        [DataNames("EmpId", "EmpId")]
        public long EmpId { get; set; }

        [DataNames("LoginName", "LoginName")]
        public string LoginName { get; set; }

        [DataNames("EmployeeName", "EmployeeName")]
        public string EmployeeName { get; set; }

        [DataNames("Password", "Password")]
        public string Password { get; set; }

        [DataNames("TransactionPassword", "TransactionPassword")]
        public string TransactionPassword { get; set; }

        [DataNames("ExpiryDate", "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [DataNames("LastLogin", "LastLogin")]
        public DateTime LastLogin { get; set; }

        [DataNames("OTPCode", "OTPCode")]
        public string OTPCode { get; set; }

        [DataNames("OTPExpiryDate", "OTPExpiryDate")]
        public DateTime OTPExpiryDate { get; set; }

        [DataNames("RoleId", "RoleId")]
        public int RoleId { get; set; }

        [DataNames("RoleName", "RoleName")]
        public string RoleName { get; set; }

        [DataNames("RoleType", "RoleType")]
        public string RoleType { get; set; }

        [DataNames("CompanyId", "CompanyId")]
        public long CompanyId { get; set; }

        [DataNames("CompanyCode", "CompanyCode")]
        public string CompanyCode { get; set; }

        [DataNames("EmpPhoto", "EmpPhoto")]
        public byte[] EmpPhoto { get; set; }

        [DataNames("IsActive", "IsActive")]
        public bool IsActive { get; set; }

        [DataNames("UpdatedBy", "UpdatedBy")]
        public bool UpdatedBy { get; set; }

        [DataNames("UpdatedDate", "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [DataNames("UpdatedByName", "UpdatedByName")]
        public string UpdatedByName { get; set; }

        [DataNames("CompanyBranchId", "CompanyBranchId")]
        public long CompanyBranchId { get; set; }

        [DataNames("BranchCode", "BranchCode")]
        public string BranchCode { get; set; }
    }
}