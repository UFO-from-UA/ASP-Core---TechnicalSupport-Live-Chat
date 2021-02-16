using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalSupport.Data;
using TechnicalSupport.Services;
using TechnicalSupport.Utils;

namespace TechnicalSupport.Controllers
{
    public class AccountController : Controller
    {
        private readonly SupportContext _db;
        private readonly IAuthService _authService;

        public AccountController(SupportContext db , IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(AuthModel model)
        {
            _authService.AuthenticateUserAsync(model);
            return Ok();
        }
    }
}
