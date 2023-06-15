using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipAppMVC.DbContexts;
using RecipAppMVC.Migrations;
using RecipAppMVC.Models;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;

namespace RecipAppMVC.Controllers
{
    public class RecipsController : Controller
    {
        private RecipsDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public RecipsController(IHttpContextAccessor httpContext)
        {
            _dbContext= new RecipsDbContext();
            _httpContext = httpContext;
        }

        public ActionResult Index(int flage)
        {
            
            if ( flage == 1)
            {
                var userid = _httpContext.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                List<int> recipIds = _dbContext.LikedRecip
                .Where(x => x.userId == userid)
                .Select(item => item.recipId)
                .ToList();

                var result = _dbContext.Recips
                 .Where(item => recipIds.Contains(item.Id))
                 .ToList();
                return View(result);
            }
            var response = _dbContext.Recips
                        .ToList();
            return View(response);

        }

        public ActionResult Details(int id)
        {
            var model = _dbContext.Recips.Find(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipModel model)
        {

            _dbContext.Recips.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index)); 

        }

        public ActionResult Edit(int id)
        {
            var model = _dbContext.Recips.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RecipModel model)
        {

            _dbContext.Recips.Update(model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Delete(int id)
        {
            var model = _dbContext.Recips.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RecipModel Recipmodel)
        {
            var model = _dbContext.Recips.Find(id);
            _dbContext.Recips.Remove(model);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Like(int id)
        {
            var model = _dbContext.Recips.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(int id,RecipModel model)
        {
            var userid = _httpContext.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            LikedRecip likedRecip = new LikedRecip
            {
                userId = userid,
                recipId = id

            };

            _dbContext.LikedRecip.Add(likedRecip);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
