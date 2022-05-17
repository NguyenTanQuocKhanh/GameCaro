using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caro
{
    class Oco
    {
        public const int _ChieuRong = 25;
        public const int _ChieuCao = 25;
        private int _Dong;
        public int Dong
        {
            get { return _Dong; }
            set { _Dong = value; }
        }
        private int _Cot;
        public int Cot
        {
            get { return _Cot; }
            set { _Cot = value; }
        }
        private Point _Vitri;
        public Point Vitri
        {
            get { return _Vitri; }
            set { _Vitri = value; }
        }
        private int _SoHuu;
        public int SoHuu
        {
            get { return _SoHuu; }
            set { _SoHuu = value; }
        }
        public Oco(int dong,int cot,Point vitri,int sohuu)
        {
            this._Dong = dong;
            this._Cot = cot;
            this._Vitri = vitri;
            this._SoHuu = sohuu;
        }

        public Oco()
        {
        }
    }
}
