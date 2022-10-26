using FreeCellSolitaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Tests.GameRecord
{
    public class EncryptorTests
    {
        [Test]
        public void EncryptAndDecrypt()
        {
            var rsa = new Encryptor();
            var encryptStr = rsa.Encrypt("習包子死一死");
            Assert.AreNotEqual("習包子死一死", encryptStr);

            var decryptStr = rsa.Decrypt(encryptStr);
            Assert.AreEqual("習包子死一死", decryptStr);
        }
    }
}
