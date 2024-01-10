using Bloomify.Data;
using Bloomify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User User, string Password, string ConfirmPassword, IFormFile FormFile)
        {

            using (var stream = FormFile.OpenReadStream())
            using (var reader = new BinaryReader(stream))
            {
                var byteFile = reader.ReadBytes((int)stream.Length);
                User.Image = byteFile;
            }
            User.ImageName = FormFile.FileName;
            User.ContentType = FormFile.ContentType;
            User.EmailConfirmed = true;
            User.NormalizedEmail = User.Email.ToUpper();
            User.NormalizedUserName = User.Email.ToUpper();
            User.UserName = User.Email;
            User.Role = "User";

            var result = await _userManager.CreateAsync(User, Password);

            var Id = User.Id;
            var roleId = "2";
            var userRole = new IdentityUserRole<string>
            {
                UserId = Id,
                RoleId = roleId
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            //Cart cart = new Cart();
            //cart.UserId = User.Id;
            //cart.TotalQuantity = 0;
            //cart.TotalPrice = 0;
            //_context.Carts.Add(cart);
            //_context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
