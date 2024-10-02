//drag to move, scroll to zoom
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

double n = 100; //depth

bool s11, s12, s13, s21, s22, s23, s24, s25, s26, s31, s32, s33, s34, s35, s36, s41, s42, s43, s44, s45, s46, s51, s52, s53, s54, s55, s56, s61, s62, s63, s64, s65, s66, s71, s72, s73, s74, s75, s76;
s11 = s12 = s13 = s21 = s23 = s25 = s31 = s33 = s35 = s41 = s43 = s45 = s51 = s53 = s55 = s61 = s63 = s65 = s71 = s73 = s75 = false;
s22 = s24 = s26= s32 = s34 = s36 = s42 = s44 = s46 = s52 = s54 = s56 = s62 = s64 = s66 = s72 = s74 = s76 = true;

bool f128n1, f128n2, f128n3, f128n4, f128n5, f128n6, f128n7, f128n8, f128n9, f128n10, f128n11, f128n12, f128n13, f128n14, f128n15, f128n16, f128n17, f128n18, f128n19, f128n20, f128n21;
f128n1 = f128n2 = f128n3 = f128n4 = f128n5 = f128n6 = f128n7 = f128n8 = f128n9 = f128n10 = f128n11 = f128n12 = f128n13 = f128n14 = f128n15 = f128n16 = f128n17 = f128n18 = f128n19 = f128n20 = f128n21 = false;

double scale = 0.01; //smaller number means more zoom
Point center = new Point(300, 200); //transposes the bitmap to have this point in the center
Point mouse_down = center; //use for dragging the image
bool mouse_down_bool = false;

Form scherm = new Form();
scherm.Text = "MandelbrotC";
scherm.BackColor = Color.LightGray;
scherm.ClientSize = new Size(800, 400);

Label labx = new Label();
Label laby = new Label();
Label labscale = new Label();
Label labInnerColor = new Label();
Label labOuterColor = new Label();
Label LabDepth = new Label();
Label labSmooth = new Label();
Label kleur = new Label();
TextBox center_x = new TextBox();
TextBox center_y = new TextBox();
TextBox scale_in = new TextBox();
TextBox depth = new TextBox();
Button knop = new Button();
Button innercolor = new Button();
Button outercolor = new Button();
CheckBox smoothening = new CheckBox();
ComboBox plaatjes = new ComboBox();
ComboBox kleurpre = new ComboBox();
Panel ColorNSmooth = new Panel();
Panel ColorSmooth = new Panel();

//color buttons + text
Button br1 = new Button();
Button br2 = new Button();
Button br3 = new Button();
Button br4 = new Button();
Button br5 = new Button();
Button br6 = new Button();
Button br7 = new Button();
Button bg1 = new Button();
Button bg2 = new Button();
Button bg3 = new Button();
Button bg4 = new Button();
Button bg5 = new Button();
Button bg6 = new Button();
Button bg7 = new Button();
Button bb1 = new Button();
Button bb2 = new Button();
Button bb3 = new Button();
Button bb4 = new Button();
Button bb5 = new Button();
Button bb6 = new Button();
Button bb7 = new Button();
Label Color1 = new Label();
Label Color2 = new Label();
Label Color3 = new Label();
Label Color4 = new Label();
Label Color5 = new Label();
Label Color6 = new Label();
Label Color7 = new Label();
Label R = new Label();
Label G = new Label(); 
Label B = new Label();


Color inrclr = Color.Black;
Color outrclr = Color.White;
List<Color> colors = gen_palette(inrclr, outrclr);

GenControls();
void GenControls()
{//***LABELS***
    //label for x coord
    scherm.Controls.Add(labx);
    labx.Location = new Point(6, 6);
    labx.Size = new Size(30, 20);
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
    labInnerColor.Location = new Point(80, 0);
    labInnerColor.Size = new Size(20, 20);
    labInnerColor.BackColor = inrclr;
    //label for displaying outer color
    labOuterColor.Location = new Point(80, 20);
    labOuterColor.Size = new Size(20, 20);
    labOuterColor.BackColor = outrclr;
    //label for depth
    scherm.Controls.Add(LabDepth);
    LabDepth.Location = new Point(6, 50);
    LabDepth.Size = new Size(41, 20);
    LabDepth.Text = "depth:";
    //label for smoothening
    scherm.Controls.Add(labSmooth);
    labSmooth.Location = new Point(106, 70);
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
    scale_in.Location = new Point(46, 30);
    scale_in.Size = new Size(80, 20);
    scale_in.Text = "0.01";
    //textbox for max depth
    scherm.Controls.Add(depth);
    depth.Location = new Point(47, 50);
    depth.Size = new Size(60, 20);
    depth.Text = $"{n}";

    //***BUTTONS***
    //button for calculating with given input
    scherm.Controls.Add(knop);
    knop.Location = new Point(0, 72);
    knop.Size = new Size(106, 40);
    knop.Text = "bereken";
    knop.BackColor = Color.Green;
    //button for selecting the inner color
    innercolor.Location = new Point(0, 0);
    innercolor.Size = new Size(80, 20);
    innercolor.Text = "kleur 1";
    //button for selecting outer color
    outercolor.Location = new Point(0, 20);
    outercolor.Size = new Size(80, 20);
    outercolor.Text = "kleur 2";

    //panels for grouping together color controls
    //inner color controls
    ColorNSmooth.Controls.Add(innercolor);
    ColorNSmooth.Controls.Add(labInnerColor);
    ColorNSmooth.Controls.Add(outercolor);
    ColorNSmooth.Controls.Add(labOuterColor);
    ColorNSmooth.Bounds = new Rectangle(new Point(6, 110), new Size(194, 60));
    //outer color controls
    scherm.Controls.Add(ColorSmooth);
    ColorSmooth.Bounds = new Rectangle(new Point(0, 110), new Size(200, 290));
    ColorSmooth.Controls.Add(kleur);
    kleur.Bounds = new Rectangle(new Point(2, 2), new Size(35, 20));
    kleur.Text = "kleur";
    ColorSmooth.Controls.Add(kleurpre);
    kleurpre.Bounds = new Rectangle(new Point(37, 2), new Size(113, 20));
    kleurpre.Items.Add("preset 1");
    kleurpre.Items.Add("preset 2");
    kleurpre.SelectedIndex = 0;
    //labels R G B
    ColorSmooth.Controls.Add(R);
    R.Location = new Point(80, 55);
    R.Size = new Size(40, 20);
    R.Text = "R";
    ColorSmooth.Controls.Add(G);
    G.Location = new Point(120, 55);
    G.Size = new Size(40, 20);
    G.Text = "G";
    ColorSmooth.Controls.Add(B);
    B.Location = new Point(160, 55);
    B.Size = new Size(40, 20);
    B.Text = "B";
    //labels colourlist
    ColorSmooth.Controls.Add(Color1);
    Color1.Location = new Point(10, 80);
    Color1.Size = new Size(50, 30);
    Color1.Text = "Color1";
    ColorSmooth.Controls.Add(Color2);
    Color2.Location = new Point(10, 110);
    Color2.Size = new Size(50, 30);
    Color2.Text = "Color2";
    ColorSmooth.Controls.Add(Color3);
    Color3.Location = new Point(10, 140);
    Color3.Size = new Size(50, 30);
    Color3.Text = "Color3";
    ColorSmooth.Controls.Add(Color4);
    Color4.Location = new Point(10, 170);
    Color4.Size = new Size(50, 30);
    Color4.Text = "Color4";
    ColorSmooth.Controls.Add(Color5);
    Color5.Location = new Point(10, 200);
    Color5.Size = new Size(50, 30);
    Color5.Text = "Color5";
    ColorSmooth.Controls.Add(Color6);
    Color6.Location = new Point(10, 230);
    Color6.Size = new Size(50, 30);
    Color6.Text = "Color6";
    ColorSmooth.Controls.Add(Color7);
    Color7.Location = new Point(10, 360);
    Color7.Size = new Size(50, 30);
    Color7.Text = "Color7";
    //21Color buttons
    ColorSmooth.Controls.Add(br1);
    br1.Location = new Point(66, 74);
    br1.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br2);
    br2.Location = new Point(66, 104);
    br2.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br3);
    br3.Location = new Point(66, 134);
    br3.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br4);
    br4.Location = new Point(66, 164);
    br4.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br5);
    br5.Location = new Point(66, 194);
    br5.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br6);
    br6.Location = new Point(66, 224);
    br6.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(br7);
    br7.Location = new Point(66, 254);
    br7.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg1);
    bg1.Location = new Point(107, 74);
    bg1.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg2);
    bg2.Location = new Point(107, 104);
    bg2.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg3);
    bg3.Location = new Point(107, 134);
    bg3.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg4);
    bg4.Location = new Point(107, 164);
    bg4.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg5);
    bg5.Location = new Point(107, 194);
    bg5.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg6);
    bg6.Location = new Point(107, 224);
    bg6.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bg7);
    bg7.Location = new Point(107, 254);
    bg7.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb1);
    bb1.Location = new Point(148, 74);
    bb1.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb2);
    bb2.Location = new Point(148, 104);
    bb2.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb3);
    bb3.Location = new Point(148, 134);
    bb3.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb4);
    bb4.Location = new Point(148, 164);
    bb4.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb5);
    bb5.Location = new Point(148, 194);
    bb5.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb6);
    bb6.Location = new Point(148, 224);
    bb6.Size = new Size(40, 30);
    ColorSmooth.Controls.Add(bb7);
    bb7.Location = new Point(148, 254);
    bb7.Size = new Size(40, 30);



    //Chechbox for turning on smoothening
    scherm.Controls.Add(smoothening);
    smoothening.Location = new Point(186, 70);
    smoothening.Size = new Size(14, 20);
    smoothening.CheckState = (CheckState)1;

    //Combobox for interesting points, including starting point
    scherm.Controls.Add(plaatjes);
    plaatjes.Bounds = new Rectangle(new Point(106, 90), new Size(80, 20));
    plaatjes.Items.Add("basispunt");
    plaatjes.Items.Add("Punt 1");
    plaatjes.Items.Add("Punt 2");
    plaatjes.Items.Add("Punt 3");
    plaatjes.Items.Add("Punt 4");
    plaatjes.SelectedIndex = 0;
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

void ChangeColor(object o, EventArgs e)
{
    
    if (o == br1)
    {
        s11 = !s11;
    }
    else if (o == br2)
    {
        s21 = !s21;
        s22 = !s22;
    }
    else if (o == br3)
    {
        s23 = !s23;
        s24 = !s24;
    }
    else if (o == br4)
    {

    }
    else if (o == br5)
    {

    }
    else if (o == br6)
    {

    }
    else if (o == br7)
    {

    }
    else if (o == bg1)
    {

    }
    else if (o == bg2)
    {

    }


}



s11 = true;
s12 = true;

s21 = true;
s25 = true;

s33 = true;
s36 = true;


// giga colour mixer, the "s"-values switch a previous value or keep it the same, the "f128n"-value's are true when the r, g or b from the previous step was at 128 with false representing the previous value being 0. 
Color SmoothClr(double x, double y)
{
    x = ((x - center.X) * scale); y = ((y - center.Y) * scale);
    double a = 0; double b = 0;
    double a_sq = 0; double b_sq = 0; double ab_sq = 0; //using variables for the squares of a, b and (a+b) uses less multiplications per loop cycle
    double i = 0;
    double l = 0, t = 0;
    double m = 0;
    while (t <= 4 && i < n)
    {
        a = a_sq - b_sq + x;
        b = ab_sq - a_sq - b_sq + y;//(a+b)^2-a^2-b^2 = 2ab
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




    if (m < n * 0.125) //first 1/8 total-----------------
    {
        double r1, g1, b1;
        if (s11 == true)
        {
            r1 = 128 * (m / (0.125 * n)); //to 128
            f128n1 = true;
        }
        else
        {
            r1 = 0; // remain 0 
        }
        if (s12 == true)
        {
            g1 = 128 * (m / (0.125 * n)); //to 128
            f128n2 = true;
        }
        else
        {
            g1 = 0; // remain 0   
        }
        if (s13 == true)
        {
            b1 = 128 * (m / (0.125 * n)); //to 128
            f128n3 = true;
        }
        else
        {
            b1 = 0; // remain 0
        }

        return Color.FromArgb((byte)r1, (byte)g1, (byte)b1);
    }

    else if (m < n * 0.25) //2e 1/8 total-----------------
    {
        double r2, g2, b2;



        if (s21 == true && f128n1 == true)
        {
            r2 = 128 - 128 * ((m - 0.125 * n) / (0.125 * n)); //to 0
            f128n4 = false;
        }
        else if (s22 == true && f128n1 == true)
        {
            r2 = 128; //remain 128
        }
        else if (s21 == true && f128n1 == false)
        {
            r2 = 128 * ((m - 0.125 * n) / (0.125 * n)); //to 128
            f128n4 = true;
        }
        else
        {
            r2 = 0; // remain 0
        }





        if (s23 == true && f128n2 == true)
        {
            g2 = 128 - 128 * ((m - 0.125 * n) / (0.125 * n)); //to 0
            f128n5 = false;
        }

        else if (s24 == true && f128n2 == true)
        {
            g2 = 128; //remain 128
        }

        else if (s23 == true && f128n2 == false)
        {
            g2 = 128 * ((m - 0.125 * n) / (0.125 * n)); //to 128
            f128n5 = true;
        }
        else
        {
            g2 = 0; // remain 0
        }

        if (s25 == true && f128n3 == true)
        {
            b2 = 128 - 128 * ((m - 0.125 * n) / (0.125 * n)); //to 0
            f128n6 = false;
        }

        else if (s26 == true && f128n3 == true)
        {
            b2 = 128; //remain 128
        }

        else if (s25 == true && f128n3 == false)
        {
            b2 = 128 * ((m - 0.125 * n) / (0.125 * n)); //to 128
            f128n6 = true;
        }
        else
        {
            b2 = 0; // remain 0
        }

        return Color.FromArgb((byte)r2, (byte)g2, (byte)b2);
    }

    else if (m < n * 0.375) //3e 1/8 total----------------
    {
        double r3, g3, b3;
        if (s31 == true && f128n4 == true)
        {
            r3 = 128 - 128 * ((m - 0.25 * n) / (0.125 * n)); //to 0
            f128n7 = false;
        }

        else if (s32 == true && f128n4 == true)
        {
            r3 = 128; //remain 128
        }

        else if (s31 == true && f128n4 == false)
        {
            r3 = 128 * ((m - 0.25 * n) / (0.125 * n)); //to 128
            f128n7 = true;
        }
        else
        {
            r3 = 0; // remain 0
        }

        if (s33 == true && f128n5 == true)
        {
            g3 = 128 - 128 * ((m - 0.25 * n) / (0.125 * n)); //to 0
            f128n8 = false;
        }

        else if (s34 == true && f128n5 == true)
        {
            g3 = 128; //remain 128
        }

        else if (s33 == true && f128n5 == false)
        {
            g3 = 128 * ((m - 0.25 * n) / (0.125 * n)); //to 128
            f128n8 = true;
        }
        else
        {
            g3 = 0; // remain 0
        }

        if (s35 == true && f128n6 == true)
        {
            b3 = 128 - 128 * ((m - 0.25 * n) / (0.125 * n)); //to 0
            f128n9 = false;
        }

        else if (s36 == true && f128n6 == true)
        {
            b3 = 128; //remain 128
        }

        else if (s35 == true && f128n6 == false)
        {
            b3 = 128 * ((m - 0.25 * n) / (0.125 * n)); //to 128
            f128n9 = true;
        }
        else
        {
            b3 = 0; // remain 0
        }

        return Color.FromArgb((byte)r3, (byte)g3, (byte)b3);
    }

    else if (m < n * 0.5) //4e 1/8 total----------------
    {
        double r4, g4, b4;
        if (s41 == true && f128n7 == true)
        {
            r4 = 128 - 128 * ((m - 0.375 * n) / (0.125 * n)); //to 0
            f128n10 = false;
        }

        else if (s42 == true && f128n7 == true)
        {
            r4 = 128; //remain 128
        }

        else if (s41 == true && f128n7 == false)
        {
            r4 = 128 * ((m - 0.375 * n) / (0.125 * n)); //to 128
            f128n10 = true;
        }
        else
        {
            r4 = 0; // remain 0
        }

        if (s43 == true && f128n8 == true)
        {
            g4 = 128 - 128 * ((m - 0.375 * n) / (0.125 * n)); //to 0
            f128n11 = false;
        }

        else if (s44 == true && f128n8 == true)
        {
            g4 = 128; //remain 128
        }

        else if (s43 == true && f128n8 == false)
        {
            g4 = 128 * ((m - 0.375 * n) / (0.125 * n)); //to 128
            f128n11 = true;
        }
        else
        {
            g4 = 0; // remain 0
        }


        if (s45 == true && f128n9 == true)
        {
            b4 = 128 - 128 * ((m - 0.375 * n) / (0.125 * n)); //to 0
            f128n12 = false;
        }

        else if (s46 == true && f128n9 == true)
        {
            b4 = 128; //remain 128
        }

        else if (s45 == true && f128n9 == false)
        {
            b4 = 128 * ((m - 0.375 * n) / (0.125 * n)); //to 128
            f128n12 = true;
        }
        else
        {
            b4 = 0; // remain 0
        }

        return Color.FromArgb((byte)r4, (byte)g4, (byte)b4);
    }

    else if (m < n * 0.625) //5e 1/8 total----------------
    {
        double r5, g5, b5;
        if (s51 == true && f128n10 == true)
        {
            r5 = 128 - 128 * ((m - 0.5 * n) / (0.125 * n)); //to 0
            f128n13 = false;
        }

        else if (s52 == true && f128n10 == true)
        {
            r5 = 128; //remain 128
        }

        else if (s51 == true && f128n10 == false)
        {
            r5 = 128 * ((m - 0.5 * n) / (0.125 * n)); //to 128
            f128n13 = true;
        }
        else
        {
            r5 = 0; // remain 0
        }

        if (s53 == true && f128n11 == true)
        {
            g5 = 128 - 128 * ((m - 0.5 * n) / (0.125 * n)); //to 0
            f128n14 = false;
        }

        else if (s54 == true && f128n11 == true)
        {
            g5 = 128; //remain 128
        }

        else if (s53 == true && f128n1 == false)
        {
            g5 = 128 * ((m - 0.5 * n) / (0.125 * n)); //to 128
            f128n14 = true;
        }
        else
        {
            g5 = 0; // remain 0
        }

        if (s55 == true && f128n12 == true)
        {
            b5 = 128 - 128 * ((m - 0.5 * n) / (0.125 * n)); //to 0
            f128n15 = false;
        }

        else if (s56 == true && f128n12 == true)
        {
            b5 = 128; //remain 128
        }

        else if (s55 == true && f128n12 == false)
        {
            b5 = 128 * ((m - 0.5 * n) / (0.125 * n)); //to 128
            f128n15 = true;
        }
        else
        {
            b5 = 0; // remain 0
        }

        return Color.FromArgb((byte)r5, (byte)g5, (byte)b5);
    }

    else if (m < n * 0.750) //6e 1/8 total---------------
    {
        double r6, g6, b6;
        if (s61 == true && f128n13 == true)
        {
            r6 = 128 - 128 * ((m - 0.625 * n) / (0.125 * n)); //to 0
            f128n16 = false;
        }

        else if (s62 == true && f128n13 == true)
        {
            r6 = 128; //remain 128
        }

        else if (s61 == true && f128n13 == false)
        {
            r6 = 128 * ((m - 0.625 * n) / (0.125 * n)); //to 128
            f128n16 = true;
        }
        else
        {
            r6 = 0; // remain 0
        }

        if (s63 == true && f128n14 == true)
        {
            g6 = 128 - 128 * ((m - 0.625 * n) / (0.125 * n)); //to 0
            f128n17 = false;
        }

        else if (s64 == true && f128n14 == true)
        {
            g6 = 128; //remain 128
        }

        else if (s63 == true && f128n14 == false)
        {
            g6 = 128 * ((m - 0.625 * n) / (0.125 * n)); //to 128
            f128n17 = true;
        }
        else
        {
            g6 = 0; // remain 0
        }

        if (s65 == true && f128n15 == true)
        {
            b6 = 128 - 128 * ((m - 0.625 * n) / (0.125 * n)); //to 0
            f128n18 = false;
        }
        else if (s66 == true && f128n15 == true)
        {
            b6 = 128; //remain 128
        }
        else if (s65 == true && f128n15 == false)
        {
            b6 = 128 * ((m - 0.625 * n) / (0.125 * n)); //to 128
            f128n18 = true;
        }
        else
        {
            b6 = 0; // remain 0
        }

        return Color.FromArgb((byte)r6, (byte)g6, (byte)b6);
    }

    else if (m < n * 0.875) //7e 1/8 total---------------
    {
        double r7, g7, b7;
        if (s71 == true && f128n16 == true)
        {
            r7 = 128 - 128 * ((m - 0.75 * n) / (0.125 * n)); //to 0
            f128n19 = false;
        }

        else if (s72 == true && f128n16 == true)
        {
            r7 = 128; //remain 128
        }

        else if (s71 == true && f128n16 == false)
        {
            r7 = 128 * ((m - 0.75 * n) / (0.125 * n)); //to 128
            f128n19 = true;
        }
        else
        {
            r7 = 0; // remain 0
        }

        if (s73 == true && f128n17 == true)
        {
            g7 = 128 - 128 * ((m - 0.75 * n) / (0.125 * n)); //to 0
            f128n20 = false;
        }

        else if (s74 == true && f128n17 == true)
        {
            g7 = 128; //remain 128
        }

        else if (s73 == true && f128n17 == false)
        {
            g7 = 128 * ((m - 0.75 * n) / (0.125 * n)); //to 128
            f128n20 = true;
        }
        else
        {
            g7 = 0; // remain 0
        }


        if (s75 == true && f128n18 == true)
        {
            b7 = 128 - 128 * ((m - 0.75 * n) / (0.125 * n)); //to 0
            f128n21 = false;
        }

        else if (s76 == true && f128n18 == true)
        {
            b7 = 128; //remain 128
        }

        else if (s75 == true && f128n18 == false)
        {
            b7 = 128 * ((m - 0.75 * n) / (0.125 * n)); //to 128
            f128n21 = true;
        }
        else
        {
            b7 = 0; // remain 0
        }

        return Color.FromArgb((byte)r7, (byte)g7, (byte)b7);
    }

    else //final 1/8 total-------------------
    {
        double r8, g8, b8;
        if (f128n19 == true)
        {
            r8 = 128 - 128 * ((m - 0.875 * n) / (0.125 * n)); //to 0
        }
        else
        {
            r8 = 0; // remain 0  
        }
        if (f128n20 == true)
        {
            g8 = 128 - 128 * ((m - 0.875 * n) / (0.125 * n)); //to 0
        } 
        else
        {
            g8 = 0; // remain 0 
        }
        if (f128n21 == true)
        {
            b8 = 128 - 128 * ((m - 0.875 * n) / (0.125 * n)); //to 0
        }
        else
        {
            b8 = 0; // remain 0
        }

        return Color.FromArgb((byte)r8, (byte)g8, (byte)b8);
    }
}


Bitmap plaatje() //maakt bitmap
{
    Bitmap plaatje = new Bitmap(scherm.ClientSize.Width - 200, scherm.ClientSize.Height);
    if (smoothening.CheckState == (CheckState)1)
    {
        for (double i = 0; i < scherm.ClientSize.Width - 200; i++)
            for (double j = 0; j < scherm.ClientSize.Height; j++)
                plaatje.SetPixel((int)i, (int)j, SmoothClr(i, j));
        return plaatje;
    }
    else
    {
        for (double i = 0; i < scherm.ClientSize.Width - 200; i++)
            for (double j = 0; j < scherm.ClientSize.Height; j++)
                plaatje.SetPixel((int)i, (int)j, colors[MandelNum(i, j)]);
        return plaatje;
    }
}


void zoom(object o, MouseEventArgs e) //zoom in/uit als je scrollt
{
    double dmoveX = 0;
    double dmoveY = 0;

    if (e.Delta > 0)
    {
        scale *= 0.5;
        dmoveX = center.X - (e.Location.X - 200 - center.X);
        dmoveY = center.Y - (e.Location.Y - center.Y);
    }
    else
    {
        scale *= 2.0;
        dmoveX = center.X - (center.X - e.Location.X + 200) * 0.5;
        dmoveY = center.Y - (center.Y - e.Location.Y) * 0.5;
    }

    center = new Point((int)dmoveX, (int)dmoveY);

    scherm.Invalidate();
}


void mouse_down_drag(object o, MouseEventArgs e) //bewaart de locatie waar je klikt
{
    if (e.Location.X > 200)
    {
        mouse_down = e.Location;
        mouse_down_bool = true;
    }
}


void mouse_up_drag(object o, MouseEventArgs e)   //verandert center, gebaseert op het verschil
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
    ColorControls();
    depth.Text = n.ToString();
    scale_in.Text = scale.ToString("E4");
    center_x.Text = ((0.5 * scherm.Width - center.X) * scale).ToString("F4");
    center_y.Text = ((0.5 * (scherm.Height - 52) - center.Y) * scale).ToString("F4");
    e.Graphics.DrawImage(plaatje(), 200, 0);
}


void bereken(object o, EventArgs e)
{
    try
    {
        n = double.Parse(depth.Text);
        colors = gen_palette(inrclr, outrclr);
        scale = double.Parse(scale_in.Text);
        center = new Point((int)(0.5 * scherm.Width - double.Parse(center_x.Text) / scale), (int)(0.5 * (scherm.Height - 52) - double.Parse(center_y.Text) / scale));
        scherm.Invalidate();
    }
    catch (Exception ex)
    {
        DialogResult result = MessageBox.Show(ex.Message, "Er is iets fout gegaan.", MessageBoxButtons.OK);
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
    colors = gen_palette(inrclr, outrclr);
    scherm.Invalidate();
}


void ColorControls()
{
    if (smoothening.CheckState == (CheckState)1)
    {
        scherm.Controls.Remove(ColorNSmooth);
        scherm.Controls.Add(ColorSmooth);
    }
    else
    {
        scherm.Controls.Remove(ColorSmooth);
        scherm.Controls.Add(ColorNSmooth);
    }
}


void redraw(object o, EventArgs e)
{
    scherm.Invalidate();
}


void punt(object o, EventArgs e)
{
    if (plaatjes.SelectedIndex == 0)
    {
        center.X = (scherm.ClientSize.Width - 200) / 2;
        center.Y = scherm.ClientSize.Height / 2;
        scale = 0.01;
        n = 100;
        scherm.Invalidate();
    }
    else if (plaatjes.SelectedIndex == 1)
    {
        depth.Text = 1000.ToString();
        center_x.Text = (-0.7829).ToString();
        center_y.Text = 0.1303.ToString();
        scale_in.Text = (2.824752E-6).ToString();
        bereken(knop, EventArgs.Empty);
    }
    else if (plaatjes.SelectedIndex == 2)
    {
        depth.Text = 1000.ToString();
        center_x.Text = (0.3123).ToString();
        center_y.Text = 0.0261.ToString();
        scale_in.Text = (2.4E-7).ToString();
        bereken(knop, EventArgs.Empty);
    }
    else if (plaatjes.SelectedIndex == 3)
    {
        depth.Text = 2000.ToString();
        center_x.Text = (-0.4606).ToString();
        center_y.Text = (-0.6024).ToString();
        scale_in.Text = (3.0518E-7).ToString();
        bereken(knop, EventArgs.Empty);
    }
    else if (plaatjes.SelectedIndex == 4)
    {
        depth.Text = 1000.ToString();
        center_x.Text = (-0.5621).ToString();
        center_y.Text = (-0.6427).ToString();
        scale_in.Text = (1.5E-7).ToString();
        bereken(knop, EventArgs.Empty);
    }
}


void SmoothKleur(object o, EventArgs e)
{
    if (plaatjes.SelectedIndex == 0)
    {
        //hier variabelen voor kleur preset 1
        scherm.Invalidate();
    }
    else if (plaatjes.SelectedIndex == 1)
    {
        //hier variabelen voor kleur preset 2
        scherm.Invalidate();
    }
}


kleurpre.SelectedIndexChanged += SmoothKleur;
plaatjes.SelectedIndexChanged += punt;
smoothening.CheckedChanged += redraw;
innercolor.Click += verander_kleur;
outercolor.Click += verander_kleur;
knop.Click += bereken;
scherm.MouseWheel += zoom;
scherm.MouseDown += mouse_down_drag;
scherm.MouseUp += mouse_up_drag;
scherm.Paint += teken;

bb1.Click += ChangeColor;



Application.Run(scherm);