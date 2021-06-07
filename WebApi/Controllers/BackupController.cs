using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public BackupController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet("Export")]
        public async Task<IActionResult> Export()
        {
            var buchungen = _context.Buchungen.AsNoTracking().ToList();
            var buchungenJson = System.Text.Json.JsonSerializer.Serialize(buchungen);
            var buchungenStream = new MemoryStream(Encoding.ASCII.GetBytes(buchungenJson));
            var buchungenFile = new FileStreamResult(buchungenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"buchungen_{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var dauerauftraege = _context.Dauerauftraege.AsNoTracking().ToList();
            var dauerauftraegeJson = System.Text.Json.JsonSerializer.Serialize(dauerauftraege);
            var dauerauftraegeStream = new MemoryStream(Encoding.ASCII.GetBytes(dauerauftraegeJson));
            var dauerauftraegeFile = new FileStreamResult(dauerauftraegeStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"dauerauftraege_{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var kategorien = _context.Kategorien.AsNoTracking().ToList();
            var kategorienJson = System.Text.Json.JsonSerializer.Serialize(kategorien);
            var kategorienStream = new MemoryStream(Encoding.ASCII.GetBytes(kategorienJson));
            var kategorienFile = new FileStreamResult(kategorienStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"kategorien{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var konfigurationen = await _context.Konfigurationen.AsNoTracking().ToListAsync();
            var konfigurationenJson = System.Text.Json.JsonSerializer.Serialize(konfigurationen);
            var konfigurationenStream = new MemoryStream(Encoding.ASCII.GetBytes(konfigurationenJson));
            var konfigurationenFile = new FileStreamResult(konfigurationenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"konfigurationen{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var gutscheine = await _context.Gutscheine.AsNoTracking().ToListAsync();
            var gutscheineJson = System.Text.Json.JsonSerializer.Serialize(gutscheine);
            var gutscheineStream = new MemoryStream(Encoding.ASCII.GetBytes(gutscheineJson));
            var gutscheineFile = new FileStreamResult(gutscheineStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"gutscheine{DateTime.Now:yyyyMMddHHmmss}.json"
            };

            var ruecklagen = await _context.Ruecklagen.AsNoTracking().ToListAsync();
            var ruecklagenJson = System.Text.Json.JsonSerializer.Serialize(ruecklagen);
            var ruecklagenStream = new MemoryStream(Encoding.ASCII.GetBytes(ruecklagenJson));
            var ruecklagenFile = new FileStreamResult(ruecklagenStream, new MediaTypeHeaderValue("application/json"))
            {
                FileDownloadName = $"ruecklagen{DateTime.Now:yyyyMMddHHmmss}.json"
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

        [HttpGet("Import")]
        public async Task<IActionResult> Import() { return null; }
    }
}
