using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using UserService.Interfaces;
using UserService.Dtos;

namespace CinemaScopeWeb.Controllers
{
    public class AboutUsController : Controller
    {
        private IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = _aboutUsService.GetAll();
            var model = Mapper.Map<IEnumerable<AboutUsViewModel>>(users);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAboutUsViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var user = Mapper.Map<CreateAboutUsDto>(model);
            _aboutUsService.Create(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var user = _aboutUsService.GetById(id);
            if (user is null) return RedirectToAction("Index");

            var model = Mapper.Map<AboutUsViewModel>(user);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(AboutUsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = Mapper.Map<AboutUsDto>(model);
            _aboutUsService.Update(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var user = _aboutUsService.GetById(id);
            if (user is null) return RedirectToAction("Index");

            _aboutUsService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}