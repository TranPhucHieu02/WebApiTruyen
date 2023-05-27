using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnAndroid.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace DoAnAndroid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TuongTacsController : Controller
    {
        private readonly ApiTruyenContext _context;

        public TuongTacsController(ApiTruyenContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTuongTac()
        {
            return Ok(await _context.TuongTacs.ToListAsync());
        }
        [HttpGet("get-tuongtac-user")]
        public async Task<IActionResult> GetTuongTacID(string Username)
        {
            List<TuongTac> tuongTacs = _context.TuongTacs.Where(m=>m.Username == Username).ToList();
            if (tuongTacs == null)
            {
                return NotFound();
            }
            List<Truyen> truyens = new List<Truyen>();
            for(int i=0;i<tuongTacs.Count;i++)
            {
                var truyen = await _context.Truyens.FirstOrDefaultAsync(p => p.MaTruyen.ToLower() == tuongTacs[i].MaTruyen.ToLower() && tuongTacs[i].YeuThich == true);
                if(truyen != null)
                {
                    truyens.Add(truyen);

                }
            }
            return Ok(truyens);
        }

        [HttpGet("get-tuongtac")]
        public async Task<IActionResult> GetTuongTacID(string Username,string MaTruyen)
        {
            if (MaTruyen == null || Username == null|| _context.TuongTacs == null)
            {
                return NotFound();
            }

            var tuongTac = await _context.TuongTacs.FirstOrDefaultAsync(m => m.Username == Username && m.MaTruyen == MaTruyen);
            if (tuongTac == null)
            {
                return NotFound();
            }

            return Ok(tuongTac);
        }

        [HttpPost("post-yeuthich")]
        public async Task<IActionResult> UpdateYeuThich(updateTuongTac tuongTac)
        {
            var tuongTacs = await _context.TuongTacs.FirstOrDefaultAsync(m => m.Username ==tuongTac.Username && m.MaTruyen ==tuongTac.MaTruyen);
            if (tuongTacs != null)
            { 
               
                tuongTacs.YeuThich = tuongTac?.YeuThich;
                await _context.SaveChangesAsync();
                return Ok(tuongTacs);
            }
            else
            {
                var tuongTacss = new TuongTac()
                {
                    Username = tuongTac.Username,
                    MaTruyen = tuongTac.MaTruyen,
                    YeuThich = tuongTac?.YeuThich,
                    DanhGia  = tuongTac?.DanhGia,
                    
                };

                await _context.TuongTacs.AddAsync(tuongTacss);
                await _context.SaveChangesAsync();
                return Ok(tuongTacss);
            }
        }
        [HttpPost("post-sao")]
        public async Task<IActionResult> UpdateSao(updateTuongTac tuongTac)
        {
            var tuongTacs = await _context.TuongTacs.FirstOrDefaultAsync(m => m.Username == tuongTac.Username && m.MaTruyen == tuongTac.MaTruyen);
            if (tuongTacs != null)
            {
                tuongTacs.DanhGia = tuongTac?.DanhGia;
                await _context.SaveChangesAsync();
                return Ok(tuongTacs);
            }
            else
            {
                var tuongTacss = new TuongTac()
                {
                    Username = tuongTac.Username,
                    MaTruyen = tuongTac.MaTruyen,
                    DanhGia  = tuongTac?.DanhGia,
                    YeuThich = tuongTac?.YeuThich,
  
                };

                await _context.TuongTacs.AddAsync(tuongTacss);
                await _context.SaveChangesAsync();
                return Ok(tuongTacss);
            }
        }


    }
}
