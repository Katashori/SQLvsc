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
    public class ProjectsController : Controller
    {
        private readonly SQLvcsContext _context;

        public ProjectsController(SQLvcsContext context)
        {
            _context = context;
        }

        //Добавление Project
        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,ClientId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Repo");
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName", project.ClientId);
            return View(project);
        }

        //Редактирование Project
        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProjectId, [Bind("ProjectId,ProjectName,ClientId")] Project project)
        {
            if (ProjectId != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName", project.ClientId);
            return View(project);
        }

        //Удаление Project
        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProjectId)
        {
            var project = await _context.Projects.FindAsync(ProjectId);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", "Repo");
        }

        private bool ProjectExists(int ProjectId)
        {
            return _context.Projects.Any(e => e.ProjectId == ProjectId);
        }
    }
}
