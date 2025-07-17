using Application.DTO;
using Domain.Interfaces;
using Domain.Models;

namespace Application.IService;

public interface IUserService
{
    Task<UserDTO> Add(UserDTO userDTO);
    Task AddConsumed(Guid id, string names, string surnames, string email, PeriodDateTime periodDateTime);
    Task<bool> Exists(Guid Id);
    Task<IEnumerable<UserDTO>> GetAll();
    Task<UserDTO?> GetById(Guid Id);
    Task<UserDTO?> UpdateActivation(Guid Id, ActivationDTO activationDTO);
}
