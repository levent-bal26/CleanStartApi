using CleanStartApi.Data;
using CleanStartApi.Dtos;
using CleanStartApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanStartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrunController : ControllerBase
{
    private readonly AppDbContext _db;
    public UrunController(AppDbContext db) => _db = db;

    // GET: api/urun
    [HttpGet]
    public async Task<ActionResult<List<Urun>>> GetAll()
        => Ok(await _db.Urunler.OrderBy(x => x.Id).ToListAsync());

    // GET: api/urun/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Urun>> GetById(int id)
    {
        var urun = await _db.Urunler.FindAsync(id);
        return urun is null
            ? NotFound(new { message = "Urun bulunamadı." })
            : Ok(urun);
    }

    // POST: api/urun
    [HttpPost]
    public async Task<ActionResult<Urun>> Create(UrunCreateDto dto)
    {
        var ad = (dto.Ad ?? "").Trim();
        if (string.IsNullOrWhiteSpace(ad))
            return BadRequest(new { message = "Ad boş olamaz." });

        if (await _db.Urunler.AnyAsync(x => x.Ad == ad))
            return Conflict(new { message = "Bu ürün adı zaten var." });

        var urun = new Urun { Ad = ad };
        _db.Urunler.Add(urun);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = urun.Id }, urun);
    }

    // PUT: api/urun/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UrunUpdateDto dto)
    {
        var urun = await _db.Urunler.FindAsync(id);
        if (urun is null) return NotFound(new { message = "Urun bulunamadı." });

        var ad = (dto.Ad ?? "").Trim();
        if (string.IsNullOrWhiteSpace(ad))
            return BadRequest(new { message = "Ad boş olamaz." });

        if (await _db.Urunler.AnyAsync(x => x.Ad == ad && x.Id != id))
            return Conflict(new { message = "Bu ürün adı zaten var." });

        urun.Ad = ad;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/urun/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var urun = await _db.Urunler.FindAsync(id);
        if (urun is null) return NotFound(new { message = "Urun bulunamadı." });

        _db.Urunler.Remove(urun);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
