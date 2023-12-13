using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis_Atualizacoes.Models;
using Sis_Atualizacoes.ViewModels;

namespace Sis_Atualizacoes.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly Sis_AtualizacoesContext _context;

        public ProjetosController(Sis_AtualizacoesContext context)
        {
            _context = context;
        }

        // GET: Projetos
        public async Task<IActionResult> Index()
        {
              return _context.Projetos != null ? 
                          View(await _context.Projetos.ToListAsync()) :
                          Problem("Entity set 'Sis_AtualizacoesContext.Projetos'  is null.");
        }

        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos.FirstOrDefaultAsync(p => p.IdProjeto == id);
            if (projeto == null)
            {
                return NotFound();
            }

            var pacotes = await _context.PacotesAtualizacoes
                            .Where(p => p.IdProj == id) 
                            .ToListAsync();

            if (pacotes == null)
            {
                return NotFound();
            }

            var projetoListViewModel = new ProjetoListViewModel
            {
                Projeto = projeto,
                Pacote = pacotes
            };

            return View(projetoListViewModel);
        }

        // GET: Projetos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projetos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProjeto,NomProjeto")] Projetos projetos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projetos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projetos);
        }

        // GET: Projetos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projetos = await _context.Projetos.FindAsync(id);
            if (projetos == null)
            {
                return NotFound();
            }
            return View(projetos);
        }

        // POST: Projetos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProjeto,NomProjeto")] Projetos projetos)
        {
            if (id != projetos.IdProjeto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projetos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetosExists(projetos.IdProjeto))
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
            return View(projetos);
        }

        // GET: Projetos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projetos = await _context.Projetos
                .FirstOrDefaultAsync(m => m.IdProjeto == id);
            if (projetos == null)
            {
                return NotFound();
            }

            return View(projetos);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'Sis_AtualizacoesContext.Projetos'  is null.");
            }
            var projetos = await _context.Projetos.FindAsync(id);
            if (projetos != null)
            {
                _context.Projetos.Remove(projetos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjetosExists(int id)
        {
          return (_context.Projetos?.Any(e => e.IdProjeto == id)).GetValueOrDefault();
        }
    }
}
