using NDataMapper.Attributes;

namespace NDataModel
{
    public class SubMenuModel
    {
        //Note : DataNames("Id", "Id") - Here First field "Id" is field from DataTable and second field "Id" is property Name.
        [DataNames("Id", "Id")]
        public int Id { get; set; }

        [DataNames("MainMenuId", "MainMenuId")]
        public int MainMenuId { get; set; }

        [DataNames("SubMenuName", "SubMenuName")]
        public string SubMenuName { get; set; }

        [DataNames("Controller", "Controller")]
        public string Controller { get; set; }

        [DataNames("OrderNo", "OrderNo")]
        public int OrderNo { get; set; }

        [DataNames("TitleName", "TitleName")]
        public string TitleName { get; set; }

        [DataNames("SubMenuIcon", "SubMenuIcon")]
        public string SubMenuIcon { get; set; }

        [DataNames("IsActive", "IsActive")]
        public bool IsActive { get; set; }
    }
}
