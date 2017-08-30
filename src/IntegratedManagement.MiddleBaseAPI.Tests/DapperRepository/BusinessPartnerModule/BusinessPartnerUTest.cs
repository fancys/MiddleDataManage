using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegratedManagement.RepositoryDapper.BusinessPartnerModule;
using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using System.Threading.Tasks;

namespace IntegratedManagement.MiddleBaseAPI.Tests.DapperRepository.BusinessPartnerModule
{
    [TestClass]
    public class BusinessPartnerUTest
    {
        BusinessPartnerDapperRepository _BPDR = new BusinessPartnerDapperRepository();

        [TestMethod]
        public async Task TestSaveBusinessPartner()
        {
            BusinessPartner businessPartner = new BusinessPartner();
            businessPartner.CardCode = "C1000001";
            businessPartner.CardName = "testCardCode";
            businessPartner.CreateDate = DateTime.Now;
           // businessPartner.PlatformCode = "C01";
            var rt = await _BPDR.Save(businessPartner);
            Assert.AreEqual(rt.Code, 0);
        }
    }
}
