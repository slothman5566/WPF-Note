using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfApp1.UIControls
{
    [TemplatePart(Name = TopLine, Type = typeof(Rectangle))]
    [TemplatePart(Name = RightLine, Type = typeof(Rectangle))]
    [TemplatePart(Name = LeftLine, Type = typeof(Rectangle))]
    [TemplatePart(Name = BottomLine, Type = typeof(Rectangle))]
    [TemplatePart(Name = MainEllipse, Type = typeof(Ellipse))]
    [TemplatePart(Name = MinEllipse, Type = typeof(Ellipse))]
    [TemplatePart(Name = MainCanvas, Type = typeof(Canvas))]
    public class CircleSlider : Slider
    {
        public const string TopLine = "PART_TopLine";
        public const string RightLine = "PART_RightLine";
        public const string BottomLine = "PART_BottomLine";
        public const string LeftLine = "PART_LeftLine";
        public const string MainEllipse = "PART_Ellipse";
        public const string MainCanvas = "PART_Canvas";
        public const string MinEllipse = "PART_MinEllipse";


        Rectangle _TopLine = null;
        Rectangle _RightLine = null;
        Rectangle _BottomLine = null;
        Rectangle _LeftLine = null;
        Ellipse _MainEllipse = null;
        Ellipse _MinEllipse = null;
        Canvas _MainCanvas = null;
        private bool _isPressed = false;

        static CircleSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleSlider), new FrameworkPropertyMetadata(typeof(CircleSlider)));
        }


        public override void OnApplyTemplate()
        {

            if (_LeftLine != null)
            {

                _LeftLine.MouseDown -= _LeftLine_MouseDown;

            }
            _LeftLine = GetTemplateChild(LeftLine) as Rectangle;
            if (_LeftLine != null)
            {
                _LeftLine.MouseDown += _LeftLine_MouseDown;

            }

            if (_RightLine != null)
            {

                _LeftLine.MouseDown -= _RightLine_MouseDown;

            }
            _RightLine = GetTemplateChild(RightLine) as Rectangle;
            if (_RightLine != null)
            {
                _RightLine.MouseDown += _RightLine_MouseDown;

            }

            if (_TopLine != null)
            {

                _TopLine.MouseDown -= _TopLine_MouseDown;

            }
            _TopLine = GetTemplateChild(TopLine) as Rectangle;
            if (_TopLine != null)
            {
                _TopLine.MouseDown += _TopLine_MouseDown; ;

            }

            if (_BottomLine != null)
            {

                _BottomLine.MouseDown -= _BottomLine_MouseDown;

            }
            _BottomLine = GetTemplateChild(BottomLine) as Rectangle;
            if (_BottomLine != null)
            {
                _BottomLine.MouseDown += _BottomLine_MouseDown; ;

            }



            if (_MainEllipse != null)
            {
                _MainEllipse.MouseLeftButtonDown -= _MainEllipse_MouseLeftButtonDown;
                _MainEllipse.MouseLeftButtonUp -= _MainEllipse_MouseLeftButtonUp;
                _MainEllipse.MouseLeave -= _MainEllipse_MouseLeave;
                _MainEllipse.MouseMove -= _MainEllipse_MouseMove;
            }

            _MainEllipse = GetTemplateChild(MainEllipse) as Ellipse;
            if (_MainEllipse != null)
            {
                _MainEllipse.MouseLeftButtonDown += _MainEllipse_MouseLeftButtonDown;
                _MainEllipse.MouseLeftButtonUp += _MainEllipse_MouseLeftButtonUp;
                _MainEllipse.MouseLeave += _MainEllipse_MouseLeave;
                _MainEllipse.MouseMove += _MainEllipse_MouseMove;
            }

            if (_MinEllipse != null)
            {
                _MinEllipse.MouseLeftButtonDown -= _MainEllipse_MouseLeftButtonDown;
                _MinEllipse.MouseLeftButtonUp -= _MainEllipse_MouseLeftButtonUp;

            }

            _MinEllipse = GetTemplateChild(MinEllipse) as Ellipse;
            if (_MainEllipse != null)
            {
                _MinEllipse.MouseLeftButtonDown += _MainEllipse_MouseLeftButtonDown;
                _MinEllipse.MouseLeftButtonUp += _MainEllipse_MouseLeftButtonUp;
            }


            if (_MainCanvas != null)
            {
                _MainCanvas.MouseLeave -= _MainCanvas_MouseLeave;
            }
            _MainCanvas = GetTemplateChild(MainCanvas) as Canvas;
            if (_MainCanvas != null)
            {
                _MainCanvas.MouseLeave += _MainCanvas_MouseLeave;
            }
            base.OnApplyTemplate();

        }

        private void _MainCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isPressed = false;
        }

        private void _MainEllipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("1");
            if (_isPressed)
            {

                //Find the parent canvas.
                if (_MainCanvas == null)
                {
                    _MainCanvas = GetTemplateChild(MainCanvas) as Canvas;
                    if (_MainCanvas == null) return;
                }
                //Canculate the current rotation angle and set the value.

                Point newPos = e.GetPosition(_MainCanvas);
                double angle = GetAngleR(newPos, _MainCanvas.Width / 2);
                Value = Math.Floor((Maximum - Minimum) * angle / (2 * Math.PI));
            }
        }

        private void _MainEllipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //_isPressed = false;
        }

        private void _MainEllipse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isPressed = false;
        }

        private void _MainEllipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isPressed = true;
            if (_MainCanvas == null)
            {
                _MainCanvas = GetTemplateChild(MainCanvas) as Canvas;
                if (_MainCanvas == null) return;
            }
            //Canculate the current rotation angle and set the value.

            Point newPos = e.GetPosition(_MainCanvas);
            double angle = GetAngleR(newPos, _MainCanvas.Width / 2);
            Value = Math.Floor((Maximum - Minimum) * angle / (2 * Math.PI));

        }

        private void _BottomLine_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Value = 180;
        }

        private void _TopLine_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Value = 0;
        }

        private void _RightLine_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Value = 90;
        }

        private void _LeftLine_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Value = 270;
        }



        public  double GetAngleR(Point pos, double radius)
        {
            //Calculate out the distance(r) between the center and the position
            Point center = new Point(radius, radius);
            double xDiff = center.X - pos.X;
            double yDiff = center.Y - pos.Y;
            double r = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

            //Calculate the angle
            double angle = Math.Acos((center.Y - pos.Y) / r);
            //Console.WriteLine("r:{0},y:{1},angle:{2}.", r, pos.Y, angle);
            if (pos.X < radius)
                angle = 2 * Math.PI - angle;
            if (Double.IsNaN(angle))
                return 0.0;
            else
                return angle;
        }
    }
}
