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
using AppData.Dto;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR;

namespace AppView.Controllers
{
    public class ProductsController : Controller
    {
        private HttpClient _client;

        public ProductsController(HttpClient client)
        {
            _client = client;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid productId,Guid categoryId)
        {
            ViewData["ProductId"] = productId;
            ViewData["CategoryId"] = categoryId;
            return View();
        }
    }
}
