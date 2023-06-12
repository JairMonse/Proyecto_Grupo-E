using camioncitos.Controllers.Usuario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UsuarioController obj = new UsuarioController();
            string resultado = obj.Coso();
            Assert.AreEqual("test",resultado);
        }
    }
}