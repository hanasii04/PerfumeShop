using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeShop.Models;
using System.Threading.Tasks;

namespace PerfumeShop.Controllers
{
	public class ProductsController : Controller
	{
		private readonly PerfumeShopContext _context;
		private readonly IWebHostEnvironment _environment;

		public ProductsController(PerfumeShopContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}

		public async Task<IActionResult> Index()
		{
			var sp = await _context.SanPhams.ToListAsync();
			return View(sp);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				// Nếu không nhập gì thì trả về tất cả sản phẩm
				var allProducts = await _context.SanPhams.ToListAsync();
				return View("Index", allProducts);
			}

			// Chuyển keyword về chữ thường và loại bỏ khoảng trắng đầu cuối
			var keywordLower = keyword.Trim().ToLower();
			var result = await _context.SanPhams
				.Where(sp => sp.TenSanPham.ToLower().Contains(keywordLower) || 
						     sp.ThuongHieu.ToLower().Contains(keywordLower) || 
							 sp.GioiTinh.ToLower().Contains(keywordLower) ||
							 sp.QuocGia.ToLower().Contains(keywordLower))
				.ToListAsync();
			return View("Index", result);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var sanPham = await _context.SanPhams
				.Include(sp => sp.SanPhamChiTiets) // Lấy danh sách chi tiết sản phẩm
				.ThenInclude(ct => ct.TheTich)  // Lấy thông tin thể tích của từng biến thể sản phẩm
				.FirstOrDefaultAsync(sp => sp.Id == id);

			if (sanPham == null)
			{
				return NotFound();
			}

			return View(sanPham);
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var sanPham = await _context.SanPhams
				.Include(sp => sp.SanPhamChiTiets)
					.ThenInclude(ct => ct.TheTich)
				.FirstOrDefaultAsync(sp => sp.Id == id);

			if (sanPham == null)
				return NotFound();

			// Tạo dropdown giới tính và hiển thị giới tính hiện tại của sản phẩm
			ViewBag.GioiTinhList = new SelectList(new[] { "Nam", "Nữ", "Unisex" }, sanPham.GioiTinh);
			return View(sanPham);
		}

		[HttpPost]
		public async Task<IActionResult> Update(
			int id, string TenSanPham, string ThuongHieu, string QuocGia, string GioiTinh,
			int ThoiGianLuuHuong, string MoTa, IFormFile? HinhAnhMoi)
		{
			var sanPham = await _context.SanPhams.FindAsync(id);
			if (sanPham == null)
				return NotFound();

			sanPham.TenSanPham = TenSanPham;
			sanPham.ThuongHieu = ThuongHieu;
			sanPham.QuocGia = QuocGia;
			sanPham.GioiTinh = GioiTinh;
			sanPham.ThoiGianLuuHuong = ThoiGianLuuHuong;
			sanPham.MoTa = MoTa;

			// Nếu user không chọn hình ảnh mới thì giữ nguyên hình ảnh cũ
			if (HinhAnhMoi != null)
			{
				var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
				if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + HinhAnhMoi.FileName;
				var filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await HinhAnhMoi.CopyToAsync(fileStream);
				}
				sanPham.HinhAnh = "/images/" + uniqueFileName;
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Details", new { id = sanPham.Id });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateDetails(int id)
		{
			var chiTiet = await _context.SanPhamChiTiets
				.Include(c => c.TheTich)
				.Include(c => c.SanPham)
				.FirstOrDefaultAsync(c => c.Id == id);

			if (chiTiet == null)
				return NotFound();

			// Tạo dropdown thể tích và hiển thị thể tích hiện tại của sản phẩm chi tiết
			ViewBag.TheTichList = new SelectList(_context.TheTichs, "Id", "Ten", chiTiet.Id_TheTich);
			return View(chiTiet);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateDetails(int id, int Id_TheTich, int GiaNhap, int GiaBan, int SoLuong)
		{
			// Tìm kiếm sản phẩm chi tiết cần cập nhật
			var chiTiet = await _context.SanPhamChiTiets.FindAsync(id);
			if (chiTiet == null)
				return NotFound();

			if (GiaNhap <= 0)
				ModelState.AddModelError("GiaNhap", "Giá nhập phải lớn hơn 0");
			if (GiaBan <= 0)
				ModelState.AddModelError("GiaBan", "Giá bán phải lớn hơn 0");
			if (GiaBan <= GiaNhap)
				ModelState.AddModelError("GiaBan", "Giá bán phải lớn hơn giá nhập");
			if (SoLuong < 0)
				ModelState.AddModelError("SoLuong", "Số lượng không được âm");

			// Kiểm tra xem thể tích định cập nhật đã tồn tại chưa
			bool trungTheTich = await _context.SanPhamChiTiets
				.AnyAsync(c => c.Id_SanPham == chiTiet.Id_SanPham && c.Id_TheTich == Id_TheTich && c.Id != id);
			if (trungTheTich)
				ModelState.AddModelError("Id_TheTich", "Biến thể với thể tích này đã tồn tại");

			// Nếu có lỗi thì trả về view với danh sách thể tích và thể tích đã chọn
			if (!ModelState.IsValid)
			{
				ViewBag.TheTichList = new SelectList(_context.TheTichs, "Id", "Ten", Id_TheTich);
				return View(chiTiet);
			}

			chiTiet.Id_TheTich = Id_TheTich;
			chiTiet.GiaNhap = GiaNhap;
			chiTiet.GiaBan = GiaBan;
			chiTiet.SoLuong = SoLuong;

			await _context.SaveChangesAsync();
			return RedirectToAction("Update", new { id = chiTiet.Id_SanPham });
		}

		public async Task<IActionResult> Create()
		{
			await LoadDropdownsAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(
			string tenSanPham, string thuongHieu, string quocGia, string gioiTinh, int? thoiGianLuuHuong,
			string moTa, IFormFile? hinhAnh, int? soLuong, int? giaBan, int? giaNhap, int? idTheTich)
			{
			// Xóa lỗi trước đó để tránh lỗi lặp lại
			var keysToClean = new[] { "tenSanPham", "thuongHieu", "quocGia", "gioiTinh", "moTa" };
			foreach (var key in keysToClean)
			{
				if (ModelState.ContainsKey(key))
					ModelState[key].Errors.Clear();
			}

			if (string.IsNullOrWhiteSpace(tenSanPham))
				ModelState.AddModelError("tenSanPham", "Tên sản phẩm không được bỏ trống");
			if (string.IsNullOrWhiteSpace(thuongHieu))
				ModelState.AddModelError("thuongHieu", "Thương hiệu không được bỏ trống");
			if (string.IsNullOrWhiteSpace(quocGia) || quocGia.Length > 100)
				ModelState.AddModelError("quocGia", "Quốc gia không được bỏ trống và không quá 100 ký tự.");
			if (string.IsNullOrWhiteSpace(moTa))
				ModelState.AddModelError("moTa", "Mô tả không được bỏ trống.");
			if (string.IsNullOrWhiteSpace(gioiTinh) || !new[] { "Nam", "Nữ", "Unisex" }.Contains(gioiTinh))
				ModelState.AddModelError("gioiTinh", "Giới tính không hợp lệ.");
			if (thoiGianLuuHuong == null || thoiGianLuuHuong < 1)
				ModelState.AddModelError("thoiGianLuuHuong", "Thời gian lưu hương phải lớn hơn hoặc bằng 1");
			if (hinhAnh == null)
				ModelState.AddModelError("hinhAnh", "Vui lòng chọn hình ảnh");
			if (soLuong <= 0 || soLuong == null)
				ModelState.AddModelError("soLuong", "Số lượng phải lớn hơn 0.");
			if (giaNhap <= 0 || giaNhap == null)
				ModelState.AddModelError("giaNhap", "Giá nhập phải lớn hơn 0.");
			if (giaBan <= 0 || giaBan == null)
				ModelState.AddModelError("giaBan", "Giá bán phải lớn hơn 0.");
			if (giaBan <= giaNhap)
				ModelState.AddModelError("giaBan", "Giá bán phải lớn hơn giá nhập.");
			if (!_context.TheTichs.Any(t => t.Id == idTheTich) || idTheTich == null)
				ModelState.AddModelError("idTheTich", "Vui lòng chọn thể tích hợp lệ.");

			if (ModelState.ErrorCount == 0)
			{
				try
				{
					// Kiểm tra sản phẩm có cùng tên đã tồn tại chưa
					var sanPhamTonTai = await _context.SanPhams
						.FirstOrDefaultAsync(x => x.TenSanPham == tenSanPham);

					if (sanPhamTonTai != null)
					{
						// Kiểm tra từng thuộc tính nếu sản phẩm cùng tên đã tồn tại
						// Nếu có thuộc tính nào khác với sản phẩm đã tồn tại thì báo lỗi
						if (sanPhamTonTai.ThuongHieu != thuongHieu)
							ModelState.AddModelError("thuongHieu", $"Sản phẩm đã tồn tại với thương hiệu khác: {sanPhamTonTai.ThuongHieu}");
						if (sanPhamTonTai.QuocGia != quocGia)
							ModelState.AddModelError("quocGia", $"Sản phẩm đã tồn tại với quốc gia khác: {sanPhamTonTai.QuocGia}");
						if (sanPhamTonTai.GioiTinh != gioiTinh)
							ModelState.AddModelError("gioiTinh", $"Sản phẩm đã tồn tại với giới tính khác: {sanPhamTonTai.GioiTinh}");
						if (sanPhamTonTai.ThoiGianLuuHuong != thoiGianLuuHuong)
							ModelState.AddModelError("thoiGianLuuHuong", $"Sản phẩm đã tồn tại với thời gian lưu hương khác: {sanPhamTonTai.ThoiGianLuuHuong} giờ");

						// Nếu có bất kỳ thuộc tính nào khác nhau
						if (ModelState.ErrorCount > 0)
						{
							throw new Exception("Sản phẩm đã tồn tại với thông tin khác");
						}

						// Nếu tất cả thông tin chung giống nhau, kiểm tra thể tích
						var bienTheTonTai = await _context.SanPhamChiTiets
							.FirstOrDefaultAsync(x => x.Id_SanPham == sanPhamTonTai.Id && x.Id_TheTich == idTheTich);

						if (bienTheTonTai != null)
						{
							ModelState.AddModelError("idTheTich", "Sản phẩm này đã tồn tại với thể tích này rồi");
							throw new Exception("Sản phẩm đã tồn tại với thể tích này");
						}
					}

					// Đảm bảo thư mục images tồn tại
					var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
					if (!Directory.Exists(uploadsFolder))
					{
						Directory.CreateDirectory(uploadsFolder);
					}

					// Đăt tên file và lưu hình ảnh
					string uniqueFileName = Guid.NewGuid().ToString() + "_" + hinhAnh.FileName;
					var filePath = Path.Combine(uploadsFolder, uniqueFileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await hinhAnh.CopyToAsync(fileStream);
					}

					// Gán sanPham vào 1 sp nếu đã tồn tại, hoặc tạo mới nếu chưa tồn tại
					var sanPham = sanPhamTonTai ?? new SanPham
					{
						TenSanPham = tenSanPham,
						ThuongHieu = thuongHieu,
						QuocGia = quocGia,
						GioiTinh = gioiTinh,
						ThoiGianLuuHuong = (int)thoiGianLuuHuong,
						MoTa = moTa,
						HinhAnh = $"/images/{uniqueFileName}"
					};

					// Nếu là sp mới thì lưu vào DB
					if (sanPhamTonTai == null)
					{
						_context.SanPhams.Add(sanPham);
						await _context.SaveChangesAsync();
					}

					// Tạo sản phẩm chi tiết
					var sanPhamChiTiet = new SanPhamChiTiet
					{
						Id_SanPham = sanPham.Id,
						SoLuong = (int)soLuong,
						GiaBan = (int)giaBan,
						GiaNhap = (int)giaNhap,
						Id_TheTich = (int)idTheTich
					};
					_context.SanPhamChiTiets.Add(sanPhamChiTiet);
					await _context.SaveChangesAsync();

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", $"Lỗi: {ex.Message}");
				}
			}

			await LoadDropdownsAsync(gioiTinh, idTheTich);

			// Giữ lại giá trị đã nhập
			ViewBag.TenSanPham = tenSanPham;
			ViewBag.ThuongHieu = thuongHieu;
			ViewBag.QuocGia = quocGia;
			ViewBag.GioiTinh = gioiTinh;
			ViewBag.ThoiGianLuuHuong = thoiGianLuuHuong;
			ViewBag.MoTa = moTa;
			ViewBag.SoLuong = soLuong;
			ViewBag.GiaBan = giaBan;
			ViewBag.GiaNhap = giaNhap;
			ViewBag.IdTheTich = idTheTich;

			// Truyền lỗi validation
			ViewBag.ErrorTenSanPham = ModelState["tenSanPham"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorThuongHieu = ModelState["thuongHieu"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorQuocGia = ModelState["quocGia"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorGioiTinh = ModelState["gioiTinh"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorThoiGianLuuHuong = ModelState["thoiGianLuuHuong"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorMoTa = ModelState["moTa"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorHinhAnh = ModelState["hinhAnh"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorSoLuong = ModelState["soLuong"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorGiaBan = ModelState["giaBan"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorGiaNhap = ModelState["giaNhap"]?.Errors.FirstOrDefault()?.ErrorMessage;
			ViewBag.ErrorIdTheTich = ModelState["idTheTich"]?.Errors.FirstOrDefault()?.ErrorMessage;

			return View();
		}
		// Với GET, LoadDropdownsAsync sẽ được gọi để nạp dữ liệu vào dropdown thể tích và giới tính để user có thể chọn
		// Với POST, LoadDropdownsAsync sẽ được gọi để nạp lại dữ liệu user đã nhập nếu có lỗi xảy ra khi tạo sản phẩm
		private async Task LoadDropdownsAsync(string? gioiTinh = null, int? idTheTich = null)
		{
			var theTichList = await _context.TheTichs.ToListAsync();
			ViewBag.TheTichList = new SelectList(theTichList, "Id", "Ten", idTheTich);

			ViewBag.GioiTinhList = new SelectList(
				new[] { "Nam", "Nữ", "Unisex" }.Select(g => new { Value = g, Text = g }),
				"Value", "Text", gioiTinh
			);
		}
	}
}