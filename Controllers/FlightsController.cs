using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFlight.Data;
using MvcFlight.Models;

namespace MvcFlight.Controllers
{
    public class FlightsController : Controller
    {
        private readonly MvcFlightContext _context;

        public FlightsController(MvcFlightContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(bool isItDeparture, string flightAirline, string searchString)
        {
            if (_context.Flight == null)
            {
                return Problem("Entity set 'MvcFlightContext.Flight'  is null.");
            }


            // Get all flight's in flights DB where flight IsDeparture is == to 'isItDeparture' param.
            // Note this SQL query is not executed until 'FlightAirlineViewModel' wraps and applies '.ToListAsync()' to it.
            IQueryable<Flight> flights = from f in _context.Flight
                        where f.IsDeparture == isItDeparture
                        select f;



            // Collection of airlines used to search by airlines.
            string[] allAirlines = (from f in flights
                        select f.Airline).Distinct().ToArray();
            
            // Collection of locations used to search by locations.
            string[] allLocations = (from f in flights
                                select f.Location).Distinct().ToArray();


            // Fixed search words for case sensitive DB data.
            string? flight_no = null;
            string? flight_airline = null;
            string? flight_location = null;
            if (!String.IsNullOrEmpty(searchString))            // If 'searchString' not empty
            {
                FlightIndexHelper helper = new FlightIndexHelper();
                (flight_no, flight_airline, flight_location) = helper.CleanSearchWords(searchString, allAirlines, allLocations);
            }

            

            //
            //      BELOW CODE APPLIES CONDITIONALS TO THE FINAL SQL SCRIPT TO TARGET ROWS THAT MATCH with 'searchWords' provided.
            //
            // If 'flight_no' not empty, then find all 'FlightNo's that match 'flight_no'.
            flights = !String.IsNullOrEmpty(flight_no) ? 
                                                    flights.Where(s => s.FlightNo!.Contains(flight_no)) : 
                                                    flights;
            
            // If 'flight_airline' not empty, then find all 'Airline's that match 'flight_airline'.
            flights = !String.IsNullOrEmpty(flight_airline) ? 
                                                    flights.Where(x => x.Airline == flight_airline) : 
                                                    flights;

            // If 'flight_location' not empty, then find all 'Location' that match 'flight_location'.
            flights = !String.IsNullOrEmpty(flight_location) ? 
                                                    flights.Where(x => x.Location == flight_location) : 
                                                    flights;
            //
            //      Above CODE APPLIES CONDITIONALS TO THE FINAL SQL SCRIPT TO TARGET ROWS THAT MATCH with 'searchWords' provided.
            //


            var flightAirlineVM = new FlightAirlineViewModel
            {
                Flights = await flights.ToListAsync()
            };

            return View(flightAirlineVM);
        }





        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightNo,Airline,DepartTime,ArriveTime,DepartLocation,ArriveLocation,Gate,IsDeparture,Location,Status")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNo,Airline,DepartTime,ArriveTime,DepartLocation,ArriveLocation,Gate,IsDeparture,Location,Status")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flight == null)
            {
                return Problem("Entity set 'MvcFlightContext.Flight'  is null.");
            }
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                _context.Flight.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
          return (_context.Flight?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
