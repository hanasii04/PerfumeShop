using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.DTO;
using PerfumeShop.Models;

namespace PerfumeShop.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly PerfumeShopContext _context;
        public TaiKhoanController(PerfumeShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(DangNhapVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var taiKhoan = _context.TaiKhoans
                .Include(t => t.NguoiDung)
                .FirstOrDefault(t => t.Username == model.UserName && t.Password == model.PassWord);

            if (taiKhoan == null || taiKhoan.TrangThai != 1) // 1 = HoatDong
            {
                ModelState.AddModelError("", "Tài khoản không hợp lệ hoặc đã bị khóa.");
                return View(model);
            }

            // Lưu session
            HttpContext.Session.SetInt32("ID_TK", taiKhoan.Id);
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro ?? "KhachHang");
            HttpContext.Session.SetString("TenNguoiDung", taiKhoan.NguoiDung?.HoTen ?? "");

            // Chuyển hướng theo vai trò
            if (taiKhoan.VaiTro == "QuanLy" || taiKhoan.VaiTro == "NhanVien")
            {
                return RedirectToAction("Index", "Admin"); // vào trang Admin
            }
            else
            {
                return RedirectToAction("Index", "Home"); // vào trang Home
            }
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(DangKyVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.TaiKhoans.Any(t => t.Username == model.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                return View(model);
            }

            if (_context.NguoiDungs.Any(n => n.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(model);
            }

            var tk = new TaiKhoan
            {
                Username = model.UserName,
                Password = model.PassWord,
                VaiTro = "KhachHang",
                TrangThai = 1
            };

            _context.TaiKhoans.Add(tk);
            _context.SaveChanges();

            var nd = new NguoiDung
            {
                HoTen = model.HoTen,
                SoDienThoai = model.SoDienThoai,
                Email = model.Email,
                ID_TK = tk.Id,
                NgayTao = DateTime.Now
            };

            _context.NguoiDungs.Add(nd);
            _context.SaveChanges();

            return RedirectToAction("DangNhap");
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
