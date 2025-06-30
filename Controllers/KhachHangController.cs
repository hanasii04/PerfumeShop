using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.Models;

namespace PerfumeShop.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly PerfumeShopContext _context;

        public KhachHangController(PerfumeShopContext context)
        {
            _context = context;
        }

        // GET: Danh sách khách hàng
        public IActionResult Index(string search)
        {
            var query = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .Where(nd => nd.TaiKhoan.VaiTro == "KhachHang");

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(nd =>
                    nd.HoTen.Contains(search) ||
                    nd.Email.Contains(search) ||
                    nd.SoDienThoai.Contains(search));
            }

            var danhSach = query.ToList();
            return View(danhSach);
        }

        // GET: Sửa khách hàng
        public IActionResult Edit(int id)
        {
            var nguoiDung = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .FirstOrDefault(nd => nd.Id == id);

            if (nguoiDung == null)
                return NotFound();

            return View(nguoiDung);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NguoiDung model)
        {
            string newPassword = Request.Form["NewPassword"];
            string currentPassword = Request.Form["CurrentPassword"];
            string username = Request.Form["Username"];

            if (!ModelState.IsValid)
                return View(model);

            var nguoiDung = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .FirstOrDefault(nd => nd.Id == model.Id);

            if (nguoiDung == null || nguoiDung.TaiKhoan == null)
                return NotFound();

            // Kiểm tra mật khẩu hiện tại
            if (!string.IsNullOrEmpty(newPassword))
            {
                if (string.IsNullOrEmpty(currentPassword) ||
                    !BCrypt.Net.BCrypt.Verify(currentPassword, nguoiDung.TaiKhoan.Password))
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác.");
                    return View(model);
                }

                nguoiDung.TaiKhoan.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            nguoiDung.HoTen = model.HoTen;
            nguoiDung.SoDienThoai = model.SoDienThoai;
            nguoiDung.Email = model.Email;
            nguoiDung.NgayCapNhat = DateTime.Now;
            nguoiDung.TaiKhoan.Username = username;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Xem chi tiết
        public IActionResult Details(int id)
        {
            var nguoiDung = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .FirstOrDefault(nd => nd.Id == id);

            if (nguoiDung == null)
                return NotFound();

            return View(nguoiDung);
        }

        // GET: Hiển thị xác nhận KHÓA
        public IActionResult Khoa(int id)
        {
            var tk = _context.TaiKhoans
                .Include(t => t.NguoiDung)
                .FirstOrDefault(t => t.Id == id);

            if (tk == null)
                return NotFound();

            return View(tk);
        }

        // POST: Khóa thật
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KhoaConfirmed(int id)
        {
            var tk = _context.TaiKhoans.Find(id);
            if (tk == null)
                return NotFound();

            tk.TrangThai = 0;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Hiển thị xác nhận MỞ KHÓA
        public IActionResult MoKhoa(int id)
        {
            var tk = _context.TaiKhoans
                .Include(t => t.NguoiDung)
                .FirstOrDefault(t => t.Id == id);

            if (tk == null)
                return NotFound();

            return View(tk);
        }

        // POST: Mở khóa thật
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoKhoaConfirmed(int id)
        {
            var tk = _context.TaiKhoans.Find(id);
            if (tk == null)
                return NotFound();

            tk.TrangThai = 1;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: JSON trả về danh sách khách hàng tìm kiếm
        [HttpGet]
        public IActionResult GetKhachHang(string search)
        {
            var query = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .Where(nd => nd.TaiKhoan.VaiTro == "KhachHang");

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(nd =>
                    nd.HoTen.Contains(search) ||
                    nd.Email.Contains(search) ||
                    nd.SoDienThoai.Contains(search));
            }

            var data = query.Select(nd => new
            {
                nd.Id,
                nd.HoTen,
                nd.Email,
                nd.SoDienThoai,
                nd.TaiKhoan.TrangThai,
                IdTaiKhoan = nd.TaiKhoan.Id
            }).ToList();

            return Json(data);
        }
    }
}
