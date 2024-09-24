using HealthCenter.Data;
using HealthCenter.Models;
using HealthCenter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthCenter.Controllers
{
	public class PatientsController : Controller
	{
		private readonly PatientsDbContext patientsDbContext;
		public PatientsController(PatientsDbContext patientsDbContext)
		{
			this.patientsDbContext = patientsDbContext;
		}

		public IActionResult Patients()
		{
			List<Patient> patients = this.patientsDbContext.Patients.ToList();
			return View(patients);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(PatientViewModel patientViewModel)
		{
			if (ModelState.IsValid)
			{
				Patient patient = new Patient();
				patient.FirstName = patientViewModel.FirstName;
				patient.LastName = patientViewModel.LastName;
				patient.Address = patientViewModel.Address;
				patient.Email = patientViewModel.Email;

				patientsDbContext.Patients.Add(patient);
				patientsDbContext.SaveChanges();

				return RedirectToAction("Patients", "Patients");
			}
			return View(patientViewModel);
		}

		[HttpGet]
		public IActionResult Edit(string id)
		{
			Patient patient = this.patientsDbContext.Patients.Find(id);

			return View(patient);
		}

		[HttpPost]
		public IActionResult Edit(Patient patient)
		{
			if (ModelState.IsValid)
			{
				patientsDbContext.Patients.Update(patient);
				patientsDbContext.SaveChanges();

				return RedirectToAction("Patients", "Patients");
			}
			return View(patient);
		}

		public IActionResult Delete(string id)
		{
			Patient patient = this.patientsDbContext.Patients.Find(id);
			patientsDbContext.Patients.Remove(patient);
			patientsDbContext.SaveChanges();

			return RedirectToAction("Patients", "Patients");
		}
	}
}
