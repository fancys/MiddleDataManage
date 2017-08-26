using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using IntegratedManagement.RepositoryDapper.SalesModule;
using IntegratedManagement.Entity.SalesModule.InvoicesReturn;

namespace IntegratedManagement.MiddleBaseAPI.Tests.DapperRepository.SalesModule
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {

            InvoicesReturnDapperRepository dr = new InvoicesReturnDapperRepository();
            InvoicesReturn invoiceReturn = new InvoicesReturn();
            invoiceReturn.ZFPDM = "tset";
            invoiceReturn.ZFPHM = "1111";
            invoiceReturn.ZFPJE = 542;
            invoiceReturn.ZFPZT = "N";
            invoiceReturn.ZKPRQ = DateTime.Now;

            var rt = await dr.Save(invoiceReturn);
            Assert.AreEqual(rt.Code, 0);
        }

        [TestMethod]
        public async Task TestPostInvoiceReturn()
        {
            //DataHanding dh = new DataHanding();
            //string jsonstr = "[{\"ZFPHM\":'10006',\"ZFPDM\":'A005',\"ZKPRQ\":'2017-06-21',\"ZFPZT\":'TimDuncan',\"ZFPJE\":1200,\"ZSKPH\":'23'},{\"ZFPHM\":'10007',\"ZFPDM\":'A005',\"ZKPRQ\":'2017-06-21',\"ZFPZT\":'TimDuncan',\"ZFPJE\":1200,\"ZSKPH\":'23'}]";
            //var rt = await dh.PostInvoicesReturn(jsonstr);
            //var ob = Newtonsoft.Json.JsonConvert.DeserializeObject<Result<SaveResult>>(rt);
            //Assert.AreEqual(ob.Code, 0);


        }
    }
}
