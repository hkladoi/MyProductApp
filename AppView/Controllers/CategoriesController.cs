using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData.DbContexts;
using AppData.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppView.Controllers
{
    public class CategoriesController : Controller
    {
        private HttpClient _client;

        public CategoriesController(HttpClient client)
        {
            _client = client;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid categoryId)
        {
            ViewData["CategoryId"] = categoryId;
            return View();
        }
    }
}
