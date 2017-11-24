using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        [DataContract]
        public class txtJSON
        {
            [DataMember(Name = "result")]
            public Result result { get; set; }
        }
        [DataContract]
        public class Result
        {
            [DataMember(Name = "status")]
            public long status { get; set; }
            [DataMember(Name = "num_results")]
            public long num_results { get; set; }
            [DataMember(Name = "total_results")]
            public long total_results { get; set; }
            [DataMember(Name = "results_remaining")]
            public long results_remaining { get; set; }
            [DataMember(Name = "matches")]
            public Match[] matches { get; set; }
        }
        [DataContract]
        public class Match
        {
            [DataMember(Name = "match_id")]
            public long match_id { get; set; }
            [DataMember(Name = "match_seq_num")]
            public long match_seq_num { get; set; }
            [DataMember(Name = "start_time")]
            public long start_time { get; set; }
            [DataMember(Name = "lobby_type")]
            public long lobby_type { get; set; }
            [DataMember(Name = "radiant_team_id")]
            public long radiant_team_id { get; set; }
            [DataMember(Name = "dire_team_id")]
            public long dire_team_id { get; set; }
            [DataMember(Name = "players")]
            public Player[] players { get; set; }
        }
        [DataContract]
        public class Player
        {
            [DataMember(Name = "account_id")]
            public long account_id { get; set; }
            [DataMember(Name = "player_slot")]
            public long player_slot { get; set; }
            [DataMember(Name = "hero_id")]
            public long hero_id { get; set; }
        }
        static void Main(string[] args)
        {
            /*
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=5FFECD6C642F60635924C5FA6E81FBBD");
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            */

            //DateTime  date1 = new DateTime (1511540369);
            //DateTime  date1970 = new DateTime (1970, 1, 1, 0, 0, 1);
            //console.writeline(date1);
            //console.writeline(date1970);
            //date1970 = date1970.addseconds(1511540369);
            //console.writeline(date1970);

            /* DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(txtJSON));
             string fileContent = File.ReadAllText("F:\\1.json");
             txtJSON jsonn = (txtJSON)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(fileContent)));
             Console.WriteLine();
             */
            DateTime dateCur= DateTime.Now;
            bool flag = true;
            while (flag)//1 day
            {
                GETrequest;
                if (respons != error)
                {
                    delete first match;
                    saveAllMatchId;//parse json
                    if ((lastMatchIdStartTime - curData) >= 1 day)
			flag = false;
                    start_at_match_id = lastMatchId;
                }
                else
                {
                    flag = false;
                    write(error);
                }
            }
        }
    }
}
