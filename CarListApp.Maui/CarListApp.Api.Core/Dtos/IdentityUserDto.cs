﻿using System.ComponentModel.DataAnnotations;

namespace CarListApp.Api.Core.Dtos;

public record IdentityUserDto
{
    public string Email { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string Role { get; set; }
}
