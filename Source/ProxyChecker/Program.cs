using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ProxyChecker
{

    class GitHubProxyList
    {
        public string[] export_address { get; set; }
        public int port { get; set; }
        double response_time { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public string host { get; set; }
        public string from { get; set; }
        public string anonymity { get; set; }
    }

    class Program
    {

        private static List<GitHubProxyList> result = new List<GitHubProxyList>();

        static void Main(string[] args)
        {
            Console.WriteLine(GetData());
            List<GitHubProxyList> initialList = GetFormProxyList(GetData()).Where(x => x.anonymity == "high_anonymous").ToList();
            Console.WriteLine();
            ParalelizeProxyChecking(10, initialList);
            File.WriteAllLines(ConfigurationManager.AppSettings["PathFile"], result.Select(x => $"{x.host}:{x.port.ToString()}"));
            Console.WriteLine("Проверка прокси завершена");
            Console.ReadLine();
        }

        static string GetData() =>
           System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(ConfigurationManager.AppSettings["ProxyListAddress"]));

        static List<GitHubProxyList> GetFormProxyList(string initialProxyString)
        {
            List<GitHubProxyList> gitHubProxyList = new List<GitHubProxyList>();
            string[] initialProxyList = initialProxyString.Split('\n');
            for (int i = 0; i < initialProxyList.Length - 1; i++)
                gitHubProxyList.Add(JsonConvert.DeserializeObject<GitHubProxyList>(initialProxyList[i]));
            return gitHubProxyList;
        }

        static void ParalelizeProxyChecking(int chunkSize, List<GitHubProxyList> gitHubProxyList)
        {
            List<List<GitHubProxyList>> gitHubProxyListSplited =
                gitHubProxyList.Select((x, i) => new { Index = i, Value = x })
                               .GroupBy(x => x.Index / chunkSize)
                               .Select(x => x.Select(v => v.Value).ToList())
                               .ToList();
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < gitHubProxyListSplited.Count - 1; i++)
            {
                threadList.Add(new Thread(() => { gitHubProxyListSplited.ElementAt(i).ForEach(x => TestProxy(x, Thread.CurrentThread.ManagedThreadId)); }));
                threadList.ElementAt(i).Start();
            }
            threadList.ForEach(x => x.Join());
        }

        static void TestProxy(GitHubProxyList proxySettings, int ThreadID)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/fate0/proxylist/master/proxy.list");
                WebProxy myproxy = new WebProxy(proxySettings.host, proxySettings.port);
                myproxy.BypassProxyOnLocal = false;
                webRequest.Proxy = myproxy;
                webRequest.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                Console.WriteLine($"{ThreadID} ===========> success {proxySettings.host} {proxySettings.port}");
                lock (result) result.Add(proxySettings);
            }
            catch (Exception) { }
        }

    }
}
