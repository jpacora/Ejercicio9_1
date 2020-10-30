using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ejercicio9_1.Models;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ejercicio9_1.Controllers
{
    public class Ejercicio9_1Controller : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public FileContentResult GenerarExcel()
        {
            List<Ejercicio9_1Model> notas = new List<Ejercicio9_1Model>()
            {
                new Ejercicio9_1Model{ NombreCompleto = "Roby Zuñiga", Curso = "Web II", Nota = 15.60},
                new Ejercicio9_1Model{ NombreCompleto = "Jorge Pacora", Curso = "Seguridad Informática", Nota = 18.40}
            };

            string csv = ConvertirCSV(notas);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Notas.csv");

        }

        private string ConvertirCSV<T>(IEnumerable<T> notas)
        {
            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();
            sList.Append(string.Join(",", props.Select(p => p.Name)));
            sList.Append(Environment.NewLine);

            foreach (var element in notas)
            {
                sList.Append(string.Join(",", props.Select(p => p.GetValue(element, null))));
                sList.Append(Environment.NewLine);
            }

            return sList.ToString();
        }
    }
}
