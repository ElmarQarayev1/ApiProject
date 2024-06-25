using System;
using Flower.UI.Exception;
using Flower.UI.Models;
using Flower.UI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Flower.UI.Controllers
{
	public class SliderController:Controller
	{

        private HttpClient _client;
        private readonly ICrudService _crudService;

        public SliderController(ICrudService crudService)
        {
            _crudService = crudService;
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(int page = 1, int size = 4)
        {
            try
            {
                return View(await _crudService.GetAllPaginated<SliderListItemGetResponse>("sliders", page, size));
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
        public async Task<IActionResult> Create()
        {
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, Request.Cookies["token"]);

         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateRequest createRequest)
        {
            try
            {
                await _crudService.CreateFromForm(createRequest, "sliders");
                return RedirectToAction("index");
            }
            catch (ModelException ex)
            {
                foreach (var item in ex.Error.Errors)
                    ModelState.AddModelError(item.Key, item.Message);

                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crudService.Delete("sliders/" + id);
                return Ok();
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.Status);
            }
        }

    }
}

