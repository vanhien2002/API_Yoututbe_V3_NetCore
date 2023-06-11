using BlueTube.NETCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;


using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Linq;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Policy;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using static BlueTube.NETCore.Models.VideoModel;

namespace BlueTube.NETCore.Controllers
{
    public class BaseController : Controller
    {
        //private readonly IHttpClientFactory _clientFactory;
        private static readonly HttpClient client = new HttpClient();
        private string keylanguagecookie = "keylang";
        private string keylangCache = "en"; //'vi" hoặc "en"
        API api = new API();
        public string getkeylangCache()
        {
            return keylangCache;
        }

        public string getkeylanguagecookie()
        {
                return keylanguagecookie;
        }
       public void getkeylanguagecookie(string k)
        {
            this.keylanguagecookie = k;
        }
        public void loadLanguage_key(string key)
        {
            //key "vi hoặc en"
            if (CacheManager.getFromCache(key)== null)
            {
                //chưa lưu vào cache 
                CacheManager.addToCache(key,GetLanguageJson(key));
            }
        }
        private void loadLanguage()
        {
            string l = getCookieLanguage(keylanguagecookie);
            if (l == null)
            {
                l = getLanguageBorower();
                addCookie(keylanguagecookie, l);
                keylangCache = l;
            }
            else
            {
                keylangCache = l;
            }
            if (CacheManager.getFromCache(keylangCache) == null)
            {
                //chưa lưu vào cache
                CacheManager.addToCache(l, GetLanguageJson(keylangCache));
            }
        }         
        private string getLanguageBorower()
        {
            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            return browserLang.Substring(0,2);
        }
        private void addCookie(string key, string values)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            option.IsEssential = true;
            option.Path = "/";
            HttpContext.Response.Cookies.Append(key, values,option);
        }
        private void deleteCookie(string key)
        {
            var Cookieoption1 = new CookieOptions();
            Cookieoption1.Path = HttpContext.Request.PathBase;

            foreach (var cookieKey in Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(cookieKey, Cookieoption1);
            }
        }
        private string getCookieLanguage(string key)
        {
            if (Request.Cookies[key] != null)
            {
                var value = Request.Cookies[key].ToString();
                return value;
            }
            else
            {
                return "en";
            }
        }
        public LanguageModel GetLanguageJson(string keyLanguage)
        {
            using (StreamReader r = new StreamReader("myData/Language.json"))
            {
                string json = r.ReadToEnd();
                lstLanguages items = JsonConvert.DeserializeObject<lstLanguages>(json);
                foreach (LanguageModel item in items.Languages)
                {
                    string _id = item.id;
                    if (item.id == keyLanguage)
                    {
                        return item;
                    }
                }
                return null;
            }
            return null;

        }
        public async Task<VideoModel.detailVideoModel> getDetailVideo(string idVideo)
        {
            VideoModel.detailVideoModel videoDetail = new VideoModel.detailVideoModel();
            try
            {
             
                string url =api.API_WatchVideo + idVideo;
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Object data = await response.Content.ReadAsStringAsync();
                JObject jObData = JObject.Parse(data.ToString());

                VideoModel.detailVideoModel detailVideo = new VideoModel.detailVideoModel();
                JObject items = JObject.Parse(jObData["items"][0].ToString());
                //
                detailVideo.kind = items["kind"].ToString();
                detailVideo.etag = items["etag"].ToString();
                detailVideo.id = items["id"].ToString();
                VideoModel.setSnippet _snippet = new VideoModel.setSnippet();
                _snippet.publishedAt = items["snippet"]["publishedAt"].ToString();
                _snippet.channelId = items["snippet"]["channelId"].ToString();
                _snippet.title = items["snippet"]["title"].ToString();
                _snippet.description = items["snippet"]["description"].ToString();
                setThumbnails _thumbnils = new setThumbnails();
                VideoModel.setmedium _medium = new VideoModel.setmedium();
                _medium.url = items["snippet"]["thumbnails"]["medium"]["url"].ToString();
                _medium.width = items["snippet"]["thumbnails"]["medium"]["width"].ToString();
                _medium.height = items["snippet"]["thumbnails"]["medium"]["height"].ToString();
                _thumbnils.medium = _medium;
                _snippet.thumbnails = _thumbnils;
                detailVideo.snippet = _snippet;
                //detailVideo.channelTitle = items["channelTitle"].ToString();
                List<string> listTags = new List<string>();
                try
                {
                    foreach (var item in items["snippet"]["tags"])
                    {
                        listTags.Add(item.ToString());
                    }
                }
                catch {
                    listTags = null;
                      }
                detailVideo.tags = listTags;
                detailVideo.categoryId = items["snippet"]["categoryId"].ToString();
                detailVideo.liveBroadcastContent = items["snippet"]["liveBroadcastContent"].ToString();
                //
                return detailVideo;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
        public async Task<List<VideoModel>> getVideo(string key)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(api.API_Search+ key);
                response.EnsureSuccessStatusCode();

                Object data = await response.Content.ReadAsStringAsync();
                JObject jObData = JObject.Parse(data.ToString());
                List<VideoModel> list = new List<VideoModel>();
                foreach (var item in jObData["items"])
                {
                    VideoModel video = new VideoModel();
                    string kind = item["kind"].ToString();
                    string etag = item["kind"].ToString();
                    VideoModel.setid _id = new VideoModel.setid();
                    _id.videoID = item["id"]["videoId"].ToString();
                    _id.kind = item["id"]["kind"].ToString();
                    setid id = _id;
                    VideoModel.setSnippet _snippet = new VideoModel.setSnippet();
                    _snippet.publishedAt = item["snippet"]["publishedAt"].ToString();
                    _snippet.channelId = item["snippet"]["channelId"].ToString();
                    _snippet.title = item["snippet"]["title"].ToString();
                    _snippet.description = item["snippet"]["description"].ToString();
                    setThumbnails _thumbnils = new setThumbnails();
                    VideoModel.setmedium _medium = new VideoModel.setmedium();
                    _medium.url = item["snippet"]["thumbnails"]["medium"]["url"].ToString();
                    _medium.width = item["snippet"]["thumbnails"]["medium"]["width"].ToString();
                    _medium.height = item["snippet"]["thumbnails"]["medium"]["height"].ToString();
                    _thumbnils.medium = _medium;
                    _snippet.thumbnails = _thumbnils;
                    video.kind = kind;
                    video.etag = etag;
                    video.id = id;
                    video.snippet = _snippet;
                    list.Add(video);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public void addCacheListDetailVideo(string idvDetailVideo, List<VideoModel> lstDetailVideo)
        {
            CacheManager.addToCache(idvDetailVideo, lstDetailVideo);
        }
        public object  getListDetailVideo(string idDetailVideo)
        {
            return CacheManager.getFromCache(idDetailVideo);
        }
        public void AddCacheDetailVideo(string keyVideo,VideoModel.detailVideoModel DetailVideo)
        {
            CacheManager.addToCache(keyVideo, DetailVideo);
        }
        public object getDetailVideoFromCache(string keyVideo)
        {
           return  CacheManager.getFromCache(keyVideo);
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            loadLanguage();
            LanguageModel l = (LanguageModel)CacheManager.getFromCache("en");
            // Do something...
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
