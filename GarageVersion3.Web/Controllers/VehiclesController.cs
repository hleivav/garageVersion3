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
    public class VehiclesController : Controller
    {
        private readonly GarageVersion3Context _context;
        private readonly IMapper mapper;

        public VehiclesController(GarageVersion3Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper; 
        }
        public async Task<IActionResult> Filter(string RegNrId)
        {
            var model = string.IsNullOrEmpty(RegNrId) ?
                _context.Vehicle :
                _context.Vehicle.Where(m => m.RegNrId!.StartsWith(RegNrId));

            //model = VehicleType == null ?
            //    model :
            //    model.Where(m => m.FullName == VehicleType);

            var viewModel = mapper.Map<IEnumerable<VehicleIndexViewModel>>(model);

            return View(nameof(Index), viewModel.ToList());

        }
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            //var garageVersion3Context = _context.Vehicle.Include(v => v.Member).Include(v => v.VehicleType);
            //return View(await garageVersion3Context.ToListAsync());

            var ViewModel = await mapper.ProjectTo<VehicleIndexViewModel>(_context.Vehicle).ToListAsync();
            return View(ViewModel);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            //var vehicle = await _context.Vehicle
            //    .Include(v => v.Member)
            //    .Include(v => v.VehicleType)
            //    .FirstOrDefaultAsync(m => m.RegNrId == id);

            var vehicle = await mapper.ProjectTo<VehicleDetailsViewModel>(_context.Vehicle)
                .FirstOrDefaultAsync(s => s.RegNrId == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            //ViewData["MemberId"] = new SelectList(_context.Member, "PersNrId", "FullName");
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "KindOfVehicle");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = mapper.Map<Vehicle>(viewModel);

                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["MemberId"] = new SelectList(_context.Member, "PersNrId", "FullName", vehicle.MemberId);
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "KindOfVehicle", vehicle.VehicleTypeId);
            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            //var vehicle = await _context.Vehicle.FindAsync(id);
            var vehicle = await mapper.ProjectTo<VehicleEditViewModel>(_context.Vehicle)
                .FirstOrDefaultAsync(s => s.RegNrId == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "PersNrId", "FullName", vehicle.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "KindOfVehicle", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, VehicleEditViewModel viewModel)
        {
            if (id != viewModel.RegNrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicle = await _context.Vehicle.Include(s => s.Member) //   KOLLA HÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄR
                            .FirstOrDefaultAsync(S => S.RegNrId == id);

                    mapper.Map(viewModel, vehicle);

                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(viewModel.RegNrId))
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
            //ViewData["MemberId"] = new SelectList(_context.Member, "PersNrId", "PersNrId", vehicle.MemberId);
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(viewModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.RegNrId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'GarageVersion3Context.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(string id)
        {
          return (_context.Vehicle?.Any(e => e.RegNrId == id)).GetValueOrDefault();
        }
    }
}
