using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PCRCalculator.UI;

namespace PCRCalculator.ImageTool
{
    public class ImageTool
    {
        /// <summary>
        /// 图片上嵌入文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddTextToImage(System.Drawing.Image imgSrc,string addText, int posx,int posy)
        {
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
                using (Font f = new Font("宋体", 20))
                {
                    using (Brush b = new SolidBrush(Color.Black))
                    {
                        g.DrawString(addText, f, b, posx, posy);
                    }
                }
            }
            //string fontpath = Server.MapPath(@"image/FontMark.bmp");
            //imgSrc.Save(fontpath, System.Drawing.Imaging.ImageFormat.Bmp);
            //this.image_Water.ImageUrl = @"~/image/FontMark.bmp";

        }
        public static Image DrawStateImage(int sizex,int sizey,List<UnitTimeLineGroupImage> groupImages)
        {
            Bitmap objBitmap = new Bitmap(sizex, sizey);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.FillRectangle(new SolidBrush(Color.LightBlue), 0, 0, 120, 30);
            foreach(var group in groupImages)
            {
                foreach(var data in group.otherTexts)
                {
                    Draw(objGraphics, data);   
                }
                foreach(var data in group.stateButtons)
                {
                    foreach(var dd in data.ImageDraws)
                    {
                        Draw(objGraphics, dd);

                    }
                }
                foreach (var data in group.buffButtons)
                {
                    foreach (var dd in data.ImageDraws)
                    {
                        Draw(objGraphics, dd);

                    }
                }
            }
            objGraphics.DrawString("角色行动条", new Font("宋体", 8), new SolidBrush(Color.Green), 16, 8);
            return (Image)objBitmap;
        }
        private static void Draw(Graphics graphics,ImageDrawData dd)
        {
            if (dd.ImageType == 1)
            {
                graphics.FillRectangle(new SolidBrush(dd.color), dd.posX, dd.posY, dd.sizeX, dd.sizeY);
            }
            else if (dd.ImageType == 2)
            {
                graphics.DrawString(dd.text, new Font("宋体", dd.fontSize), new SolidBrush(dd.color), dd.posX, dd.posY);
            }
        }
    }
}
