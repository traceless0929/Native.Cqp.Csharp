using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Site.Traceless.Tools.Crawler
{
    public class WeiBoUtil
    {
        /// <summary>
        /// 获取话题Id
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        public static string GetWeiBoTopicId(string topicName)
        {
            string topicUrl = "";
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("https://s.weibo.com/weibo/" + topicName + "&Refer=weibo_weibo&xsort=time&realtimeweibo=1");
            var ress = JavaScriptAnalyzer.Decode(doc.DocumentNode.InnerHtml);
            HtmlNodeCollection ContentList = doc.DocumentNode.SelectNodes("//a[@class='W_btn_b6']");
            var item = ContentList.FirstOrDefault();
            if (item == null) return null;
            else
            {
                var res = item.Attributes["action-data"];
                topicUrl = res.Value;
            }

            var ret = topicUrl.Substring(topicUrl.LastIndexOf(':') + 1);
            return ret;
        }

        public static List<WeiBoContentItem> GetWeiBoTopicContentV2(string topicName, string targetName = "")
        {
            List<WeiBoContentItem> res = new List<WeiBoContentItem>();
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("https://s.weibo.com/weibo/" + topicName + "&Refer=weibo_weibo&xsort=time&realtimeweibo=1");
            doc.DocumentNode.InnerHtml = JavaScriptAnalyzer.Decode(doc.DocumentNode.InnerHtml);
            HtmlNodeCollection ContentList = doc.DocumentNode.SelectNodes("//div[@class='content clearfix']");
            //获取一个话题项
            ContentList.ToList().ForEach(p =>
            {
                var item = new WeiBoContentItem();
                //获取时间
                var timeItem = p.SelectNodes(".//a[@class='W_textb']");
                item.Time = Convert.ToDateTime(timeItem.FirstOrDefault()?.InnerText);
                var nickName = p.SelectNodes(".//a[@class='W_texta W_fb']");
                item.Author = nickName.FirstOrDefault()?.InnerText.Trim();
                var content = p.SelectNodes(".//p[@class='comment_txt']");
                item.ContentStr = content.FirstOrDefault()?.InnerText.Trim();
                var pic = p.SelectNodes(".//img[@action-type='feed_list_media_img']");
                item.Pic = "https:" + pic.FirstOrDefault()?.Attributes.FirstOrDefault(c => c.Name == "src")?.Value.Replace("thumbnail", "large");
                res.Add(item);
            });
            return res.Where(p => p.Author.Trim().Contains(targetName)).OrderByDescending(p => p.Time).ToList();
        }

        /// <summary>
        /// 获取微博话题内容列表(使用微博话题api),此接口返回内容详细，非常好用
        /// </summary>
        /// <param name="topicId">话题id</param>
        /// <param name="tragetName">指定发送者名称</param>
        /// <returns></returns>
        public static List<WeiBoContentItem> GetWeiBoTopicContentV3(string topicId, string targetName = "")
        {
            var res = JavaScriptAnalyzer.Decode(Http.HttpHelper.GetAPI("https://m.weibo.cn/api/container/getIndex?containerid=" + topicId));
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiBoTopicRes>(res);
            var card_Groups = new List<WeiBoTopicRes.Card_Group>();
            ret.data.cards.Where(p => p.card_group != null).Select(p => p).ToList().ForEach(
                c =>
                {
                    card_Groups.AddRange(c.card_group);
                });

            List<WeiBoContentItem> theres = new List<WeiBoContentItem>();
            card_Groups.ForEach(p =>
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(p.mblog.text);
                WeiBoContentItem item = new WeiBoContentItem
                {
                    Pic = p.mblog.original_pic,
                    Author = p.mblog.user.screen_name,
                    ContentStr = htmlDocument.DocumentNode?.InnerText
                };
                if (p.mblog.created_at.Contains("分钟"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("分钟前", ""));
                    item.Time = DateTime.Now.AddMinutes(-getNum);
                }
                else if (p.mblog.created_at.Contains("小时"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("小时前", ""));
                    item.Time = DateTime.Now.AddHours(-getNum);
                }
                else if (p.mblog.created_at.Contains("昨天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("昨天", "").Trim());
                    item.Time = getNum.AddDays(-1);
                }
                else if (p.mblog.created_at.Contains("前天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("前天", "").Trim());
                    item.Time = getNum.AddDays(-2);
                }
                else
                {
                    item.Time = Convert.ToDateTime(p.mblog.created_at);
                }

                theres.Add(item);
            });
            return theres.Where(p => p.Author.Trim().Contains(targetName)).OrderByDescending(p => p.Time).ToList();
        }

        public static List<WeiBoContentItem> GetWeiboByUid(string Uid, string ContainerId, string TopicFilter = "")
        {
            var res = Http.HttpHelper.GetAPI($"https://m.weibo.cn/api/container/getIndex?type=uid&value={Uid}&containerid={ContainerId}");
            //JavaScriptAnalyzer.Decode( )
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiBoDirectContentItem.WeiBoDirectRes>(res);
            var card_Groups = ret.data.cards.ToList();
            List<WeiBoContentItem> theres = new List<WeiBoContentItem>();
            card_Groups.ForEach(p =>
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                if (p.mblog == null)
                {
                    return;
                }
                htmlDocument.LoadHtml(p.mblog.text);
                WeiBoContentItem item = new WeiBoContentItem
                {
                    Pic = p.mblog.original_pic,
                    Author = p.mblog.user.screen_name,
                    ContentStr = htmlDocument.DocumentNode?.InnerText
                };
                if (p.mblog.created_at.Contains("分钟"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("分钟前", ""));
                    item.Time = DateTime.Now.AddMinutes(-getNum);
                }
                else if (p.mblog.created_at.Contains("小时"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("小时前", ""));
                    item.Time = DateTime.Now.AddHours(-getNum);
                }
                else if (p.mblog.created_at.Contains("昨天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("昨天", "").Trim());
                    item.Time = getNum.AddDays(-1);
                }
                else if (p.mblog.created_at.Contains("前天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("前天", "").Trim());
                    item.Time = getNum.AddDays(-2);
                }
                else
                {
                    item.Time = Convert.ToDateTime(p.mblog.created_at);
                }

                theres.Add(item);
            });
            return theres.Where(p => p.ContentStr.Contains(TopicFilter)).OrderByDescending(p => p.Time).ToList();
        }

        /// <summary>
        /// 获取微博话题内容列表(使用微博话题api),此接口返回内容详细，非常好用
        /// </summary>
        /// <param name="topicId">话题名</param>
        /// <param name="tragetName">指定发送者名称</param>
        /// <returns></returns>
        public static List<WeiBoContentItem> GetWeiBoTopicContentV1(string topicName, string targetName = "")
        {
            var encode = System.Web.HttpUtility.UrlEncode(topicName);
            var res = JavaScriptAnalyzer.Decode(Http.HttpHelper.GetAPI($"https://m.weibo.cn/api/container/getIndex?type=uid&value=1761587065"));
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiBoTopicRes>(res);
            var card_Groups = new List<WeiBoTopicRes.Card_Group>();
            ret.data.cards.Where(p => p.card_group != null).Select(p => p).ToList().ForEach(
                c =>
                {
                    card_Groups.AddRange(c.card_group);
                });

            List<WeiBoContentItem> theres = new List<WeiBoContentItem>();
            card_Groups.ForEach(p =>
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(p.mblog.text);
                WeiBoContentItem item = new WeiBoContentItem
                {
                    Pic = p.mblog.original_pic,
                    Author = p.mblog.user.screen_name,
                    ContentStr = htmlDocument.DocumentNode?.InnerText
                };
                if (p.mblog.created_at.Contains("分钟"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("分钟前", ""));
                    item.Time = DateTime.Now.AddMinutes(-getNum);
                }
                else if (p.mblog.created_at.Contains("小时"))
                {
                    var getNum = Convert.ToInt32(p.mblog.created_at.Replace("小时前", ""));
                    item.Time = DateTime.Now.AddHours(-getNum);
                }
                else if (p.mblog.created_at.Contains("昨天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("昨天", "").Trim());
                    item.Time = getNum.AddDays(-1);
                }
                else if (p.mblog.created_at.Contains("前天"))
                {
                    var getNum = Convert.ToDateTime(p.mblog.created_at.Replace("前天", "").Trim());
                    item.Time = getNum.AddDays(-2);
                }
                else
                {
                    item.Time = Convert.ToDateTime(p.mblog.created_at);
                }

                theres.Add(item);
            });
            return theres.Where(p => p.Author.Trim().Contains(targetName)).OrderByDescending(p => p.Time).ToList();
        }
    }

    public class WeiBoContentItem
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 微博正文
        /// </summary>
        public string ContentStr { get; set; }
        /// <summary>
        /// 微博图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }

    public class WeiBoTopicRes
    {

        public long ok { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public Pageinfo pageInfo { get; set; }
            public Card[] cards { get; set; }
            public long showAppTips { get; set; }
            public string scheme { get; set; }
        }

        public class Pageinfo
        {
            public long page_size { get; set; }
            public string v_p { get; set; }
            public string containerid { get; set; }
            public string page_type_name { get; set; }
            public string title_top { get; set; }
            public string title_icon { get; set; }
            public string nick { get; set; }
            public string page_title { get; set; }
            public string page_attr { get; set; }
            public string page_url { get; set; }
            public long display_arrow { get; set; }
            public string oid { get; set; }
            public long follow_relation { get; set; }
            public string page_view { get; set; }
            public string detail_desc { get; set; }
            public string adid { get; set; }
            public string desc_scheme { get; set; }
            public string background_scheme { get; set; }
            public string portrait { get; set; }
            public string portrait_sub_text { get; set; }
            public string[] desc_more { get; set; }
            public string desc { get; set; }
            public Cardlist_Head_Cards[] cardlist_head_cards { get; set; }
            public Toolbar_Menus[] toolbar_menus { get; set; }
            public long total { get; set; }
            public string since_id { get; set; }
            public long adhesive { get; set; }
            public long show_style { get; set; }
            public string huati_exposure { get; set; }
            public string page_type { get; set; }
            public long attitudes_count { get; set; }
            public long attitudes_status { get; set; }
            public Button[] buttons { get; set; }
        }

        public class Cardlist_Head_Cards
        {
            public long head_type { get; set; }
            public string head_type_name { get; set; }
            public bool show_menu { get; set; }
            public string menu_scheme { get; set; }
            public Channel_List[] channel_list { get; set; }
        }

        public class Channel_List
        {
            public string id { get; set; }
            public string name { get; set; }
            public string containerid { get; set; }
            public string scheme { get; set; }
            public long must_show { get; set; }
            public long default_add { get; set; }
            public Filter_Group_Info filter_group_info { get; set; }
            public Filter_Group[] filter_group { get; set; }
        }

        public class Filter_Group_Info
        {
            public string title { get; set; }
            public string icon { get; set; }
            public string icon_name { get; set; }
            public string icon_scheme { get; set; }
        }

        public class Filter_Group
        {
            public string name { get; set; }
            public string containerid { get; set; }
            public string scheme { get; set; }
            public string title { get; set; }
        }

        public class Toolbar_Menus
        {
            public string type { get; set; }
            public long sub_type { get; set; }
            public long show_loading { get; set; }
            public string name { get; set; }
            public Params _params { get; set; }
            public Actionlog1 actionlog { get; set; }
            public object scheme { get; set; }
            public string pic { get; set; }
        }

        public class Params
        {
            public string uid { get; set; }
            public string type { get; set; }
            public string scheme { get; set; }
            public Menu_List[] menu_list { get; set; }
        }

        public class Menu_List
        {
            public string type { get; set; }
            public string name { get; set; }
            public Params1 _params { get; set; }
            public Actionlog actionlog { get; set; }
            public string scheme { get; set; }
        }

        public class Params1
        {
            public string scheme { get; set; }
        }

        public class Actionlog
        {
            public long act_code { get; set; }
            public string ext { get; set; }
            public string fid { get; set; }
        }

        public class Actionlog1
        {
            public long act_code { get; set; }
            public string ext { get; set; }
            public string fid { get; set; }
        }

        public class Button
        {
            public string type { get; set; }
            public string sub_type { get; set; }
            public string name { get; set; }
            public Params2 _params { get; set; }
            public string scheme { get; set; }
        }

        public class Params2
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Card
        {
            public long card_type { get; set; }
            public string card_type_name { get; set; }
            public long is_asyn { get; set; }
            public string itemid { get; set; }
            public string async_api { get; set; }
            public string _appid { get; set; }
            public string _cur_filter { get; set; }
            public string title { get; set; }
            public long show_type { get; set; }
            public string buttontitle { get; set; }
            public string scheme { get; set; }
            public string[] hide_oids { get; set; }
            public Card_Group[] card_group { get; set; }
        }

        public class Card_Group
        {
            public long card_type { get; set; }
            public string itemid { get; set; }
            public long show_type { get; set; }
            public string scheme { get; set; }
            public Mblog mblog { get; set; }
        }

        public class Mblog
        {
            public string created_at { get; set; }
            public string id { get; set; }
            public string idstr { get; set; }
            public string mid { get; set; }
            public bool can_edit { get; set; }
            public string text { get; set; }
            public long textLength { get; set; }
            public string source { get; set; }
            public bool favorited { get; set; }
            public string thumbnail_pic { get; set; }
            public string bmiddle_pic { get; set; }
            public string original_pic { get; set; }
            public bool is_paid { get; set; }
            public long mblog_vip_type { get; set; }
            public User user { get; set; }
            public long reposts_count { get; set; }
            public long comments_count { get; set; }
            public long attitudes_count { get; set; }
            public long pending_approval_count { get; set; }
            public bool isLongText { get; set; }
            public Visible visible { get; set; }
            public string topic_id { get; set; }
            public bool sync_mblog { get; set; }
            public bool is_imported_topic { get; set; }
            public string rid { get; set; }
            public long more_info_type { get; set; }
            public long content_auth { get; set; }
            public long mblog_show_union_info { get; set; }
            public long is_controlled_by_server { get; set; }
            public string timestamp_text { get; set; }
            public long expire_after { get; set; }
            public long weibo_position { get; set; }
            public Page_Info page_info { get; set; }
            public Product_Struct[] product_struct { get; set; }
            public string bid { get; set; }
            public Pic[] pics { get; set; }
        }

        public class User
        {
            public long id { get; set; }
            public string screen_name { get; set; }
            public string profile_image_url { get; set; }
            public string profile_url { get; set; }
            public long statuses_count { get; set; }
            public bool verified { get; set; }
            public long verified_type { get; set; }
            public long verified_type_ext { get; set; }
            public string verified_reason { get; set; }
            public bool close_blue_v { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public long mbtype { get; set; }
            public long urank { get; set; }
            public long mbrank { get; set; }
            public bool follow_me { get; set; }
            public bool following { get; set; }
            public long followers_count { get; set; }
            public long follow_count { get; set; }
            public string cover_image_phone { get; set; }
            public string avatar_hd { get; set; }
            public bool like { get; set; }
            public bool like_me { get; set; }
            public Badge badge { get; set; }
        }

        public class Badge
        {
            public long dzwbqlx_2016 { get; set; }
            public long user_name_certificate { get; set; }
            public long suishoupai_2018 { get; set; }
            public long wenchuan_10th { get; set; }
        }

        public class Visible
        {
            public long type { get; set; }
            public long list_id { get; set; }
        }

        public class Page_Info
        {
            public Page_Pic page_pic { get; set; }
            public string page_url { get; set; }
            public string page_title { get; set; }
            public string content1 { get; set; }
            public string content2 { get; set; }
            public string type { get; set; }
            public string object_id { get; set; }
        }

        public class Page_Pic
        {
            public string url { get; set; }
        }

        public class Product_Struct
        {
            public string url { get; set; }
            public string name { get; set; }
            public string img { get; set; }
            public string page_id { get; set; }
            public Actionlog2 actionlog { get; set; }
            public Button1[] buttons { get; set; }
        }

        public class Actionlog2
        {
            public string act_code { get; set; }
            public long uid { get; set; }
            public long mid { get; set; }
            public string oid { get; set; }
            public string uicode { get; set; }
            public string cardid { get; set; }
            public string fid { get; set; }
            public string luicode { get; set; }
            public string lfid { get; set; }
            public string ext { get; set; }
            public string source { get; set; }
        }

        public class Button1
        {
            public long sub_type { get; set; }
            public Actionlog3 actionlog { get; set; }
        }

        public class Actionlog3
        {
            public long act_code { get; set; }
            public long uid { get; set; }
            public long mid { get; set; }
            public string oid { get; set; }
            public string uicode { get; set; }
            public string cardid { get; set; }
            public string fid { get; set; }
            public string luicode { get; set; }
            public string lfid { get; set; }
            public string ext { get; set; }
            public string source { get; set; }
        }

        public class Pic
        {
            public string pid { get; set; }
            public string url { get; set; }
            public string size { get; set; }
            public Geo geo { get; set; }
            public Large large { get; set; }
        }

        public class Geo
        {
            public long width { get; set; }
            public long height { get; set; }
            public bool croped { get; set; }
        }

        public class Large
        {
            public string size { get; set; }
            public string url { get; set; }
            public Geo1 geo { get; set; }
        }

        public class Geo1
        {
            public string width { get; set; }
            public string height { get; set; }
            public bool croped { get; set; }
        }

    }

    public class WeiBoDirectContentItem
    {

        public class WeiBoDirectRes
        {
            public long ok { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public Cardlistinfo cardlistInfo { get; set; }
            public Card[] cards { get; set; }
            public long showAppTips { get; set; }
            public string scheme { get; set; }
        }

        public class Cardlistinfo
        {
            public string containerid { get; set; }
            public long v_p { get; set; }
            public long show_style { get; set; }
            public long total { get; set; }
            public long page { get; set; }
        }

        public class Card
        {
            public long card_type { get; set; }
            public string itemid { get; set; }
            public string scheme { get; set; }
            public Mblog mblog { get; set; }
            public long show_type { get; set; }
        }

        public class Mblog
        {
            public string created_at { get; set; }
            public string id { get; set; }
            public string idstr { get; set; }
            public string mid { get; set; }
            public bool can_edit { get; set; }
            public string text { get; set; }
            public string source { get; set; }
            public bool favorited { get; set; }
            public bool is_paid { get; set; }
            public long mblog_vip_type { get; set; }
            public User user { get; set; }
            public long pid { get; set; }
            public Retweeted_Status retweeted_status { get; set; }
            public long reposts_count { get; set; }
            public long comments_count { get; set; }
            public long attitudes_count { get; set; }
            public long pending_approval_count { get; set; }
            public bool isLongText { get; set; }
            public long hide_flag { get; set; }
            public Visible1 visible { get; set; }
            public long more_info_type { get; set; }
            public long content_auth { get; set; }
            public Edit_Config1 edit_config { get; set; }
            public long mblogtype { get; set; }
            public long isTop { get; set; }
            public long weibo_position { get; set; }
            public string obj_ext { get; set; }
            public string raw_text { get; set; }
            public string bid { get; set; }
            public Title title { get; set; }
            public long textLength { get; set; }
            public string thumbnail_pic { get; set; }
            public string bmiddle_pic { get; set; }
            public string original_pic { get; set; }
            public string topic_id { get; set; }
            public bool sync_mblog { get; set; }
            public bool is_imported_topic { get; set; }
            public Page_Info1 page_info { get; set; }
            public Product_Struct[] product_struct { get; set; }
            public Pic1[] pics { get; set; }
        }

        public class User
        {
            public long id { get; set; }
            public string screen_name { get; set; }
            public string profile_image_url { get; set; }
            public string profile_url { get; set; }
            public long statuses_count { get; set; }
            public bool verified { get; set; }
            public long verified_type { get; set; }
            public long verified_type_ext { get; set; }
            public string verified_reason { get; set; }
            public bool close_blue_v { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public long mbtype { get; set; }
            public long urank { get; set; }
            public long mbrank { get; set; }
            public bool follow_me { get; set; }
            public bool following { get; set; }
            public long followers_count { get; set; }
            public long follow_count { get; set; }
            public string cover_image_phone { get; set; }
            public string avatar_hd { get; set; }
            public bool like { get; set; }
            public bool like_me { get; set; }
            public Badge badge { get; set; }
        }

        public class Badge
        {
            public long dzwbqlx_2016 { get; set; }
            public long user_name_certificate { get; set; }
            public long suishoupai_2018 { get; set; }
            public long wenchuan_10th { get; set; }
        }

        public class Retweeted_Status
        {
            public string created_at { get; set; }
            public string id { get; set; }
            public string idstr { get; set; }
            public string mid { get; set; }
            public bool can_edit { get; set; }
            public string text { get; set; }
            public long textLength { get; set; }
            public string source { get; set; }
            public bool favorited { get; set; }
            public bool is_paid { get; set; }
            public long mblog_vip_type { get; set; }
            public User1 user { get; set; }
            public long reposts_count { get; set; }
            public long comments_count { get; set; }
            public long attitudes_count { get; set; }
            public long pending_approval_count { get; set; }
            public bool isLongText { get; set; }
            public long hide_flag { get; set; }
            public Visible visible { get; set; }
            public long more_info_type { get; set; }
            public long content_auth { get; set; }
            public Edit_Config edit_config { get; set; }
            public long weibo_position { get; set; }
            public Page_Info page_info { get; set; }
            public long retweeted { get; set; }
            public string bid { get; set; }
            public long expire_time { get; set; }
            public string thumbnail_pic { get; set; }
            public string bmiddle_pic { get; set; }
            public string original_pic { get; set; }
            public Pic[] pics { get; set; }
            public string topic_id { get; set; }
            public bool sync_mblog { get; set; }
            public bool is_imported_topic { get; set; }
            public string cardid { get; set; }
            public string picStatus { get; set; }
        }

        public class User1
        {
            public long id { get; set; }
            public string screen_name { get; set; }
            public string profile_image_url { get; set; }
            public string profile_url { get; set; }
            public long statuses_count { get; set; }
            public bool verified { get; set; }
            public long verified_type { get; set; }
            public long verified_type_ext { get; set; }
            public string verified_reason { get; set; }
            public bool close_blue_v { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public long mbtype { get; set; }
            public long urank { get; set; }
            public long mbrank { get; set; }
            public bool follow_me { get; set; }
            public bool following { get; set; }
            public long followers_count { get; set; }
            public long follow_count { get; set; }
            public string cover_image_phone { get; set; }
            public string avatar_hd { get; set; }
            public bool like { get; set; }
            public bool like_me { get; set; }
            public Badge1 badge { get; set; }
        }

        public class Badge1
        {
            public long bind_taobao { get; set; }
            public long follow_whitelist_video { get; set; }
            public long user_name_certificate { get; set; }
            public long suishoupai_2018 { get; set; }
            public long super_star_2018 { get; set; }
            public long unread_pool { get; set; }
            public long unread_pool_ext { get; set; }
            public long dzwbqlx_2016 { get; set; }
            public long panda { get; set; }
            public long wenchuan_10th { get; set; }
        }

        public class Visible
        {
            public long type { get; set; }
            public long list_id { get; set; }
        }

        public class Edit_Config
        {
            public bool edited { get; set; }
        }

        public class Page_Info
        {
            public Page_Pic page_pic { get; set; }
            public string page_url { get; set; }
            public string page_title { get; set; }
            public string content1 { get; set; }
            public string content2 { get; set; }
            public string type { get; set; }
            public Media_Info media_info { get; set; }
            public string object_id { get; set; }
        }

        public class Page_Pic
        {
            public string url { get; set; }
        }

        public class Media_Info
        {
            public string stream_url { get; set; }
        }

        public class Pic
        {
            public string pid { get; set; }
            public string url { get; set; }
            public string size { get; set; }
            public Geo geo { get; set; }
            public Large large { get; set; }
        }

        public class Geo
        {
            public long width { get; set; }
            public long height { get; set; }
            public bool croped { get; set; }
        }

        public class Large
        {
            public string size { get; set; }
            public string url { get; set; }
            public Geo1 geo { get; set; }
        }

        public class Geo1
        {
            public string width { get; set; }
            public string height { get; set; }
            public bool croped { get; set; }
        }

        public class Visible1
        {
            public long type { get; set; }
            public long list_id { get; set; }
        }

        public class Edit_Config1
        {
            public bool edited { get; set; }
        }

        public class Title
        {
            public string text { get; set; }
            public long base_color { get; set; }
        }

        public class Page_Info1
        {
            public Page_Pic1 page_pic { get; set; }
            public string page_url { get; set; }
            public string page_title { get; set; }
            public string content1 { get; set; }
            public string content2 { get; set; }
            public string type { get; set; }
            public string object_id { get; set; }
        }

        public class Page_Pic1
        {
            public string url { get; set; }
        }

        public class Product_Struct
        {
            public string url { get; set; }
            public string name { get; set; }
            public string img { get; set; }
            public string page_id { get; set; }
            public Actionlog actionlog { get; set; }
            public Button[] buttons { get; set; }
        }

        public class Actionlog
        {
            public string act_code { get; set; }
            public long uid { get; set; }
            public long mid { get; set; }
            public string oid { get; set; }
            public string uicode { get; set; }
            public string cardid { get; set; }
            public string fid { get; set; }
            public string luicode { get; set; }
            public string lfid { get; set; }
            public string ext { get; set; }
            public string source { get; set; }
        }

        public class Button
        {
            public string name { get; set; }
            public string pic { get; set; }
            public string type { get; set; }
            public Params _params { get; set; }
            public long sub_type { get; set; }
            public Actionlog1 actionlog { get; set; }
        }

        public class Params
        {
            public string uid { get; set; }
            public string scheme { get; set; }
            public string type { get; set; }
        }

        public class Actionlog1
        {
            public long act_code { get; set; }
            public long uid { get; set; }
            public long mid { get; set; }
            public string oid { get; set; }
            public string uicode { get; set; }
            public string cardid { get; set; }
            public string fid { get; set; }
            public string luicode { get; set; }
            public string lfid { get; set; }
            public string ext { get; set; }
            public string source { get; set; }
        }

        public class Pic1
        {
            public string pid { get; set; }
            public string url { get; set; }
            public string size { get; set; }
            public Geo2 geo { get; set; }
            public Large1 large { get; set; }
        }

        public class Geo2
        {
            public long width { get; set; }
            public long height { get; set; }
            public bool croped { get; set; }
        }

        public class Large1
        {
            public string size { get; set; }
            public string url { get; set; }
            public Geo3 geo { get; set; }
        }

        public class Geo3
        {
            public string width { get; set; }
            public string height { get; set; }
            public bool croped { get; set; }
        }

    }
}
