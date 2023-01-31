using Newtonsoft.Json;
using Questao2;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

public class Program
{
    private static readonly string _URL = "https://jsonmock.hackerrank.com/api/football_matches";

    private static int _PageTotal = 1;

    private static int QtdJogos = 2;

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

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
        List<CampeonatoFutebol> TodosJogos = new List<CampeonatoFutebol>();
        int totalGols = 0;
        int totalGoalsTime1 = 0;
        int totalGoalsTime2 = 0;

        using (var client = Service())
        {
            for (int t = 1; t <= QtdJogos; t++)
            {
                for (int i = 1; i <= _PageTotal; i++)
                {

                    HttpResponseMessage response = client.GetAsync($"?year={year}&team{t}={team}&page={i}")
                            .GetAwaiter()
                            .GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync()
                            .GetAwaiter()
                            .GetResult();

                        var campeonatos = JsonConvert.DeserializeObject<CampeonatoFutebol>(data);

                        if (campeonatos != null)
                        {

                            if (_PageTotal != campeonatos.total_pages)
                                _PageTotal = campeonatos.total_pages;

                            TodosJogos.Add(campeonatos);
                        }
                    }
                }
                _PageTotal = 1;
            }

            totalGoalsTime1 = TodosJogos.Select(e => e.data.Where(i => i.team1 == team).Sum(o => o.team1goals)).Sum();
            totalGoalsTime2 = TodosJogos.Select(e => e.data.Where(i => i.team2 == team).Sum(o => o.team2goals)).Sum();
        }
        return totalGols = (totalGoalsTime1 + totalGoalsTime2);
    }

    private static HttpClient Service()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_URL);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return client;

    }

  

}