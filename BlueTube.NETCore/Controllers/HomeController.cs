using BlueTube.NETCore.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BlueTube.NETCore.Controllers
{
    public class HomeController : BaseController
    {
        
        private static LanguageModel lang; //ngôn ngữ chín       
        public LanguageModel LoadNgonNgu (string l)
        {
            if (l == null)
            {
                lang = (LanguageModel)CacheManager.getFromCache(base.getkeylangCache());
            }
            else if (l != null)
            {
                base.loadLanguage_key(l);
                lang = (LanguageModel)CacheManager.getFromCache(l);
            }
            return lang;
        }
        public async Task<IActionResult> Index(string l, string p)
        {                      
            LanguageModel lang =  LoadNgonNgu(l);
            if(lang == null)
            {
                return View("Error");
            }
            Task<List<VideoModel>> _take = null;           
            if (p == null)
            {
                try
                {
                    _take = base.getVideo(lang.search);
                }
                catch
                {
                    return View("Error");
                }
            }
            else if(p!= null)
            {
                 _take = base.getVideo(p);
            }
            List<VideoModel> lst = await _take;
            ViewBag.listSong = lst;
            ViewBag.Languages = lang;
            ViewData["Title"] = "Bluetube";
            return View();
        }
        public async Task<ActionResult> search(string search, string l)
        {           
            if (search == null)
            {
                search = "music";
            }
            if (l == null)
            {
                search = "vi";
            }
            LoadNgonNgu(l);
            Task<List < VideoModel > >_task = getVideo(search);
            List<VideoModel> lstVideo = await _task;
            ViewBag.listSong = lstVideo;
            ViewBag.Languages = lang;
            return View();
        }
        public async Task<ActionResult> Watch(string id)
        {
            VideoModel.detailVideoModel detailvideo = null;
            List<VideoModel> listVideo = null; 
            string keyCachDetailVideo = "Detail" + id;
            string keyCachListVideo = "ListDetail" + id;
            try
            {
                detailvideo = (VideoModel.detailVideoModel)base.getDetailVideoFromCache(keyCachDetailVideo);
                listVideo = (List<VideoModel>)base.getListDetailVideo(keyCachListVideo);
            }
            catch
            {
                //Deatail Video
                Task<VideoModel.detailVideoModel> _take = base.getDetailVideo(id);
                detailvideo = await _take;
                base.AddCacheDetailVideo(keyCachDetailVideo, detailvideo);

                //List Detail Video
                Task<List<VideoModel>> _takelstVideo = base.getVideo(id);
                listVideo = await _takelstVideo;
                VideoModel v = null;
                v = listVideo.First();
                if (v != null)
                {
                    listVideo.Remove(v);
                }
                base.addCacheListDetailVideo(keyCachListVideo, listVideo);
            }
            if (detailvideo == null)
            {
                Task<VideoModel.detailVideoModel> _take = base.getDetailVideo(id);
                detailvideo = await _take;
                base.AddCacheDetailVideo(keyCachDetailVideo, detailvideo);
            }
            if(listVideo == null)
            {
                Task<List<VideoModel>> _takelstVideo = base.getVideo(id);
                listVideo = await _takelstVideo;
                VideoModel v = null;
                v = listVideo.First();
                if (v != null)
                {
                    listVideo.Remove(v);
                }
                base.addCacheListDetailVideo(keyCachListVideo,listVideo);
            }
            ViewBag.detailVideo = detailvideo;
            ViewBag.lstVideo = listVideo;
            LanguageModel l = lang;
            ViewBag.Languages = lang;
           
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Languages = lang;
            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Languages = lang;
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
