using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.IRepository.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.Repository.PurchaseModule
{
    public class PurchaseReturnRepository : IPurchaseReturnRepository
    {
        public async Task<List<PurchaseReturn>> GetPurchaseReturn(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public PurchaseReturn GetPurchaseReturn(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public Task<SaveResult> Save(PurchaseReturn PurchaseReturn)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSyncData(DocumentSync documentSyncReuslt)
        {
            throw new NotImplementedException();
        }
    }
}
