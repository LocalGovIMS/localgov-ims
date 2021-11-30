using BusinessLogic.Interfaces.Services.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Classes.Cryptography
{
    public class MD5CryptographyService : ICryptographyService
    {
        public string GetHash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                output.Append(result[i].ToString("x2"));
            }

            return output.ToString();
        }
    }
}
