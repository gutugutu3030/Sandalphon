using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    public async void test()
    {
        await Search("",null);

    }
    public async System.Threading.Tasks.Task<string[]> Search(String query,Page page)
    {
        HtmlDocument doc = new HtmlDocument();
        using (var client = new HttpClient())
        using (var stream = await client.GetStreamAsync(new Uri(url+query)))
        {
            // HtmlAgilityPack.HtmlDocumentオブジェクトにHTMLを読み込ませる
            doc.Load(stream, Encoding.UTF8);
        }
        var titles = doc.DocumentNode.Descendants("title");
        // 最初のtitleタグのノードを取り出す
        var titleNode = titles.First();

        // titleタグの値(サイトのタイトルといて表示される部分)を取得する
        var title = titleNode.InnerText;
        Debug.WriteLine(doc.ToString());
        Debug.WriteLine(title);
        page.Frame.Navigate(typeof(Sandalphon.Choosing3dModelPage),this);
        string[] ans = { doc.ToString() };

        return ans;
    }

    public string[] getNextImageUrl()
    {
        index = Math.Max(index - 9, 0);
        string[] data = new string[9];
        for (int i = 0; i + index < items.Length && i < 9; i++)
        {
            data[i] = items[i + index].imageUrl;
        }
        return data;
    }
    public string[] gePreviousImageUrl()
    {
        index = Math.Min(index +9, items.Length-1);
        string[] data = new string[9];
        for (int i = 0; i + index < items.Length && i < 9; i++)
        {
            data[i] = items[i + index].imageUrl;
        }
        return data;
    }


    class ModelItem
    {
        public string imageUrl;
        public string id;
        ModelItem(string id,string imageUrl)
        {
            this.id = id;
            this.imageUrl = imageUrl;
        }
    }
}
