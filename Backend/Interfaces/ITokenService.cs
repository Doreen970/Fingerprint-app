﻿using Backend.Models;

namespace Backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Staff user);
    }
}
