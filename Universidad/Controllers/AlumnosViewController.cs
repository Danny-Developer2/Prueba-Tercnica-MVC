using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidad.Models;
using Microsoft.EntityFrameworkCore;
using Universidad.Data;

public class AlumnosViewController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly UniversidadContext _context; // Contexto de la base de datos

    public AlumnosViewController(HttpClient httpClient, UniversidadContext context)
    {
        _httpClient = httpClient;
        _context = context;
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
                    TempData["SuccessMessage"] = "Alumno creado exitosamente.";
                    return RedirectToAction("Card"); 
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al crear el alumno.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado: {ex.Message}";
            }
        }

        return View(alumno); 
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
                    return RedirectToAction("Index"); // Redirigir a la lista de alumnos después de la actualización
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al actualizar el alumno.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado: {ex.Message}";
            }
        }

        return View(alumno); // Mostrar el formulario nuevamente con los errores de validación
    }

    public IActionResult ConfirmDelete(int id)
    {
        var alumno = _context.Alumnos.Find(id);
        if (alumno == null)
        {
            return NotFound();
        }
        return View(alumno);
    }


    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5095/api/Alumnos/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Alumno eliminado con éxito.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el alumno.";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error inesperado: {ex.Message}";
        }

        return RedirectToAction("Card");
    }

    public IActionResult Hola()
    {
        return View("HolaMundo");
    }
}
