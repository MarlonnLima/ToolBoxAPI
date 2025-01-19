using System.Security.Cryptography;
using System.Text;

namespace ToolBoxAPI.Models
{
    public class Link
    {
        public int Id { get; set; }
        /// <summary>
        /// hash code to locate link
        /// </summary>
        public string Hash { get; set; }
        public string Destination { get; set; }

        public void GenerateHash()
        {
            using(HashAlgorithm algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(Destination));
                StringBuilder sb = new StringBuilder();
                foreach(byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                Hash = sb.ToString().Substring(0, 8);
            }
        }
    }
}
