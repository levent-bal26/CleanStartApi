using CleanStartApi.Data;
using CleanStartApi.Dtos;
using CleanStartApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanStartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarkaController : ControllerBase
{
    private readonly AppDbContext _db;
    public MarkaController(AppDbContext db) => _db = db;

    // GET: api/marka
    [HttpGet]
    public async Task<ActionResult<List<Marka>>> GetAll()
        => Ok(await _db.Markalar.OrderBy(x => x.Id).ToListAsync());

    // GET: api/marka/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Marka>> GetById(int id)
    {
        var marka = await _db.Markalar.FindAsync(id);
        return marka is null
            ? NotFound(new { message = "Marka bulunamadı." })
            : Ok(marka);
    }

    // POST: api/marka
    [HttpPost]
    public async Task<ActionResult<Marka>> Create(MarkaCreateDto dto)
    {
        var ad = (dto.Ad ?? "").Trim();
        if (string.IsNullOrWhiteSpace(ad))
            return BadRequest(new { message = "Ad boş olamaz." });

        if (await _db.Markalar.AnyAsync(x => x.Ad == ad))
            return Conflict(new { message = "Bu marka adı zaten var." });

        var marka = new Marka { Ad = ad };
        _db.Markalar.Add(marka);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = marka.Id }, marka);
    }

    // PUT: api/marka/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, MarkaUpdateDto dto)
    {
        var marka = await _db.Markalar.FindAsync(id);
        if (marka is null) return NotFound(new { message = "Marka bulunamadı." });

        var ad = (dto.Ad ?? "").Trim();
        if (string.IsNullOrWhiteSpace(ad))
            return BadRequest(new { message = "Ad boş olamaz." });

        if (await _db.Markalar.AnyAsync(x => x.Ad == ad && x.Id != id))
            return Conflict(new { message = "Bu marka adı zaten var." });

        marka.Ad = ad;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/marka/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var marka = await _db.Markalar.FindAsync(id);
        if (marka is null) return NotFound(new { message = "Marka bulunamadı." });

        _db.Markalar.Remove(marka);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
