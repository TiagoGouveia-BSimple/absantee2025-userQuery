using Domain.Models;

namespace InterfaceAdapters.Messages;

public record UserCreatedMessage(Guid Id, string Names, string Surnames, string Email, PeriodDateTime PeriodDateTime);
