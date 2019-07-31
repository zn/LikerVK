using System.Collections.Generic;

namespace CommentsObject
{
    public class SubItem
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int post_id { get; set; }
        public int owner_id { get; set; }

        public List<object> parents_stack { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public int reply_to_user { get; set; }
        public int reply_to_comment { get; set; }
    }

    public class Thread
    {
        public int count { get; set; }
        public List<SubItem> items { get; set; }
        public bool can_post { get; set; }
        public bool show_reply_button { get; set; }
        public bool groups_can_post { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int post_id { get; set; }
        public int owner_id { get; set; }
        public List<object> parents_stack { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public Thread thread { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
        public int current_level_count { get; set; }
        public bool can_post { get; set; }
        public bool show_reply_button { get; set; }
        public bool groups_can_post { get; set; }
    }

    public class WallGetComments
    {
        public Response response { get; set; }
    }
}
