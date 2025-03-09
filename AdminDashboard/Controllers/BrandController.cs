using LoadFit.Core;
using LoadFit.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
	public class BrandController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public BrandController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var brands = await _unitOfWork.Repository<VehicleBrand>().GetAllAsync();

			return View(brands);
		}

		public async Task<IActionResult> Create(VehicleBrand brand)
		{
			try
			{
				await _unitOfWork.Repository<VehicleBrand>().AddAsync(brand);
				await _unitOfWork.CompleteAsync();
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{

				ModelState.AddModelError("Name", "Please Enter New Brand");
				return View("Index", await _unitOfWork.Repository<VehicleBrand>().GetAllAsync());
			}
		}

		public async Task<IActionResult> Delete(int id)
		{
			var brand = await _unitOfWork.Repository<VehicleBrand>().GetAsync(id);

			_unitOfWork.Repository<VehicleBrand>().DeleteAsync(brand);

			await _unitOfWork.CompleteAsync();

			return RedirectToAction("Index");
		}
	}
}
