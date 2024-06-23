using System;
using Flower.Service.Dtos.UserDtos;

namespace Flower.Service.Interfaces
{
	public interface IAuthService
	{
        string Login(UserLoginDto loginDto);
    }
}

