namespace CarListApp.Api.Core.Dtos;

public record SendEmailDto(string From, string To, string Subject, string Body);
