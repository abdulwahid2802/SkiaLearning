using System.ComponentModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasView_PaintSurface;
            Content = canvasView;
        }

        private void OnCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 100
            };

            canvas.DrawCircle(info.Width / 2, info.Height / 2, 500, paint);

            paint.Style = SKPaintStyle.Fill;
            paint.Color = Color.DeepPink.ToSKColor();

            canvas.DrawCircle(info.Width / 2, info.Height / 2, 500, paint);
        }
    }
}
