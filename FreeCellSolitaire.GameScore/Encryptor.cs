using System;
using System.Security.Cryptography;
using System.Text;

namespace FreeCellSolitaire.Data
{
    public class Encryptor
    {
        private string _privateKey = "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";
        private string _publicKey = "MIICITANBgkqhkiG9w0BAQEFAAOCAg4AMIICCQKCAgBtRBb2k2p3gE7Li4QQmFaCcqOt+7e8GRRF5jZ4Vlq1QYJ9y33soMC0V3uTMeccG/9n5FMpg7g22pHblkhBJNpjVxmcpq+p7WyKWj8pDYKlDnryRvuWPCFa/TJeOQNOZ0M3iRI0+KlETckbegFuIBrJQnxLDmu6PFkOUYzEXzjU5k7pBoLXtVOkM8a8+Z31I+Ms5GInbialkQCloKp/zawWOXBdFlVe8ZzP8djLuHizEeINx2FaXNqb+pHM0WT/UxUnoeymsA1WVazC3VKeq8bTJGn/DyjFGL9yUmt/NZuRHGlZj3F61Ju2ft1rEjtB9T7P+Fw/nJ0HBTab76WegfvlB3gCGh8d4NKv9gXDDdyj3ktu5N98lae7t/MdWEPB0DEEumN8OnZf1eC5MnmEfIaxcoUNew6n2KpLXJlPs3Lxz6tGN8wL8ERFFcSPuadT74QFYj0lThgfq3fH5BVs6HTg7elxfWWnu3fPCyTkMkQ3WhrrAZVt7A+3IayZlue/XkD/2LA1eJK/6Ff2m9sAmjIri/Koyq6yvnm+CfFGhahfJFTW/v4dgThDCKTfkJqGnF7FTRJdko1Ha/sI9M72MlR06qoXZ6Q5V65ynKZMFpqPKtNw/0804ou4+4meiY+ky11W890L99/Q+kkJt5bojcKtfsIRhD2Wu7YdGoRlDju6YQIDAQAB";
        public Encryptor()
        {
        }
        public string Encrypt(string content)
        {
            using var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_publicKey), out _);
            var encryptString = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(content),
                RSAEncryptionPadding.OaepSHA512));
            return encryptString;
        }

        public string Decrypt(string encryptText)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(_privateKey), out _);
            byte[] textBytes = Convert.FromBase64String(encryptText);
            byte[] decrypted = rsa.Decrypt(textBytes, RSAEncryptionPadding.OaepSHA512);
            string decryptedText = Encoding.UTF8.GetString(decrypted);
            return decryptedText;
        }
    }
}
