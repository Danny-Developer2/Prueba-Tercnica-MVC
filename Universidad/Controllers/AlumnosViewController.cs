using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidad.Models;

public class AlumnosViewController : Controller
{
    private readonly HttpClient _httpClient;

    public AlumnosViewController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Card()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5095/api/Alumnos");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var alumnos = JsonSerializer.Deserialize<IEnumerable<Alumno>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(alumnos); // Enviar los datos a la vista
            }
            else
            {
                // Manejar error de solicitud
                ModelState.AddModelError("", "Error al recuperar la lista de alumnos");
            }
        }
        catch (Exception ex)
        {
            // Manejar excepciones generales
            ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
        }

        return View(new List<Alumno>()); // En caso de error, mostrar una lista vacía
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Alumno());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Alumno alumno)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5095/api/Alumnos", alumno);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Card"); // Redirigir a la vista de alumnos después de la creación
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear el alumno");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
            }
        }

        return View(alumno); // Mostrar el formulario nuevamente con los errores de validación
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://localhost:5095/api/Alumnos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var alumno = JsonSerializer.Deserialize<Alumno>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(alumno); // Enviar los datos a la vista de edición
            }
            else
            {
                ModelState.AddModelError("", "Alumno no encontrado");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
        }

        return RedirectToAction("Card");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Alumno alumno)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5095/api/Alumnos/{alumno.Id}", alumno);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Card"); // Redirigir a la vista de alumnos después de la edición
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar el alumno");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
            }
        }

        return View(alumno); // Mostrar el formulario nuevamente con los errores de validación
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5095/api/Alumnos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Card");
            }
            else
            {
                ModelState.AddModelError("", "Error al eliminar el alumno");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
        }

        return RedirectToAction("Card");
    }
}
