using System.ComponentModel.DataAnnotations;

namespace MvcFlight.Models;

/// <summary>
/// Class <c>FlightIndexHelper</c> 
/// </summary>
public class FlightIndexHelper
{
    /// <summary>
    /// Method <c>CleanSearchWords</c> Main method to correct every search word before using on case sensitive DB data.
    /// </summary>
    /// <param name="searchString">User inputed search string.</param>
    /// <param name="allAirlines">Array of all distinct airlines in DB.</param>
    /// <param name="allLocations">Array of all distinct locations in DB.</param>
    public (string? flight_no, string? flight_airline, string? flight_location) CleanSearchWords(string searchString, string[] allAirlines, string[] allLocations)
    {
        // Break 'searchString' into single words. e.g 'searchString' == "NZ265 Hamilton" --> 'words' == [NZ265, Hamilton].
        string[] searchWords = searchString.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);       // Split by ' ' character & remove any white space added to 'searchWords'.
        
        // Variables to return.
        string? flight_no = null;
        string? flight_airline = null;
        string? flight_location = null;

        
        bool airlineMatch = false;
        bool locactionMatch = false;

        // Loop through each word inputed in search.
        string word;
        foreach(string sWord in searchWords) {
            word = ConformString(sWord);                    // Fix lowercasing & uppercasing in search word.


            // Do a search in DB collection on search word that match with a 'Flight.Airline'.
            if (!airlineMatch) {
                (airlineMatch, flight_airline) = MatchAirline(word, allAirlines);

                if (airlineMatch) { continue; }
            }
            

            // Do a search in DB collection on search word that match with a 'Flight.DepartLocation'.
            if (!locactionMatch) {
                (locactionMatch, flight_location) = MatchLocation(word, allLocations);

                if (locactionMatch) { continue; }
            }
            

            // If 'airlineMatch' & 'locactionMatch' fail(are false) do this.
            flight_no = word.ToUpper();
        }
        return(flight_no, flight_airline, flight_location);
    }


    /// <summary>
    /// Method <c>ConformString</c> Capitalize first character & lowercase anything that follows.
    /// </summary>
    /// <param name="word">Inputed search word</param>
    private string ConformString(string word)
    {
        if (word.Length <= 1) {                // In case string length is 1.
            return word.ToUpper();
        }

        word = word.ToLower();
        word = char.ToUpper(word[0]) + word.Substring(1);        // Make first letter uppercase
        return word;
    }



    /// <summary>
    /// Method <c>MatchAirline</c> See if inputed search word matches with an airline in 'allAirlines'
    /// </summary>
    /// <param name="word">Inputed search word</param>
    /// <param name="allAirlines">Collection of airlines in DB</param>
    private (bool matched, string airline) MatchAirline(string word, string[] allAirlines) {
        foreach (string airline in allAirlines)
        {
            if (airline.Contains(word))                 // If word matchs with a 'Flight.Airline'.
            {
                return (true, airline);                 // return 'airline' instead of 'word'. Example circumstance. 'word' == "New", 'airline' == "New Zealand".
            };
        }
        
        return (false, "");
    }


    /// <summary>
    /// Method <c>MatchLocation</c> See if inputed search word matches with an location in 'allLocations'
    /// </summary>
    /// <param name="word">Inputed search word</param>
    /// <param name="allLocations">Collection of locations in DB</param>
    private (bool matched, string location) MatchLocation(string word, string[] allLocations) {
        foreach (string location in allLocations)
        {
            if (location.Contains(word))                    // If word matchs with a 'Flight.DepartLocation'.
            {
                return (true, location);                    // return 'location' instead of 'word'. Example circumstance. 'word' == "A", 'location' == "Auckland".
            };
        }

        return (false, "");
    }
    
}  

