using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.Models;

namespace PerfumeShop.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly PerfumeShopContext _context;
        public NhanVienController(PerfumeShopContext context)
        {
            _context = context;
        }
        public IActionResult Index(string tenSearch, string sdtSearch, int? trangThaiFilter)
        {
            var nhanVienQuery = _context.NguoiDungs
                                 .Where(nd => nd.TaiKhoan.VaiTro == "NhanVien");

            if (!string.IsNullOrEmpty(tenSearch))
            {
                nhanVienQuery = nhanVienQuery.Where(nd => nd.HoTen.Contains(tenSearch));
            }

            if (!string.IsNullOrEmpty(sdtSearch))
            {
                nhanVienQuery = nhanVienQuery.Where(nd => nd.SoDienThoai.Contains(sdtSearch));
            }

            if (trangThaiFilter.HasValue)
            {
                nhanVienQuery = nhanVienQuery.Where(nd => nd.TaiKhoan.TrangThai == trangThaiFilter.Value);
            }

            // Gửi danh sách trạng thái xuống View để tạo dropdown
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Tất cả", Value = "" },
                new SelectListItem { Text = "Hoạt động", Value = "1" },
                new SelectListItem { Text = "Bị khóa", Value = "0" }
            }, "Value", "Text", trangThaiFilter?.ToString());

            return View(nhanVienQuery.ToList());
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NguoiDung nguoiDung)
        {
            // Bước 1: Lưu người dùng trước và lấy object thật sự
            var newNguoiDung = _context.NguoiDungs.Add(nguoiDung).Entity;
            _context.SaveChanges();

            // Bước 2: Tạo tài khoản tương ứng
            var taiKhoan = new TaiKhoan
            {
                Username = newNguoiDung.HoTen,  // hoặc xử lý để tránh trùng
                Password = "123456",            // nên mã hoá
                VaiTro = "NhanVien",
                TrangThai = 1
            };

            _context.TaiKhoans.Add(taiKhoan);
            _context.SaveChanges(); // sinh Id cho tài khoản

            // Bước 3: Gán ID_TK vào người dùng
            newNguoiDung.ID_TK = taiKhoan.Id;
            _context.NguoiDungs.Update(newNguoiDung);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // AJAX: Kiểm tra email
        [HttpGet]
        public JsonResult IsEmailAvailable(string email)
        {
            bool exists = _context.NguoiDungs.Any(nd => nd.Email == email);
            return Json(!exists); // true = hợp lệ
        }

        // AJAX: Kiểm tra số điện thoại
        [HttpGet]
        public JsonResult IsPhoneAvailable(string soDienThoai)
        {
            bool exists = _context.NguoiDungs.Any(nd => nd.SoDienThoai == soDienThoai);
            return Json(!exists); // true = hợp lệ
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
        public IActionResult Edit(int id, NguoiDung nguoiDung)
        {
            if (id != nguoiDung.Id)
                return NotFound();

            // Lấy dữ liệu người dùng cũ từ CSDL, bao gồm cả thông tin tài khoản
            var nguoiDungCu = _context.NguoiDungs
                .Include(nd => nd.TaiKhoan)
                .FirstOrDefault(nd => nd.Id == id);

            if (nguoiDungCu == null)
                return NotFound();

            // Cập nhật các trường của NguoiDung
            nguoiDungCu.HoTen = nguoiDung.HoTen;
            nguoiDungCu.SoDienThoai = nguoiDung.SoDienThoai;
            nguoiDungCu.Email = nguoiDung.Email;
            nguoiDungCu.NgayCapNhat = DateTime.Now;

            // Nếu có liên kết với tài khoản thì cập nhật TrạngThai
            if (nguoiDungCu.TaiKhoan != null)
            {
                nguoiDungCu.TaiKhoan.TrangThai = nguoiDung.TaiKhoan.TrangThai;

                // Chỉ đánh dấu cập nhật thuộc tính TrangThai
                _context.Entry(nguoiDungCu.TaiKhoan).Property(tk => tk.TrangThai).IsModified = true;
            }

            // Đánh dấu các thuộc tính cần cập nhật của người dùng
            _context.Entry(nguoiDungCu).Property(nd => nd.HoTen).IsModified = true;
            _context.Entry(nguoiDungCu).Property(nd => nd.SoDienThoai).IsModified = true;
            _context.Entry(nguoiDungCu).Property(nd => nd.Email).IsModified = true;
            _context.Entry(nguoiDungCu).Property(nd => nd.NgayCapNhat).IsModified = true;

            // Lưu thay đổi
            _context.SaveChanges();

            // Quay về trang danh sách
            return RedirectToAction("Index");
        }
    }
}
