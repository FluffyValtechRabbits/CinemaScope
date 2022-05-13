using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using UserService.Managers;
using AutoMapper;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]        
        public async Task<ActionResult> Profile(UserProfileViewModel model)
        {    
            if(model.UserName is null)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                model = Mapper.Map<UserProfileViewModel>(user);
            }            

            return View(model);
        }
    }
}