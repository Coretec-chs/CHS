namespace Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetMenus:Entity
    {

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string MenuTitle { get; set; }

        [Required]
        [StringLength(150)]
        public string MenuName { get; set; }

        [Required]
        [StringLength(128)]
        public string ParentId { get; set; }

        [Required]
        [StringLength(150)]
        public string Action { get; set; }

        [Required]
        [StringLength(150)]
        public string Controller { get; set; }

        [StringLength(150)]
        public string RouteData { get; set; }

        public int MenuOrder { get; set; }

        [StringLength(50)]
        public string MenuIcon { get; set; }

        public int MenuLevel { get; set; }

        public bool ExpandOnly { get; set; }

        public bool IsVisible { get; set; }
    }
}
