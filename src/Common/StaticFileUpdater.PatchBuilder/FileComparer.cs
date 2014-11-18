using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StaticFileUpdater.PatchBuilder
{
    public class FileComparer
    {
		public static Task<bool> FilesAreEqualAsync(string filePath1, string filePath2)
		{
		    return Task.Run(() => FilesAreEqual(filePath1, filePath2));
		}

        public static bool FilesAreEqual(string filePath1, string filePath2)
        {
            bool isEqual = false;

            using (var reader1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read))
                {
                    byte[] hash1;
                    byte[] hash2;

                    using (var md51 = new MD5CryptoServiceProvider())
                    {
                        md51.ComputeHash(reader1);
                        hash1 = md51.Hash;
                    }

                    using (var md52 = new MD5CryptoServiceProvider())
                    {
                        md52.ComputeHash(reader2);
                        hash2 = md52.Hash;
                    }

                    int j = 0;
                    for (j = 0; j < hash1.Length; j++)
                    {
                        if (hash1[j] != hash2[j])
                        {
                            break;
                        }
                    }

                    if (j == hash1.Length)
                        isEqual = true;
                }
            }

            return isEqual;
        }
    }
}
