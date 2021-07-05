using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRequestor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make a request to a webservice 
            var client = new RestClient("https://covid-api.mmediagroup.fr/v1/");
            var request = new RestRequest("cases?country=Germany", DataFormat.Json);
            var response = client.Get<Dictionary<string, object>>(request);

            // Define the schema of the incoming data in yml for now
            var yml = @"
            All: 
                confirmed: number
                recovered: number
                country: string
            Baden-Wurttemberg: 
                lat: number
                long: number
                deaths: string
            
            ";

            // serialize the yaml schema
            var inInterpreter = new YAMLInputInterpreter();
            inInterpreter.DeSerialize(yml);

            // interpret the value into a Dictionary of ITypeables (Types-folder)
            var webServiceResponseAsTypeable = inInterpreter.Interpret(response.Data);

            // Define the schema you want your data mapped into
            yml = @"
            Alle:
                bestätigt: All.confirmed
                genesen: All.recovered
                land: 
                    name: All.country
                    bestätigtInObj: All.confirmed
            BW:
                lati: Baden-Wurttemberg.lat
                looong: Baden-Wurttemberg.long
                tode: Baden-Wurttemberg.deaths

            ";
            // Serialize that as well
            var outInterpreter = new YAMLOutputInterpreter();
            outInterpreter.DeSerialize(yml);

            // Interpret that as well which creates a Dictionary<string,object> that contains dictionaries and Typables (wip)
            var mappedWebServiceResp = outInterpreter.Interpret(webServiceResponseAsTypeable);

            //flatten it to make it printable
            var flattendOriginalWebServiceResp = outInterpreter.Flatten(webServiceResponseAsTypeable);
            var flattendMappedWebServiceResp = outInterpreter.Flatten(mappedWebServiceResp);

            Console.WriteLine("FROM:");
            foreach (var keyVal in flattendOriginalWebServiceResp)
            {
                Console.WriteLine(keyVal.Key + " : " + keyVal.Value.GetStringValue());
            }


            Console.WriteLine("\n\nTO:");
            foreach (var keyVal in flattendMappedWebServiceResp)
            {
                Console.WriteLine(keyVal.Key + " : " + keyVal.Value.GetStringValue());
            }
            Console.WriteLine("\n\n Press enter to exit:");
            Console.ReadLine();
        }
    }
}
