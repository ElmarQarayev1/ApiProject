using System;
using System.Text.Json;
using Flower.UI.Exception;
using Flower.UI.Models;
using Flower.UI.Service;
using Microsoft.AspNetCore.Mvc;

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
        private async Task<List<CategoryListItemGetResponse>> getCategories()
        {
            using (var response = await _client.GetAsync("https://localhost:7061/api/categories/all"))
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

