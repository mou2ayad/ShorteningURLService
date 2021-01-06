using App.Component.ShorteningURL.Contracts;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using App.Component.Common.Models;
using App.Component.Utilities.ErrorHandling;

namespace App.Component.ShorteningURL.Services
{
    public class UrlFormatService : IUrlFormatService
    {
        private readonly ShorteningUrlConfigs config;
        private readonly Regex ProtocolRegexVerification;
        private readonly Regex UrlRgx;
        public UrlFormatService(IOptions<ShorteningUrlConfigs> options)
        {
            ProtocolRegexVerification = new Regex("^(?:[a-z]+:)?//");
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            UrlRgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            config = options.Value;
        }
        private bool IsValidURL(string URL)
        {
            if (string.IsNullOrEmpty(URL))
                return false;
            return UrlRgx.IsMatch(URL);
        }
        public string FormateURL(string url)
        {
            
            url =HttpUtility.UrlDecode(url);
            if(!IsValidURL(url))
                throw new InvalidRequestException("invalid Url");

            // check if the url contains protocol, if not, we will append the default protocol to the url
            if (!ProtocolRegexVerification.IsMatch(url))
                url = $"{config.DefaultSchema}://{url}";

            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))               
                throw new InvalidRequestException("invalid Url");
                       
            string sortedQuerystring = string.Empty;

            /*
             To check if the url contains parameters(querysting), we will order the parameters by the key 
             this is useful when we get the same url many time with same parameters but in a different order
            */
            if (!string.IsNullOrEmpty(uri.Query))
            {
                var queryStrings = HttpUtility.ParseQueryString(uri.Query);
                var sortedDict = new SortedDictionary<string, string>(queryStrings.AllKeys.ToDictionary(k => k, k => queryStrings[k]));
                if(sortedDict.Count>0)
                    sortedQuerystring = $"?{string.Join("&", sortedDict.Select(e => string.Format("{0}={1}", e.Key, e.Value)))}";
            }           
            UriBuilder uriBuilder = new UriBuilder($"{uri.Scheme}://{uri.Authority}{uri.AbsolutePath}{sortedQuerystring}");
            return uriBuilder.Uri.AbsoluteUri;
        }
        
    }
}
