using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLvcs.Models;

namespace SQLvcs.Controllers
{
    public class DacpacsController : Controller
    {
        private readonly SQLvcsContext _context;

        public DacpacsController(SQLvcsContext context)
        {
            _context = context;
        }

        //Добавление Dacpac
        // POST: Dacpacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DacpacId,DacpacName,DatabaseId")] Dacpac dacpac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dacpac);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Repo");
            }
            ViewData["DatabaseId"] = new SelectList(_context.Databases, "DatabaseId", "DatabaseName", dacpac.DatabaseId);
            return RedirectToAction("Manage", "Repo");
        }

        //Редактирование Dacpac
        // POST: Dacpacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int DacpacId, [Bind("DacpacId,DacpacName,DatabaseId")] Dacpac dacpac)
        {
            if (DacpacId != dacpac.DacpacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dacpac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DacpacExists(dacpac.DacpacId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Manage", "Repo");
            }
            ViewData["DatabaseId"] = new SelectList(_context.Databases, "DatabaseId", "DatabaseName", dacpac.DatabaseId);
            return View(dacpac);
        }

        //Удаление Dacpac
        // POST: Dacpacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DacpacId)
        {
            var dacpac = await _context.Dacpacs.FindAsync(DacpacId);
            _context.Dacpacs.Remove(dacpac);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", "Repo");
        }

        private bool DacpacExists(int DacpacId)
        {
            return _context.Dacpacs.Any(e => e.DacpacId == DacpacId);
        }
    }
}
