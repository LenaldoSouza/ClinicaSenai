using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaCrud.Models;

namespace ClinicaCrud.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ClinicaMedicaContext _context;

        public ConsultaController(ClinicaMedicaContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var clinicaMedicaContext = _context.Consulta.Include(c => c.Medico).Include(c => c.Paciente);
            return View(await clinicaMedicaContext.ToListAsync());
        }

        // GET: Consulta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome");
            return View();
        }

        // POST: Consulta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaId,DataHora,MedicoId,PacienteId,Observacoes")] Consultum consultum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", consultum.PacienteId);
            return View(consultum);
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", consultum.PacienteId);
            return View(consultum);
        }

        // POST: Consulta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultaId,DataHora,MedicoId,PacienteId,Observacoes")] Consultum consultum)
        {
            if (id != consultum.ConsultaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultumExists(consultum.ConsultaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", consultum.PacienteId);
            return View(consultum);
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consulta == null)
            {
                return Problem("Entity set 'ClinicaMedicaContext.Consulta'  is null.");
            }
            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum != null)
            {
                _context.Consulta.Remove(consultum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultumExists(int id)
        {
          return (_context.Consulta?.Any(e => e.ConsultaId == id)).GetValueOrDefault();
        }
    }
}
