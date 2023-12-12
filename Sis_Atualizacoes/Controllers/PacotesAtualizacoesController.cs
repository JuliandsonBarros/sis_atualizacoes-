using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis_Atualizacoes.Models;

namespace Sis_Atualizacoes.Controllers
{
    public class PacotesAtualizacoesController : Controller
    {
        private readonly Sis_AtualizacoesContext _context;

        public PacotesAtualizacoesController(Sis_AtualizacoesContext context)
        {
            _context = context;
        }

        // GET: PacotesAtualizacoes
        public async Task<IActionResult> Index()
        {
            var sis_AtualizacoesContext = _context.PacotesAtualizacoes.Include(p => p.IdProjNavigation);
            return View(await sis_AtualizacoesContext.ToListAsync());
        }

        // GET: PacotesAtualizacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacotesAtualizacoes = await _context.PacotesAtualizacoes
                .Include(p => p.IdProjNavigation)
                .FirstOrDefaultAsync(m => m.IdPacote == id);
            if (pacotesAtualizacoes == null)
            {
                return NotFound();
            }

            return View(pacotesAtualizacoes);
        }

        // GET: PacotesAtualizacoes/Create
        public IActionResult Create()
        {
            ViewData["IdProj"] = new SelectList(_context.Projetos, "IdProjeto", "IdProjeto");
            return View();
        }

        // POST: PacotesAtualizacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPacote,IdProj,NumVersao,RegistroAlteracoes,DtLancamento")] PacotesAtualizacoes pacotesAtualizacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacotesAtualizacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProj"] = new SelectList(_context.Projetos, "IdProjeto", "IdProjeto", pacotesAtualizacoes.IdProj);
            return View(pacotesAtualizacoes);
        }

        // GET: PacotesAtualizacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacotesAtualizacoes = await _context.PacotesAtualizacoes.FindAsync(id);
            if (pacotesAtualizacoes == null)
            {
                return NotFound();
            }
            ViewData["IdProj"] = new SelectList(_context.Projetos, "IdProjeto", "IdProjeto", pacotesAtualizacoes.IdProj);
            return View(pacotesAtualizacoes);
        }

        // POST: PacotesAtualizacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPacote,IdProj,NumVersao,RegistroAlteracoes,DtLancamento")] PacotesAtualizacoes pacotesAtualizacoes)
        {
            if (id != pacotesAtualizacoes.IdPacote)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacotesAtualizacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacotesAtualizacoesExists(pacotesAtualizacoes.IdPacote))
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
            ViewData["IdProj"] = new SelectList(_context.Projetos, "IdProjeto", "IdProjeto", pacotesAtualizacoes.IdProj);
            return View(pacotesAtualizacoes);
        }

        // GET: PacotesAtualizacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacotesAtualizacoes = await _context.PacotesAtualizacoes
                .Include(p => p.IdProjNavigation)
                .FirstOrDefaultAsync(m => m.IdPacote == id);
            if (pacotesAtualizacoes == null)
            {
                return NotFound();
            }

            return View(pacotesAtualizacoes);
        }

        // POST: PacotesAtualizacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PacotesAtualizacoes == null)
            {
                return Problem("Entity set 'Sis_AtualizacoesContext.PacotesAtualizacoes'  is null.");
            }
            var pacotesAtualizacoes = await _context.PacotesAtualizacoes.FindAsync(id);
            if (pacotesAtualizacoes != null)
            {
                _context.PacotesAtualizacoes.Remove(pacotesAtualizacoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacotesAtualizacoesExists(int id)
        {
          return (_context.PacotesAtualizacoes?.Any(e => e.IdPacote == id)).GetValueOrDefault();
        }
    }
}
