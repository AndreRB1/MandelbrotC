using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

int n = 100; //depth
double scale = 0.01;
Point center = new Point(200, 200);
Point new_center = new Point(200, 200);
Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(400, 400);

Color in_set(double x,double y)
{
    x = (x- scherm.ClientSize.Width*0.5 + 1.5*(new_center.X - center.X)) *scale;y= (y- scherm.ClientSize.Height*0.5 + 1.5*(new_center.Y - center.Y)) * scale;
    double a = 0; double b = 0;
    int i = 0;
    while (Math.Sqrt((a - x) * (a - x) + (b - y) * (b - y)) <= 2 && i < n)
    {
        double copy_a = a;
        a = a * a - b * b + x;
        b = 2 * copy_a * b + y;
        i++;
    }
    if (i % 2 == 0)
        return Color.Black;
    else return Color.White;
}
void klik(object o, MouseEventArgs e)
{
    new_center = e.Location;
    if (e.Button == MouseButtons.Left)
        scale = 0.7 * scale;
    else
    {
        scale = 1.4 * scale;
    }
    scherm.Invalidate();
}
void verander_grootte(object o, EventArgs e)
{
    scherm.Invalidate();
}
void teken(object o, PaintEventArgs e)
{
    e.Graphics.DrawImage(plaatje(),0,0);
    center.X = scherm.ClientSize.Width/2; center.Y = scherm.ClientSize.Height / 2;
}
Bitmap plaatje()
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width, scherm.ClientSize.Height);
    for (double i = 0; i < scherm.ClientSize.Width; i++)
        for (double j = 0; j < scherm.ClientSize.Height; j++)
            plaatje.SetPixel((int) i, (int) j,in_set(i,j));
    return plaatje;
}

scherm.SizeChanged += verander_grootte;
scherm.MouseClick += klik;  
scherm.Paint += teken;
Application.Run(scherm);