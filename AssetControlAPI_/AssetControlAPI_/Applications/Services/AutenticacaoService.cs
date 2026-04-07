using AssetControlAPI_.Applications.Autenticacao;
using AssetControlAPI_.Applications.DTOs.AutenticacaoDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class AutenticacaoService
    {

        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJWT _tokenJWT;
        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJWT tokenJWT)
        { 
            _repository = repository;
            _tokenJWT = tokenJWT;
            
        }

      
        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitada = CriptografarUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitada.SequenceEqual(senhaHashBanco);
        }

        public TokenDTO Login(LoginDTO loginDTO)
        {

            Usuario usuario = _repository.BuscarPor_NIF_TipoUsuario(loginDTO.NIF);
            if (usuario == null)
                throw new DomainException("NIF ou senha inválidos ");

            if (usuario.Ativo != true)
                throw new DomainException("Usuário inativo");

            if (!VerificarSenha(loginDTO.Senha, usuario.Senha))
                throw new DomainException("NIF ou senha inválidos");

            string token = _tokenJWT.GerarToken(usuario);

            return new TokenDTO
            {
                Token = token,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.Nome
            };


        }

        public void TrocarPrimeiraSenha(Guid usuarioId, TrocarPrimeiraSenha dto)
        {
            ValidarCriacaoDTO.ValidarSenha(dto.SenhaAtual);
            ValidarCriacaoDTO.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioId);
            if (usuario == null)
                throw new DomainException("Usuário inválido ");

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha))
                throw new DomainException("Senha atual inválido");

            if (dto.SenhaAtual == dto.NovaSenha)
                throw new DomainException("As senhas devem ser diferentes");

            usuario.Senha = CriptografarUsuario.CriptografarSenha(dto.SenhaAtual);
            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }

    }
}
