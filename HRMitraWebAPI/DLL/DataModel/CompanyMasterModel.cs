using System;
using NDataMapper.Attributes;

namespace NDataModel
{
    public class CompanyMasterModel
    {
        [DataNames("Id", "Id")]
        public long Id { get; set; }

        [DataNames("CompanyCode", "CompanyCode")]
        public string CompanyCode { get; set; }

        [DataNames("CompanyName", "CompanyName")]
        public string CompanyName { get; set; }

        [DataNames("Address1", "Address1")]
        public string Address1 { get; set; }

        [DataNames("Address2", "Address2")]
        public string Address2 { get; set; }

        [DataNames("Country", "Country")]
        public string Country { get; set; }

        [DataNames("State", "State")]
        public string State { get; set; }

        [DataNames("City", "City")]
        public string City { get; set; }

        [DataNames("PinCode", "PinCode")]
        public string PinCode { get; set; }

        [DataNames("CompanyEmail", "CompanyEmail")]
        public string CompanyEmail { get; set; }

        [DataNames("ContactNumber", "ContactNumber")]
        public string ContactNumber { get; set; }

        [DataNames("WhatsAppNumber", "WhatsAppNumber")]
        public string WhatsAppNumber { get; set; }

        [DataNames("IsGSTRegister", "IsGSTRegister")]
        public int IsGSTRegister { get; set; }

        [DataNames("IsGSTRegisterName", "IsGSTRegisterName")]
        public string IsGSTRegisterName { get; set; }

        [DataNames("GSTNumber", "GSTNumber")]
        public string GSTNumber { get; set; }

        [DataNames("CINNumber", "CINNumber")]
        public string CINNumber { get; set; }

        [DataNames("WebSite", "WebSite")]
        public string WebSite { get; set; }

        [DataNames("LinkedInPage", "LinkedInPage")]
        public string LinkedInPage { get; set; }

        [DataNames("InstagramPage", "InstagramPage")]
        public string InstagramPage { get; set; }

        [DataNames("FaceBookPage", "FaceBookPage")]
        public string FaceBookPage { get; set; }

        [DataNames("Logo", "Logo")]
        public byte[] Logo { get; set; }

        [DataNames("IsActive", "IsActive")]
        public bool IsActive { get; set; }

        [DataNames("IsActiveName", "IsActiveName")]
        public string IsActiveName { get; set; }

        [DataNames("UpdatedBy", "UpdatedBy")]
        public int UpdatedBy { get; set; }

        [DataNames("UpdatedByName", "UpdatedByName")]
        public string UpdatedByName { get; set; }

        [DataNames("UpdatedDate", "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
