using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{

    public class UserGroup
    {
        [Key]
        public int GroupID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}--{0} Characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 用户组类型<br />
        /// 0普通类型（普通注册用户），1特权类型（像VIP之类的类型），3管理类型（管理权限的类型）
        /// </summary>
        [Required(ErrorMessage = "GroupType is required")]
        [Display(Name = "Group Type")]
        public int GroupType { get; set; }


        [Required(ErrorMessage = "Description")]
        [StringLength(50, ErrorMessage = "Description shoud be more than {0} letters")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

}
