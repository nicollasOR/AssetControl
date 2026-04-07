using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetControlAPI_.Applications.Autenticacao
{
    public class GeradorTokenJWT
    {

        private readonly IConfiguration _config;
        public GeradorTokenJWT(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GerarToken(Usuario usuario)
        {
            if (usuario.Ativo == false)
            {
                throw new DomainException("Usuario Inátivo. Você não pode realizar alterações!");
            }
            //var chave = _config["Jwt:Key"]!;
            var chave = Environment.GetEnvironmentVariable("JWT_KEY");
            if (string.IsNullOrEmpty(chave))
                throw new DomainException("Chave não configurada no ENV");
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiraEmMinutos = int.Parse(_config["Jwt:ExpireEmMinutos"]!);
            
            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if (keyBytes.Length < 32)
            {
                throw new DomainException("JWT: Key precisa ter pelo menos 32 caracteres (256 bits).");
            }
            
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Cria as claims (informações) que serão incluídas no token
            //essas informações podem ser recuperadas pela API para identificar quem esta logado e quais permissões ele tem
            var claims = new List<Claim>
            {
        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nome),
        new Claim(ClaimTypes.Email, usuario.Email),
        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Nome),
        new Claim("NIF", usuario.NIF)
            };
            var token = new JwtSecurityToken(
                issuer: issuer,     //quem emitiu o token
                audience: audience, //para quem o token foi criado
                claims: claims,     //informações sobre o usuário e suas permissões
                expires: DateTime.Now.AddMinutes(expiraEmMinutos), //tempo de expiração do token
                signingCredentials: credentials //assinatura de segurança para garantir a integridade do token
            );

            //converte o token para uma string compacta que pode ser enviada ao cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
