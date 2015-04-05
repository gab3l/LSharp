using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Shapes;
namespace Bot_Stablelizer
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Rct
    {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public Rct(Rct Rectangle)
            : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }
        public Rct(int Left, int Top, int Right, int Bottom)
        {
            _Left = Left;
            _Top = Top;
            _Right = Right;
            _Bottom = Bottom;
        }

        public int X
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Y
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public int Bottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        public int Height
        {
            get { return _Bottom - _Top; }
            set { _Bottom = value + _Top; }
        }
        public int Width
        {
            get { return _Right - _Left; }
            set { _Right = value + _Left; }
        }
        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set
            {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set
            {
                _Right = value.Width + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public static implicit operator System.Drawing.Rectangle(Rct Rectangle)
        {
            return new System.Drawing.Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
        }
        public static implicit operator Rct(System.Drawing.Rectangle Rectangle)
        {
            return new Rct(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }
        public static bool operator ==(Rct Rectangle1, Rct Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        public static bool operator !=(Rct Rectangle1, Rct Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(Rct Rectangle)
        {
            return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
        }

        public override bool Equals(object Object)
        {
            if (Object is Rct)
            {
                return Equals((Rct)Object);
            }
            else if (Object is System.Drawing.Rectangle)
            {
                return Equals(new Rct((System.Drawing.Rectangle)Object));
            }

            return false;
        }
    } 
}