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
            var encryptStr = rsa.Encrypt("我沒上車");
            Console.WriteLine($"encrypted: {encryptStr}");
            Assert.AreNotEqual("我沒上車", encryptStr);

            var decryptStr = rsa.Decrypt(encryptStr);
            Console.WriteLine($"decrypted: {decryptStr}");
            Assert.AreEqual("我沒上車", decryptStr);
        }
    }
}
