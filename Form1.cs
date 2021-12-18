using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private List<CW> CWS;
        public Form1()
        {
            InitializeComponent();
            CWS = new List<CW>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CWS.Clear();
            textBox5.Clear();
            if (radioButton1.Checked == true)
            {
                string gx = textBox1.Text.ToString();
                int k = int.Parse(textBox3.Text.ToString());
                string er = textBox2.Text.ToString();
                string m = textBox4.Text.ToString();
                int i, a, b, E, rb, rg = 0, rm = 0, gx_ = 0, er_ = 0, m_ = 0, cx, mx, n;

                for (i = 0; i < gx.Length; i++)
                {
                    if (gx[i] == '1')
                    {
                        gx_ += pow(2, gx.Length - i - 1);
                        if (rg < gx.Length - i - 1)
                        {
                            rg = gx.Length - i - 1;
                        }
                    }
                }

                for (i = 0; i < er.Length; i++)
                {
                    if (er[i] == '1')
                    {
                        er_ += pow(2, er.Length - i - 1);
                    }
                }
                
                if (k != m.Length)
                {
                    k = m.Length;
                }

                for (i = 0; i < k; i++)
                {
                    if (m[i] == '1')
                    {
                        m_ += pow(2, k - i - 1);
                        if (rm < gx.Length - i - 1)
                        {
                            rm = gx.Length - i - 1;
                        }
                    }
                }

                mx = m_ << rg; //m(x)*x^r
                rm = rang(mx);
                cx = s_cx(mx, rm, gx_, rg);
                a = mx + cx;
                b = a ^ er_;
                rb = rang(b);
                E = s_cx(b, rb, gx_, rg);
                conclusion(E, mx, cx, a, b);

            }
            else
            {
                string gx = textBox1.Text.ToString();
                int k = int.Parse(textBox3.Text.ToString());
                int n = int.Parse(textBox2.Text.ToString());
                int d = int.Parse(textBox4.Text.ToString());


                Form2 f = new Form2();
                f.Owner = this;
                f.ShowDialog();
                string a = textBox5.Text.ToString();

                if (n != a.Length)
                {
                    n = a.Length;
                    k = n - d;
                }

                int m_ = 0, gx_ = 0, rg = 0, rm = 0;

                for (int i = 0; i < gx.Length; i++)
                {
                    if (gx[i] == '1')
                    {
                        gx_ += pow(2, gx.Length - i - 1);
                        if (rg < gx.Length - i - 1)
                        {
                            rg = gx.Length - i - 1;
                        }
                    }
                }

                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == '1')
                    {
                        m_ += pow(2, a.Length - i - 1);
                        if (rm < gx.Length - i - 1)
                        {
                            rm = gx.Length - i - 1;
                        }
                    }
                }

                int s = pow(2, n);
                s--;
                for (int i = gx_; i < s+1; i++)
                {
                    int ri = rang(i);
                    if(ri > rg)
                    {
                        int cx = s_cx(i, ri, gx_, rg);
                        if (cx == 0)
                        {
                            int dec = i;
                            string bin = bina_1(i, n);
                            int wei = weight(bin, bin.Length);
                            CWS.Add(new CW(dec, wei, bin));
                        }
                    }
                    else
                    {
                        int cx = i;
                        cx ^= gx_;
                        if (cx == 0)
                        {
                            int dec = i;
                            string bin = bina(i);
                            int wei = weight(bin, ri);
                            CWS.Add(new CW(dec, wei, bin));
                        }
                    }
                }

                textBox5.AppendText(" - a \r\n");
                textBox5.AppendText("Список всех е, вес которых < d-1\r\n");
                foreach (CW j in CWS)
                {
                    if ((j.weight < d-1) && (m_ != j.dec))
                    {
                        textBox5.AppendText(j.bin + "\r\n");
                    }
                }
            }

        }

        public int weight(string a, int r)
        {
            int sum = 0;
            for (int i = 0; i != r; i++)
            {
                if(a[i] == '1')
                {
                    sum++;
                }
            }
            return (sum);
        }

        public void conclusion(int E, int mx, int cx, int ax, int bx)
        {
            if(checkBox1.Checked == true)
            {
                string m = bina(mx);
                textBox5.AppendText("m(x)*x^r = " + m + "\r\n");
            }
            if (checkBox2.Checked == true)
            {
                string c = bina(cx);
                textBox5.AppendText("c(x) = m(x)*x^r mod g(x) = " + c + "\r\n");
            }
            if (checkBox3.Checked == true)
            {
                string a = bina(ax);
                textBox5.AppendText("a = m(x)*x^r+c(x) = " + a + "\r\n");
            }
            if (checkBox4.Checked == true)
            {
                string b = bina(bx);
                textBox5.AppendText("b = a XOR e = " + b + "\r\n");
            }
            if (E == 0)
            {
                textBox5.AppendText("S = 0 => E = 0, ошибок нет");
            }
            else
            {
                textBox5.AppendText("S = " + E + " => E = 1, есть ошибка");
            }
        }

        public string bina_1(int a, int n)
        {
            string b = "0";
            int r = rang(a);
            for (int i = 0; i < r + 1; i++)
            {
                if (i == 0)
                {
                    if (a % 2 == 1)
                    {
                        b = b.Replace('0', '1');
                    }
                }
                else
                {
                    if (a % 2 == 1)
                    {
                        b += "1";
                    }
                    else
                    {
                        b += "0";
                    }
                }
                a /= 2;
            }
            if (r < n-1)
            {
                for (int i = 0; i < n-r-1; i++)
                {
                    b += "0";
                }
            }
            return (pov(b));
        }

        public string bina(int a)
        {
            string b = "0";
            int r = rang(a);
            for (int i = 0; i < r + 1; i++)
            {
                if (i == 0)
                {
                    if (a % 2 == 1)
                    {
                        b = b.Replace('0', '1');
                    }
                }
                else
                {
                    if (a % 2 == 1)
                    {
                        b += "1";
                    }
                    else
                    {
                        b += "0";
                    }
                }
                a /= 2;
            }
            return (pov(b));
        }

        public string pov(string a)
        {
            int r = a.Length;
            string b = "0";
            for (int i = r; i > 0; i--)
            {
                if (i == r)
                {
                    if (a[i-1] == '1')
                    {
                        b = b.Replace('0', '1');
                    }
                }
                else
                {
                    b += a[i - 1];
                }
            }
            return (b);
        }

        public int rang(int a)
        {
            int sum = 0;
            while(a != 0)
            {
                a /= 2;
                sum++;
            }
            sum--;
            return (sum);
        }

        public int s_cx(int m, int r, int gx, int rx)
        {
            if (r > rx)
            {
                int s = r;
                r -= rx; 
                for (int i = r+1; i != 0; i--)
                {
                    if (s == rx + i - 1)
                    {
                        int gx1 = gx << i - 1;
                        m ^= gx1;
                        s = rang(m);
                    }  
                }
                return (m);
            }
            else
            {
                m ^= gx;
                return (m);
            }
        }

        public int pow(int a, int b)
        {
            int s = 1;
            if (b == 0)
            {
                return (s);
            }
            else
            {
                for (int i = 0; i < b; i++)
                {
                    s *= a;
                }
                return (s);
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton1.Checked = false;
        }
    }
}
