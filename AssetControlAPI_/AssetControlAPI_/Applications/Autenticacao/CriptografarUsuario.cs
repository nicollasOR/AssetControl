using System.Security.Cryptography;
using System.Text;

namespace AssetControlAPI_.Applications.Autenticacao
{
    public class CriptografarUsuario
    {

        public static byte[] CriptografarSenha(string senha)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] byteSenha = Encoding.UTF8.GetBytes(senha);
            byte[] senhaCriptografada = sha256.ComputeHash(byteSenha);

            return senhaCriptografada;
        }

    }
}
