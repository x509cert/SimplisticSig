using System.Text;
using System.Security.Cryptography;

string data = "Hello, world!";

using var rsa = new RSACryptoServiceProvider();

byte[] dataBytes = Encoding.UTF8.GetBytes(data);
byte[] hash = SHA256.HashData(dataBytes);

byte[] signature = SignHash(hash, rsa.ExportParameters(true));

Console.WriteLine("Data: " + data);
Console.WriteLine("Hash: " + BitConverter.ToString(hash).Replace("-",""));
Console.WriteLine("Signature: " + BitConverter.ToString(signature).Replace("-",""));

bool isVerified = VerifyHash(hash, signature, rsa.ExportParameters(false));
Console.WriteLine("Signature Verified: " + isVerified);
static byte[] SignHash(byte[] hash, RSAParameters privateKey)
{
    using var rsa = new RSACryptoServiceProvider();
    rsa.ImportParameters(privateKey);
    return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
}

static bool VerifyHash(byte[] hash, byte[] signature, RSAParameters publicKey)
{
    using var rsa = new RSACryptoServiceProvider();
    rsa.ImportParameters(publicKey);
    return rsa.VerifyHash(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
}

