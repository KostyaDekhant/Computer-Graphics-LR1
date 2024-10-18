using System;
using System.Drawing;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Computer_Graphics_LR1
{
    public class CircularTrackBar : Control
    {
        private int _value = 1;
        private int _maximum = 360;
        private Color _circleColor = Color.White;
        private Color _textColor = Color.Blue;
        private Color _sliderColor = Color.Blue;
        private Color _trackColor = Color.LightGray;
        public event EventHandler ValueChanged;
        public int Value
        {
            get => _value;
            set
            {
                int newValue = Math.Max(1, Math.Min(value, _maximum));
                if (_value != newValue)
                {
                    _value = newValue;
                    Invalidate();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = Math.Max(1, value);
                Invalidate();
            }
        }

        public Color CircleColor
        {
            get => _circleColor;
            set
            {
                _circleColor = value;
                Invalidate();
            }
        }

        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Invalidate();
            }
        }

        public Color SliderColor
        {
            get => _sliderColor;
            set
            {
                _sliderColor = value;
                Invalidate();
            }
        }

        public Color TrackColor
        {
            get => _trackColor;
            set
            {
                _trackColor = value;
                Invalidate();
            }
        }

        public CircularTrackBar()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.BackColor = Color.LightGray; // Background color
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCircularTrackBar(e.Graphics);
        }

        private void DrawCircularTrackBar(Graphics g)
        {
            g.Clear(BackColor);

            int margin = 20;
            int size = Math.Max(0, Math.Min(Width, Height) - margin * 2);
            //int size = Math.Min(Width, Height) - margin * 2;
            Rectangle rect = new Rectangle(margin, margin, size, size);
            Rectangle outerRect = new Rectangle(margin - 5, margin - 5, Math.Max(0, size + 10), Math.Max(0, size + 10));
            //Rectangle outerRect = new Rectangle(margin - 5, margin - 5, size + 10, size + 10);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw the background circle
            using (Brush brush = new SolidBrush(_circleColor))
            {
                g.FillEllipse(brush, rect);
            }

            // Draw the background arc (track)
            using (Pen pen = new Pen(_trackColor, 10))
            {
                g.DrawArc(pen, outerRect, -90, 360);
            }

            // Draw the progress arc (slider)
            using (Pen pen = new Pen(_sliderColor, 12))
            {
                float sweepAngle = Math.Max(0, Math.Min(360, (_value == _maximum ? 360 : (float)(_value % _maximum) / _maximum * 360)));
                //float sweepAngle = (_value == _maximum ? 360 : (float)(_value % _maximum) / _maximum * 360);
                g.DrawArc(pen, outerRect, -90, sweepAngle);
            }

            // Draw the value in the center
            string text = (_value).ToString();
            using (Font font = new Font("Arial", 14, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(text, font);
                PointF textPosition = new PointF(
                    (Width - textSize.Width) / 2,
                    (Height - textSize.Height) / 2);
                using (Brush textBrush = new SolidBrush(_textColor))
                {
                    g.DrawString(text, font, textBrush, textPosition);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            UpdateValue(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValue(e.Location);
            }
        }

        private void UpdateValue(Point location)
        {
            int centerX = Width / 2;
            int centerY = Height / 2;
            double angle = Math.Atan2(location.Y - centerY, location.X - centerX) * (180 / Math.PI) + 90;
            if (angle < 0) angle += 360;
            int newValue = (int)(angle / 360 * _maximum) + 1;
            if (newValue <= _maximum)
            {
                Value = newValue;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int size = Math.Min(Width, Height);
            this.Size = new Size(size, size);
        }
    }
}