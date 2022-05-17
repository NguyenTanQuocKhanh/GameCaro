using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace caro
{
 
    
    class DieuKhienCaro
    {
        private Random rand = new Random();
        
        private Oco[,] MangOco;
        private BanCo _BanCo;
        private int LuotDi;
        private Stack<Oco> stack_CacNuocDaDi;
        

        private int mode;

        public int CheDoChoi
        {
            get { return mode; }
            set {mode = value; }
        }

        private bool sansang;
        public bool SanSang
        {
            get { return sansang; }
            set { sansang = value; }
        }



        public DieuKhienCaro()
        {
            
            _BanCo = new BanCo(21, 21);
            MangOco = new Oco[_BanCo.SoDong, _BanCo.SoCot];
            stack_CacNuocDaDi = new Stack<Oco>();       
            //LuotDi=1;            
     
        }

        

        public void VeBanCo(Graphics g)
        {
            _BanCo.VeBanCo(g);
        }
        public void KhoiTaoMangOco()
        {
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    MangOco[i, j] = new Oco(i, j, new Point(j * Oco._ChieuRong, i * Oco._ChieuCao), 0);
                }
            }
        }

        

        public bool DanhCo(int MouseX, int MouseY,Graphics g)
        {           
            int Cot = MouseX / Oco._ChieuRong;
            int Dong = MouseY / Oco._ChieuCao;
            if (MouseX % Oco._ChieuRong == 0 || MouseY % Oco._ChieuCao == 0)
                return false;
            
            if(MangOco[Dong, Cot].SoHuu == 0)
            {
                if (LuotDi==1)
                {
                    _BanCo.VeQuanCoOX(g, Cot * Oco._ChieuCao, Dong * Oco._ChieuRong, LuotDi);
                    MangOco[Dong, Cot].SoHuu = 1;
                    Oco oco = new Oco(MangOco[Dong, Cot].Dong, MangOco[Dong, Cot].Cot,MangOco[Dong,Cot].Vitri, MangOco[Dong, Cot].SoHuu);
                    stack_CacNuocDaDi.Push(oco);
                    LuotDi = 2;
                }
                else
                {
                    _BanCo.VeQuanCoOX(g, Cot * Oco._ChieuCao, Dong * Oco._ChieuRong, LuotDi);
                    MangOco[Dong, Cot].SoHuu = 2;
                    Oco oco = new Oco(MangOco[Dong, Cot].Dong, MangOco[Dong, Cot].Cot, MangOco[Dong, Cot].Vitri, MangOco[Dong, Cot].SoHuu);
                    stack_CacNuocDaDi.Push(oco);
                    LuotDi = 1;
                }
            }
            else
            {
                return false;
            }
            return true;                        
            
        }
        public void PhucHoiQuanCo(Graphics g)
        {
            if (stack_CacNuocDaDi.Count !=0)
            {
                foreach(Oco oco in stack_CacNuocDaDi)
                {
                    _BanCo.VeQuanCoOX(g, oco.Cot * Oco._ChieuRong, oco.Dong * Oco._ChieuCao, oco.SoHuu);
                }
            }
                
           
            
        }
        public void PlayervsPlayer(Graphics g)
        {
            sansang = true;
            stack_CacNuocDaDi = new Stack<Oco>();
            KhoiTaoMangOco();
            LuotDi =rand.Next(0,2);
            if(LuotDi==1)
            {
                MessageBox.Show("Quân O đi trước");
            }
            else
            {
                MessageBox.Show("Quân X đi trước");
            }
           mode = 1;
            VeBanCo(g);


        }
        public void PlayerVsCom(Graphics g)
        {
            sansang = true;       
            stack_CacNuocDaDi = new Stack<Oco>();        
            KhoiTaoMangOco();            
            LuotDi =1;
            mode = 2;         
            VeBanCo(g);
            KhoiDongComputer(g);
        }
        #region Undo
        public void Undo(Graphics g)
        {

            if (stack_CacNuocDaDi.Count != 0)
            {
                Oco oco = stack_CacNuocDaDi.Pop();
                MangOco[oco.Dong, oco.Cot].SoHuu = 0;
                _BanCo.Delete(g, oco.Vitri);


            }
            else
            {
                MessageBox.Show("Chưa có đánh quân cờ nào!!");
            }

            VeBanCo(g);
            PhucHoiQuanCo(g);
            if (LuotDi == 1)
                LuotDi = 2;
            else
                LuotDi = 1;
            
        }    
        #endregion

        #region Duyệt chiến thắng
        public void ketThucTroChoi(Oco oco)
        {
            
            if (mode == 1)
            {
                if (oco.SoHuu == 1)
                {
                    MessageBox.Show("Quân O thắng");
                }
                else
                    MessageBox.Show("Quân X thắng");
            }
            else
            {
                if (oco.SoHuu == 1)
                    MessageBox.Show("Máy thắng");
                else
                    MessageBox.Show("Người chơi thắng");
            }

            sansang = false;
        }
        public bool KiemTraChienThang()
        {
            if (stack_CacNuocDaDi.Count==_BanCo.SoCot*_BanCo.SoDong)
            {
                MessageBox.Show("Hòa cờ");
                return true;
            }
            foreach(Oco oco in stack_CacNuocDaDi)
            {
                if(DuyetDoc(oco.Dong,oco.Cot,oco.SoHuu)||DuyetNgang(oco.Dong,oco.Cot,oco.SoHuu) ||DuyetCheoXuoi(oco.Dong,oco.Cot,oco.SoHuu) || DuyetCheoNguoc(oco.Dong,oco.Cot,oco.SoHuu) )
                {
                    ketThucTroChoi(oco);
                    return true;
                }
                    
            }
               
                   
            return false;

        }
        public bool DuyetDoc(int DongHienTai, int CotHienTai, int SoHuuHienTai)
        {
            if (DongHienTai > _BanCo.SoDong - 5)
                return false;
            int dem;
            for (dem=1;dem<5;dem++)
            {
                if (MangOco[DongHienTai + dem, CotHienTai].SoHuu != SoHuuHienTai)
                    return false;
            }
            if (DongHienTai == 0 || DongHienTai + dem == _BanCo.SoDong)
                return true;
          
            if (MangOco[DongHienTai - 1, CotHienTai].SoHuu == 0 || MangOco[DongHienTai + dem, CotHienTai].SoHuu == 0)
                return true;
            return false;
           


        }
        public bool DuyetNgang(int DongHienTai, int CotHienTai, int SoHuuHienTai)
        {
            if (CotHienTai > _BanCo.SoCot - 5)
                return false;
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[DongHienTai, CotHienTai+dem].SoHuu != SoHuuHienTai)
                    return false;
            }
            if (CotHienTai == 0 || CotHienTai + dem == _BanCo.SoCot)
                return true;

            if (MangOco[DongHienTai, CotHienTai-1].SoHuu == 0 || MangOco[DongHienTai, CotHienTai+dem].SoHuu == 0)
                return true;
            return false;



        }
        public bool DuyetCheoXuoi(int DongHienTai, int CotHienTai, int SoHuuHienTai)
        {
            if (CotHienTai > _BanCo.SoCot - 5 || DongHienTai>_BanCo.SoDong-5)
                return false;
            int dem;
            for (dem = 1; dem < 5; dem++)
            {
                if (MangOco[DongHienTai+dem, CotHienTai + dem].SoHuu != SoHuuHienTai)
                    return false;
            }
            if (DongHienTai == 0 || DongHienTai + dem == _BanCo.SoDong ||CotHienTai == 0 || CotHienTai + dem == _BanCo.SoCot)
                return true;

            if (MangOco[DongHienTai-1, CotHienTai - 1].SoHuu == 0 || MangOco[DongHienTai+dem, CotHienTai + dem].SoHuu == 0)
                return true;
            return false;



        }
        public bool DuyetCheoNguoc(int DongHienTai, int CotHienTai, int SoHuuHienTai)
        {
            if (DongHienTai < 4|| DongHienTai > _BanCo.SoCot - 5)
                return false;
            int dem;
            for (dem = 1; dem <5; dem++)
            {
                if (MangOco[DongHienTai - dem, CotHienTai + dem].SoHuu != SoHuuHienTai)
                    return false;
            }
            if (DongHienTai == 4 || DongHienTai == _BanCo.SoDong-1 || CotHienTai==0 || CotHienTai+dem==_BanCo.SoCot )
                return true;

            if (MangOco[DongHienTai + 1, CotHienTai - 1].SoHuu == 0 || MangOco[DongHienTai - dem, CotHienTai + dem].SoHuu == 0)
                return true;
            return false;



        }

        #endregion

        

    #region AI
        private long[] MangDiemTanCong = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };
        private long[] MangDiemPhongThu = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };
        public void KhoiDongComputer(Graphics g)
        {
            
            if (stack_CacNuocDaDi.Count == 0)
            {
                // đánh giữa bàn cờ. +1 để bỏ các vị trí tại biên(bội của 25 đã xét trước đó)
                DanhCo(_BanCo.SoCot / 2 * Oco._ChieuRong + 1, _BanCo.SoDong / 2 * Oco._ChieuCao + 1, g);
                
            }
            else
            {
                Oco oco = TimKiemNuocDi();
                DanhCo(oco.Vitri.X + 1, oco.Vitri.Y + 1, g);
            }
        }
        
        private Oco TimKiemNuocDi()
        {
            Oco ocoResult = new Oco();
            long diemMax = 0;

            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                 
                  
                    if (MangOco[i, j].SoHuu == 0)
                    {
                        long diemTanCong = DiemTanCong_DuyetDoc(i, j) + DiemTanCong_DuyetNgang(i, j) + DiemTanCong_DuyetCheoXuoi(i, j) + DiemTanCong_DuyetCheoNguoc(i, j);
                        long diemPhongThu = DiemPhongNgu_DuyetDoc(i, j) + DiemPhongNgu_DuyetNgang(i, j) + DiemPhongNgu_DuyetCheoXuoi(i, j) + DiemPhongNgu_DuyetCheoNguoc(i, j);
                        // Lấy điểm tạm bằng cách so sánh điểm tấn công, phòng thủ
                        long diemTam = diemTanCong > diemPhongThu ? diemTanCong : diemPhongThu;
                        // tìm điểm max
                        if (diemMax < diemTam)
                        {
                            diemMax = diemTam;
                            ocoResult = new Oco(MangOco[i, j].Dong, MangOco[i, j].Cot, MangOco[i, j].Vitri, MangOco[i, j].SoHuu);
                        }
                    }
                }
            }

            return ocoResult;
        }

        #region Các điểm Tấn Công
        
        private long DiemTanCong_DuyetDoc(int curDong, int curCot)
        {
            long DiemTemp = 0;
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curDong + dem < _BanCo.SoDong; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong + dem, curCot].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong + dem, curCot].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                    DiemTemp -= 9;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curDong - dem >= 0; dem++)
            {
                
                if (MangOco[curDong - dem, curCot].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong - dem, curCot].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // nếu bị chặn cả 2 đầu thì nước đang xét sẽ không còn giá trị nữa
            if (soQuanDich == 2)
                return 0;
            // Giảm diemTong dựa trên số quân địch
            // Do số quân địch tối đa chỉ có thể là 1, nên ta +1 để tăng số điểm bị trừ
            diemTong -= MangDiemPhongThu[soQuanDich];
            // tăng diemTong dựa trên số quân ta
            diemTong += MangDiemTanCong[soQuanTa];
            diemTong += DiemTemp;

            return diemTong;
        }
        // Duyệt Ngang
        private long DiemTanCong_DuyetNgang(int curDong, int curCot)
        {
            long DiemTemp = 0;
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curCot + dem < _BanCo.SoCot; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong, curCot + dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curCot - dem >= 0; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong, curCot - dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // nếu bị chặn cả 2 đầu thì nước đang xét sẽ không còn giá trị nữa
            if (soQuanDich == 2)
                return 0;
            // Giảm diemTong dựa trên số quân địch
            // Do số quân địch tối đa chỉ có thể là 1, nên ta +1 để tăng số điểm bị trừ
            diemTong -= MangDiemPhongThu[soQuanDich];
            // tăng diemTong dựa trên số quân ta
            diemTong += MangDiemTanCong[soQuanTa];
            diemTong += DiemTemp;

            return diemTong;
        }
        // Chéo xuôi
        private long DiemTanCong_DuyetCheoXuoi(int curDong, int curCot)
        {
            long DiemTemp = 0;
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curDong + dem < _BanCo.SoDong && curCot + dem < _BanCo.SoCot; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong + dem, curCot + dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong + dem, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curDong - dem >= 0 && curCot - dem >= 0; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong - dem, curCot - dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong - dem, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // nếu bị chặn cả 2 đầu thì nước đang xét sẽ không còn giá trị nữa
            if (soQuanDich == 2)
                return 0;
            // Giảm diemTong dựa trên số quân địch
            // Do số quân địch tối đa chỉ có thể là 1, nên ta +1 để tăng số điểm bị trừ
            diemTong -= MangDiemPhongThu[soQuanDich];
            // tăng diemTong dựa trên số quân ta
            diemTong += MangDiemTanCong[soQuanTa];
            diemTong += DiemTemp;

            return diemTong;
        }
        // Duyệt chéo ngược
        private long DiemTanCong_DuyetCheoNguoc(int curDong, int curCot)
        {
            long DiemTemp = 0;
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp         // đang duyệt chéo ngược lại lên trên  
            for (int dem = 1; dem < 6 && curCot + dem < _BanCo.SoCot && curDong - dem >= 0; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong - dem, curCot + dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong - dem, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ trên chéo ngược xuống dưới                  
            for (int dem = 1; dem < 6 && curCot - dem >= 0 && curDong + dem < _BanCo.SoDong; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                //
                if (MangOco[curDong + dem, curCot - dem].SoHuu == 1)
                    soQuanTa++;
                else if (MangOco[curDong + dem, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    DiemTemp -= 9;
                    soQuanDich++;
                    break; // nếu gặp quân địch(bị chặn) => thoát vòng lặp
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // nếu bị chặn cả 2 đầu thì nước đang xét sẽ không còn giá trị nữa
            if (soQuanDich == 2)
                return 0;
            // Giảm diemTong dựa trên số quân địch
            // Do số quân địch tối đa chỉ có thể là 1, nên ta +1 để tăng số điểm bị trừ
            diemTong -= MangDiemPhongThu[soQuanDich];
            // tăng diemTong dựa trên số quân ta
            diemTong += MangDiemTanCong[soQuanTa];
            diemTong += DiemTemp;

            return diemTong;
        }
        #endregion

        #region  Các điểm Phòng Ngự

        // Duyệt dọc
        private long DiemPhongNgu_DuyetDoc(int curDong, int curCot)
        {
            //long DiemTemp = 0;
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curDong + dem < _BanCo.SoDong; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong + dem, curCot].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong + dem, curCot].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curDong - dem >= _BanCo.SoDong; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong - dem, curCot].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong - dem, curCot].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // quân ta đã chặn 2 đầu, nên k cần xét nữa
            if (soQuanTa == 2)
                return 0;
            // 
            // 

            // tăng điểm phòng ngự dựa trên số quân địch
            diemTong += MangDiemPhongThu[soQuanDich];
            if (soQuanDich > 0)
            {
                diemTong -= MangDiemTanCong[soQuanTa] * 2;
            }

            return diemTong;
        }
        // Duyệt Ngang
        private long DiemPhongNgu_DuyetNgang(int curDong, int curCot)
        {
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curCot + dem < _BanCo.SoCot; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong, curCot + dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curCot - dem >= 0; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong, curCot - dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            if (soQuanTa == 2)
                return 0;


            // tăng điểm phòng ngự
            diemTong += MangDiemPhongThu[soQuanDich];
            if (soQuanDich > 0)
            {
                diemTong -= MangDiemTanCong[soQuanTa] * 2;
            }

            return diemTong;
        }
        // Chéo xuôi
        private long DiemPhongNgu_DuyetCheoXuoi(int curDong, int curCot)
        {
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp            
            for (int dem = 1; dem < 6 && curDong + dem < _BanCo.SoDong && curCot + dem < _BanCo.SoCot; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong + dem, curCot + dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong + dem, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ dòng dưới ngược lên trên                     
            for (int dem = 1; dem < 6 && curDong - dem >= 0 && curCot - dem >= 0; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong - dem, curCot - dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong - dem, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }


            if (soQuanTa == 2)
                return 0;
            // tăng diemTong dựa trên số quân ta
            diemTong += MangDiemPhongThu[soQuanDich];
            if (soQuanDich > 0)
            {
                diemTong -= MangDiemTanCong[soQuanTa] * 2;
            }

            return diemTong;
        }
        // Duyệt chéo ngược
        private long DiemPhongNgu_DuyetCheoNguoc(int curDong, int curCot)
        {
            long diemTong = 0;
            int soQuanTa = 0; // máy
            int soQuanDich = 0; // người

            // duyệt từ dòng trên xuống dưới
            // duyệt 5 con để biết ô tiếp theo(thứ 6) bị chặn thì ta xử lí phù hợp         // đang duyệt chéo ngược lại lên trên  
            for (int dem = 1; dem < 6 && curCot + dem < _BanCo.SoCot && curDong - dem >= 0; dem++)
            {

                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                if (MangOco[curDong - dem, curCot + dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong - dem, curCot + dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }

            // duyệt từ trên chéo ngược xuống dưới                  
            for (int dem = 1; dem < 6 && curCot - dem >= 0 && curDong + dem < _BanCo.SoDong; dem++)
            {
                // Do Player1 luôn là máy(quân ta) đánh nên SoHuu = 1
                //
                if (MangOco[curDong + dem, curCot - dem].SoHuu == 1)
                {
                    soQuanTa++;
                    break;
                }
                else if (MangOco[curDong + dem, curCot - dem].SoHuu == 2) // quân địch(player 2)
                {
                    soQuanDich++;
                }
                else // nếu gặp ô trống => thoát
                {
                    break;
                }
            }


            if (soQuanTa == 2)
                return 0;
            diemTong += MangDiemPhongThu[soQuanDich];
            if (soQuanDich > 0)
            {
                diemTong -= MangDiemTanCong[soQuanTa] * 2;
            }

            return diemTong;
        }

        #endregion

        #endregion
    }
}