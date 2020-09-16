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
    public class DoctorsController : Controller
    {
        Uri baseaddress = new Uri("https://localhost:44323/api");

        HttpClient client;

        public DoctorsController()

        {

            client = new HttpClient();

            client.BaseAddress = baseaddress;
        }

        public IActionResult Index()
        {
            List<Doctor> ls = new List<Doctor>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Doctors").Result;

            if (response.IsSuccessStatusCode)

            {

                string data = response.Content.ReadAsStringAsync().Result;

                ls = JsonConvert.DeserializeObject<List<Doctor>>(data);

            }

            return View(ls);
        }
        public IActionResult Create()

        {

            return View();

        }

        [HttpPost]
        public IActionResult Create(Doctor obj)

        {

            string data = JsonConvert.SerializeObject(obj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Doctors", content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }
        public IActionResult Edit(int id)

        {

            Doctor ls = new Doctor();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Doctors" + id).Result;

            if (response.IsSuccessStatusCode)

            {

                string data = response.Content.ReadAsStringAsync().Result;

                ls = JsonConvert.DeserializeObject<Doctor>(data);

            }

            return View(ls);
        }

        [HttpPost]

        public IActionResult Edit(Doctor obj)

        {

            string data = JsonConvert.SerializeObject(obj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Doctors" + obj.DoctorId, content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }
        public IActionResult Delete(int id)

        {

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Doctors" + id).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return BadRequest();

        }

    }

}