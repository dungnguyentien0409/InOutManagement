using System;
using Interfaces;
using Entities;
using Dto;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Implementations
{
	public class UserService : IUserService
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;
		IPasswordHelper _passwordHelepr;
		IConfiguration _config;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHelper passwordHelper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_passwordHelepr = passwordHelper;
			_config = config;
		}

		public bool SignUp(UserInfoDto userDto)
		{
			if (!VerifyUserInfo(userDto))
			{
				return false;
			}

			var userInfo = new UserInfo();
			userInfo = _mapper.Map<UserInfo>(userDto);
			userInfo.Id = Guid.NewGuid();
			userInfo.Salt = _passwordHelepr.GenerateSalt();
			userInfo.HashedPassword = _passwordHelepr.HashPassword(userDto.Password, userInfo.Salt);

			_unitOfWork.UserInfo.Add(userInfo);

			_unitOfWork.Save();

			return true;
		}

		public string SignIn(UserInfoDto userDto)
		{
			var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userDto.UserName).FirstOrDefault();

			if (userItem == null
				|| !_passwordHelepr.VerifyPassword(userDto.Password, userItem.Salt, userItem.HashedPassword))
			{
				return "";
			}

			return GenerateJwtToken(userDto);
		}

		private bool VerifyUserInfo(UserInfoDto userDto)
		{
			if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
			{
				return false;
			}

			var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userDto.UserName).FirstOrDefault();

			if (userItem != null)
			{
				return false;
			}

			return true;
		}

		private string GenerateJwtToken(UserInfoDto userDto)
		{
			var secretKey = _config.GetValue<string>("Jwt:SecretKey");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.UserName),
				new Claim(ClaimTypes.Role, "Admin")
            };
			var issuer = _config.GetValue<string>("Jwt:Issuer");
			var audience = _config.GetValue<string>("Jwt:Audience");
			var expire = _config.GetValue<int>("Jwt:ExpireInMinutes");

            var token = new JwtSecurityToken(issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateCurrentToken(string token)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = _config.GetValue<string>("Issuer");
            var myAudience = _config.GetValue<string>("Audience");

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

