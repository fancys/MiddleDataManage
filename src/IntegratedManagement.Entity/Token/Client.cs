using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 14:59:15
    *	客户端信息
	===============================================================================================================================*/
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
