//scroll to zoom, drag to move

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Numerics;


double n = 1000; //depth

double scale = 0.01; //smaller number means more zoom
Point center = new Point(300, 174); //transposes the bitmap to have this point in the center
Point mouse_down = new (0, 0); //use for dragging the image
bool mouse_down_bool = false;
bool Cobool = true;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(600, 400);

Label labx = new Label();
Label laby = new Label();
Label labscale = new Label();
Label labInnerColor = new Label();
Label labOuterColor = new Label();
Label LabDepth = new Label();
Label labSmooth = new Label();
TextBox center_x = new TextBox();
TextBox center_y = new TextBox();
TextBox scale_in = new TextBox();
TextBox depth = new TextBox();
Button knop = new Button();
Button innercolor = new Button();
Button outercolor = new Button();
Button CE = new Button();
Button Pbasis = new Button();
Button P1 = new Button();
Button P2 = new Button();
CheckBox smoothening = new CheckBox();
Panel plaatjes = new Panel();

Color inrclr = Color.Black;
Color outrclr = Color.White;
List<Color> colors = gen_palette(inrclr, outrclr);

GenControls();
void GenControls()
{//***LABELS***
    //label for x coord
    scherm.Controls.Add(labx);
    labx.Location = new Point(6, 6);
    labx.Size = new Size(30, 30);
    labx.Text = "x:";
    //label for y coord
    scherm.Controls.Add(laby);
    laby.Location = new Point(91, 6);
    laby.Size = new Size(30, 20);
    laby.Text = "y:";
    //label for scale
    scherm.Controls.Add(labscale);
    labscale.Location = new Point(6, 30);
    labscale.Size = new Size(41, 20);
    labscale.Text = "schaal:";
    //label for displaying inner color
    scherm.Controls.Add(labInnerColor);
    labInnerColor.Location = new Point(286, 6);
    labInnerColor.Size = new Size(20, 20);
    labInnerColor.BackColor = inrclr;
    //label for displaying outer color
    scherm.Controls.Add(labOuterColor);
    labOuterColor.Location = new Point(286, 26);
    labOuterColor.Size = new Size(20, 20);
    labOuterColor.BackColor = outrclr;
    //label for depth
    scherm.Controls.Add(LabDepth);
    LabDepth.Location = new Point(306, 6);
    LabDepth.Size = new Size(41, 20);
    LabDepth.Text = "depth:";
    //label for smoothening
    scherm.Controls.Add(labSmooth);
    labSmooth.Location = new Point(306, 30);
    labSmooth.Size = new Size(80, 20);
    labSmooth.Text = "smoothening:";
    
    //***TEXTBOXES***
    //textbox for x coord
    scherm.Controls.Add(center_x);
    center_x.Location = new Point(46, 6);
    center_x.Size = new Size(45, 20);
    center_x.Text = "300";
    //textbox for y coord
    scherm.Controls.Add(center_y);
    center_y.Location = new Point(126, 6);
    center_y.Size = new Size(45, 20);
    center_y.Text = "200";
    //textbox for scale
    scherm.Controls.Add(scale_in);
    scale_in.Location = new Point(46, 26);
    scale_in.Size = new Size(80, 20);
    scale_in.Text = "0.01";
    //textbox for max depth
    scherm.Controls.Add(depth);
    depth.Location = new Point(347, 6);
    depth.Size = new Size(60, 20);
    depth.Text = $"{n}";

 //***BUTTONS***
    //button for calculating with given input
    scherm.Controls.Add(knop);
    knop.Location = new Point(126, 26);
    knop.Size = new Size(80, 20);
    knop.Text = "bereken";
    //button for selecting the inner color
    scherm.Controls.Add(innercolor);
    innercolor.Location = new Point(206, 6); 
    innercolor.Size = new Size(80, 20); 
    innercolor.Text = "kleur 1";
    //button for selecting outer color
    scherm.Controls.Add(outercolor);
    outercolor.Location = new Point(206, 26);
    outercolor.Size = new Size(80, 20);
    outercolor.Text = "kleur 2";

    //Chechbox for turning on smoothening
    scherm.Controls.Add(smoothening);
    smoothening.Location = new Point(386, 30);
    smoothening.Size = new Size(20, 20);
    smoothening.CheckState = (CheckState) 1;

    //panel for interesting points, including starting point
    scherm.Controls.Add(plaatjes);
    plaatjes.Bounds = new Rectangle(new Point(407, 6), new Size(180, scherm.Height));
    plaatjes.BackColor = Color.Transparent;
    plaatjes.BringToFront();
    CE.Bounds = new Rectangle(new Point(0, 2), new Size(80, 20));//button for collapsing and expanding
    CE.Text = "˅   Punten";
    Pbasis.Bounds = new Rectangle(new Point(0, 22), new Size(80, 20));//base point
    Pbasis.Text = "Basispunt";
    Pbasis.BackColor = Color.White;
    P1.Bounds = new Rectangle(new Point(0, 42), new Size(80, 20));
    P1.Text = "Punt 1";
    P1.BackColor = Color.White;
    P2.Bounds = new Rectangle(new Point(0, 62), new Size(80, 20));
    P2.Text = "Punt 2";
    P2.BackColor = Color.White;
    plaatjes.Controls.Add(CE);

}


List<Color> gen_palette(Color zero, Color outer)//create color palatte
{
    List<Color> colors = new List<Color>();
    for (double i = 0; i <= n; i++)
    {   // gradient from color zero to color outer with n steps
        byte r = (byte)(zero.R * (i / n) + outer.R * (1 - i / n));
        byte g = (byte)(zero.G * (i / n) + outer.G * (1 - i / n));
        byte b = (byte)(zero.B * (i / n) + outer.B * (1 - i / n));
        Color colori = Color.FromArgb(r, g, b);
        colors.Add(colori);
    }
    return colors;
}


int MandelNum(double x, double y) //checkt voor elk input punt wat het mandel getal is
{
    x = ((x - center.X) * scale); y = ((y - center.Y) * scale);
    double a = 0; double b = 0;
    double a_sq = 0; double b_sq = 0; double ab_sq = 0; //using variables for the squares of a, b and (a+b) uses less multiplications per loop cycle
    int i = 0;
    while (a_sq + b_sq <= 4 && i < n)
    {
        ab_sq = (a + b) * (a + b);
        a_sq = a * a;
        b_sq = b * b;
        a = a_sq - b_sq + x;
        b = ab_sq - a_sq - b_sq + y;//(a+b)^2-a^2-b^2 = 2ab
        i++;
    }
    return i;
}


Color SmoothClr(double x,double y) //geeft smooth kleuren
{
    x = ((x- center.X) * scale); y= ((y-center.Y) * scale);
    double a = 0; double b = 0;
    double a_sq = 0; double b_sq = 0; double ab_sq = 0; //using variables for the squares of a, b and (a+b) uses less multiplications per loop cycle
    double i = 0;
    double l = 0, t = 0;
    double m = 0;
    while (t <= 4 && i < n)
    {
        a = a_sq - b_sq + x;
        b = ab_sq - a_sq- b_sq + y;//(a+b)^2-a^2-b^2 = 2ab
        ab_sq = (a + b) * (a + b);
        a_sq = a * a;
        b_sq = b * b;
        t = (a_sq + b_sq);
        i++;
        if (t >= 4)
        {
            m = i + ((4.0 - l) / (t - l));
            break;
        }
        l = t;
    }
    
    double o = inrclr.R * (1 - m / n) + outrclr.R * (m / n);
    double p = inrclr.G * (1 - m / n) + outrclr.G * (m / n);
    double q = inrclr.B * (1 - m / n) + outrclr.B * (m / n);
    /*for (int j = 0; j < 100;j++)
    {
        if (m < ()) ;
    }
    if (m < (n * 0.3))
    {
        o *= 0.3;
        p *= 0.3;
        q *= 0.3;
    }
    else if (m < n * 0.6) 
    {
        o *= 0.6;
        p *= 0.6;
        q *= 0.6;
    }

    */

    return Color.FromArgb((byte) o, (byte) p, (byte) q);
}


Bitmap plaatje() //maakt bitmap
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width, scherm.ClientSize.Height-52);
    if (smoothening.CheckState == (CheckState)1)
    {
        for (double i = 0; i < scherm.ClientSize.Width; i++)
            for (double j = 0; j < scherm.ClientSize.Height - 52; j++)
                plaatje.SetPixel((int)i, (int)j, SmoothClr(i, j));
        return plaatje;
    }
    else
    {
        for (double i = 0; i < scherm.ClientSize.Width; i++)
            for (double j = 0; j < scherm.ClientSize.Height - 52; j++)
                plaatje.SetPixel((int)i, (int)j, colors[MandelNum(i, j)]);
        return plaatje;
    }
}



void zoom(object o, MouseEventArgs e) //zoom in/uit als je scrollt
{
    double dmoveX = 0;
    double dmoveY = 0;
    int moveX;
    int moveY;
    

    if (e.Delta > 0)
    {
        scale *= 0.5;
        dmoveX = center.X - (e.Location.X - center.X);
        dmoveY = center.Y - (e.Location.Y - 52 - center.Y);
    }
    else
    {
        scale *= 2.0;
        dmoveX = center.X - (center.X - e.Location.X) * 0.5;
        dmoveY = center.Y - (center.Y - e.Location.Y +52) * 0.5;
    }

    moveX = Convert.ToInt32(dmoveX);
    moveY = Convert.ToInt32(dmoveY);
    center = new Point(moveX, moveY);

    scherm.Invalidate();
}


void mouse_down_drag(object o, MouseEventArgs e) //bewaart de locatie waar je klikt
{
    if (e.Location.Y > 52)
    {
        mouse_down = e.Location;
        mouse_down_bool = true;
    }
}


void mouse_up_drag(object o,MouseEventArgs e)   //verandert center, gebaseert op het verschil
{                                               //tussen de locatie waar je klikt en waar je loslaat
    if (mouse_down_bool)
    {
        center = Point.Subtract(center, (Size)Point.Subtract(mouse_down, (Size)e.Location));
        mouse_down_bool = false;
        scherm.Invalidate();
    }
}


void teken(object o, PaintEventArgs e)
{
    depth.Text = n.ToString();
    scale_in.Text = scale.ToString("E");
    center_x.Text = ((0.5 * scherm.Width - center.X) * scale).ToString("F4");
    center_y.Text = ((0.5 * (scherm.Height-52) - center.Y) * scale).ToString("F4");
    e.Graphics.DrawImage(plaatje(),0,52);
}


void bereken(object o, EventArgs e)
{
    try
    {
        n = double.Parse(depth.Text);
        colors = gen_palette(inrclr, outrclr);
        scale = double.Parse(scale_in.Text);
        center = new Point((int)(0.5 * scherm.Width - double.Parse(center_x.Text)/scale),(int)(0.5 * (scherm.Height - 52) - double.Parse(center_y.Text)/scale));
        scherm.Invalidate();
    }
    catch (Exception ex)
    {
        DialogResult result = MessageBox.Show(ex.Message, "Er is iets fout gegaan.",  MessageBoxButtons.OK);
    }
}


void verander_kleur(object o, EventArgs e)
{
    ColorDialog dlg = new ColorDialog();
    dlg.ShowDialog();
    if (o == innercolor)
    {
        inrclr = dlg.Color;
        labInnerColor.BackColor = inrclr;
    }
    else
    { 
        outrclr = dlg.Color;
        labOuterColor.BackColor = outrclr;
    }
    //colors = gen_palette(inrclr, outrclr);
    scherm.Invalidate();
}


void redraw(object o, EventArgs e)
{
    scherm.Invalidate();
}


void CoEx(object o, EventArgs e)
{
    if (Cobool)
    {
        plaatjes.Controls.Add(Pbasis);
        plaatjes.Controls.Add(P1);
        plaatjes.Controls.Add(P2);
        Cobool = false;
    }
    else
    {
        plaatjes.Controls.Remove(Pbasis);
        plaatjes.Controls.Remove(P1);
        plaatjes.Controls.Remove(P2);
        Cobool = true;
    }
}


void punt(object o, EventArgs e)
{
    if (o == Pbasis)
    {
        center.X = scherm.ClientSize.Width / 2;
        center.Y = (scherm.ClientSize.Height - 52) / 2;
        scale = 0.01;
        n = 100;
        scherm.Invalidate();
    }
    else if (o == P1)
    {
        n = 1000;
        center_x.Text = (-0.7829).ToString();
        center_y.Text = 0.1303.ToString();
        scale_in.Text = (2.824752E-6).ToString();
        bereken(knop, EventArgs.Empty);
    }
    else if (o == P2)
    {
        n = 1000;
        center_x.Text = (0.3123).ToString();
        center_y.Text = 0.0261.ToString();
        scale_in.Text = (2.4E-7).ToString();
        bereken(knop, EventArgs.Empty);
    }
}


P2.Click += punt;
P1.Click += punt;
Pbasis.Click += punt;
CE.Click += CoEx;
smoothening.CheckedChanged += redraw;
innercolor.Click += verander_kleur;
outercolor.Click += verander_kleur;
knop.Click += bereken;
scherm.MouseWheel += zoom;
scherm.MouseDown += mouse_down_drag;
scherm.MouseUp += mouse_up_drag;
scherm.Paint += teken;

Application.Run(scherm);