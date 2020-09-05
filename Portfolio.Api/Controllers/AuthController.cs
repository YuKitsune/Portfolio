using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Api.Models;
using Portfolio.Application;
using Portfolio.Infrastructure;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Portfolio.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly PortfolioDbContext portfolioDbContext;
        private readonly TokenService tokenService;
        
        public async Task<IActionResult> Authenticate(AuthenticationModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                // Get the user
                IdentityUser user = await portfolioDbContext.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (user == null) return BadRequest();

                SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (result.Succeeded)
                {
                    TokenModel tokenModel = new TokenModel
                    {
                        Token = tokenService.GenerateToken(user.Id)
                    };
                    
                    return Json(tokenModel);
                }

                // Not supporting anything else at the moment
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add new project.");
                return BadRequest();
            }
        }
    }
}