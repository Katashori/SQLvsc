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
    public class DatabasesController : Controller
    {
        private readonly SQLvcsContext _context;

        public DatabasesController(SQLvcsContext context)
        {
            _context = context;
        }

        //Добавление Database
        // POST: Databases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DatabaseId,DatabaseName,InstanceId")] Database database)
        {
            if (ModelState.IsValid)
            {
                _context.Add(database);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Repo");
            }
            ViewData["InstanceId"] = new SelectList(_context.Instances, "InstanceId", "InstanceName", database.InstanceId);
            return View(database);
        }

        //Редактирование Database
        // POST: Databases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int DatabaseId, [Bind("DatabaseId,DatabaseName,InstanceId")] Database database)
        {
            if (DatabaseId != database.DatabaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(database);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatabaseExists(database.DatabaseId))
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
            ViewData["InstanceId"] = new SelectList(_context.Instances, "InstanceId", "InstanceName", database.InstanceId);
            return View(database);
        }

        //Удаление Database
        // POST: Databases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DatabaseId)
        {
            var database = await _context.Databases.FindAsync(DatabaseId);
            _context.Databases.Remove(database);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", "Repo");
        }

        private bool DatabaseExists(int DatabaseId)
        {
            return _context.Databases.Any(e => e.DatabaseId == DatabaseId);
        }
    }
}
