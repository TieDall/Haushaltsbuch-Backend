using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private const string filePrefixBuchungen = "buchungen_";
        private const string filePrefixDauerauftraege = "dauerauftraege_";
        private const string filePrefixKategorien = "kategorien_";
        private const string filePrefixKonfigurationen = "konfigurationen_";
        private const string filePrefixGutscheine = "gutscheine_";
        private const string filePrefixRuecklagen = "ruecklagen_";

        private readonly HaushaltsbuchContext _context;
        private readonly IConfiguration _config;

        public BackupController(
            HaushaltsbuchContext context,
            IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("Export")]
        public async Task<IActionResult> Export()
        {
            var buchungen = _context.Buchungen.AsNoTracking().ToList();
            var buchungenJson = System.Text.Json.JsonSerializer.Serialize(buchungen);
            var buchungenStream = new MemoryStream(Encoding.ASCII.GetBytes(buchungenJson));
            var buchungenFile = new FileStreamResult(buchungenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixBuchungen}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var dauerauftraege = _context.Dauerauftraege.AsNoTracking().ToList();
            var dauerauftraegeJson = System.Text.Json.JsonSerializer.Serialize(dauerauftraege);
            var dauerauftraegeStream = new MemoryStream(Encoding.ASCII.GetBytes(dauerauftraegeJson));
            var dauerauftraegeFile = new FileStreamResult(dauerauftraegeStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixDauerauftraege}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var kategorien = _context.Kategorien.AsNoTracking().ToList();
            var kategorienJson = System.Text.Json.JsonSerializer.Serialize(kategorien);
            var kategorienStream = new MemoryStream(Encoding.ASCII.GetBytes(kategorienJson));
            var kategorienFile = new FileStreamResult(kategorienStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixKategorien}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var konfigurationen = await _context.Konfigurationen.AsNoTracking().ToListAsync();
            var konfigurationenJson = System.Text.Json.JsonSerializer.Serialize(konfigurationen);
            var konfigurationenStream = new MemoryStream(Encoding.ASCII.GetBytes(konfigurationenJson));
            var konfigurationenFile = new FileStreamResult(konfigurationenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixKonfigurationen}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var gutscheine = await _context.Gutscheine.AsNoTracking().ToListAsync();
            var gutscheineJson = System.Text.Json.JsonSerializer.Serialize(gutscheine);
            var gutscheineStream = new MemoryStream(Encoding.ASCII.GetBytes(gutscheineJson));
            var gutscheineFile = new FileStreamResult(gutscheineStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixGutscheine}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var ruecklagen = await _context.Ruecklagen.AsNoTracking().ToListAsync();
            var ruecklagenJson = System.Text.Json.JsonSerializer.Serialize(ruecklagen);
            var ruecklagenStream = new MemoryStream(Encoding.ASCII.GetBytes(ruecklagenJson));
            var ruecklagenFile = new FileStreamResult(ruecklagenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"{filePrefixRuecklagen}{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            // In ZIP packen

            MemoryStream memoryStream = new MemoryStream();
            using (ZipArchive archive = new ZipArchive(
                stream: memoryStream,
                mode: ZipArchiveMode.Create,
                leaveOpen: true
            ))
            {
                ZipArchiveEntry buchungenEntry = archive.CreateEntry(buchungenFile.FileDownloadName);
                using (Stream entryStream = buchungenEntry.Open())
                {
                    await buchungenFile.FileStream.CopyToAsync(entryStream);
                }

                ZipArchiveEntry dauerauftraegeEntry = archive.CreateEntry(dauerauftraegeFile.FileDownloadName);
                using (Stream entryStream = dauerauftraegeEntry.Open())
                {
                    await dauerauftraegeFile.FileStream.CopyToAsync(entryStream);
                }

                ZipArchiveEntry kategorienEntry = archive.CreateEntry(kategorienFile.FileDownloadName);
                using (Stream entryStream = kategorienEntry.Open())
                {
                    await kategorienFile.FileStream.CopyToAsync(entryStream);
                }

                ZipArchiveEntry konfigurationenEntry = archive.CreateEntry(konfigurationenFile.FileDownloadName);
                using (Stream entryStream = konfigurationenEntry.Open())
                {
                    await konfigurationenFile.FileStream.CopyToAsync(entryStream);
                }

                ZipArchiveEntry gutscheineEntry = archive.CreateEntry(gutscheineFile.FileDownloadName);
                using (Stream entryStream = gutscheineEntry.Open())
                {
                    await gutscheineFile.FileStream.CopyToAsync(entryStream);
                }

                ZipArchiveEntry ruecklagenEntry = archive.CreateEntry(ruecklagenFile.FileDownloadName);
                using (Stream entryStream = ruecklagenEntry.Open())
                {
                    await ruecklagenFile.FileStream.CopyToAsync(entryStream);
                }
            }
            memoryStream.Position = 0;
            return new FileStreamResult(memoryStream, System.Net.Mime.MediaTypeNames.Application.Zip)
            {
                FileDownloadName = $"haushaltsbuch_{DateTime.Now:yyyyMMddHHmmss}.zip"
            };
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            // unzip
            MemoryStream memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);

            // extract data
            Buchung[] buchungen = new Buchung[0];
            Dauerauftrag[] dauerauftraege = new Dauerauftrag[0];
            Kategorie[] kategorien = new Kategorie[0];
            Gutschein[] gutscheine = new Gutschein[0];
            Ruecklage[] ruecklagen = new Ruecklage[0];
            Konfiguration[] konfigurationen = new Konfiguration[0];

            foreach (var entry in archive.Entries)
            {
                using (var stream = entry.Open())
                {
                    var aa = new StreamReader(stream);
                    var bb = aa.ReadToEnd();

                    var match = Regex.Match(entry.Name, "[0-9]");
                    var filename = entry.Name.Substring(0, match.Index);

                    switch (filename)
                    {
                        case filePrefixBuchungen:
                            buchungen = System.Text.Json.JsonSerializer.Deserialize<Buchung[]>(bb);
                            break;
                        case filePrefixDauerauftraege:
                            dauerauftraege = System.Text.Json.JsonSerializer.Deserialize<Dauerauftrag[]>(bb);
                            break;
                        case filePrefixKategorien:
                            kategorien = System.Text.Json.JsonSerializer.Deserialize<Kategorie[]>(bb);
                            break;
                        case filePrefixGutscheine:
                            gutscheine = System.Text.Json.JsonSerializer.Deserialize<Gutschein[]>(bb);
                            break;
                        case filePrefixRuecklagen:
                            ruecklagen = System.Text.Json.JsonSerializer.Deserialize<Ruecklage[]>(bb);
                            break;
                        case filePrefixKonfigurationen:
                            konfigurationen = System.Text.Json.JsonSerializer.Deserialize<Konfiguration[]>(bb);
                            break;
                    }                    
                }
            }

            // Reset database
            _context.Buchungen.RemoveRange(_context.Buchungen.ToList());
            _context.Dauerauftraege.RemoveRange(_context.Dauerauftraege.ToList());
            _context.Kategorien.RemoveRange(_context.Kategorien.ToList());

            _context.Gutscheine.RemoveRange(_context.Gutscheine.ToList());
            _context.Ruecklagen.RemoveRange(_context.Ruecklagen.ToList());

            _context.ReportItems.RemoveRange(_context.ReportItems.ToList());
            _context.ReportRows.RemoveRange(_context.ReportRows.ToList());
            _context.Reports.RemoveRange(_context.Reports.ToList());

            _context.Konfigurationen.RemoveRange(_context.Konfigurationen.ToList());
            //await _context.Konfigurationen.AddAsync(new Konfiguration() { Parameter = "Start", Wert = "0" });

            await _context.SaveChangesAsync();

            // Store import data
            var transaction1 = _context.Database.BeginTransaction();            
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Kategorien ON;");
            }
            _context.Kategorien.AddRange(kategorien);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Kategorien OFF;");
            }
            transaction1.Commit();

            var transaction2 = _context.Database.BeginTransaction();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Buchungen ON;");
            }
            _context.Buchungen.AddRange(buchungen);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Buchungen OFF;");
            }
            transaction2.Commit();

            var transaction3 = _context.Database.BeginTransaction();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Dauerauftraege ON;");
            }
            _context.Dauerauftraege.AddRange(dauerauftraege);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Dauerauftraege OFF;");
            }
            transaction3.Commit();

            var transaction4 = _context.Database.BeginTransaction();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Gutscheine ON;");
            }
            _context.Gutscheine.AddRange(gutscheine);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Gutscheine OFF;");
            }
            transaction4.Commit();

            var transaction5 = _context.Database.BeginTransaction();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Konfigurationen ON;");
            }
            _context.Konfigurationen.AddRange(konfigurationen);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Konfigurationen OFF;");
            }
            transaction5.Commit();

            var transaction6 = _context.Database.BeginTransaction();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Ruecklagen ON;");
            }
            _context.Ruecklagen.AddRange(ruecklagen);
            await _context.SaveChangesAsync();
            if (_config["DbProvider"].Equals("MsSql"))
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Ruecklagen OFF;");
            }
            transaction6.Commit();

            return Ok(); 
        }
    }
}
