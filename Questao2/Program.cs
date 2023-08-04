using Newtonsoft.Json;
using RestSharp;
using System.Dynamic;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int totalPaginas = 0;
        using (var clientRest = CriaRestClient(string.Format("year={0}&team1={1}", year, team)))
        {
            var request = new RestRequest() { RequestFormat = DataFormat.Json, Method = Method.Get };
            var response = clientRest.Execute(request);
            if (response != null)
            {
                dynamic retorno = JsonConvert.DeserializeObject<ExpandoObject>(response.Content);
                totalPaginas = ((IDictionary<string, object>)retorno).ContainsKey("total_pages") ? ((int)retorno.total_pages) : 0;
            }
        }

        var page = 1;
        for (int i = 0; i < totalPaginas; i++)
        {
            using (var clientRest = CriaRestClient(string.Format("year={0}&team1={1}&page={2}", year, team, page)))
            {
                var request = new RestRequest() { RequestFormat = DataFormat.Json, Method = Method.Get };
                var response = clientRest.Execute(request);
                if (response != null)
                {
                    dynamic retorno = JsonConvert.DeserializeObject<ExpandoObject>(response.Content);
                    foreach (var iten in retorno.data)
                    {
                        totalGoals += Convert.ToInt32(iten.team1goals);
                    }
                }
            }

            using (var clientRest = CriaRestClient(string.Format("year={0}&team2={1}&page={2}", year, team, page)))
            {
                var request = new RestRequest() { RequestFormat = DataFormat.Json, Method = Method.Get };
                var response = clientRest.Execute(request);
                if (response != null)
                {
                    dynamic retorno = JsonConvert.DeserializeObject<ExpandoObject>(response.Content);
                    foreach (var iten in retorno.data)
                    {
                        totalGoals += Convert.ToInt32(iten.team2goals);
                    }
                }
            }
            page++;
        }            
        return totalGoals;
    }

    protected static RestClient CriaRestClient(string chave)
    {
        var client = new RestClient(string.Format(@"https://jsonmock.hackerrank.com/api/football_matches?{0}", chave));
        return client;
    }

}