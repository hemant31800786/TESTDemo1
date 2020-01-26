using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IFSPRojectTest.Persitance.model
{
    public class Common
    {
        public const string CandidateUserId = "CandidateUserId";
        public const string CandidateUserName = "CandidateUserName";



        public static List<Candidate> GetCandidateListFromWebServices()
        {
            List<JshonDataView> JshonDataView = new List<JshonDataView>();
            List<Candidate> objCandidate = new List<Candidate>();
            Task taskGetJsonData = null;
            try
            {
                taskGetJsonData = Task.Run(async () =>
                {
                    string url = "https://v1.ifs.aero//candidates";

                    var nextUrl = url;

                    using (var httpClient = new System.Net.Http.HttpClient())
                    {
                        System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync(nextUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();

                            var pageResponse = JsonConvert.DeserializeObject<List<Candidate>>(json).ToArray();

                            objCandidate.AddRange(pageResponse);
                        }
                    }
                });
                taskGetJsonData.Wait();
                return objCandidate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                taskGetJsonData.Dispose();
            }
        }

    }
}