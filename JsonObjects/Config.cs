using System;
using System.Collections.Generic;
using System.Text;

namespace Liker
{
    public class Config
    {
        public string AccessToken { get; set; }
        public string ApiVersion { get; set; }
        public int CommentsCount { get; set; }
        public int PostsCount { get; set; }
        public int PostsOffset { get; set; }
        public string[] Groups { get; set; }
    }
}
