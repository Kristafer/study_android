using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Neiron
{
	public class ConvertImage
	{
        public static void Clear(PictureBox picture, int width, int height)
        {
            picture.Image = (Image)new Bitmap(width, height);
        }

        public static double[,] GetImageData(Bitmap bp, int size)
        {
	        var data = new double[bp.Height, bp.Width];
	        for (int i = 0; i < bp.Height; i++)
	        {
		        for (int j = 0; j < bp.Width; j++)
		        {
			        data[i,j] = bp.GetPixel(i,j).ToArgb() == 0 ? 0 : 1;
                }
            }

	        return data;
        }
	}
}
