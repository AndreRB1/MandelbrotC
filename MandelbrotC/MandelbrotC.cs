using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


double n = 100; //depth
double scale = 0.01; //smaller number means more zoom
Point center = new Point(300, 200); //transposes the bitmap to have this point in the center
Point mouse_down = new (0, 0); //use for dragging the image
bool mouse_down_bool = false;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(600, 400);

//***LABELS***
//label for x coord
Label labx = new Label(); 
scherm.Controls.Add(labx);
labx.Location = new Point(6, 6); 
labx.Size = new Size(30, 30); 
labx.Text = "x:";
//label for y coord
Label laby = new Label();
scherm.Controls.Add(laby);
laby.Location = new Point(91, 6); 
laby.Size = new Size(30, 20); 
laby.Text = "y:";
//label for scale
Label labscale = new Label();
scherm.Controls.Add(labscale);
labscale.Location = new Point(6, 30); 
labscale.Size = new Size(41, 20); 
labscale.Text = "schaal:";

//***TEXTBOXES***
//textbox for x coord
TextBox center_x = new TextBox();
scherm.Controls.Add(center_x);
center_x.Location = new Point(46, 6);
center_x.Size = new Size(45, 20); 
center_x.Text = "300";
//textbox for y coord
TextBox center_y = new TextBox();
scherm.Controls.Add(center_y);
center_y.Location = new Point(126, 6); 
center_y.Size = new Size(45, 20); 
center_y.Text = "200";
//textbox for scale
TextBox scale_in = new TextBox();
scherm.Controls.Add(scale_in);
scale_in.Location = new Point(46, 26); 
scale_in.Size = new Size(80, 20); 
scale_in.Text = "0.01";

//***BUTTONS***
//button for calculating with given input
Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(126, 26); 
knop.Size = new Size(80, 20); 
knop.Text = "bereken";
//button for selecting the inner color
Button innercolor = new Button();
scherm.Controls.Add(innercolor);
innercolor.Location = new Point(206, 6); innercolor.Size = new Size(80, 20); innercolor.Text = "kleur 1";
//button for selecting outer color
Button outercolor = new Button();
scherm.Controls.Add(outercolor);
outercolor.Location = new Point(206, 26); 
outercolor.Size = new Size(80, 20); 
outercolor.Text = "kleur 2";
//generate a starting list of colors
Color inrclr = Color.Black;
Color outrclr = Color.White;
List<Color> colors = gen_palette(inrclr, outrclr);
List<Color> gen_palette(Color zero, Color outer)//create color palatte
{
    List<Color> colors = new List<Color>();
    for (double i = 0; i <= n; i++)
    {
        // gradient from color zero to color outer with n steps
        byte r = (byte)(zero.R * (i / n) + outer.R * (1 - i / n));
        byte g = (byte)(zero.G * (i / n) + outer.G * (1 - i / n));
        byte b = (byte)(zero.B * (i / n) + outer.B * (1 - i / n));
        //*/
        Color colori = Color.FromArgb(r, g, b);
        colors.Add(colori);
        
    }
    return colors;
}
Color in_set(double x,double y) //checkt voor elk input punt wat het mandel getal is, en output een kleur
{
    
    x = ((x- center.X) *scale); y= ((y-center.Y) *scale);
    double a = 0; double b = 0;
    double a_sq = 0; double b_sq = 0; double ab_sq = 0;
    int i = 0;
    while (a_sq + b_sq <= 4 && i < n)
    {
        ab_sq = (a + b) * (a + b);
        a_sq = a * a; 
        b_sq = b*b;
        a = a_sq - b_sq + x;
        b = ab_sq - a_sq- b_sq + y;
        i++;
    }
    return colors[i];
    /*
    int red = Convert.ToInt32(255 * i * 2 / n);
    if (red > 255) red = 255;
    int green = Convert.ToInt32(255 * i * 3 / n);
    if (green > 255) green = 255;
    int blue =  Convert.ToInt32(255 * i * 4 / n);
    if (blue > 255) blue = 255;
    
    return Color.FromArgb(0, 0,255 - blue);
    */
    // Ik heb alle berekeningen buiten FromArgb gebracht. Deze neemt alleen ints dus bijv 255 * 0,7 werkt niet.
    // De 255, 255, 255 is wit dus ik heb het geinvert. De formules erboven geven altijd een waarde tussen 0 en 255
    // de kleuren kunnen niet boven 255, dus kan vermenigvuldigd worden voor ander kleurenspectrum.
    // Focussen op zoom functie, uitgezoomed zinn er simpelweg weinig kleuren te zien.

}
Bitmap plaatje() //maakt bitmap, gebruikt vorige functie om kleur te bepalen
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width, scherm.ClientSize.Height-52);
    for (double i = 0; i < scherm.ClientSize.Width; i++)
        for (double j = 0; j < scherm.ClientSize.Height-52; j++)
            plaatje.SetPixel((int) i, (int) j,in_set(i,j));
    return plaatje;
}
void scroll(object o, MouseEventArgs e) //zoom in/uit als je scrollt
{
    center = Point.Subtract(center,new Size(Point.Subtract(e.Location,new Size(center))));
    if (e.Delta > 0)
    {
        scale *=0.7;
    }
    else if (e.Delta < 0)
    {
        scale *= 1.3;
    }
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
    scale_in.Text = scale.ToString();
    center_x.Text = center.X.ToString();
    center_y.Text = center.Y.ToString();
    e.Graphics.DrawImage(plaatje(),0,52);
}
void bereken(object o, EventArgs e)
{
    try
    {
        center = new Point(int.Parse(center_x.Text), int.Parse(center_y.Text));
        scale = double.Parse(scale_in.Text);
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
        innercolor.BackColor = inrclr;
        
    }
    else
    { 
        outrclr = dlg.Color;
        outercolor.BackColor = outrclr;
        
    }
    colors = gen_palette(inrclr, outrclr);
    scherm.Invalidate();
}

innercolor.Click += verander_kleur;
outercolor.Click += verander_kleur;
knop.Click += bereken;
scherm.MouseWheel += scroll;
scherm.SizeChanged += verander_grootte;
scherm.MouseDown += mouse_down_drag;
scherm.MouseUp += mouse_up_drag;
scherm.Paint += teken;
Application.Run(scherm);