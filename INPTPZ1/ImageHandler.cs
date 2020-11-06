using System;
using System.Drawing;

namespace INPTPZ1
{
	class ImageHandler
	{
		private static readonly Color[] colors = new Color[]
		{
				Color.Red, Color.Blue, Color.Green
		};

		private Bitmap ImageBitmap { get; set; }

		public ImageHandler(ResolutionHolder resolutionHolder)
		{
			ImageBitmap = new Bitmap(resolutionHolder.Width, resolutionHolder.Height);
		}


		public void SaveImage(string path)
		{
			ImageBitmap.Save(path ?? "../../../out.png");
		}

		public void SetPixel(int x, int y, Color color)
		{
			ImageBitmap.SetPixel(x, y, color);
		}

		public static Color GetColorByParams(int iteration, int rootCount)
		{
			var color = colors[rootCount % colors.Length];
			color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iteration * 2), 255), Math.Min(Math.Max(0, color.G - iteration * 2), 255), Math.Min(Math.Max(0, color.B - iteration * 2), 255));
			return color;
		}
	}
}
