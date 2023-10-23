using API.DTOs;
using API.Models;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly SchoolDBContext _context;
              private readonly IConfiguration _configuration;

        public UserRepository(IMapper mapper, SchoolDBContext context,IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
             _configuration = configuration;
        }


public async Task<UserDTO> GetLogin(UserDTO UserDTO)
{
    var user = await _context.Users
        .FirstOrDefaultAsync(c => c.Email == UserDTO.email && c.Password == UserDTO.password);

    if (user != null)
    {
        return _mapper.Map<UserDTO>(user);
    }

    return null;
}

 public async Task<UserDTO> PostUser(UserDTO UserDTO)
{
    Console.WriteLine(UserDTO);
    var user = _mapper.Map<User>(UserDTO);

    // Save the user to the database
    _context.Users.Add(user);
    _context.SaveChanges();

    // Generate a JWT token for the newly created user
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // User ID as subject
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at
        // Add other claims as needed (e.g., user roles, etc.)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: DateTime.UtcNow.AddMinutes(10), // Set the token expiration time as needed
        signingCredentials: signIn);

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    // Update the user's Token property in the database
    user.Token = tokenString;
    _context.SaveChanges();

    // Return the newly created user DTO along with the token
    return new UserDTO
    {
        userId = user.UserId,
        name = user.Name,
    
        token = tokenString
    };
}
    }

}
