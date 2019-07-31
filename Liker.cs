using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

using WallObject;
using CommentsObject;
using LikesObject;

namespace Liker
{
    class Liker
    {
        private int postsOffset;
        private int postsCount;
        private int commentsCount;

        private string accessToken;
        private string version;
        private Uri baseUrl;

        public string[] Groups { get; set; }
        private Random rand; // Берём случайные посты
        
        public Liker(Config options)
        {
            postsOffset = options.PostsOffset;
            postsCount = options.PostsCount;
            commentsCount = options.CommentsCount;

            accessToken = options.AccessToken;
            version = options.ApiVersion;
            Groups = options.Groups;

            rand = new Random();
            baseUrl = new Uri($"https://api.vk.com/method/");
        }


        public async Task<int> Like()
        {
            int counter = 0; // Счётчик для подсчета поставленных лайков
            foreach (string group in Groups)
            {
                WallGet posts = await getPosts(group, rand.Next(postsOffset), postsCount); // получаем рандомные postsCount постов
                foreach (var post in posts.response.items)
                {
                    if (commentsCount > post.comments.count)
                        commentsCount = post.comments.count;
                    // Получаем рандомные комментарии поста
                    WallGetComments comments = await getComments(group, post.id.ToString(), commentsCount);
                    foreach (var item in comments.response.items)
                    {
                        if(await pressLike(group, item.id.ToString())) // Жмакаем "лайк"
                            counter++;

                        await Task.Delay(350); // "Too many requests per second ©"
                    }
                }
            }
            return counter;
        }

        private async Task<WallGet> getPosts(string ownerId, int offset, int count)
        {
            string parameters = 
                $"wall.get?owner_id=-{ownerId}&offset={offset}&count={count}&access_token={accessToken}&v={version}";

            Uri requestUri = new Uri(baseUrl, parameters);
            HttpWebRequest request = WebRequest.CreateHttp(requestUri);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            string responseString = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<WallGet>(responseString);
        }

        private async Task<WallGetComments> getComments(string ownerId, string postId, int count, int offset=0)
        {
            string parameters =
                $"wall.getComments?owner_id=-{ownerId}&post_id={postId}&count={count}&offset={offset}&thread_items_count=10&access_token={accessToken}&v={version}";

            Uri requestUri = new Uri(baseUrl, parameters);
            HttpWebRequest request = WebRequest.CreateHttp(requestUri);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            string responseString = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<WallGetComments>(responseString);
        }

        private async Task<bool> pressLike(string ownerId, string itemId)
        {
            string parameters =
                $"likes.add?owner_id=-{ownerId}&item_id={itemId}&type=comment&access_key={accessToken}&access_token={accessToken}&v={version}";
            
            Uri requestUri = new Uri(baseUrl, parameters);
            HttpWebRequest request = WebRequest.CreateHttp(requestUri);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            string responseString = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<LikesAdd>(responseString) != null;
        }
    }
}