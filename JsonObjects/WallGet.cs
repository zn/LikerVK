using System.Collections.Generic;

namespace WallObject
{
    public class Size
    {
        public string type { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public int user_id { get; set; }
        public List<Size> sizes { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public int post_id { get; set; }
        public string access_key { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Photo photo { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public int date { get; set; }
        public int marked_as_ads { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public List<Attachment> attachments { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public Views views { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class WallGet
    {
        public Response response { get; set; }
    }
}
