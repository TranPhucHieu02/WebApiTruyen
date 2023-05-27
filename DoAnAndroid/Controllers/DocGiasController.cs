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
    public class DocGiasController : Controller
    {
        private readonly ApiTruyenContext _context;

        public DocGiasController(ApiTruyenContext context)
        {
            _context = context;
        }

       


        //=============================        GET       =========================================

        [HttpGet]
        public async Task<IActionResult> GetDocGia()
        {
            return Ok(await _context.DocGia.ToListAsync());
        }

        [HttpGet("check-user")]
        public async Task<IActionResult> CheckUser(string Username)
        {
            if (Username == null || _context.DocGia == null)
            {
                return NotFound();
            }
            var docGia = await _context.DocGia.FirstOrDefaultAsync(m => m.Username == Username);
            if (docGia == null)
            {
                return NotFound();
            }

            return Ok(docGia);
        }

        [HttpGet("check-user-email")]
        public async Task<IActionResult> CheckUserEmail(string Username,string Email)
        {
            if (Username == null || Email == null || _context.DocGia == null)
            {
                return NotFound();
            }
            var docGia = await _context.DocGia.FirstOrDefaultAsync(m => m.Username == Username && m.Email==Email);
            if (docGia == null)
            {
                return NotFound();
            }

            return Ok(docGia);
        }


        [HttpGet("get-user")]
        //[Route("get_user")]
        public async Task<IActionResult> GetDocGiaID(string Username, string password)
        {
            if (Username == null || password == null || _context.DocGia == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGia.FirstOrDefaultAsync(m => m.Username == Username && m.Password == password);
            if (docGia == null)
            {
                return NotFound();
            }

            return Ok(docGia);
        }
        //==================================   POST      ===========================================


        [HttpPost]
        public async Task<IActionResult> AddDocGia(DocGia docGia)
        {
            var docGias = new DocGia()
            {
                Username = docGia.Username,
                Password = docGia.Password,
                TenDg = docGia.TenDg,
                GioiTinh = docGia.GioiTinh,
                Email = docGia.Email,
                Sdt = docGia.Sdt,
            };
            await _context.DocGia.AddAsync(docGia);
            await _context.SaveChangesAsync();
            return Ok(docGias);
        }

        //===================================   PUT          ==========================================


        [HttpPut("update-password")]
        public async Task<IActionResult> UpdateUser1(string Username, string Password, UpdateUser updateUser)
        {
            var docGias = await _context.DocGia.FirstOrDefaultAsync(p => p.Username == Username && p.Password == Password);
            if (docGias == null)
            {
                return NotFound();
            }
            {
                docGias.Password = updateUser.Password;
                await _context.SaveChangesAsync();
                return Ok(docGias);
            }
        }
        [HttpPut("update-password-quenmk")]
        public async Task<IActionResult> UpdateUser2(string Username, string Email, UpdateUser updateUser)
        {
            var docGias = await _context.DocGia.FirstOrDefaultAsync(p => p.Username == Username && p.Email == Email);
            if (docGias == null)
            {
                return NotFound();
            }
            {
                docGias.Password = updateUser.Password;
                await _context.SaveChangesAsync();
                return Ok(docGias);
            }
        }
        
        [HttpPut("update-docgia")]
        public async Task<IActionResult> UpdateDocGia(string Username, UpdateDocGia updateDocGia)
        {
            var docGias = await _context.DocGia.FirstOrDefaultAsync(p => p.Username == Username);
            if (docGias == null)
            {
                return NotFound();
            }
            {
                docGias.TenDg = updateDocGia.TenDg;
                docGias.Sdt = updateDocGia.Sdt;
                docGias.Email = updateDocGia.Email;
                docGias.GioiTinh=updateDocGia.GioiTinh;
                await _context.SaveChangesAsync();
                return Ok(docGias);
            }
        }
    }
}

