using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap img = new Bitmap(Application.StartupPath + @"\img\fondo2.jpg");
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TablaSubred_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Double RangoIp = Convert.ToDouble(textBox1.Text);
            //************************  PARA LA CLASE A **********************************
            if (RangoIp >= 0 && 128 > RangoIp)
            {
                textMask.Text = "255.0.0.0";
                textClass.Text = "A";
                CargandoSubred();
            }
            //************************  PARA LA CLASE B **********************************
            else if (RangoIp >= 128 && 192 > RangoIp)
            {
                textMask.Text = "255.255.0.0";
                textClass.Text = "B";
                CargandoSubred();
            }
            //************************  PARA LA CLASE C **********************************
            else if (RangoIp >= 192 && 224 > RangoIp)
            {
                textMask.Text = "255.255.255.0";
                textClass.Text = "C";
                CargandoSubred();
            }
            else if (RangoIp >= 224)
            {
                MessageBox.Show("Numero Fuera de Rango de Ip", " Ip de red", MessageBoxButtons.OKCancel);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textSub.Text = "";
            }
            //************************** CARGANDO LAS SUBREDES EN LA TABLA ****************************
        }
        public void CargandoSubred()
        {
            //***** numero de subred total: 0,2,6,14,30,62 --> (2^i )- 2 = resultado
            Double NumSubredTotal = 0;
            Double CantSubred = 0;
            Double NumSubred = 0;
            int SubredIngresada = Convert.ToInt32(textSub.Text);
            for (int i = 1; i < 9; i++)
            {
                //******************TOTAL DE SUBRED *****************
                NumSubredTotal = (Math.Pow(2, i) - 2);
                if (SubredIngresada <= NumSubredTotal)
                {
                    //************* cantidad de bits utilizados *******************
                    CantSubred = Math.Pow(2, 8 - i);
                    NumSubred = Math.Pow(2, i);

                    listarId_Red(CantSubred, NumSubred);
                    listarId_Broadcast(NumSubred, CantSubred);
                    listarIp_Configurable(NumSubred, CantSubred);
                    pintarSubred(SubredIngresada);
                    break;
                }
            }
        }

        //************************** LISTANDO LOS DATOS DE LA ID_RED ****************************

        Double conthost1;
        Double conthost2;
        Double conthost3;
        String ipRed = "";
        public void listarId_Red(Double CatSubred, Double NumSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;
            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA CLASE A *************************
                if (textClass.Text.Equals("A"))
                {
                    ipRed = textBox1.Text + "." + Convert.ToString(conthost1) + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3);
                    conthost1 = conthost1 + CatSubred;
                }
                //************************PARA LA CLASE B *************************
                else if (textClass.Text.Equals("B"))
                {
                    ipRed = textBox1.Text + "." + textBox2.Text + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3);
                    conthost2 = conthost2 + CatSubred;
                }
                //************************PARA LA CLASE C *************************
                else if (textClass.Text.Equals("C"))
                {
                    ipRed = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + Convert.ToString(conthost3);
                    conthost3 = conthost3 + CatSubred;
                }

                TablaSubred.Rows.Add();
                TablaSubred.Rows[i].Cells[0].Value = i;
                TablaSubred.Rows[i].Cells[1].Value = ipRed;

            }
        }

        ////************************** LISTANDO LOS DATOS DE LA RANGO_IP_CONFIGURABLE ****************************

        String ipRed1 = "";
        String ipRed2 = "";
        public void listarIp_Configurable(Double NumSubred, Double CatSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;

            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA CLASE A *************************
                if (textClass.Text.Equals("A"))
                {
                    ipRed1 = textBox1.Text + "." + Convert.ToString(conthost1) + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3 + 1);
                    conthost1 = conthost1 + CatSubred;
                    ipRed2 = textBox1.Text + "." + Convert.ToString(conthost1 - 1) + "." + Convert.ToString(conthost2 + 255) + "." + Convert.ToString((conthost3 + 255) - 1);
                }
                //************************PARA LA CLASE B *************************
                else if (textClass.Text.Equals("B"))
                {
                    ipRed1 = textBox1.Text + "." + textBox2.Text + "." + Convert.ToString(conthost2) + "." + Convert.ToString(conthost3 + 1);
                    conthost2 = conthost2 + CatSubred;
                    //contultimo = contultimo + CatSubred;
                    ipRed2 = textBox1.Text + "." + textBox2.Text + "." + Convert.ToString(conthost2 - 2) + "." + Convert.ToString((conthost3 + 255) - 1);
                }
                //************************PARA LA CLASE C *************************
                else if (textClass.Text.Equals("C"))
                {
                    ipRed1 = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + Convert.ToString(conthost3 + 1);
                    conthost3 = conthost3 + CatSubred;
                    ipRed2 = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + Convert.ToString(conthost3 - 2);
                }

                String ipRangoRed = ipRed1 + " -- " + ipRed2;
                TablaSubred.Rows.Add();
                TablaSubred.Rows[i].Cells[2].Value = ipRangoRed;

            }

        }

        //************************** LISTANDO LOS DATOS DE LA ID_BROADCAST ****************************

        public void listarId_Broadcast(Double NumSubred, Double CatSubred)
        {
            conthost1 = 0;
            conthost2 = 0;
            conthost3 = 0;
            for (int i = 0; i < NumSubred; i++)
            {
                //************************PARA LA CLASE A *************************
                if (textClass.Text.Equals("A"))
                {
                    conthost1 = conthost1 + CatSubred;
                    ipRed = textBox1.Text + "." + Convert.ToString(conthost1 - 1) + "." + Convert.ToString(conthost3 + 255) + "." + Convert.ToString(conthost3 + 255);
                }
                //************************PARA LA CLASE B *************************
                else if (textClass.Text.Equals("B"))
                {
                    conthost2 = conthost2 + CatSubred;
                    ipRed = textBox1.Text + "." + textBox2.Text + "." + Convert.ToString(conthost2 - 1) + "." + Convert.ToString(conthost3 + 255);
                }
                //************************PARA LA CLASE C *************************
                else if (textClass.Text.Equals("C"))
                {
                    conthost3 = conthost3 + CatSubred;
                    ipRed = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + Convert.ToString(conthost3 - 1);
                }
                TablaSubred.Rows.Add();
                TablaSubred.Rows[i].Cells[3].Value = ipRed;

            }
        }

        //************************** PINTANDO LA CANTIDAD DE SUBREDES DE LA TABLA ****************************

        public void pintarSubred(double subred)
        {
            for (int i = 0; i < subred; i++)
            {
                // TablaSubred.Rows[i + 1].DefaultCellStyle.Equals(BackColor = Color.Red);
                TablaSubred.Rows[i + 1].DefaultCellStyle.BackColor = Color.Orange;
                //TablaSubred.RowsDefaultCellStyle.BackColor = Color.Blue;
            }
        }





    }
}

