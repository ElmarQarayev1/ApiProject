using System;
using System.Text.Json;
using Flower.UI.Exception;
using Flower.UI.Models;
using Flower.UI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Flower.UI.Controllers
{
	public class RoseController:Controller
	{

        private HttpClient _client;
        private readonly ICrudService _crudService;

        public  RoseController(ICrudService crudService)
        {
            _crudService = crudService;
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(int page = 1, int size = 4)
        {
            try
            {
       

                return View(await _crudService.GetAllPaginated<RoseListItemGetResponse>("roses", page, size));
                
            }
            catch (HttpException e)
            {
                if (e.Status == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "auth");
                }
                else
                {
                    throw;
                }
            }
            catch (System.Exception e)
            {
                throw;
            }
          

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crudService.Delete("roses/" + id);
                return Ok();
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.Status);
            }
        }


        public async Task<IActionResult> Create()
        {
          

            ViewBag.Categories = await getCategories();

            if (ViewBag.Categories == null) return RedirectToAction("error", "home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoseCreateRequest createRequest)
        {
            try
            {
                await _crudService.CreateFromForm(createRequest, "roses");
                return RedirectToAction("index");
            }
            catch (ModelException ex)
            {
                foreach (var item in ex.Error.Errors)
                    ModelState.AddModelError(item.Key, item.Message);

                ViewBag.Categories = await _crudService.Get<List<CategoryListItemGetResponse>>("categories/all");

                return View();
            }
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var rose = await _crudService.Get<RoseGetRequest>("roses/" + id);

        //    if (rose == null)
        //    {
        //        return NotFound();
        //    }

        //    RoseEditRequest roseEdit = new RoseEditRequest
        //    {
        //        Name = rose.Name,
        //        Desc = rose.Desc,
        //        Value = rose.Value,
        //        CategoryIds = rose.CategoryIds
        //    };

        //    ViewBag.Categories = await _crudService.Get<List<CategoryListItemGetResponse>>("categories/all");

        //    ViewBag.Pictures = roseEdit.FileUrls;
        //    return View(roseEdit);
        //}
        public async Task<IActionResult> Edit(int id)
        {
            var rose = await _crudService.Get<RoseGetResponse>("roses/" + id);

            if (rose == null)
            {
                return NotFound();
            }

            RoseEditRequest roseEdit = new RoseEditRequest
            {
                Name = rose.Name,
                Desc = rose.Desc,
                Value = rose.Value,
                CategoryIds = rose.CategoryIds,
               //PictureIds = rose.PictureIds,
                FileUrls = rose.Pictures
            };

            ViewBag.Categories = await _crudService.Get<List<CategoryListItemGetResponse>>("categories/all");
            ViewBag.Pictures = rose.Pictures;
            //ViewBag.PictureIds = rose.PictureIds;

            return View(roseEdit);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoseEditRequest editRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _crudService.Get<List<CategoryListItemGetResponse>>("categories/all");
                return View(editRequest);
            }

            try
            {
                await _crudService.EditFromForm(editRequest, $"roses/{id}");
                return RedirectToAction("Index");
            }
            catch (ModelException ex)
            {
                foreach (var item in ex.Error.Errors)
                    ModelState.AddModelError(item.Key, item.Message);

                ViewBag.Categories = await _crudService.Get<List<CategoryListItemGetResponse>>("categories/all");
                return View(editRequest);
            }
        }


        private async Task<List<CategoryListItemGetResponse>> getCategories()
        {
            using (var response = await _client.GetAsync("https://localhost:7120/api/Categories/all"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var data = JsonSerializer.Deserialize<List<CategoryListItemGetResponse>>(await response.Content.ReadAsStringAsync(), options);

                    return data;
                }
            }
            return null;
        }

    }

}

