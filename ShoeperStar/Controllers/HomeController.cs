using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Models;
using System.Diagnostics;

namespace ShoeperStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly Mapper _mapper;

        public HomeController(IRepositoryManager repositoryManager, Mapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}