using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using TenApplication.Data;
using System.Security.Claims;
using TenApplication.Helpers;
using Microsoft.EntityFrameworkCore;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using TenApplication.Models;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Dtos;

namespace TenApplication.Repositories
{
    public class DesignerRepository : IDesignerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Designer> _passwordHasher;
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DesignerRepository(
            ApplicationDbContext context,
            IPasswordHasher<Designer> passwordHasher,
            IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<List<DesignerDto>> GetDesigners()
        {
            List<DesignerDto>? Designers = await _cache.GetRecordAsync<List<DesignerDto>>("AllDesigners");
            if (Designers is null)
            {
                Designers = await _context.Designers.AsNoTracking().Select(d => new DesignerDto()
                {
                    UserId = d.UserId,
                    Name = d.Name    
    }).OrderBy(n => n.Name).ToListAsync();
                await _cache.SetRecordAsync("AllDesigners", Designers);
            }
            return Designers;
        }

        public async Task<DesignerDto> GetProfile(int UserId)
        {
            DesignerDto? profile = await _cache.GetRecordAsync<DesignerDto>($"Profile_{UserId}");
            if (profile is null)
            {
                profile = await _context.Designers
                    .AsNoTracking()
                    .Select(d => new DesignerDto()
                {
                    UserId = d.UserId,
                    Name = d.Name,
                    Surname = d.Surname,
                    Email = d.Email,
                    Phone = d.Phone,
                    CCtr = d.CCtr,
                    ActTyp = d.ActTyp,
                    UserRole = d.UserRole,
                    Level = d.Level,
                    TennecoStartDate = d.TennecoStartDate
                }).SingleAsync(p => p.UserId == UserId);

                await _cache.SetRecordAsync($"Profile_{UserId}", profile);
            }
            return profile;
        }

        public async Task<Designer> CreateDesigner(RegisterDto command)
        {
            Designer newDesigner = new Designer()
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                ActTyp = command.ActTyp,
                CCtr = command.CCtr,
                UserRole = UserRole.Designer,
                Level = Level.Associative_Engineer,
                TennecoStartDate = DateTime.Now        
            };

            Inbox newInbox = new()
            {
                DesignerId = newDesigner.UserId,
                InboxItems = new List<InboxItem>()
            };

            if (command.ProfilePhoto is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (memoryStream.Length < 2097152)
                    {
                        await command.ProfilePhoto.CopyToAsync(memoryStream);
                        newDesigner.Photo = memoryStream.ToArray();
                    }
                }
            }

            newDesigner.PasswordHash = _passwordHasher.HashPassword(newDesigner, command.Password);
            await _context.Designers.AddAsync(newDesigner);
            await _context.Inboxs.AddAsync(newInbox);
            await _context.SaveChangesAsync();
            return newDesigner;
        }

        public async Task<bool> LoginDesigner(LoginDto command)
        {
            var Designer = await _context.Designers.FirstOrDefaultAsync(u => u.Email == command.Email);

            if (Designer is null) throw new BadHttpRequestException("Bad");

            var result = _passwordHasher.VerifyHashedPassword(Designer, Designer.PasswordHash, command.Password);

            if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,Designer.UserId.ToString()),
                    new Claim(ClaimTypes.Name,Designer.Name),
                    new Claim(ClaimTypes.Role,Designer.UserRole.ToString()),
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = command.KeepLoggedIn
            };
            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return true;
        }

        public async Task<bool> DeleteDesigner(int UserId)
        {
            var resultQuantity = await _context.Designers.Where(u => u.UserId == UserId).ExecuteDeleteAsync();
            if (resultQuantity != 1) throw new BadHttpRequestException("Bad");
            await _cache.DeleteRecordAsync<Designer>($"Profile_{UserId}");
            return true;
        }

        public async Task<Designer> UpdateDesigner(UpdateDto command,int userId)
        {
            var currentDesigner = await _context.Designers.SingleAsync(r => r.UserId == userId);

            if (currentDesigner is null) throw new BadHttpRequestException("Bad");

            currentDesigner.Name = command.Name is not null ? command.Name : currentDesigner.Name;
            currentDesigner.CCtr = command.CCtr is not null ? command.CCtr : currentDesigner.CCtr;
            currentDesigner.ActTyp = command.ActTyp is not null ? command.ActTyp : currentDesigner.ActTyp;

            if (command.ProfilePhoto is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (memoryStream.Length < 2097152)
                    {
                        await command.ProfilePhoto.CopyToAsync(memoryStream);
                        currentDesigner.Photo = memoryStream.ToArray();
                    }
                }
            }
            await _cache.DeleteRecordAsync<Designer>($"Profile_{userId}");
            _context.SaveChanges();
            return currentDesigner;
        }

        public async Task<bool> ForgotPassword(string DesignerEmail)
        {
            var Designer = await _context.Designers.FirstOrDefaultAsync(u => u.Email == DesignerEmail);

            if (Designer is null) throw new BadHttpRequestException("Bad");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            Designer.PasswordHash = _passwordHasher.HashPassword(Designer, newPassword);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ellen81@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(DesignerEmail));
            email.Subject = "Tekst email sub";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = newPassword };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ellen81@ethereal.email", "ZjgDjPpsKN6WaFRrWz");
            smtp.Send(email);
            smtp.Disconnect(true);
            return true;
        }

        public async Task<bool> ChangePassword(int UserId, string oldPassword, string newPassword, string newPasswordRepeat)
        {
            if (newPassword != newPasswordRepeat) throw new BadHttpRequestException("New passwords are not the same");

            var Designer = await _context.Designers.FirstOrDefaultAsync(u => u.UserId == UserId);

            if (Designer is null) throw new BadHttpRequestException("Bad");

            var result = _passwordHasher.VerifyHashedPassword(Designer, Designer.PasswordHash, oldPassword);

            if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");

            await _context.Designers.Where(u => u.UserId == UserId).ExecuteUpdateAsync(
                s => s.SetProperty(b => b.PasswordHash, b => _passwordHasher.HashPassword(Designer, newPassword)));

            return true;
        }

        public async Task<bool> ChangeRole(int UserId, UserRole role)
        {

            var Designer = await _context.Designers.FirstOrDefaultAsync(u => u.UserId == UserId);

            if (Designer is null) throw new BadHttpRequestException("Bad");

            await _context.Designers.Where(u => u.UserId == UserId).ExecuteUpdateAsync(
                s => s.SetProperty(b => b.UserRole, b => role));

            return true;
        }
    }
}
