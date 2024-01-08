using NDataMapper.Attributes;

namespace NDataModel
{
    public class MainMenuModel
    {
        //Note : DataNames("Id", "Id") - Here First field "Id" is field from DataTable and second field "Id" is property Name.
        [DataNames("Id", "Id")]
        public int Id { get; set; }

        [DataNames("MenuName", "MenuName")]
        public string MenuName { get; set; }

        [DataNames("OrderNo", "OrderNo")]
        public int OrderNo { get; set; }

        [DataNames("Controller", "Controller")]
        public string Controller { get; set; }

        [DataNames("TitleName", "TitleName")]
        public string TitleName { get; set; }

        [DataNames("MenuIcon", "MenuIcon")]
        public string MenuIcon { get; set; }

        [DataNames("IsActive", "IsActive")]
        public bool IsActive { get; set; }
    }
}
