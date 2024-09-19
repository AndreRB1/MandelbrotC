using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(400, 400);

void teken(object o, PaintEventArgs pea)
    ;
bool distance_less_2(double a,double b,double x,double y)
{
    if (Math.Sqrt((a - x) * (a - x) + (b - y)*(b - y)) < 2)
        return true;
    else return false;
}

scherm.Paint += teken;
Application.Run(scherm);