namespace CarListApp.Api.Core.Dtos;

public record PasswordResetDto(string Email, string Token, string NewPassword);