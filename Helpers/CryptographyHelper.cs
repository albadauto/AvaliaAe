using System.Security.Cryptography;
using System.Text;

namespace AvaliaAe.Helpers
{
	public class CryptographyHelper
	{
		private readonly HashAlgorithm _crypto;
		public CryptographyHelper(HashAlgorithm crypto)
		{
			_crypto = crypto;
		}
		public string hashPassword(string password)
		{
			byte[] pass = Encoding.Default.GetBytes(password);
			var crypto = _crypto.ComputeHash(pass);
			return Convert.ToBase64String(crypto);
		}

		public bool ComparePassword(string password, string salt) {
			if (string.IsNullOrEmpty(salt)) throw new NullReferenceException("Insert a password");

			var encryptedPassword = _crypto.ComputeHash(Encoding.UTF8.GetBytes(password));

			var sb = new StringBuilder();

			foreach(var caracter in encryptedPassword)
			{
				sb.Append(caracter.ToString("X2"));
			}

			return sb.ToString() == salt;
		
		}
	}
}
