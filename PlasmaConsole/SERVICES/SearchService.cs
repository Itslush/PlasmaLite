using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasmaLite.SERVICES
{
    internal class SearchService
    {
        /*
        
        TODO:

        Add a List<IPlatformSearchService> to hold platform-specific search services.

        In the constructor, inject the PlatformServiceFactory and use it to get instances of each
        IPlatformSearchService. Add these instances to the _platformSearchServices list.

        Implement SearchAsync(string query):

        > Loop through each IPlatformSearchService in _platformSearchServices.
        > Call SearchAsync on each service.
        > Aggregate the results into a single list of Song objects.
        > Implement basic result ranking or de-duplication. (LOW PRIORITY MAY ADD AS TOGGLE)
        > Store results in SearchResults.

        Implement a mechanism to clear SearchResults before each new search.
        Implement Console Output:

        > Display search results in the console in a readable format.

        */
    }
}
