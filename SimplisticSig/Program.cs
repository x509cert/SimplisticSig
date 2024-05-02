using System.Text;
using System.Security.Cryptography;

string data = "We attack over the ridge at 1300Z";
byte[] dataBytes = Encoding.UTF8.GetBytes(data);

using var ecdsa = new ECDsaCng();
byte[] ecdsaSignature = ecdsa.SignData(dataBytes, HashAlgorithmName.SHA512);
Console.WriteLine($"ECDSA Sig: {Convert.ToBase64String(ecdsaSignature)}");
bool ecdsaIsVerified = ecdsa.VerifyData(dataBytes, ecdsaSignature);
Console.WriteLine($"ECDSA Signature Verified: {ecdsaIsVerified}");

Console.WriteLine();

using var rsa = new RSACryptoServiceProvider();
byte[] rsaSignature = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
Console.WriteLine($"RSA Sig: {Convert.ToBase64String(rsaSignature)}");
bool rsaIsVerified = rsa.VerifyData(dataBytes, rsaSignature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
Console.WriteLine($"RSA Signature Verified: {rsaIsVerified}");

