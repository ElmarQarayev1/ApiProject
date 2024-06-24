﻿using System;
using Flower.UI.Exception;
using Flower.UI.Filter;
using Flower.UI.Models;
using Flower.UI.Service;
using Microsoft.AspNetCore.Mvc;

namespace Flower.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class CategoryController:Controller
	{
        private HttpClient _client;
        private readonly ICrudService _crudService;

        public CategoryController(ICrudService crudService)
        {
            _client = new HttpClient();
            _crudService = crudService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                return View(await _crudService.GetAllPaginated<CategoryListItemDetailedGetResponse>("categories", page));
            }
            catch (HttpException e)
            {
                if (e.Status == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "auth");
                }
                else return RedirectToAction("error", "home");
            }
            catch (System.Exception e)
            {
                return RedirectToAction("error", "home");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest createRequest)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _crudService.Create<CategoryCreateRequest>(createRequest, "groups");
                return RedirectToAction("index");
            }
            catch (ModelException e)
            {
                foreach (var item in e.Error.Errors) ModelState.AddModelError(item.Key, item.Message);
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _crudService.Get<CategoryCreateRequest>("categories/" + id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryCreateRequest editRequest, int id)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _crudService.Update<CategoryCreateRequest>(editRequest, "categories/" + id);
                return RedirectToAction("index");
            }
            catch (ModelException e)
            {
                foreach (var item in e.Error.Errors)
                    ModelState.AddModelError(item.Key, item.Message);

                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crudService.Delete("categories/" + id);
                return Ok();
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.Status);
            }
        }

    }
}
