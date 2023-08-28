using System.ComponentModel.DataAnnotations;

namespace MvcFlight.Models;

public class Flight
{
    public int Id { get; set; }
    public required string FlightNo { get; set; }
    public required string Airline { get; set; }
    //
    // Now Redundant Code Below
    //
    // DepartTime, ArriveTime, DepartLocation, ArriveLocation
    // Rows in DB has been made redundant after introducing Boolean IsDeparture & Location rows.
    //
    [Display(Name = "Depart Time")]
    public DateTime DepartTime { get; set; }
    [Display(Name = "Arrive Time")]
    public DateTime ArriveTime { get; set; }
    [Display(Name = "Depart Location")]
    public required string DepartLocation { get; set; }
    [Display(Name = "Arrive Location")]
    public required string ArriveLocation { get; set; }
    //
    //Now Redundant Code Above
    //
    public int Gate { get; set; }
    [Display(Name = "Is Departure")]
    public required Boolean IsDeparture { get; set; }
    public required string Location { get; set; }
    public string? Status { get; set; }
}

