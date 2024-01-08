using System;
using System.Collections.Generic;
using NDataMapper.Attributes;

namespace NDataModel
{
    public class MainMenuMaster
    {
        //public int Id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }

        public List<SubMenu> Child;
    }

    public class SubMenu
    {
        public string text { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }
    }
}