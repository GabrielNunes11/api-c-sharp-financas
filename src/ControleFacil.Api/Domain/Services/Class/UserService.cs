using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Class;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.UserDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControleFacil.Api.Domain.Services.Class
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, TokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponseDTO> AuthUserLogin(UserLoginRequestDTO userLoginRequest)
        {
            UserResponseDTO user = await GetByEmail(userLoginRequest.Email);

            var passwordHash = PasswordHashGenerator(userLoginRequest.Password);

            if (user is null || user.Password != passwordHash)
            {
                throw new AuthenticationException("Usuário e/ou Senha inválidos.");
            }

            return new UserLoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = _tokenService.TokenGenerator(_mapper.Map<User>(user))
            };
        }

        public async Task<UserResponseDTO> Add(UserRequestDTO entity, Guid userId)
        {
            var user = _mapper.Map<User>(entity);

            user.Password = PasswordHashGenerator(user.Password);
            user.RegisterDate = DateTime.Now;

            user = await _userRepository.Add(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAll(Guid userId)
        {
            var users = await _userRepository.GetAll();

            return users.Select(user => _mapper.Map<UserResponseDTO>(user));
        }

        public async Task<UserResponseDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> GetById(Guid id, Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task Inactivation(Guid id, Guid userId)
        {
            var user = await GetById(Guid.Empty, userId) ?? throw new Exception("Usuário não encontrado.");

            await _userRepository.Delete(_mapper.Map<User>(user));
        }

        public async Task<UserResponseDTO> UpdateById(Guid id, UserRequestDTO entity, Guid userId)
        {
            _ = await _userRepository.GetById(userId) ?? throw new Exception("Usuário não encontardo.");

            var user = _mapper.Map<User>(entity);
            user.Id = userId;
            user.Password = PasswordHashGenerator(entity.Password);

            await _userRepository.Update(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        private string PasswordHashGenerator(string password)
        {
            string passwordHash;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytePassword = Encoding.UTF8.GetBytes(password);

                byte[] bytePasswordHash = sha256.ComputeHash(bytePassword);
                passwordHash = BitConverter.ToString(bytePasswordHash).Replace("-", "").Replace("-", "").ToLower();
            }

            return passwordHash;
        }
    }
}