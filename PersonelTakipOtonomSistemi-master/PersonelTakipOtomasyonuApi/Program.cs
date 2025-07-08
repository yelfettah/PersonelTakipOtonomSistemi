using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonuApi.Data;
using PersonelTakipOtomasyonuApi.Dtos;
using PersonelTakipOtomasyonuApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Personel Takip Otomasyonu API",
        Version = "v1",
        Description = "Personel yönetimi için RESTful API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "API Destek",
            Email = "destek@company.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddDbContext<PersonelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personel Takip API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/personeller", async (PersonelDbContext db) =>
    await db.Personeller.ToListAsync());

app.MapGet("/personeller/{id}", async (PersonelDbContext db, int id) =>
{
    var personel = await db.Personeller.FindAsync(id);
    return personel is not null ? Results.Ok(personel) : Results.NotFound();
});

app.MapPost("/personeller", async (PersonelDbContext db, PersonelOlusturDto dto) =>
{
    var personel = new Personel
    {
        Ad = dto.Ad,
        Soyad = dto.Soyad,
        TCKimlikNo = dto.TCKimlikNo,
        DogumTarihi = dto.DogumTarihi,
        Eposta = dto.Eposta,
        TelefonNo = dto.TelefonNo,
        IseBaslamaTarihi = dto.IseBaslamaTarihi,
        Maas = dto.Maas,
        aktifMi = dto.aktifMi ? "Aktif" : "Pasif",
        yillikIzinHakki = dto.yillikIzinHakki,
        Pozisyon = dto.Pozisyon,
        Departman = dto.Departman,
        Sifre = dto.Sifre,
        EkleyenKullanici = dto.EkleyenKullanici,
        EklenmeTarihi = dto.EklenmeTarihi
    };
    
    db.Personeller.Add(personel);
    await db.SaveChangesAsync();
    return Results.Created($"/personeller/{personel.PersonelID}", personel);
});

app.MapPut("/personeller", async (PersonelDbContext db, PersonelGuncelleDto dto) =>
{
    var existingPersonel = await db.Personeller.FindAsync(dto.PersonelID);
    if (existingPersonel == null)
        return Results.NotFound();
    existingPersonel.Ad = dto.Ad;
    existingPersonel.Soyad = dto.Soyad;
    existingPersonel.DogumTarihi = dto.DogumTarihi;
    existingPersonel.TelefonNo = dto.TelefonNo;
    existingPersonel.Maas = dto.Maas;
    existingPersonel.aktifMi = dto.aktifMi ? "Aktif" : "Pasif";
    existingPersonel.yillikIzinHakki = dto.yillikIzinHakki;
    existingPersonel.IseBaslamaTarihi = dto.IseBaslamaTarihi;
    existingPersonel.TCKimlikNo = dto.TCKimlikNo;
    existingPersonel.Departman = dto.Departman;
    existingPersonel.Pozisyon = dto.Pozisyon;
    existingPersonel.Sifre = dto.Sifre;
    existingPersonel.Eposta = dto.Eposta;
    existingPersonel.GuncelleyenKullanici = dto.GuncelleyenKullanici;
    existingPersonel.GuncellenmeTarihi = dto.GuncellenmeTarihi;
    try
    {
        await db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!db.Personeller.Any(e => e.PersonelID == dto.PersonelID))
            return Results.NotFound();
        else
            throw;
    }
    return Results.NoContent();
});

app.MapDelete("/personeller/{id}", async (PersonelDbContext db, int id) =>
{
    var personel = await db.Personeller.FindAsync(id);
    if (personel == null)
        return Results.NotFound();
    db.Personeller.Remove(personel);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/login", async (PersonelDbContext db, LoginDto dto) =>
{
    var personel = await db.Personeller
        .FirstOrDefaultAsync(p => p.TCKimlikNo == dto.TCKimlikNo && p.Sifre == dto.Sifre);

    if (personel == null)
        return Results.Unauthorized();

    return Results.Ok(personel);
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PersonelDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı oluşturulurken hata oluştu.");
    }
}

app.Run();
