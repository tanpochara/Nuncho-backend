using Agoda.IoC.Core;
using Nuncho.WebApi.entities;
using Nuncho.WebApi.repository;

namespace Nuncho.WebApi.service;

[RegisterTransient]
public class UserService
{
    private readonly UserRepository _userRepository;
    
    public UserService(UserRepository userRepository)
    {
        this._userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await this._userRepository.GetAllUsers();
    }
    
}