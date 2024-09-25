using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

int n = 100; //depth
double scale = 0.01; //smaller number means more zoom
Point center = new Point(200, 200); //transposes the bitmap to have this point in the center
Point mouse_down = new (0, 0); //use for dragging the image
bool mouse_down_bool = false;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(400, 400);
Color in_set(double x,double y) //checkt voor elk input punt wat het mandel getal is, en output een kleur
{
    x = (x- center.X) *scale; y= (y-center.Y) * scale;
    double a = 0; double b = 0;
    int i = 1;
    while (Math.Sqrt((a - x) * (a - x) + (b - y) * (b - y)) <= 10 && i < n)
    {   double copy_a = a;
        a = a * a - b * b + x;
        b = 2 * copy_a * b + y;
        i++;
    }
    /*
    if (i == n)
        return Color.Black;
    else    
        return Color.FromArgb(255,0, 255-255 * i / n);
    */
    //return Color.FromArgb(Math.Min(25*i,255),0,255-Math.Min(25*i,255));
    /*
    if (i % 2 == 1)
        return Color.White;
    for (int j = 2; j<n; j+=2)
        if (i % j != 0)
            return Color.FromArgb(255-255*j/n, 0,255-255 * j / n);
    return Color.FromArgb(0, 255, 0);
    //*/
    // z/w WERKT   
    if (i % 2 == 0)
        return Color.Black;
    else return Color.White;
    //*/
}
Bitmap plaatje() //maakt bitmap, gebruikt vorige functie om kleur te bepalen
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width, scherm.ClientSize.Height);
    for (double i = 0; i < scherm.ClientSize.Width; i++)
        for (double j = 0; j < scherm.ClientSize.Height; j++)
            plaatje.SetPixel((int) i, (int) j,in_set(i,j));
    return plaatje;
}
void scroll(object o, MouseEventArgs e) //zoom in/uit als je scrollt
{
    center = Point.Subtract(e.Location,new Size(center)); //zou moeten inzoomen op cursor, werkt niet

    if (e.Delta > 0)
    {
        scale = 0.8 * scale;
    }
    else if (e.Delta < 0)
    {
        scale = 1.2 * scale;
    }
    scherm.Invalidate();
}
void mouse_down_drag(object o, MouseEventArgs e) //bewaart de locatie waar je klikt
{   mouse_down_bool = true;
    mouse_down = e.Location;
}
void mouse_up_drag(object o,MouseEventArgs e) //verandert center, gebaseert op het verschil
{ //tussen de locatie waar je klikt en waar je loslaat
    if (mouse_down_bool)
    {
        center = Point.Subtract(center, (Size)Point.Subtract(mouse_down, (Size)e.Location));
        mouse_down_bool = false;
        scherm.Invalidate();
    }
}
void verander_grootte(object o, EventArgs e)
{
    scherm.Invalidate();
}
void teken(object o, PaintEventArgs e)
{
    
    e.Graphics.DrawImage(plaatje(),0,0);
}
scherm.MouseWheel += scroll;
scherm.SizeChanged += verander_grootte;
scherm.MouseDown += mouse_down_drag;
scherm.MouseUp += mouse_up_drag;
scherm.Paint += teken;
Application.Run(scherm);