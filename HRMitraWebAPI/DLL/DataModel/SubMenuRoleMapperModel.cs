using NDataMapper.Attributes;

namespace NDataModel
{
    public class SubMenuRoleMapperModel
    {
        //Note : DataNames("Id", "Id") - Here First field "Id" is field from DataTable and second field "Id" is property Name.
        [DataNames("Id", "Id")]
        public int Id { get; set; }

        [DataNames("RoleId", "RoleId")]
        public int? RoleId { get; set; }

        [DataNames("RoleName", "RoleName")]
        public string RoleName { get; set; }

        [DataNames("SubMenuId", "SubMenuId")]
        public int? SubMenuId { get; set; }

        [DataNames("TitalName", "TitalName")]
        public string TitalName { get; set; }

        [DataNames("CanAdd", "CanAdd")]
        public bool CanAdd { get; set; }

        [DataNames("CanEdit", "CanEdit")]
        public bool CanEdit { get; set; }

        [DataNames("CanDelete", "CanDelete")]
        public bool CanDelete { get; set; }

        [DataNames("CanView", "CanView")]
        public bool CanView { get; set; }

        [DataNames("IsActive", "IsActive")]
        public bool IsActive { get; set; }
    }
}
