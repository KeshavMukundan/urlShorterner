
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UrlShorterner;

[ApiController]
[Route("/shorten")]

public class urlShort : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public urlShort(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{placeholder}")]
    public IActionResult Get(string placeholder)
    {
        var data = _context.Urls.FirstOrDefault(p => p.Short == placeholder);
        if (data != null)
        {
            return Ok(data);
        }

        return NotFound("Not found");
    }
    

    
    
    [HttpPost]
    public async Task<IActionResult> Shorten([FromBody] Url payload)
    {
        Random rnd = new();
        DateTime dt = DateTime.Today;
        
        string main = "abcdefghijklmnopqrstuvwxyz";
        string clean = "";
        for (int i = 0; i < 3; i++)
        {
            clean += main[rnd.Next(26)];
        }

        Response r = new Response
        {
            Url = payload.url,
            Short = clean,
            CreateAt = dt.ToString("yyyy-M-ddd h:mm:ss"),
            UpdatedAt = dt.ToString("yyyy-M-ddd h:mm:ss")
        };

        _context.Urls.Add(r);
        await _context.SaveChangesAsync();

        return Ok(r); 
    }

    [HttpPut("{placeholder}")]
    public async Task<IActionResult> Put(string placeholder, [FromBody] Url payload)
    {
        var data = _context.Urls.FirstOrDefault(i => i.Short == placeholder);
        if (data != null)
        {
            data.Url = payload.url;
            _context.Urls.Update(data);
            await _context.SaveChangesAsync();
            return Ok("Updated");
        }

        return NotFound("Not Found!");
    }
    
    [HttpDelete("{placeholder}")]
    public async Task<IActionResult> Delete(string placeholder)
    {
        var data = _context.Urls.FirstOrDefault(i => i.Short == placeholder);
        if (data != null)
        {

            _context.Urls.Remove(data);
            await _context.SaveChangesAsync();
            
            return Ok("Deleted!");
        }
       
        
        return NotFound("id not found");

        
    }
    
    
}

public record Url(string url); 


