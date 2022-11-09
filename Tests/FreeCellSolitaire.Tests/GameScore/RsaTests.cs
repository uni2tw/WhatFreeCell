using FreeCellSolitaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Tests.GameScore
{
    public class EncryptorTests
    {
        [Test]
        public void EncryptAndDecrypt()
        {
            var rsa = new Encryptor();
            var encryptStr = rsa.Encrypt("昔日吃包子噎死");
            Assert.AreNotEqual("昔日吃包子噎死", encryptStr);

            var decryptStr = rsa.Decrypt(encryptStr);
            Assert.AreEqual("昔日吃包子噎死", decryptStr);
        }
    }
}
