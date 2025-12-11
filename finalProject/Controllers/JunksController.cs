using finalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace finalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class JunkController : Controller
    {
        private readonly HttpClient _http;

        public JunkController(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _http.GetAsync("junkapi");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Junk>>(json);
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _http.GetAsync($"junkapi/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var junk = JsonConvert.DeserializeObject<Junk>(json);
            return View(junk);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Junk junk)
        {
            var json = JsonConvert.SerializeObject(junk);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("junkapi", body);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _http.GetAsync($"junkapi/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var junk = JsonConvert.DeserializeObject<Junk>(json);
            return View(junk);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Junk junk)
        {
            var json = JsonConvert.SerializeObject(junk);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PutAsync($"junkapi/{id}", body);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _http.GetAsync($"junkapi/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var junk = JsonConvert.DeserializeObject<Junk>(json);
            return View(junk);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _http.DeleteAsync($"junkapi/{id}");
            return RedirectToAction("Index");
        }
    }
}
