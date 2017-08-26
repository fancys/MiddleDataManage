using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 15:00:04
	===============================================================================================================================*/
    public class RefreshToken
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }
    }
}
