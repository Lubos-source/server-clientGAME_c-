using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace NetMazeServer
{
    public partial class Form1 : Form
    {
        int[,] m_Maze = new Int32[64, 64];
        Image m_Back;
        Image m_Wall;
        Image m_Destination;
        TcpServer m_TCPServer;
        Thread m_TcpThread;
        CAvatars m_Avatars = new CAvatars();
        Point m_DestinationPoint;
      
        
        public Form1()
        {
            InitializeComponent();
            //*** horni strana
            for (int i = 0; i < m_Maze.GetLength(0); i++) m_Maze[i, 0] = 1;
            //*** spodni strana
            for (int i = 0; i < m_Maze.GetLength(0); i++) m_Maze[i, m_Maze.GetLength(1)-1] = 1;
            //*** bocni strany
            for (int i = 0; i < m_Maze.GetLength(1); i++)
            {
                m_Maze[0, i] = 1;
                m_Maze[m_Maze.GetLength(0)-1, i] = 1;
            }

            m_Back = Image.FromFile("nic.bmp");
            m_Wall = Image.FromFile("zed.bmp");
            m_Destination = Image.FromFile("destination.bmp");

            panelMaze.BackColor = Color.Transparent;

        }

        public bool SetAvatarPos(int id, int x, int y)
        {
            bool result = false;
            lock (this)
            {
                if (m_Maze[x, y] != 1) //na nove pozici neni zed
                {
                    m_Maze[m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.X, m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.Y] = 0; //smazeme ho z puvodni pozice 
                    m_Maze[x, y] = 2 + id; //nastavime ho na novou pozici
                    m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].SetPozici(x, y); //zapamatujeme si novou pozici
                    result = true; //presun probehl uspesne
                }
            }

            //Rectangle r = new Rectangle(x * 10, y * 10, 10, 10);
            panelMaze.Invalidate();
            return (result);
        }

        public Point GetDestinationPoint()
        {
            Point bod;

            lock (this)
            {
                bod = m_DestinationPoint;
            }
            return (bod);
        }

        public int GetMazeItem(int id, int x, int y)
        {
            int value=0;
            lock (this)
            { //*** pokud je pozice kam se divame dal od pozice avatara o vice nez 1 v kazdem smeru, potom vratime nulu - prevence, proti skenovani mapy
                if ((Math.Abs(m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.X - x) > 1) ||
                        (Math.Abs(m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.Y - y) > 1)) value = 0;
                else  value = m_Maze[x, y];
            }
            return (value);
        }

        public int RegisterAvatar(string name)
        {
            int id;
            lock (this)
            {
                id = m_Avatars.AddAvatar(name);
            }

            RefreshListBoxPlayers();
            return (id);
        }

        public void UnregisterAvatar(int id)
        {
       
            lock (this)
            {
                m_Maze[m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.X, m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(id)].m_Pozice.Y] = 0; //smazeme ho z puvodni pozice 
                m_Avatars.m_Avatars.RemoveAt(m_Avatars.GetAvatarByID(id));
                Debug.WriteLine("Pdregistrovan: " + id.ToString());  
            }

            RefreshListBoxPlayers();
        }

        private void RefreshListBoxPlayers()
        {
            string text;

            lock (this)
            {
                listPlayers.Items.Clear();

                foreach (CAvatar avatar in m_Avatars.m_Avatars)
                {
                    text = avatar.m_Name + " #" + avatar.m_ID.ToString();
                    listPlayers.Items.Add(text);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }


        private void onPaint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < m_Maze.GetLength(1); i++)
            {
                for (int a = 0; a < m_Maze.GetLength(0); a++)
                {
                   
                        if (m_Maze[a, i] == 0) e.Graphics.DrawImageUnscaled(m_Back, a * 10, i * 10);
                        if (m_Maze[a, i] == 1) e.Graphics.DrawImageUnscaled(m_Wall, a * 10, i * 10);
                        if (m_Maze[a, i] >= 2)
                        {
                            lock (this)
                            {
                                if (m_Avatars.GetAvatarByID(m_Maze[a, i] - 2)>=0)
                                e.Graphics.DrawImageUnscaled(m_Avatars.m_Avatars[m_Avatars.GetAvatarByID(m_Maze[a, i] - 2)].m_Image, a * 10, i * 10);
                            }
                        }
                    
                }
            }

            e.Graphics.DrawImageUnscaled(m_Destination, m_DestinationPoint.X*10,m_DestinationPoint.Y*10);

        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_Maze[e.X / 10, e.Y / 10] = 1;
            }
            else m_Maze[e.X / 10, e.Y / 10] = 0;

            Rectangle r = new Rectangle((e.X/10*10), (e.Y/10)*10, 10, 10);
            panelMaze.Invalidate(r);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "maze files (*.maze)|*.maze|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Save(saveFileDialog1.FileName);
            }
            
        }
        private void Save(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Create);
            // Create the writer for data.
            BinaryWriter w = new BinaryWriter(fs);

            for (int i = 0; i < m_Maze.GetLength(1); i++)
            {
                for (int a = 0; a < m_Maze.GetLength(0); a++)
                {
                    w.Write(m_Maze[a, i]);
                }
            }
            w.Close();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "maze files (*.maze)|*.maze|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                LoadFile(openFileDialog1.FileName);
            }

            panelMaze.Invalidate();
        }

        private void LoadFile(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            // Create the reader for data.
            BinaryReader w = new BinaryReader(fs);
            
            for (int i = 0; i < m_Maze.GetLength(1); i++)
            {
                for (int a = 0; a < m_Maze.GetLength(0); a++)
                {
                    (m_Maze[a, i]) = w.ReadInt32();
                }
            }
            w.Close();
        }

        private void StartServerbutton_Click(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = host.AddressList[0];

            IPAddress ipAddress = IPAddress.Any; // budeme poslouchat na vsech lokalnich adresach

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 6000);

            //*** vypiseme vsechny lokalni IPv4 Adresy, ktere jsou k dispozici
            adresaServeru.Text = "";
            foreach (IPAddress add in host.AddressList)
            {
                string txt = add.ToString();
                if (txt.Contains('.')) //filtrujeme jenom IPv4 adresy
                adresaServeru.Text += txt + " ";
            }
            
            try
            {
                // Create a Socket that will use Tcp protocol      
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                m_TCPServer = new TcpServer(listener);

                m_TCPServer.SetForm(this);

                m_TcpThread = new Thread(m_TCPServer.ThreadProc);
                m_TcpThread.Start();

            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
            }

            StartServerbutton.Enabled = false;
        }

        private void onClosed(object sender, FormClosedEventArgs e)
        {
            m_TCPServer.SetExit(); // signalizujeme ze koncime 

            m_TcpThread.Join(1000); // Close the TCP server thread

        }

        private void repaintBtn_Click(object sender, EventArgs e)
        {
            panelMaze.Invalidate();
           
        }

        private void setDestinationPointBtn_Click(object sender, EventArgs e)
        {
            m_DestinationPoint.X = Convert.ToInt32(destinationX.Value);
            m_DestinationPoint.Y = Convert.ToInt32(destinationY.Value);
        }
    }
}
