using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace DotNetProject.Models
{
    public static class ExchangeRate
    {
        private static Dictionary<string, double> currencyRate = new Dictionary<string, double>();
        private static Dictionary<string, double> currencyIncrease = new Dictionary<string, double>();
        public static void setValues(string baseCurrency)
        {
            var json = new WebClient().DownloadString(@"http://api.fixer.io/latest?base=" + baseCurrency);
            dynamic root = JObject.Parse(json);
            var mydic = root.rates;
            string jsonOnlyWithRates = Convert.ToString(root.rates);
            currencyRate = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonOnlyWithRates);
            setIncreases(baseCurrency);
        }
        public static void setValues(string baseCurrency, DateTime date)
        {
            string myDate = date.ToString("yyyy-MM-dd");
            var json = new WebClient().DownloadString(@"http://api.fixer.io/" + myDate + "?base=" + baseCurrency);
            dynamic root = JObject.Parse(json);
            var mydic = root.rates;
            string jsonOnlyWithRates = Convert.ToString(root.rates);
            currencyRate = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonOnlyWithRates);
            setIncreases(baseCurrency, date);
        }
        public static double ratioBetweenTwoCurrencys(string baseCurrency, string secondCurrency)
        {
            var json = new WebClient().DownloadString(@"http://api.fixer.io/latest?base=" + baseCurrency);
            dynamic root = JObject.Parse(json);
            var mydic = root.rates;
            string jsonOnlyWithRates = Convert.ToString(root.rates);
            Dictionary<string, double> temp = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonOnlyWithRates);
            return temp[secondCurrency];
        }
        private static void setIncreases(string baseCurrency)
        {
            DateTime date = DateTime.Today.AddDays(-1);
            string myDate = date.ToString("yyyy-MM-dd");
            var json = new WebClient().DownloadString(@"http://api.fixer.io/" + myDate + "?base=" + baseCurrency);
            dynamic root = JObject.Parse(json);
            var mydic = root.rates;
            string jsonOnlyWithRates = Convert.ToString(root.rates);
            Dictionary<string, double> YesterdayCurrencyRate = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonOnlyWithRates);
            foreach (var item in YesterdayCurrencyRate)
            {
                currencyIncrease.Add(item.Key, currencyRate[item.Key] - item.Value);
            }
        }
        private static void setIncreases(string baseCurrency, DateTime date)
        {
            date = date.AddDays(-1);
            string myDate = date.ToString("yyyy-MM-dd");
            var json = new WebClient().DownloadString(@"http://api.fixer.io/" + myDate + "?base=" + baseCurrency);
            dynamic root = JObject.Parse(json);
            var mydic = root.rates;
            string jsonOnlyWithRates = Convert.ToString(root.rates);
            Dictionary<string, double> YesterdayCurrencyRate = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonOnlyWithRates);
            foreach (var item in YesterdayCurrencyRate)
            {
                currencyIncrease.Add(item.Key, currencyRate[item.Key] - item.Value);
            }
        }
        public static Dictionary<string, double> CurrencyRate
        {
            get { return currencyRate; }
        }
        public static List<string> CurrencyList()
        {
            List<string> temp = new List<string>();
            temp.Add("EUR");
            temp.Add("USD");
            temp.Add("GBP");
            temp.Add("RUB");
            temp.Add("PLN");

            temp.Add("AUD");
            temp.Add("BGN");
            temp.Add("BRL");
            temp.Add("CAD");
            temp.Add("CHF");
            temp.Add("CNY");
            temp.Add("CZK");
            temp.Add("HKD");
            temp.Add("HRK");
            temp.Add("HUF");
            temp.Add("IDR");
            temp.Add("ILS");
            temp.Add("INR");
            temp.Add("JPY");
            temp.Add("KRW");
            temp.Add("MXN");
            temp.Add("MYR");
            temp.Add("NOK");
            temp.Add("NZD");
            temp.Add("PHP");
            temp.Add("RON");
            temp.Add("SEK");
            temp.Add("SGD");
            temp.Add("THB");
            temp.Add("TRY");
            temp.Add("ZAR");
            return temp;
        }
    }
}