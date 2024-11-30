using AiIntegration.Models;
using GroqSharp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AiIntegration.Controllers
{
    public class HomeController : Controller
    {
        private IGroqClient _groqClient;

        public HomeController(IGroqClient groqClient)
        {
            _groqClient = groqClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string question)
        {
            question = String.Concat(question, " Please answer in 50 characters or less with a Chamath Palihapitiya impersonation.");
            string answer = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = question });
            ViewBag.Answer = answer;
            return View();
        }


    }
}
