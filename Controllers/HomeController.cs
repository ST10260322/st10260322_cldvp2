using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using st10260322_cldvp2.Models; // Replace with your actual namespace
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public IActionResult Index()
    {
        return View(); // This should return the Index view
    }

    [HttpPost]
    public async Task<IActionResult> StoreData(TableEntityModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model); // Return to Index with validation errors
        }

        var content = new StringContent(
            JsonConvert.SerializeObject(model),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("https://4functionapp.azurewebsites.net/api/StoreTableInfo?code=DoHDgHDt9SNz2wjHIKA97c9mvUYTpJWVMRUlMZbegSzgAzFuUueEFA%3D%3D", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        // Create an error view model to pass to the error view
        var errorViewModel = new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier // Include the request ID for debugging
        };
        return View("Error", errorViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> WriteBlob(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var content = new ByteArrayContent(stream.ToArray());
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await _httpClient.PostAsync("https://4functionapp.azurewebsites.net/api/UploadBlob?code=bHrIFsrcz6z-6C4AgTjSt7KbL-BG3fxe_Yyg9PjRD45QAzFuvwcYbw%3D%3D", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> QueueTransaction(string transactionData)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(new { TransactionData = transactionData }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("https://4functionapp.azurewebsites.net/api/ProcessQueueMessage?code=YGKAg6FQYDqO26ryyHvCtm4_wga1cxz5kPLxMmrer2kTAzFuFTAJ7w%3D%3D", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> WriteFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var content = new ByteArrayContent(stream.ToArray());
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await _httpClient.PostAsync("https://4functionapp.azurewebsites.net/api/UploadFile?code=lch0-aLmNBwAB4lLY0_W-y9ylnRjs6DEvKAaBJ9X6P4mAzFuLpat3A%3D%3D", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }
}
