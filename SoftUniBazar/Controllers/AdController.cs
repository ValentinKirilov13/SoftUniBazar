using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Constants;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext _context;

        public AdController(BazarDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _context.Ads
                .AsNoTracking()
                .Select(x => new AdInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    CreatedOn = x.CreatedOn.ToString(DataConstants.DateFormat),
                    Category = x.Category.Name,
                    Price = $"{x.Price:F2}",
                    Owner = x.Owner.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormAddViewModel
            {
                Categories = await GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            var entity = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                OwnerId = GetUserId(),
                Price = model.Price,
                CreatedOn = DateTime.Now,
            };

            await _context.Ads.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetAdByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var viewModel = new AdEditFormViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                Price = model.Price,
                Categories = await GetCategoriesAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdEditFormViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            var model = await GetAdByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Categories = await GetCategoriesAsync();
                return View(viewModel);
            }

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.ImageUrl = viewModel.ImageUrl;
            model.Price = viewModel.Price;
            model.CategoryId = viewModel.CategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var model = await GetAdByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (!await _context.AdsBuyers.AnyAsync(x => x.BuyerId == GetUserId() && x.AdId == id))
            {
                await _context.AdsBuyers.AddAsync(new AdBuyer()
                {
                    BuyerId = GetUserId(),
                    AdId = id
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Cart));
            }
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var model = await _context.AdsBuyers
                .FirstOrDefaultAsync(x => x.BuyerId == GetUserId() && x.AdId == id);

            if (model == null)
            {
                return NotFound();
            }

            _context.AdsBuyers.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = await _context.AdsBuyers
                .AsNoTracking()
                .Where(x => x.BuyerId == GetUserId())
                .Select(x => new AdInfoViewModel()
                {
                    Id = x.Ad.Id,
                    Name = x.Ad.Name,
                    Description = x.Ad.Description,
                    ImageUrl = x.Ad.ImageUrl,
                    CreatedOn = x.Ad.CreatedOn.ToString(DataConstants.DateFormat),
                    Category = x.Ad.Category.Name,
                    Price = $"{x.Ad.Price:F2}",
                    Owner = x.Ad.Owner.UserName
                })
                .ToListAsync();

            return View(model);
        }


        private async Task<Ad?> GetAdByIdAsync(int id)
        {
            return await _context.Ads
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        private async Task<ICollection<CategoryInfoViewModel>> GetCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new CategoryInfoViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }
    }
}
