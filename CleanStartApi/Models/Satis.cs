namespace CleanStartApi.Models;

public class Satis
{
    public int Id { get; set; }

    public int UrunId { get; set; }
    public Urun Urun { get; set; } = null!;

    public int MarkaId { get; set; }
    public Marka Marka { get; set; } = null!;

    public int SubeId { get; set; }
    public Sube Sube { get; set; } = null!;

    public int Adet { get; set; }
    public decimal BirimFiyat { get; set; }
    public DateTime Tarih { get; set; }
}
