using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.Models;

namespace PerfumeShop.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly PerfumeShopContext _context;

        public NguoiDungController(PerfumeShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var nhanVienList = _context.NguoiDungs
                              .Where(nd => nd.TaiKhoan.VaiTro == "NhanVien")
                              .ToList();

            return View(nhanVienList);
        }

        public IActionResult Details(int id)
        {
            var nguoiDung = _context.NguoiDungs
                                    .Include(nd => nd.TaiKhoan)
                                    .FirstOrDefault(nd => nd.Id == id);
            if (nguoiDung == null)
                return NotFound();
            return View(nguoiDung);
        }
        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Tạo tài khoản mặc định cho nhân viên
                var taiKhoan = new TaiKhoan
                {
                    Username = "nhanvien_" + Guid.NewGuid().ToString("N").Substring(0, 6),
                    Password = "123456", // nên hash nếu thực tế
                    VaiTro = "NhanVien",
                    TrangThai = 1
                };

                _context.TaiKhoans.Add(taiKhoan);
                _context.SaveChanges();

                // Gán khóa ngoại
                nguoiDung.ID_TK = taiKhoan.Id;

                _context.NguoiDungs.Add(nguoiDung);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: NguoiDung/Edit/5
        public IActionResult Edit(int id)
        {
            var nguoiDung = _context.NguoiDungs
                                    .Include(nd => nd.TaiKhoan)
                                    .FirstOrDefault(nd => nd.Id == id);
            if (nguoiDung == null)
                return NotFound();

            return View(nguoiDung);
        }

        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NguoiDung nguoiDung)
        {
            if (id != nguoiDung.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(nguoiDung);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }
        // GET: NguoiDung/Delete/5
        public IActionResult Delete(int id)
        {
            var nguoiDung = _context.NguoiDungs
                                    .Include(nd => nd.TaiKhoan)
                                    .FirstOrDefault(nd => nd.Id == id);
            if (nguoiDung == null)
                return NotFound();

            return View(nguoiDung);
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nguoiDung = _context.NguoiDungs
                                    .Include(nd => nd.TaiKhoan)
                                    .FirstOrDefault(nd => nd.Id == id);

            if (nguoiDung != null)
            {
                // Xóa tài khoản liên kết nếu cần
                if (nguoiDung.TaiKhoan != null)
                    _context.TaiKhoans.Remove(nguoiDung.TaiKhoan);

                _context.NguoiDungs.Remove(nguoiDung);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
