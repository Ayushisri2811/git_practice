using HealthAppMVC.Services.Interface;
using SharedDto.DoctorDtos;
using SharedDto.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorApiService _doctorService;

        public DoctorController(IDoctorApiService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: Doctor
        public async Task<ActionResult> Index(string specialisation = "")
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            if (!string.IsNullOrWhiteSpace(specialisation))
            {
                doctors = await _doctorService
                    .GetDoctorsBySpecialisationAsync(specialisation);
            }

            ViewBag.Specialisations =
                Enum.GetNames(typeof(SpecialisationType));

            return View(doctors);
        }

        // GET: Doctor/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return HttpNotFound();
                }

                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index", "Doctor");
            }
        }

        // GET: Doctor/DetailsPartial/5
        // Used for popup details modal
        public async Task<ActionResult> DetailsPartial(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return HttpNotFound();
                }

                return PartialView("_DoctorDetailsModal", doctor);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            ViewBag.Specialisations =
                Enum.GetNames(typeof(SpecialisationType));

            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDoctorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Specialisations =
                        Enum.GetNames(typeof(SpecialisationType));

                    return View(dto);
                }

                await _doctorService.AddDoctorAsync(dto);

                TempData["Success"] =
                    "Doctor Registered Successfully";

                return RedirectToAction("Index", "Doctor");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Specialisations =
                    Enum.GetNames(typeof(SpecialisationType));

                return View(dto);
            }
        }

        // GET: Doctor/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return HttpNotFound();
                }

                CreateDoctorDto dto = new CreateDoctorDto
                {
                    FullName = doctor.FullName,
                    Specialisation = doctor.Specialisation,
                    YearsOfExperience = doctor.YearsOfExperience,
                    ConsultationFee = doctor.ConsultationFee,
                    DoctorEmail = doctor.DoctorEmail,
                    DoctorPhoneNo = doctor.DoctorPhoneNo
                };

                ViewBag.Specialisations =
                    Enum.GetNames(typeof(SpecialisationType));

                ViewBag.DoctorId = id;

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index", "Doctor");
            }
        }

        // GET: Doctor/EditPartial/5
        // Used for popup edit modal
        public async Task<ActionResult> EditPartial(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return HttpNotFound();
                }

                CreateDoctorDto dto = new CreateDoctorDto
                {
                    FullName = doctor.FullName,
                    Specialisation = doctor.Specialisation,
                    YearsOfExperience = doctor.YearsOfExperience,
                    ConsultationFee = doctor.ConsultationFee,
                    DoctorEmail = doctor.DoctorEmail,
                    DoctorPhoneNo = doctor.DoctorPhoneNo
                };

                ViewBag.Specialisations =
                    Enum.GetNames(typeof(SpecialisationType));

                ViewBag.DoctorId = id;

                return PartialView("_DoctorEditModal", dto);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateDoctorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Specialisations =
                        Enum.GetNames(typeof(SpecialisationType));

                    ViewBag.DoctorId = id;

                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_DoctorEditModal", dto);
                    }

                    return View(dto);
                }

                await _doctorService.UpdateDoctorAsync(id, dto);

                TempData["Success"] =
                    "Doctor Details Updated Successfully";

                if (Request.IsAjaxRequest())
                {
                    return Json(new
                    {
                        success = true,
                        message = "Doctor details updated successfully."
                    });
                }

                return RedirectToAction("Index", "Doctor");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Specialisations =
                    Enum.GetNames(typeof(SpecialisationType));

                ViewBag.DoctorId = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_DoctorEditModal", dto);
                }

                return View(dto);
            }
        }

        // GET: Doctor/ChangeStatus/5
        public async Task<ActionResult> ChangeStatus(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return HttpNotFound();
                }

                bool newStatus = !doctor.IsActive;

                await _doctorService.ChangeStatusAsync(id, newStatus);

                TempData["Success"] =
                    "Doctor status updated.";

                return RedirectToAction("Index", "Doctor");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index", "Doctor");
            }
        }

        // GET: Doctor/SearchDoctor
        public async Task<ActionResult> SearchDoctor()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            return View(doctors);
        }

        // GET: Doctor/SearchDoctorNames?term=abc
        public async Task<JsonResult> SearchDoctorNames(string term)
        {
            var doctors = await _doctorService.SearchByNameAsync(term);

            var result = doctors
                .Select(d => new
                {
                    id = d.DoctorId,
                    label = d.FullName,
                    value = d.DoctorId
                })
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Doctor/EditDoctorByName/5
        public ActionResult EditDoctorByName(int id)
        {
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Doctor/DoctorSearch
        public async Task<ActionResult> DoctorSearch(
            string doctorName,
            string specialisation)
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            if (!string.IsNullOrWhiteSpace(doctorName))
            {
                doctors = doctors
                    .Where(d =>
                        d.FullName
                            .ToLower()
                            .Contains(doctorName.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(specialisation))
            {
                doctors = doctors
                    .Where(d =>
                        d.Specialisation.Equals(
                            specialisation,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(doctors);
        }
    }
}