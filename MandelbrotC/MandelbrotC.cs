using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(400, 400);

bool distance_less_2(double a,double b,double x,double y)
{
    if (Math.Sqrt((a - x) * (a - x) + (b - y)*(b - y)) < 2)
        return true;
    else return false;
}


Application.Run(scherm);