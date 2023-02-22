using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using TenApplication.Data;
using TenApplication.Dtos;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Helpers;
using TenApplication.Models;


namespace TenApplication.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            ApplicationDbContext context,
            IDistributedCache cache,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<UserRepository> logger
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<UserDto>> GetUsers()
        {
            List<UserDto>? Users = await _cache.GetRecordAsync<List<UserDto>>("AllUsers");
            if (Users is null)
            {
                Users = await _context.Users.AsNoTracking().Select(d => new UserDto()
                {

                    UserName = d.UserName
                }).OrderBy(n => n.UserName).ToListAsync();
                await _cache.SetRecordAsync("AllUsers", Users);
            }
            return Users;
        }

        public async Task<UserDto> GetProfile(Guid userId)
        {
            UserDto? profile = await _cache.GetRecordAsync<UserDto>($"Profile_{userId}");
            if (profile is null)
            {

                profile = await _context.Users
                    .AsNoTracking()
                    .Select(d => new UserDto()
                    {
                        Id = d.Id,
                        UserName = d.UserName,
                        Email = d.Email,
                        CCtr = d.CCtr,
                        ActTyp = d.ActTyp,
                        Level = d.Level,
                        TennecoStartDate = d.TennecoStartDate
                    }).SingleAsync(p => p.Id == userId);

                if (profile is null) throw new BadHttpRequestException("Profile do not exist!");

                await _cache.SetRecordAsync($"Profile_{userId}", profile);
            }
            return profile;
        }

        public async Task<bool> CreateUser(RegisterDto model)
        {

            User? userExist = await _userManager.FindByEmailAsync(model.Email);

            if (userExist is not null)
            {
                _logger.LogInformation("Email already used!");
                throw new BadHttpRequestException("User already exist!");
            }

                User newUser = new User()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    ActTyp = model.ActTyp,
                    CCtr = model.CCtr,
                    Level = Level.Associative_Engineer,
                    TennecoStartDate = DateTime.Now
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole("User"));
                    }

                    await _userManager.AddToRoleAsync(newUser, "User");

                    Inbox newInbox = new()
                    {
                        UserId = newUser.Id,
                        InboxItems = new List<InboxItem>()
                    };

                    if (model.ProfilePhoto is not null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            if (memoryStream.Length < 2097152)
                            {
                                await model.ProfilePhoto.CopyToAsync(memoryStream);
                                newUser.Photo = memoryStream.ToArray();
                            }
                        }
                    }

                    await _context.Inboxs.AddAsync(newInbox);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new BadHttpRequestException("Sth went wrong!");
                }      
        }

        public async Task<string> LogIn(LoginDto model)
        {

            User? user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new BadHttpRequestException("User not found!");

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPassword) throw new BadHttpRequestException("User not found!");

            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                               model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim("CCtr",user.CCtr),
                        new Claim("ActTyp",user.ActTyp),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };

                //if (user.Gender == "Man") await _userManager.AddClaimAsync(user, new Claim("Gender", user.Gender));

                foreach (string userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }               
                return "Success";
            }

            if (result.IsLockedOut) return "Lockout";

             return "Invalid_attempt";
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> DeleteUser(Guid UserId)
        {
            var resultQuantity = await _context.Users.Where(u => u.Id == UserId).ExecuteDeleteAsync();
            if (resultQuantity != 1) throw new BadHttpRequestException("Bad");
            await _cache.DeleteRecordAsync<User>($"Profile_{UserId}");
            return true;
        }

        public async Task<User> UpdateUser(UpdateDto command, Guid userId)
        {
            var currentUser = await _context.Users.SingleAsync(r => r.Id == userId);

            if (currentUser is null) throw new BadHttpRequestException("Bad");

            currentUser.UserName = command.UserName is not null ? command.UserName : currentUser.UserName;
            currentUser.CCtr = command.CCtr is not null ? command.CCtr : currentUser.CCtr;
            currentUser.ActTyp = command.ActTyp is not null ? command.ActTyp : currentUser.ActTyp;

            if (command.ProfilePhoto is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (memoryStream.Length < 2097152)
                    {
                        await command.ProfilePhoto.CopyToAsync(memoryStream);
                        currentUser.Photo = memoryStream.ToArray();
                    }
                }
            }
            await _cache.DeleteRecordAsync<User>($"Profile_{userId}");
            _context.SaveChanges();
            return currentUser;
        }

        public async Task<bool> ForgotPassword(string UserEmail)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserEmail);

            if (User is null) throw new BadHttpRequestException("Bad");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            //User.PasswordHash = _passwordHasher.HashPassword(User, newPassword);

            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("ellen81@ethereal.email"));
            //email.To.Add(MailboxAddress.Parse(UserEmail));
            //email.Subject = "Tekst email sub";
            //email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = newPassword };

            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("ellen81@ethereal.email", "ZjgDjPpsKN6WaFRrWz");
            //smtp.Send(email);
            //smtp.Disconnect(true);
            return true;
        }

        public async Task<bool> ChangePassword(Guid UserId, string oldPassword, string newPassword, string newPasswordRepeat)
        {
            //if (newPassword != newPasswordRepeat) throw new BadHttpRequestException("New passwords are not the same");

            //var User = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

            //if (User is null) throw new BadHttpRequestException("Bad");

            //var result = _passwordHasher.VerifyHashedPassword(User, User.PasswordHash!, oldPassword);

            //if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");

            //await _context.Users.Where(u => u.UserId == UserId).ExecuteUpdateAsync(
            //    s => s.SetProperty(b => b.PasswordHash, b => _passwordHasher.HashPassword(User, newPassword)));

            return true;
        }

        public async Task<bool> ChangeRole(Guid UserId, UserRole role)
        {

            //var User = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

            //if (User is null) throw new BadHttpRequestException("Bad");

            //await _context.Users.Where(u => u.UserId == UserId).ExecuteUpdateAsync(
            //    s => s.SetProperty(b => b.UserRole, b => role));

            return true;
        }
    }
}
