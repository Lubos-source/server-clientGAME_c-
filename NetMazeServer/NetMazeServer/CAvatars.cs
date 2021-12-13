using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NetMazeServer
{
    class CAvatars
    {
        public List<CAvatar> m_Avatars = new List<CAvatar>();
        private int m_AvatarIDs = 0;

        private int Exist(string name)
        {
            for (int i = 0; i < m_Avatars.Count; i++)
            {
                if (m_Avatars[i].m_Name == name) return (i);
            }
            return (-1);
        }

        public int AddAvatar(string name)
        {
            int value = Exist(name);
            if (value >= 0)
            {
                m_Avatars.RemoveAt(value);
                value = -1;
            }

            if (value < 0)
            {
                CAvatar avatar = new CAvatar();
                avatar.m_Name = name;
                avatar.m_ID = m_AvatarIDs;
                m_AvatarIDs++;
                avatar.m_Image = Image.FromFile(name+".bmp");
                m_Avatars.Add(avatar);
                return (avatar.m_ID);
            }

            return (value);
        }

        public int GetAvatarByID(int id)
        {
            for (int i = 0; i < m_Avatars.Count; i++)
            {
                if (m_Avatars[i].m_ID == id) return (i);
            }
            return (-1);
        }
    }
}
