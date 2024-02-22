using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    
    public partial class Form1 : Form
    {
        PictureBox[,] f =  new PictureBox[8,8];
        public static TextBox t; 
        public Form1()
        {
            InitializeComponent();
            initialize();
            view_pm();
            update_status("first_pre");
            t = textBox1;
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    f[i, j].Dock = DockStyle.Fill;
                    f[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    
                    if((i+j)%2==1)
                    {
                        f[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        f[i, j].BackColor = Color.Black;
                        if(i<=2)
                        {
                            f[i, j].Image = imageList1.Images[1];
                        }
                        else if(i>=5)
                        {
                            f[i, j].Image = imageList1.Images[0];
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.game.setStart();
            Program.game.status = "first_pre";
            update();
        }
        
        public void initialize()
        {
            f[0, 0] = pictureBox1;
            f[0, 1] = pictureBox2;
            f[0, 2] = pictureBox3;
            f[0, 3] = pictureBox4;
            f[0, 4] = pictureBox5;
            f[0, 5] = pictureBox6;
            f[0, 6] = pictureBox7;
            f[0, 7] = pictureBox8;
            f[1, 0] = pictureBox9;
            f[1, 1] = pictureBox10;
            f[1, 2] = pictureBox11;
            f[1, 3] = pictureBox12;
            f[1, 4] = pictureBox13;
            f[1, 5] = pictureBox14;
            f[1, 6] = pictureBox15;
            f[1, 7] = pictureBox16;
            f[2, 0] = pictureBox17;
            f[2, 1] = pictureBox18;
            f[2, 2] = pictureBox19;
            f[2, 3] = pictureBox20;
            f[2, 4] = pictureBox21;
            f[2, 5] = pictureBox22;
            f[2, 6] = pictureBox23;
            f[2, 7] = pictureBox24;
            f[3, 0] = pictureBox25;
            f[3, 1] = pictureBox26;
            f[3, 2] = pictureBox27;
            f[3, 3] = pictureBox28;
            f[3, 4] = pictureBox29;
            f[3, 5] = pictureBox30;
            f[3, 6] = pictureBox31;
            f[3, 7] = pictureBox32;
            f[4, 0] = pictureBox33;
            f[4, 1] = pictureBox34;
            f[4, 2] = pictureBox35;
            f[4, 3] = pictureBox36;
            f[4, 4] = pictureBox37;
            f[4, 5] = pictureBox38;
            f[4, 6] = pictureBox39;
            f[4, 7] = pictureBox40;
            f[5, 0] = pictureBox41;
            f[5, 1] = pictureBox42;
            f[5, 2] = pictureBox43;
            f[5, 3] = pictureBox44;
            f[5, 4] = pictureBox45;
            f[5, 5] = pictureBox46;
            f[5, 6] = pictureBox47;
            f[5, 7] = pictureBox48;
            f[6, 0] = pictureBox49;
            f[6, 1] = pictureBox50;
            f[6, 2] = pictureBox51;
            f[6, 3] = pictureBox52;
            f[6, 4] = pictureBox53;
            f[6, 5] = pictureBox54;
            f[6, 6] = pictureBox55;
            f[6, 7] = pictureBox56;
            f[7, 0] = pictureBox57;
            f[7, 1] = pictureBox58;
            f[7, 2] = pictureBox59;
            f[7, 3] = pictureBox60;
            f[7, 4] = pictureBox61;
            f[7, 5] = pictureBox62;
            f[7, 6] = pictureBox63;
            f[7, 7] = pictureBox64;
        }
        public void clicked(int i, int j)
        {
            cell pos = new cell();
            pos.x = i;
            pos.y = j;
            if(Program.game.status=="first_pre")
            {
                foreach (cell go in Program.game.pm)
                {
                    if (go.x == pos.x && go.y == pos.y)
                    {
                        Program.game.move.start = pos;
                        update_status("first_pos");
                        Program.game.update_pm(pos);
                        view_pm();
                        break;
                    }
                }

            }
            else if(Program.game.status=="first_pos")
            {
                foreach(cell go in Program.game.pm)
                {
                    if (go.x == pos.x && go.y == pos.y)
                    {
                        Program.game.move.end = pos;
                        Program.game.move_ch();
                        update();
                        if (Program.game.find_beat_moves(pos).Count != 0 && Program.game.move.type=="beat")
                        {
                            Program.game.update_pm(pos);
                            view_pm();
                            Program.game.move.start = pos;
                        }
                        else
                        {
                            update_status("second_pre");
                            Program.game.set_start_positions(2);
                            view_pm();
                        }
                        break;
                    }
                }
            }
            else if (Program.game.status == "second_pre")
            {
                foreach (cell go in Program.game.pm)
                {
                    if (go.x == pos.x && go.y == pos.y)
                    {
                        Program.game.move.start = pos;
                        update_status("second_pos");
                        Program.game.update_pm(pos);
                        view_pm();
                        break;
                    }
                }
            }
            else if( Program.game.status == "second_pos")
            {
                foreach (cell go in Program.game.pm)
                {
                    if (go.x == pos.x && go.y == pos.y)
                    {
                        Program.game.move.end = pos;
                        Program.game.move_ch();
                        update();
                        if (Program.game.find_beat_moves(pos).Count != 0 && Program.game.move.type=="beat")
                        {
                            Program.game.update_pm(pos);
                            view_pm();
                            Program.game.move.start = pos;
                        }
                        else
                        {
                            update_status("first_pre");
                            Program.game.set_start_positions(1);
                            view_pm();
                        }
                        break;
                    }
                }
            }

        }
        public void view_pm()
        {
            update();
            foreach(cell cell in Program.game.pm)
            {
                if (Program.game.position[cell.x, cell.y] == 1)
                {
                    f[cell.x, cell.y].Image = imageList1.Images[6];
                }
                else if(Program.game.position[cell.x, cell.y] == 2)
                {
                    f[cell.x, cell.y].Image = imageList1.Images[7];
                }
                else if(Program.game.position[cell.x, cell.y] == 3)
                {
                    f[cell.x, cell.y].Image = imageList1.Images[8];
                }
                else if(Program.game.position[cell.x, cell.y] == 4)
                {
                    f[cell.x, cell.y].Image = imageList1.Images[9];
                }
                else
                {
                    f[cell.x, cell.y].Image = imageList1.Images[5];
                }
            }
        }
       
        public void update_status(string str)
        {
            Program.game.status = str;
            if (str == "first_pre" || str == "first_pos")
            {
                textBox1.Text = "Ходят белые";
            }
            else 
            {
                textBox1.Text = "Ходят черные";
            }
        }
        public void endgame()
        {
            Program.game.status = "end";
            t.Text = "end";
        }
        public  void update()
        {
            int w=0;
            int b=0;
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if (Program.game.position[i, j]==0)
                    {
                        f[i, j].Image  = null;
                    }
                    else if(Program.game.position[i, j]==1)
                    {
                        f[i, j].Image = imageList1.Images[1];
                        w++;
                    }
                    else if( Program.game.position[i, j]==2)
                    {
                        f[i, j].Image = imageList1.Images[0];
                        b++;
                    }
                    else if (Program.game.position[i, j]==3)
                    {
                        f[i, j].Image = imageList1.Images[2];
                        w++;
                    }
                    else
                    {
                        f[i, j].Image = imageList1.Images[3];
                        b++;
                    }
                }
            }
            if(b*w==0)
            {
                endgame();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e) { clicked(0, 0); }
        private void pictureBox2_Click(object sender, EventArgs e) { clicked(0, 1); }
        private void pictureBox3_Click(object sender, EventArgs e) { clicked(0, 2); }
        private void pictureBox4_Click(object sender, EventArgs e) { clicked(0, 3); }
        private void pictureBox5_Click(object sender, EventArgs e) { clicked(0, 4); }
        private void pictureBox6_Click(object sender, EventArgs e) { clicked(0, 5); }
        private void pictureBox7_Click(object sender, EventArgs e) { clicked(0, 6); }
        private void pictureBox8_Click(object sender, EventArgs e) { clicked(0, 7); }
        private void pictureBox9_Click(object sender, EventArgs e) { clicked(1, 0); }
        private void pictureBox10_Click(object sender, EventArgs e) { clicked(1, 1); }
        private void pictureBox11_Click(object sender, EventArgs e) { clicked(1, 2); }
        private void pictureBox12_Click(object sender, EventArgs e) { clicked(1, 3); }
        private void pictureBox13_Click(object sender, EventArgs e) { clicked(1, 4); }
        private void pictureBox14_Click(object sender, EventArgs e) { clicked(1, 5); }
        private void pictureBox15_Click(object sender, EventArgs e) { clicked(1, 6); }
        private void pictureBox16_Click(object sender, EventArgs e) { clicked(1, 7); }
        private void pictureBox17_Click(object sender, EventArgs e) { clicked(2, 0); }
        private void pictureBox18_Click(object sender, EventArgs e) { clicked(2, 1); }
        private void pictureBox19_Click(object sender, EventArgs e) { clicked(2, 2); }
        private void pictureBox20_Click(object sender, EventArgs e) { clicked(2, 3); }
        private void pictureBox21_Click(object sender, EventArgs e) { clicked(2, 4); }
        private void pictureBox22_Click(object sender, EventArgs e) { clicked(2, 5); }
        private void pictureBox23_Click(object sender, EventArgs e) { clicked(2, 6); }
        private void pictureBox24_Click(object sender, EventArgs e) { clicked(2, 7); }
        private void pictureBox25_Click(object sender, EventArgs e) { clicked(3, 0); }
        private void pictureBox26_Click(object sender, EventArgs e) { clicked(3, 1); }
        private void pictureBox27_Click(object sender, EventArgs e) { clicked(3, 2); }
        private void pictureBox28_Click(object sender, EventArgs e) { clicked(3, 3); }
        private void pictureBox29_Click(object sender, EventArgs e) { clicked(3, 4); }
        private void pictureBox30_Click(object sender, EventArgs e) { clicked(3, 5); }
        private void pictureBox31_Click(object sender, EventArgs e) { clicked(3, 6); }
        private void pictureBox32_Click(object sender, EventArgs e) { clicked(3, 7); }
        private void pictureBox33_Click(object sender, EventArgs e) { clicked(4, 0); }
        private void pictureBox34_Click(object sender, EventArgs e) { clicked(4, 1); }
        private void pictureBox35_Click(object sender, EventArgs e) { clicked(4, 2); }
        private void pictureBox36_Click(object sender, EventArgs e) { clicked(4, 3); }
        private void pictureBox37_Click(object sender, EventArgs e) { clicked(4, 4); }
        private void pictureBox38_Click(object sender, EventArgs e) { clicked(4, 5); }
        private void pictureBox39_Click(object sender, EventArgs e) { clicked(4, 6); }
        private void pictureBox40_Click(object sender, EventArgs e) { clicked(4, 7); }
        private void pictureBox41_Click(object sender, EventArgs e) { clicked(5, 0); }
        private void pictureBox42_Click(object sender, EventArgs e) { clicked(5, 1); }
        private void pictureBox43_Click(object sender, EventArgs e) { clicked(5, 2); }
        private void pictureBox44_Click(object sender, EventArgs e) { clicked(5, 3); }
        private void pictureBox45_Click(object sender, EventArgs e) { clicked(5, 4); }
        private void pictureBox46_Click(object sender, EventArgs e) { clicked(5, 5); }
        private void pictureBox47_Click(object sender, EventArgs e) { clicked(5, 6); }
        private void pictureBox48_Click(object sender, EventArgs e) { clicked(5, 7); }
        private void pictureBox49_Click(object sender, EventArgs e) { clicked(6, 0); }
        private void pictureBox50_Click(object sender, EventArgs e) { clicked(6, 1); }
        private void pictureBox51_Click(object sender, EventArgs e) { clicked(6, 2); }
        private void pictureBox52_Click(object sender, EventArgs e) { clicked(6, 3); }
        private void pictureBox53_Click(object sender, EventArgs e) { clicked(6, 4); }
        private void pictureBox54_Click(object sender, EventArgs e) { clicked(6, 5); }
        private void pictureBox55_Click(object sender, EventArgs e) { clicked(6, 6); }
        private void pictureBox56_Click(object sender, EventArgs e) { clicked(6, 7); }
        private void pictureBox57_Click(object sender, EventArgs e) { clicked(7, 0); }
        private void pictureBox58_Click(object sender, EventArgs e) { clicked(7, 1); }
        private void pictureBox59_Click(object sender, EventArgs e) { clicked(7, 2); }
        private void pictureBox60_Click(object sender, EventArgs e) { clicked(7, 3); }
        private void pictureBox61_Click(object sender, EventArgs e) { clicked(7, 4); }
        private void pictureBox62_Click(object sender, EventArgs e) { clicked(7, 5); }
        private void pictureBox63_Click(object sender, EventArgs e) { clicked(7, 6); }
        private void pictureBox64_Click(object sender, EventArgs e) { clicked(7, 7); }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
    
}
