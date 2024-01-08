using NDataMapper.Attributes;
using System;

namespace NDataModel
{
    public class APIMasterModel
    {
        //Note : DataNames("Id", "Id") - Here First field "Id" is field from DataTable and second field "Id" is property Name.
        [DataNames("Id", "Id")]
        public int Id { get; set; }

        [DataNames("UserId", "UserId")]
        public int UserId { get; set; }

        [DataNames("APIKey", "ApiKey")]
        public string ApiKey { get; set; }

        [DataNames("CreatedDate", "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [DataNames("ExpiryDate", "ExpiryDate")]
        public DateTime? ExpiryDate { get; set; }
    }
}
