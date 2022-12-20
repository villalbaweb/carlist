﻿using System.ComponentModel.DataAnnotations;

namespace CarListApp.Api.Core.Dto;

public record IdentityUserDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string Role { get; set; }
}