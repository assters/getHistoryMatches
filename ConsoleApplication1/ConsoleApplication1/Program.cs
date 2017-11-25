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
            //Console.WriteLine(date1);
            //Console.WriteLine(date1970);
            //date1970 = date1970.AddSeconds(1511540369);
            //Console.WriteLine(date1970);

            /* DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(txtJSON));
             string fileContent = File.ReadAllText("F:\\1.json");
             txtJSON jsonn = (txtJSON)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(fileContent)));
             Console.WriteLine();
             */
            DateTime startProgramDate = DateTime.Now;
            bool flag = true;

            // выполнить один запрос для получения стартового ID матча
            //GETrequest
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=5FFECD6C642F60635924C5FA6E81FBBD");
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            int length = Out.Length;
            //
            if (length > 99)
            {
                //deserialize
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(txtJSON));
                txtJSON jsonn = (txtJSON)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Out)));
            }
            else
            {
                flag = false;
                Console.WriteLine("Error, request error, size < 99");
            }
                        
            // открыть файл для записи в него Id матчей
            BinaryWriter binaryWriter = new BinaryWriter(File.Open("F:\\12.bin", FileMode.OpenOrCreate));
            while (flag)//1 day 
            {
                //GETrequest
                req = System.Net.WebRequest.Create("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=5FFECD6C642F60635924C5FA6E81FBBD");
                resp = req.GetResponse();
                stream = resp.GetResponseStream();
                sr = new System.IO.StreamReader(stream);
                Out = sr.ReadToEnd();
                sr.Close();
                length = Out.Length;
                //
                if (length > 99)
                {
                    //deserialize
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(txtJSON));
                    txtJSON jsonn = (txtJSON)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Out)));
                    //Match[] sassas = new Match[3];
                    //for (int i = 0; i < 3; i++)
                    //    sassas[i] = jsonn.result.matches[i];
                    //Console.WriteLine();


                    //     сохранили все id матчей кроме первого, так как первый был сохранен при прошлом запросе
                    for (int counterMatch = 1; counterMatch < jsonn.result.num_results; counterMatch++)
                    {
                        binaryWriter.Write(jsonn.result.matches[counterMatch].match_id);
                    }

                    // перевод даты последнего в запросе матча
                   // DateTime date1 = new DateTime(jsonn.result.matches[jsonn.result.num_results - 1].start_time);
                    DateTime lastMatchDate = new DateTime(1970, 1, 1, 0, 0, 1);
                    lastMatchDate = lastMatchDate.AddSeconds(jsonn.result.matches[jsonn.result.num_results - 1].start_time);
                    //Console.WriteLine(lastMatchDate);
                    //Console.WriteLine(lastMatchDate.Subtract(startProgramDate));
                    
                    //Console.WriteLine(lastMatchDate.Hour);
                    //Console.WriteLine(lastMatchDate);
                    //задать промежуток для выполнения запросов
                    System.TimeSpan timeSpan = new TimeSpan(-6,0,0); //разница -  -6 часов, 0 минут, 0 секунд

                    int lope = TimeSpan.Compare(timeSpan, lastMatchDate.Subtract(startProgramDate));
                    //Console.WriteLine(lastMatchDate);
                    if (lope > 0)       // если вышли за -6 часов
                    {
                        flag = false; //останавливаемся
                    }
                    //start_at_match_id = lastMatchId;  // меняем 
                }
                else
                {
                    flag = false;
                    Console.WriteLine("Error, request error, size < 99");
                }
            }
        }
    }
}
