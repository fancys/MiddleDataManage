using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegratedManagement.Core;
using IntegratedManagement.Entity.Token;
using IntegratedManageMent.Application.Token;
using System.Threading.Tasks;

namespace IntegratedManagement.MiddleBaseAPI.Tests.OAuthUnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public async Task TestMD5()
        {
            string userName = "KaiSheng";
            string password = "kaisheng1q2w3e!@#";
            string passMd5 = Md5.GetMd5Hash(password);
            User user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.UserName = userName;
            user.Password = passMd5;
            user.IsActive = "Y";
            user.IsDelete = "N";
            user.CreateDate = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            UserApp _userApp = new UserApp();
            var saveRt = await _userApp.Save(user);
            Assert.IsTrue(saveRt);

            var checkUser = await _userApp.GetUser(userName, password);

            Assert.AreEqual(checkUser.UserName, userName);
            Assert.AreEqual(checkUser.Password, passMd5);

        }
    }
}
