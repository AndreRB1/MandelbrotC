using System;
using System.Drawing;
using System.Windows.Forms;


int n = 100; //depth
double scale = 0.01; //smaller number means more zoom
Point center = new Point(300, 200); //transposes the bitmap to have this point in the center
Point mouse_down = new (0, 0); //use for dragging the image

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.White;
scherm.ClientSize = new Size(600, 400);
Label labx = new Label(); Label laby = new Label();Label labscale = new Label();
TextBox center_x = new TextBox(); 
TextBox center_y = new TextBox();
TextBox scale_in = new TextBox();
Button knop = new Button();
scherm.Controls.Add(labx);scherm.Controls.Add(laby);scherm.Controls.Add(labscale);
scherm.Controls.Add(center_x);scherm.Controls.Add(center_y);scherm.Controls.Add(scale_in);scherm.Controls.Add(knop);
labx.Location = new Point(6, 6); labx.Size = new Size(30, 30); labx.Text = "x:";
center_x.Location = new Point(46, 6);center_x.Size = new Size(45, 20); center_x.Text = "300";
laby.Location = new Point(91, 6); laby.Size = new Size(30, 20); laby.Text = "y:";
center_y.Location = new Point(126, 6);center_y.Size = new Size(45, 20); center_y.Text = "200";
labscale.Location = new Point(6, 30); labscale.Size = new Size(41, 20); labscale.Text = "schaal:";
scale_in.Location = new Point(46, 26); scale_in.Size = new Size(80, 20); scale_in.Text = "0.01";
knop.Location = new Point(126, 26); knop.Size = new Size(80, 20); knop.Text = "bereken";



Color in_set(double x,double y) //checkt voor elk input punt wat het mandel getal is, en output een kleur
{
    x = ((x- center.X) *scale); y= ((y-center.Y) *scale);
    double a = 0; double b = 0;
    int i = 1;
    while (Math.Sqrt((a - x) * (a - x) + (b - y) * (b - y)) <= 4 && i < n)
    {
        double copy_a = a;
        a = a * a - b * b + x;
        b = 2 * copy_a * b + y;
        i++;
    }

    int red = Convert.ToInt32(255 * i * 2 / n);
    if (red > 255) red = 255;
    int green = Convert.ToInt32(255 * i * 3 / n);
    if (green > 255) green = 255;
    int blue =  Convert.ToInt32(255 * i * 4 / n);
    if (blue > 255) blue = 255;

    return Color.FromArgb(255 - red, 255 - green, 255 - blue);

    // Ik heb alle berekeningen buiten FromArgb gebracht. Deze neemt alleen ints dus bijv 255 * 0,7 werkt niet.
    // De 255, 255, 255 is wit dus ik heb het geinvert. De formules erboven geven altijd een waarde tussen 0 en 255
    // de kleuren kunnen niet boven 255, dus kan vermenigvuldigd worden voor ander kleurenspectrum.
    // Focussen op zoom functie, uitgezoomed zinn er simpelweg weinig kleuren te zien.

}

Bitmap plaatje() //maakt bitmap, gebruikt vorige functie om kleur te bepalen
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width, scherm.ClientSize.Height-40);
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
    mouse_down = e.Location;
}
void mouse_up_drag(object o,MouseEventArgs e) //verandert center, gebaseert op het verschil
{ //tussen de locatie waar je klikt en waar je loslaat
    if (mouse_down != new Point(0, 0))
    {
        center = Point.Subtract(center, (Size)Point.Subtract(mouse_down, (Size)e.Location));
        mouse_down = new Point(0, 0);
        scherm.Invalidate();
    }
}
void verander_grootte(object o, EventArgs e)
{
    scherm.Invalidate();
}
void teken(object o, PaintEventArgs e)
{
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

knop.Click += bereken;
scherm.MouseWheel += scroll;
scherm.SizeChanged += verander_grootte;
scherm.MouseDown += mouse_down_drag;
scherm.MouseUp += mouse_up_drag;
scherm.Paint += teken;
Application.Run(scherm);