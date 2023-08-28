using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcFlight.Data;
using System;
using System.Linq;

namespace MvcFlight.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcFlightContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcFlightContext>>()))
        {
            // Look for any Flights.
            if (context.Flight.Any())
            {
                return;   // DB has been seeded
            }
            context.Flight.AddRange(
                new Flight
                {
                    FlightNo = "NZ265",
                    Airline = "New Zealand",
                    DepartTime = DateTime.Parse("2023-08-21 09:47 PM"),
                    ArriveTime = DateTime.Parse("2023-08-21 11:02 PM"),
                    DepartLocation = "Hamilton",
                    ArriveLocation = "Christchurch",
                    Gate = 3,
                    IsDeparture = true,
                    Location = "Christchurch",
                    Status = "CANCELLED"
                },new Flight
                {
                    FlightNo = "AUS789",
                    Airline = "Australia",
                    DepartTime = DateTime.Parse("2023-08-21 10:15 PM"),
                    ArriveTime = DateTime.Parse("2023-08-21 11:50 PM"),
                    DepartLocation = "Auckland",
                    ArriveLocation = "Christchurch",
                    Gate = 17,
                    IsDeparture = false,
                    Location = "Auckland",
                    Status = "DELAYED"
                },new Flight
                {
                    FlightNo = "NZ111",
                    Airline = "New Zealand",
                    DepartTime = DateTime.Parse("2023-08-21 07:35 PM"),
                    ArriveTime = DateTime.Parse("2023-08-21 8:11 PM"),
                    DepartLocation = "Queenstown",
                    ArriveLocation = "Christchurch",
                    Gate = 16,
                    IsDeparture = false,
                    Location = "Queenstown",
                    Status = ""
                },
                new Flight
                {
                    FlightNo = "NZ555",
                    Airline = "New Zealand",
                    DepartTime = DateTime.Parse("2023-08-21 11:00 PM"),
                    ArriveTime = DateTime.Parse("2023-08-21 12:23 PM"),
                    DepartLocation = "Nelson",
                    ArriveLocation = "Christchurch",
                    Gate = 5,
                    IsDeparture = false,
                    Location = "Nelson",
                    Status = ""
                },new Flight
                {
                    FlightNo = "AUS789",
                    Airline = "Australia",
                    DepartTime = DateTime.Parse("2023-08-21 9:15 PM"),
                    ArriveTime = DateTime.Parse("2023-08-21 11:04 PM"),
                    DepartLocation = "Christchurch",
                    ArriveLocation = "Auckland",
                    Gate = 2,
                    IsDeparture = true,
                    Location = "Auckland",
                    Status = "DELAYED"
                },new Flight
                {
                    FlightNo = "NZ111",
                    Airline = "New Zealand",
                    DepartTime = DateTime.Parse("2023-08-21 10:35 AM"),
                    ArriveTime = DateTime.Parse("2023-08-21 11:12 PM"),
                    DepartLocation = "Christchurch",
                    ArriveLocation = "Queenstown",
                    Gate = 13,
                    IsDeparture = true,
                    Location = "Queenstown",
                    Status = ""
                }
            );
            context.SaveChanges();
        }
    }
}