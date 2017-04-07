using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

public class GetThingiverse
{
    String url;
    int index = 0;
    ModelItem[] items;
    int gettingItemsMax = 50;

    public GetThingiverse()
	{
        url = "http://www.thingiverse.com/search?q=";
	}
    public async Task Search(String query,Page page,TextBlock tb)
    {

        HtmlDocument doc = new HtmlDocument();
        using (var client = new HttpClient()) {
            List<ModelItem> getData = new List<ModelItem>();

            tb.Text += url + query;

            using (var stream = await client.GetStreamAsync(new Uri(url + query/* + "/page:" + 1*/)))
            {
                    // HtmlAgilityPack.HtmlDocumentオブジェクトにHTMLを読み込ませる
                    doc.Load(stream, Encoding.UTF8);
            }
            if (doc == null)
            {
                return;
            }
            foreach (HtmlNode node in doc.DocumentNode.Descendants("a"))
            {
                if (!node.Attributes.Contains("class"))
                {
                    continue;
                }
                if (node.Attributes["class"].Value.Equals("thing-img-wrapper"))
                {
                    var item = new ModelItem();
                    item.id = node.GetAttributeValue("href", "");
                    string s=node.InnerHtml.Substring(node.InnerHtml.IndexOf("src=\"")+5);
                    item.imageUrl = s.Substring(0, s.IndexOf("\""));
                    getData.Add(item);
                }
            }
            if (getData != null)
            {
                items = new ModelItem[getData.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = getData[i];
                }
                page.Frame.Navigate(typeof(Sandalphon.Choosing3dModelPage), this);

            }
        }
    }

    public string[] getImageUrl()
    {
        string[] data = new string[9];
        for (int i = 0; i + index < items.Length && i < 9; i++)
        {
            data[i] = items[i + index].imageUrl;
        }
        return data;
    }

    public string[] getNextImageUrl()
    {
        index = Math.Max(index - 9, 0);
        return getImageUrl();
    }
    public string[] gePreviousImageUrl()
    {
        index = Math.Min(index +9, items.Length-1);
        return getImageUrl();
    }


    class ModelItem
    {
        public string imageUrl;
        public string id;
        public ModelItem()
        {

        }
        public ModelItem(string id,string imageUrl)
        {
            this.id = id;
            this.imageUrl = imageUrl;
        }
        
    }
}
