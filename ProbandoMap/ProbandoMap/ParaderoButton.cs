using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace ProbandoMap
{
    public class ParaderoButton : Control
    {
        public int x { get; set; }
        public int y { get; set; }
        public int indiceParadero { get; set; }
        public bool pressed { get; set; }

        public Image backgroundImage, pressedImage;

        public ParaderoButton(int pX,int pY,int pIndiceParadero)
        {
            string p = Directory.GetCurrentDirectory();
            backgroundImage = Image.FromFile(p + @"\marker.png");
            pressedImage = Image.FromFile(p + @"\marker_azul.png");
         
            x = pX;
            y = pY;  
            this.Bounds = new Rectangle(x - backgroundImage.Width / 2, y - backgroundImage.Height, backgroundImage.Width, backgroundImage.Height);

            pressed = false;
            this.indiceParadero = pIndiceParadero;

        }

        public void rePosition(double newX,double newY){
            x =(int) newX;
            y =(int) newY;
            this.Bounds = new Rectangle(x - backgroundImage.Width / 2, y - backgroundImage.Height, backgroundImage.Width, backgroundImage.Height);
    

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //empty implementation<
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Image i;
            if (pressed) i = pressedImage;
            else i = backgroundImage;

            e.Graphics.DrawImage(i,0,0);

           // e.Graphics.DrawString(indiceParadero.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 2), new SolidBrush(Color.Black), x + 5, y - pressedImage.Height / 2);

        }


        // When the mouse button is pressed, set the "pressed" flag to true  
        // and invalidate the form to cause a repaint.  The .NET Compact Framework  
        // sets the mouse capture automatically. 
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.pressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        // When the mouse is released, reset the "pressed" flag 
        // and invalidate to redraw the button in the unpressed state. 
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.pressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

    }
}
