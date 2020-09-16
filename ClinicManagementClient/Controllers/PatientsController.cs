using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicManagementClient.Controllers
{
    public class PatientsController : Controller

    {
        Uri baseaddress = new Uri("https://localhost:44323/api");

        HttpClient client;

        public PatientsController()

        {

            client = new HttpClient();

            client.BaseAddress = baseaddress;

        }
        public IActionResult Index()
        {
            List<Patient> ls = new List<Patient>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Patients").Result;

            if (response.IsSuccessStatusCode)

            {

                string data = response.Content.ReadAsStringAsync().Result;

                ls = JsonConvert.DeserializeObject<List<Patient>>(data);

            }

            return View(ls);
        }

        public IActionResult Create()

        {

            return View();

        }

        [HttpPost]
        public IActionResult Create(Patient obj)

        {

            string data = JsonConvert.SerializeObject(obj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Patients", content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }
        public IActionResult Edit(int id)

        {

            Patient ls = new Patient();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Patients/" + id).Result;

            if (response.IsSuccessStatusCode)

            {

                string data = response.Content.ReadAsStringAsync().Result;

                ls = JsonConvert.DeserializeObject<Patient>(data);

            }

            return View(ls);
        }

        [HttpPost]

        public IActionResult Edit(Patient obj)

        {

            string data = JsonConvert.SerializeObject(obj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Patients/" + obj.PatientId, content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }
        public IActionResult Delete(int id)

        {

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Patients/" + id).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return BadRequest();

        }

    }

}