using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Sandalphon
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class Choosing3dModelPage : Page
    {
        Image[] images;
        public Choosing3dModelPage()
        {
            this.InitializeComponent();
            images = new Image[] { image1, image2, image3, image4, image5, image6, image7, image8, image9 };

        }
        GetThingiverse thingiverse;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // このようにe.Parameterで前のページから渡された値を取得できます。
            // 値はキャストして取り出します。
            thingiverse = e.Parameter as GetThingiverse;
            base.OnNavigatedTo(e);
            setImage();
        }
        void setImage()
        {
            string[] data = thingiverse.getImageUrl();
            for(int i = 0; i < Math.Min(data.Length, images.Length); i++)
            {
                var bitmap = new BitmapImage(new Uri(data[i]));
                images[i].Source = bitmap;
            }
        }

    }
}
