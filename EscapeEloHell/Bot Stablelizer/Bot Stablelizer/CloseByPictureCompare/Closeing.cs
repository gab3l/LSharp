using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Bot_Stablelizer.CloseByPictureCompare
{
   public class Closeing
    {

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rct lpRct);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            Rct rc;
            GetWindowRect(hwnd, out rc);

            var bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            var gfxBmp = Graphics.FromImage(bmp);
            var hdcBitmap = gfxBmp.GetHdc();

            PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private Bitmap GetBitmap(BitmapSource source)
        {
            var bmp = new Bitmap(source.PixelWidth, source.PixelHeight, PixelFormat.Format32bppPArgb);
            var data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }

        public static Bitmap CaptureApplication(Process proc)
        {
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            var width = rect.right - rect.left;
            var height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return bmp;
        }

        public static void ClickAcceptOnEnd(Process client)
        {
            SetForegroundWindow(client.MainWindowHandle);
            var windowBitmap = CaptureApplication(client);
            var search = new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"continue.bmp"));
            var found = searchBitmap(search, windowBitmap, 0.7);

            if (found.X != 0 && found.Y != 0)
            {
                client.Kill();
            }
        }

        private static Rectangle searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            var smallData = smallBmp.LockBits(
                new Rectangle(0, 0, smallBmp.Width, smallBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bigData = bigBmp.LockBits(
                new Rectangle(0, 0, bigBmp.Width, bigBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            var smallStride = smallData.Stride;
            var bigStride = bigData.Stride;

            var bigWidth = bigBmp.Width;
            var bigHeight = bigBmp.Height - smallBmp.Height + 1;
            var smallWidth = smallBmp.Width * 3;
            var smallHeight = smallBmp.Height;

            var location = Rectangle.Empty;
            var margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                var pSmall = (byte*)(void*)smallData.Scan0;
                var pBig = (byte*)(void*)bigData.Scan0;

                var smallOffset = smallStride - smallBmp.Width * 3;
                var bigOffset = bigStride - bigBmp.Width * 3;

                var matchFound = true;

                for (var y = 0; y < bigHeight; y++)
                {
                    for (var x = 0; x < bigWidth; x++)
                    {
                        var pBigBackup = pBig;
                        var pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (var i = 0; i < smallHeight; i++)
                        {
                            var j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                var inf = pBig[0] - margin;
                                var sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound)
                            {
                                break;
                            }

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        pBig = pBigBackup;
                        pSmall = pSmallBackup;
                        pBig += 3;
                    }

                    if (matchFound)
                    {
                        break;
                    }

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }

        private class User32
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public readonly int left;
                public readonly int top;
                public readonly int right;
                public readonly int bottom;
            }
        }

    }
}
