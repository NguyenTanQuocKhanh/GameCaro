using System;
using System.Drawing;

namespace caro
{
    class BanCo
    {
        public static Pen pen;
        private Image imageO = new Bitmap(Properties.Resources.o);
        private Image imageX = new Bitmap(Properties.Resources.x);
        private int _SoDong;
        private int _SoCot;
        public int SoDong
        {
            get { return _SoDong; }
        }
        public int SoCot
        {
            get { return _SoCot; }
        }


        public BanCo()
        {
            _SoDong = 0;
            _SoCot = 0;
        }
        public BanCo(int soDong, int soCot)
        {
            _SoDong = soDong;
            _SoCot = soCot;
        }
        public void VeBanCo(Graphics g)
        {
            pen = new Pen(Color.Black);
            for (int i=0;i<_SoCot;i++)
            {
                g.DrawLine(pen, i * Oco._ChieuRong, 0, i * Oco._ChieuRong, _SoDong * Oco._ChieuCao);
            }
            for (int j = 0; j < _SoDong; j++)
            {
                g.DrawLine(pen, 0, j * Oco._ChieuCao, _SoCot * Oco._ChieuRong, j * Oco._ChieuCao);
            }
        }
        public void VeQuanCoOX(Graphics g,int X, int Y, int SoHuu)
        {
            if (SoHuu == 1)
            {
                g.DrawImage(imageO, X, Y);               
            }
            else
            {             
                g.DrawImage(imageX, X, Y);
            }
        }
        public void Delete(Graphics g, Point point)
        {
            SolidBrush sb = new SolidBrush(Color.Gray);
            g.FillRectangle(sb, point.X, point.Y, Oco._ChieuRong, Oco._ChieuCao);
               
        }
        

    }
}
