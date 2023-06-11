using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Text.Json.Nodes;

namespace BlueTube.NETCore.Models
{
    public  class API
    {
        private static string KeyAPI1 = "AIzaSyDMGVARVsY1EKMId2P20ruutKUswv1x9KQ";
        private static string KeyAPI2 = "AIzaSyC_Ma3PBprfOQld08ZkXPamYGnEAN1k9uo";
        private static string keyAPI3 = "AIzaSyDqcF2rA8c3wfLE_KtXvteEZlk3rW_LbPc";
        private  string _API_Search = "https://www.googleapis.com/youtube/v3/search?part=snippet&key=" + KeyAPI1 + "&type=video&q=";
        private  string _API_WatchVideo = "https://www.googleapis.com/youtube/v3/videos?key=" + KeyAPI1 + "&part=snippet&id=";
        public string API_WatchVideo
        {
            get { return _API_WatchVideo; }
            set { this._API_WatchVideo = value; }
        }
        public string API_Search
        {
            get { return _API_Search; }
            set { this._API_Search = value; }
        }
        public string getkey1()
        {
            return KeyAPI1;
        }
        public string getkey2()
        {
            return KeyAPI2;
        }
    }
}
