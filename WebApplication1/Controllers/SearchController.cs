using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public async Task<ActionResult> Users(string key = "")
        {
            List<ApplicationUser> users = UserManager.Users
                .Where(u => u.Email.Contains(key) &&
                u.Hometown.Contains(key) &&
                u.UserName.Contains(key)).ToList();
            string userId = null;
            try
            {
                userId = GetUserId();
                ApplicationUser currentUser = await UserManager.FindByIdAsync(userId);
                users.Remove(currentUser);
            }
            catch
            {

            }

            

            return View(users);
        }
    }
}