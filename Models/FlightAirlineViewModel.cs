using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcFlight.Models;

public class FlightAirlineViewModel
{
    public List<Flight>? Flights { get; set; }
    public SelectList? Airlines { get; set; }
    public string? FlightAirline { get; set; }
    public bool IsItDeparture { get; set; }
    public string? SearchString { get; set; }
}