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
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Sandalphon
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class Choosing3dModelPage : Page
    {
        Button[] buttons;
        public Choosing3dModelPage()
        {
            this.InitializeComponent();
            buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }
        GetThingiverse thingiverse;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // このようにe.Parameterで前のページから渡された値を取得できます。
            // 値はキャストして取り出します。
            thingiverse = e.Parameter as GetThingiverse;
            base.OnNavigatedTo(e);
        }

    }
}
