using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint2
{
    public class Paint
    {
        private bool _isMouseDown;

        private ToolsType _tool = ToolsType.FreeHand;
        private SolidBrush _brush;
        private Point _startPoint;
        private Pen _pen;
        private PictureBox _pictureBox;

        public Paint(PictureBox pictureBox)
        {
            _brush = new SolidBrush(Color.Black);
            _pen = new Pen(_brush,1);
            _pictureBox = pictureBox;
        }


        public void OnMouseDown(Point point)
        {
            _isMouseDown = true;
            _startPoint = point;
        }
        public void OnMouseMove(Point location)
        {
            if (_isMouseDown && _startPoint != null)
            {
                ImageIsNull();
                if (_tool == ToolsType.FreeHand)
                    DrawByFreeHand(location);
            }
        }
        public void OnMouseUp(Point location)
        {
            switch (_tool) 
            {
                case ToolsType.Rectangl:
                    DrawRectangle(location);
                    break;
                case ToolsType.Circle:
                    DrawCircle(location);
                    break;
            }

            ResetSettings();
        }
        public void ChangeColor(Color color)=> _pen.Color = color;
        public void ChangeBrushWidth(decimal width)=> _pen.Width = (float)width;
        public void ChangeTool(string key)
        {
            if (key == "r1")
                _tool = ToolsType.FreeHand;
            else if (key == "r2")
                _tool = ToolsType.Rectangl;
            else
                _tool = ToolsType.Circle;
        }
        public void Clear() => _pictureBox.Image = null;
        public void SaveImage(string fileName)
        {
            Bitmap temp = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            _pictureBox.DrawToBitmap(temp, new Rectangle(0, 0, _pictureBox.Width, _pictureBox.Height));
            temp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public void OpenImage(string fileName)
        {
            Bitmap openedImage = new Bitmap(fileName);
            ImageIsNull();

            using(Graphics graphics = Graphics.FromImage(_pictureBox.Image))
            {
                graphics.DrawImage(openedImage, 0, 0, _pictureBox.Width, _pictureBox.Height);
            }
        }


    
        private void ImageIsNull()
        {
            if (_pictureBox.Image == null)
                _pictureBox.Image = new Bitmap(_pictureBox.Width,_pictureBox.Height);
        }
        private void ResetSettings()
        {
            _isMouseDown = false;
            _startPoint = Point.Empty;
        }
        private void DrawByFreeHand(Point location)
        {
            using(Graphics graphics = Graphics.FromImage(_pictureBox.Image))
            {
                graphics.DrawLine(_pen, _startPoint, location);
                _pictureBox.Invalidate();
            }
            _startPoint = location;
        }
        private void DrawRectangle(Point location)
        {
            if (location.X > _startPoint.X && location.Y > _startPoint.Y)
            {
                using (Graphics graphics = Graphics.FromImage(_pictureBox.Image))
                {
                    graphics.DrawRectangle(_pen, _startPoint.X,_startPoint.Y,location.X - _startPoint.X, location.Y - _startPoint.Y);
                    _pictureBox.Invalidate();
                }
            }
            ResetSettings();
        }
        private void DrawCircle(Point location)
        {

            using (Graphics graphics = Graphics.FromImage(_pictureBox.Image))
            {
                graphics.DrawEllipse(_pen, _startPoint.X, _startPoint.Y, location.X - _startPoint.X, location.Y - _startPoint.Y);
                _pictureBox.Invalidate();
            }
            ResetSettings();
        }
    }
}
