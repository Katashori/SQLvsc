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
    public class InstancesController : Controller
    {
        private readonly SQLvcsContext _context;

        public InstancesController(SQLvcsContext context)
        {
            _context = context;
        }

        //Добавление Instance
        // POST: Instances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstanceId,InstanceName,ProjectId")] Instance instance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Repo");
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", instance.ProjectId);
            return View(instance);
        }

        //Редактирование Instance
        // POST: Instances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int InstanceId, [Bind("InstanceId,InstanceName,ProjectId")] Instance instance)
        {
            if (InstanceId != instance.InstanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstanceExists(instance.InstanceId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", instance.ProjectId);
            return View(instance);
        }

        //Удаление Instance
        // POST: Instances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int InstanceId)
        {
            var instance = await _context.Instances.FindAsync(InstanceId);
            _context.Instances.Remove(instance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", "Repo");
        }

        private bool InstanceExists(int InstanceId)
        {
            return _context.Instances.Any(e => e.InstanceId == InstanceId);
        }
    }
}
