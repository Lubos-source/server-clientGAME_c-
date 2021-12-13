using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets; 
using System.Threading;
using System.Windows.Forms;

namespace NetMazeServer
{
    class TcpServer
    {
        private Socket listener;
        private bool m_ExitFlag = false;
        private List<Thread> m_Threads = new List<Thread>();
        private List<TcpServerHandler> m_Handlers = new List<TcpServerHandler>();
        private Form1 m_Form;


        public TcpServer(Socket listener_socket)
        {
            lock (this)
            {
                listener = listener_socket;
            }
        }

        public void SetForm(Form1 form) 
        {
            m_Form = form;
        }

        public void SetExit()
        {

            lock (this)
            {
                m_ExitFlag = true;
                if (listener != null) listener.Close();
            }
        }

        public bool GetExit()
        {
            bool exit = false;
         
            lock (this)
            {
                exit = m_ExitFlag;
            }
            return (exit);
        }


        public void ThreadProc()
        {
            try
            {

                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                while (GetExit() == false)
                {
                    listener.Listen(10);
                    Socket handler = listener.Accept();

                    TcpServerHandler TCPServer = new TcpServerHandler(handler,m_Form);

                    Thread TcpThread = new Thread(TCPServer.ThreadProc);
                    TcpThread.Start();

                    m_Threads.Add(TcpThread);

                }

                //*** signalizuje konec vsem vlaknum
                foreach (TcpServerHandler tcphndl in m_Handlers)
                {
                    tcphndl.SetExit();
                }
                
                //*** a ukoncime vsechna vlakna
                foreach (Thread thread in m_Threads)
                {
                    thread.Join(1000);
                }
 
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
    }
}
