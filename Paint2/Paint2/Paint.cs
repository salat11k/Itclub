using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paint2
{
    public class Paint
    {
        private bool _isMouseDown;

        private Tools _tool = Tools.FreeHand;
        private SolidBrush _brush;
        private Point _startPoint;
        private Pen _pen;


        public Paint()
        {
            _brush = new SolidBrush(Color.Black);
            _pen = new Pen(_brush,1);
        }


        public void SetSettingsOnMouseDown(Point point)
        {
            _isMouseDown = true;
            _startPoint = point;
        }

        public void Draw(Color color,float brushWidth)
        {
            if (_isMouseDown && _startPoint != null)
            {
                switch (_tool)
                {
                    case Tools.FreeHand:
                        DrawByFreeHand();
                        break;
                }
            }
        }

        private void DrawByFreeHand()
        {

        }

    }
}
