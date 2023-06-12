using Microsoft.VisualStudio.TestTools.UnitTesting;
using camioncitos.Controllers.Usuario;

namespace CamioncitoUnitTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CrearUsuario()
        {
            string result = UsuarioController.Coso();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("test",result);
        }

        [TestMethod]
        public void Test1()
        {
        }
    }
}