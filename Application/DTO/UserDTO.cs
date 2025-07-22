using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO;

public record UserDTO
{
    public Guid Id { get; set; }
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public PeriodDateTime Period { get; set; }
    public UserDTO()
    {
    }
}
