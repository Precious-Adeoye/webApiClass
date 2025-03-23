using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using webApiClass.DTO;
using webApiClass.Iservice;
using webApiClass.model;
using static webApiClass.DTO.Responses;

namespace webApiClass.Services
{
    public class Authservices : IAuth
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public Authservices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }
        public async Task<GeneralResponse> CreateUser(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return new GeneralResponse(false, "cannot be null/empty");
            }
            var newUser = new ApplicationUser()
            {
                fullName = registerDTO.FullName,
                Email = registerDTO.EmailAddress,
                PasswordHash = registerDTO.Password,
                UserName = registerDTO.EmailAddress
            };

            //var newUser = mapper.Map<ApplicationUser>(registerDTO);

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new GeneralResponse(false, "This Email already exist");
            }
            var createUser = await userManager.CreateAsync(newUser!, registerDTO.Password);

            if (!createUser.Succeeded)
            {
                return new GeneralResponse(false, "An erorr occured");
            }
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin == null)
            { 
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Admin Account created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser == null)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "user" });
                    await userManager.AddToRoleAsync(newUser, "User");
                    return new GeneralResponse(true, "User successfully created");
                }
            }

            return new GeneralResponse(false, "Could not create account");
        }

        public async Task <LogInResponse> LogInUser(LoginDTO loginDTO)
        {
           if(loginDTO == null)
            {
                return new LogInResponse(false, null, "This model cannot be null");
            }

            var getUser = await userManager.FindByEmailAsync(loginDTO.EmailAddress);
            if (getUser == null)
            {
                return new LogInResponse(false, null, "This email wasn't found");
            }

            bool isPasswordConfirmed = await userManager.CheckPasswordAsync(getUser, loginDTO.password);
            if (!isPasswordConfirmed)
            {
                return new LogInResponse(false, null, "Password is incorrect");
            }

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.fullName, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);

            return new LogInResponse(true, token, "Login successful");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id),
                new Claim(ClaimTypes.Name, user.fullname),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT: Issuer"],
                audience: configuration["JWT:audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
