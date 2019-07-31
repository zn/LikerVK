using System;
using System.Collections.Generic;
using System.Text;

namespace Liker
{
    class LikerOptions
    {
        public int PostsOffset { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }

        public string AccessToken { get; set; }
        public string Version { get; set; }
        public string[] Groups { get; set; }
    }
}
