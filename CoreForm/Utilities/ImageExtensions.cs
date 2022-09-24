using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoreForm.Utilities
{
    public static class ControlExtensions
    {
        public static ToolStripItem AddEvent(this ToolStripItem control, string eventName, EventHandler handler)
        {
            var evt = control.GetType().GetEvent(eventName);
            if (evt != null)
            {
                evt.AddEventHandler(control, handler);
            }
            return control;
        }
    }
    /// <summary>
    /// see https://softwarebydefault.com/2013/03/03/colomatrix-image-filters/
    /// </summary>
    public static class ImageExtensions
    {
        public static Bitmap DrawAsBlueLight(this Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                                new float[] {-1, 0, 0, 0, 0},
                                new float[] {0, 1, 0, 0, 0},
                                new float[] {0, 0, 1, 0, 0},
                                new float[] {0, 0, 0, 1, 0},
                                new float[] {1, 1, 1, 0, 1}
                           });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }
        public static Bitmap DrawAsNegative(this Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }
        private static Bitmap ApplyColorMatrix(this Image sourceImage, ColorMatrix colorMatrix)
        {
            Bitmap bmp32BppSource = GetArgbCopy(sourceImage);
            Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
            {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);                

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);


            }


            bmp32BppSource.Dispose();


            return bmp32BppDest;
        }

        private static Bitmap GetArgbCopy(Image sourceImage)
        {
            Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }


            return bmpNew;
        }
    }

    public class ImageHelper
    {
        public static Image LoadFromResource(string resourceName)
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            Stream resource = assembly
                .GetManifestResourceStream($"FreeCellSolitaire.assets.{resourceName}");
            Image img = Image.FromStream(resource);
            return img;
        }
    }
}
