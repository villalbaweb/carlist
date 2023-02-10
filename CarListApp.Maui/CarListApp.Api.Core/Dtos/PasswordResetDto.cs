namespace CarListApp.Api.Core.Dtos;

public record PasswordResetDto(string email, string token, string newPassword);