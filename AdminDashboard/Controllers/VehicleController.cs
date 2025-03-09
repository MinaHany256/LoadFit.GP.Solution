using AdminDashboard.Helpers;
using AdminDashboard.Models;
using AutoMapper;
using LoadFit.Core;
using LoadFit.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _unitOfWork.Repository<Vehicle>().GetAllAsync();
            var mappedVehicles = _mapper.Map<IReadOnlyList<Vehicle>, IReadOnlyList<VehicleViewModel>>(vehicles);
            return View(mappedVehicles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }

                else
                    model.PictureUrl = "images/products/01.png";

                var mappedProduct = _mapper.Map<VehicleViewModel, Vehicle>(model);

                await _unitOfWork.Repository<Vehicle>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");


            }

            return View(model);
        }

		public async Task<IActionResult> Edit(int id)
		{
			var Vehicle = await _unitOfWork.Repository<Vehicle>().GetAsync(id);

			var mappedVehicle = _mapper.Map<Vehicle, VehicleViewModel>(Vehicle);

			return View(mappedVehicle);
		}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    if (model.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "products");
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                    }

                    else
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

                    var mappedProduct = _mapper.Map<VehicleViewModel, Vehicle>(model);

                    _unitOfWork.Repository<Vehicle>().UpdateAsync(mappedProduct);

                    var result = await _unitOfWork.CompleteAsync();

                    if (result > 0)
                        return RedirectToAction("Index");
                }
            }

            return View(model);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Repository<Vehicle>().GetAsync(id);

            var mappedProduct = _mapper.Map<Vehicle, VehicleViewModel>(product);

            return View(mappedProduct);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, VehicleViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            try
            {
                var product = await _unitOfWork.Repository<Vehicle>().GetAsync(id);

                if (product.PictureUrl != null)
                    PictureSettings.DeleteFile(product.PictureUrl, "products");

                _unitOfWork.Repository<Vehicle>().DeleteAsync(product);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {

                return View(model);
            }
        }

    }
}
