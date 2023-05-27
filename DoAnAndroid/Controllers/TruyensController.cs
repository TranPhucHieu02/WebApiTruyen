using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnAndroid.Models;

namespace DoAnAndroid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruyensController : Controller
    {
        private readonly ApiTruyenContext _context;

        public TruyensController(ApiTruyenContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTruyen()
        {
            List<Truyen> truyens = new List<Truyen>();
            truyens =await _context.Truyens.ToListAsync();
            truyens.Sort((a, b) => b.LuotDoc.Value.CompareTo(a.LuotDoc.Value));
            return Ok(truyens);
            //return Ok(await _context.Truyens.ToListAsync());
        }

        [HttpGet("get-truyen-id")]
        public async Task<IActionResult> GetTruyenID(string MaTruyen)
        {
            if (MaTruyen == null || _context.Truyens == null)
            {
                return NotFound();
            }

            var truyen = await _context.Truyens
                .FirstOrDefaultAsync(m => m.MaTruyen == MaTruyen);
            if (truyen == null)
            {
                return NotFound();
            }

            return Ok(truyen);
        }
        [HttpGet("get-truyen-tl")]
        public async Task<IActionResult> GetTruyenTL(string TheLoai)
        {
            List<Truyen> truyen = _context.Truyens.Where(m => m.TheLoai == TheLoai).ToList();
            truyen.Sort((a, b) => b.LuotDoc.Value.CompareTo(a.LuotDoc.Value));
            if (truyen == null)
            {
                return NotFound();
            }

            return Ok(truyen);
        }

        [HttpPut("update-luotdoc")]
        public async Task<IActionResult> UpdateLuotDoc(string Matruyen)
        {
            var truyens = await _context.Truyens.FirstOrDefaultAsync(p => p.MaTruyen == Matruyen);
            if (truyens == null)
            {
                return NotFound();
            }
            {
                truyens.LuotDoc = truyens.LuotDoc+1;
                await _context.SaveChangesAsync();
                return Ok(truyens);
            }
        }
    }
}
