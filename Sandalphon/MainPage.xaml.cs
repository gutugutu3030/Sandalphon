using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Sandalphon
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechRecognizer Recognizer { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Recognizer = new SpeechRecognizer();
            await this.Recognizer.CompileConstraintsAsync();
        }

        private async void ButtonWithUI_Click(object sender, RoutedEventArgs e)
        {

            {
                var text = "start. 起動しました";

                var synthesizer = new SpeechSynthesizer();
                synthesizer.Voice = null;
                var stream = await synthesizer.SynthesizeTextToStreamAsync(text);
                var mediaElement = new Windows.UI.Xaml.Controls.MediaElement();
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
            var result = await this.Recognizer.RecognizeWithUIAsync();
            var dialog = new MessageDialog(result.Text);
            await dialog.ShowAsync();
            {
                var text = "認識終了です";

                var synthesizer = new SpeechSynthesizer();
                synthesizer.Voice = null;
                var stream = await synthesizer.SynthesizeTextToStreamAsync(text);
                var mediaElement = new Windows.UI.Xaml.Controls.MediaElement();
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
        }

        private async void ButtonNoUI_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.Recognizer.RecognizeAsync();
            var dialog = new MessageDialog(result.Text);
            await dialog.ShowAsync();
        }
    }
}
