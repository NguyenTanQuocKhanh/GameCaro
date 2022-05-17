using System;
using System.Drawing;
using System.Windows.Forms;

namespace caro
{
    public partial class frmCoCaro : Form
    {
        private DieuKhienCaro dieukhienCaro;
        private Graphics grs;
       
        

        public frmCoCaro()
        {
            InitializeComponent();
            btnPlayervsPlayer.Click += new EventHandler(PvsP);
            playerVsComToolStripMenuItem.Click += PvsC;
            btnPlayervsCom.Click += PvsC;
            dieukhienCaro = new DieuKhienCaro();
            dieukhienCaro.KhoiTaoMangOco();
            grs = pnlBanCo.CreateGraphics();
        }
        private void frmCoCaro_Load(object sender, EventArgs e)
        {
            
        }

        private void frmCoCaro_Paint(object sender, PaintEventArgs e)
        {
            dieukhienCaro.VeBanCo(grs);
            dieukhienCaro.PhucHoiQuanCo(grs);
        }

        private void PnlBanCo_MouseClick(object sender, MouseEventArgs e)
        {
            if (dieukhienCaro.SanSang)
            {
                if (dieukhienCaro.CheDoChoi==1)
                {
                    dieukhienCaro.DanhCo(e.Location.X, e.Location.Y,grs);
                    dieukhienCaro.KiemTraChienThang();
                }
                else
                {
                    dieukhienCaro.DanhCo(e.Location.X, e.Location.Y, grs);
                    if(!dieukhienCaro.KiemTraChienThang())
                    {
                        dieukhienCaro.KhoiDongComputer(grs);
                        dieukhienCaro.KiemTraChienThang();
                    }
                }
            }
                
                    
            
        }

        private void PvsP(object sender, EventArgs e)
        {
            dieukhienCaro.CheDoChoi = 1;
            grs.Clear(pnlBanCo.BackColor);
            dieukhienCaro.PlayervsPlayer(grs);
        }

        private void btnPlayervsPlayer_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grs.Clear(pnlBanCo.BackColor);
            dieukhienCaro.Undo(grs);
        }

        private void PnlBanCo_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            
            dieukhienCaro.Undo(grs);
            if (dieukhienCaro.CheDoChoi == 2)
                dieukhienCaro.Undo(grs);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            DialogResult mess=  MessageBox.Show("Bạn có muốn thoát game", "Thông báo", MessageBoxButtons.OKCancel);
            if (mess==DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnPlayervsCom_Click(object sender, EventArgs e)
        {

        }
        private void PvsC(object sender, EventArgs e)
        {
            dieukhienCaro.CheDoChoi = 2;
            grs.Clear(pnlBanCo.BackColor);
            dieukhienCaro.PlayerVsCom(grs);
        }
    }
}
