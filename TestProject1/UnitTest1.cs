using Microsoft.VisualStudio.TestTools.UnitTesting;
using camioncitos.Controllers.Usuario;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        string resultado = new UsuarioController().Coso();
            Assert("test", resultado);
        }
    }
}