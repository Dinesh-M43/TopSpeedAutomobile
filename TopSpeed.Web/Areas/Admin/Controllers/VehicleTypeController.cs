using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TopSpeed.Application.ApplicationConstants;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehicleTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VehicleType> VehicleType = await _unitOfWork.VehicleType.GetAllAsync();
            return View(VehicleType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType VehicleType)
        {      

            if(ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Create(VehicleType);
                await _unitOfWork.SaveAsync();

                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            VehicleType VehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(VehicleType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VehicleType VehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id); 

            return View(VehicleType);
        }

        [HttpPost]
        public async Task <IActionResult> Edit(VehicleType VehicleType)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Update(VehicleType);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;


                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            VehicleType VehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(VehicleType);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VehicleType VehicleType)
        {
            await _unitOfWork.VehicleType.Delete(VehicleType);
            await _unitOfWork.SaveAsync();
           

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));



        }

    }
}
