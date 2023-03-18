using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;

using Microsoft.Extensions.Caching.Distributed;
using dotnet.Repository;

namespace dotnet.Controllers;

public class HomeController : Controller
{
    private IDistributedCache _cache;
    private IUserRepository _userRepository;

    public HomeController(IDistributedCache cache, IUserRepository userRepository)
    {
        _cache = cache;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        List<User>? users;
        string recordKey = $"Users_{DateTime.Now:yyyyMMdd_hhmm}";

        users = await _cache.GetRecordAsync<List<User>>(recordKey); // Get data from cache

        if (users is null) // Data not available in the Cache
        {
            users = await _userRepository.GetUsersAsync(); // Read data from database
            await _cache.SetRecordAsync(recordKey, users); // Set cache
        }

        return View(users); // Return data
    }


}
