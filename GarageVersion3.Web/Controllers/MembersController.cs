using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageVersion3.Core;
using GarageVersion3.Web.Data;
using AutoMapper;
using GarageVersion3.Web.Models;

namespace GarageVersion3.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly GarageVersion3Context _context;
        private readonly IMapper mapper;

        public MembersController(GarageVersion3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper; 
        }

        public async Task<IActionResult> Filter(string MemberId)
        {
            var model = string.IsNullOrEmpty(MemberId) ?
                _context.Member :
                _context.Member.Where(m => m.PersNrId!.StartsWith(MemberId));

            //model = VehicleType == null ?
            //    model :
            //    model.Where(m => m.FullName == VehicleType);

            var viewModel = mapper.Map<IEnumerable<MemberIndexViewModel>>(model);

            return View(nameof(Index), viewModel.ToList());

        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            //return _context.Member != null ? 
            //            View(await _context.Member.ToListAsync()) :
            //            Problem("Entity set 'GarageVersion3Context.Member'  is null.");

            var ViewModel = await mapper.ProjectTo<MemberIndexViewModel>(_context.Member).ToListAsync();
            return View(ViewModel); 
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            //var member = await _context.Member
            //    .FirstOrDefaultAsync(m => m.PersNrId == id);

            var member = await mapper.ProjectTo<MemberDetailsViewModel>(_context.Member)
                .FirstOrDefaultAsync(s => s.PersNrId == id); 

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = mapper.Map<Member>(viewModel);

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            //var member = await _context.Member.FindAsync(id);
            var member = await mapper.ProjectTo<MemberEditViewModel>(_context.Member)
                .FirstOrDefaultAsync(s => s.PersNrId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MemberEditViewModel viewModel)
        {
            if (id != viewModel.PersNrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _context.Member.Include(s => s.Vehicles)
                        .FirstOrDefaultAsync(S => S.PersNrId == id);

                    mapper.Map(viewModel, member);

                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(viewModel.PersNrId))
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
            return View(viewModel);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.PersNrId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'GarageVersion3Context.Member'  is null.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(string id)
        {
          return (_context.Member?.Any(e => e.PersNrId == id)).GetValueOrDefault();
        }
    }
}
