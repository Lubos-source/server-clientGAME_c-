using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NetMazeServer
{
    class CAvatar
    {
        public string m_Name;
        public int m_ID;
        public Image m_Image;
        public Point m_Pozice;

        public void SetPozici(int x, int y)
        {
            m_Pozice.X = x;
            m_Pozice.Y = y;
        }
    }
}
