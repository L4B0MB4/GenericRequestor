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
            var inInterpreter = new YAMLInputInterpreter();
            inInterpreter.DeSerialize(yml);
            var client = new RestClient("https://covid-api.mmediagroup.fr/v1/");
            var request = new RestRequest("cases?country=Germany", DataFormat.Json);
            var response = client.Get<Dictionary<string,object>>(request);
            var ret = inInterpreter.Interpret(response.Data);
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
            var outInterpreter = new YAMLOutputInterpreter();
            outInterpreter.DeSerialize(yml);
            Dictionary<string,object> retOut =outInterpreter.Interpret(ret);
            var res = outInterpreter.Flatten(retOut);

            var retFlatten = outInterpreter.Flatten(ret);

            Console.WriteLine("FROM:");
            foreach (var keyVal in retFlatten)
            {
                Console.WriteLine(keyVal.Key + " : " + keyVal.Value.GetStringValue());
            }


            Console.WriteLine("\n\nTO:");
            foreach (var keyVal in res)
            {
                Console.WriteLine(keyVal.Key + " : " + keyVal.Value.GetStringValue());
            }

        }
    }
}
