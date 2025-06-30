using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.DTO;
using PerfumeShop.Models;

namespace PerfumeShop.Controllers
{
    public class MaGiamGiaController : Controller
    {
        private readonly PerfumeShopContext _context;

        public MaGiamGiaController(PerfumeShopContext context)
        {
            _context = context;
        }

        // ✅ DANH SÁCH
        public IActionResult Index(string trangThai = "tatca", string tuKhoa = "", int page = 1, int pageSize = 10)
        {
            var query = _context.MaGiamGias.AsQueryable();

            // 🔍 Tìm kiếm theo tên mã
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(m => m.TenMaGiamGia.Contains(tuKhoa));
            }

            // 📂 Lọc trạng thái
            if (trangThai == "danghoatdong")
            {
                query = query.Where(m => m.TrangThai == true);
            }
            else if (trangThai == "dakhoa")
            {
                query = query.Where(m => m.TrangThai == false);
            }

            // ↕️ Sắp xếp theo ngày tạo mới nhất
            query = query.OrderByDescending(m => m.NgayTao);

            // 📄 Phân trang
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Gửi dữ liệu ra View
            ViewBag.TuKhoa = tuKhoa;
            ViewBag.TrangThai = trangThai;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(items);
        }

        // ✅ FORM TẠO MỚI
        [HttpGet]
        public IActionResult Create()
        {
            return View(new MaGiamGiaDTO
            {
                NgayBatDau = DateTime.Today,
                TrangThai = true
            });
        }

        // ✅ XỬ LÝ TẠO MỚI
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaGiamGiaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = new MaGiamGia
            {
                TenMaGiamGia = dto.TenMaGiamGia,
                PhanTramGiam = dto.PhanTramGiam,
                NgayBatDau = dto.NgayBatDau,
                NgayKetThuc = dto.NgayKetThuc,
                SoLuong = dto.SoLuong,
                TrangThai = dto.TrangThai,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            };

            _context.MaGiamGias.Add(entity);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ✅ FORM CHỈNH SỬA
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _context.MaGiamGias.Find(id);
            if (entity == null) return NotFound();

            var dto = new MaGiamGiaDTO
            {
                Id = entity.Id,
                TenMaGiamGia = entity.TenMaGiamGia,
                PhanTramGiam = entity.PhanTramGiam,
                NgayBatDau = entity.NgayBatDau,
                NgayKetThuc = entity.NgayKetThuc,
                SoLuong = entity.SoLuong,
                TrangThai = entity.TrangThai,
                NgayTao = entity.NgayTao,
                NgayCapNhat = entity.NgayCapNhat
            };

            return View(dto);
        }

        // ✅ XỬ LÝ CHỈNH SỬA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MaGiamGiaDTO dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            var entity = _context.MaGiamGias.Find(id);
            if (entity == null) return NotFound();

            entity.TenMaGiamGia = dto.TenMaGiamGia;
            entity.PhanTramGiam = dto.PhanTramGiam;
            entity.NgayBatDau = dto.NgayBatDau;
            entity.NgayKetThuc = dto.NgayKetThuc;
            entity.SoLuong = dto.SoLuong;
            entity.TrangThai = dto.TrangThai;
            entity.NgayCapNhat = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleTrangThai(int id)
        {
            var entity = _context.MaGiamGias.Find(id);
            if (entity == null)
                return NotFound();

            entity.TrangThai = !entity.TrangThai; // chuyển trạng thái hoạt động <=> đã khoá
            entity.NgayCapNhat = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // ✅ XEM CHI TIẾT
        [HttpGet]
        public IActionResult Details(int id)
        {
            var entity = _context.MaGiamGias.Find(id);
            if (entity == null) return NotFound();
            return View(entity);
        }
    }
}
