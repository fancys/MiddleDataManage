using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 9:50:42
    *	用户表
	===============================================================================================================================*/
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        
        /// <summary>
        /// 是否可用
        /// </summary>
        public string IsActive { get; set; }

        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Updator { get; set; }

        public string IsDelete { get; set; }
    }
}
