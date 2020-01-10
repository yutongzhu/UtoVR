using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;


/// <summary>
/// AES加密
/// </summary>
public class AES
{
	/// <summary>
	/// 向量
	/// </summary>
	private static byte[] iv = { 0x31, 0x52, 0x64, 0x79, 0x4F, 0x72, 0x6B, 0x79, 0x54, 0x2E, 0x6C, 0x77, 0x3D, 0x21, 0x5E, 0x2F };
	/// <summary>
	/// 创建加密对象
	/// </summary>
	/// <param name="password"></param>
	/// <returns></returns>
	private static SymmetricAlgorithm CreateRijndael(string password)
	{
		password = Utils.GetSubString(password, 32, "");
		password = password.PadRight(32, ' ');

		SymmetricAlgorithm algorithm = Rijndael.Create();
		algorithm.Mode = CipherMode.CBC;
		algorithm.Key = Encoding.UTF8.GetBytes(password);
		algorithm.IV = iv;
		algorithm.Padding = PaddingMode.Zeros;
		return algorithm;
	}
	/// <summary>
	/// 加密
	/// </summary>
	/// <param name="input"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	public static string Encrypt(string input, string password)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		using (SymmetricAlgorithm algorithm = CreateRijndael(password))
		{
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
			{
				byte[] bytes = UTF32Encoding.Default.GetBytes(input);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.Flush();
			}
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}
	/// <summary>
	/// 解密
	/// </summary>
	/// <param name="input"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	public static string Decrypt(string input, string password)
	{
		using (MemoryStream inputMemoryStream = new MemoryStream(Convert.FromBase64String(input)))
		using (SymmetricAlgorithm algorithm = CreateRijndael(password))
		{
			using (CryptoStream cryptoStream = new CryptoStream(inputMemoryStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
			{
				StreamReader sr = new StreamReader(cryptoStream);
				return sr.ReadToEnd();
			}
		}
	}
}
