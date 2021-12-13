using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;  
using System.Threading;
using System.Drawing;

namespace NetMazeServer
{
    class TcpServerHandler
    {
        private Socket handler;
        private bool m_ExitFlag = false;
        private bool m_Connected = false;
        private Form1 m_Form;


        public TcpServerHandler(Socket listener_socket,Form1 form)
        {
            lock (this)
            {
                handler = listener_socket;
                m_Form = form;
            }
        }

        public void SetExit()
        {

            lock (this)
            {
                m_ExitFlag = true;
                if (handler != null) handler.Close();
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

        public void SetConnected(bool val)
        {
            lock (this)
            {
                m_Connected = val;
            }
        }

        public bool GetConnected()
        {
            bool connected = false;
            lock (this)
            {
                connected = m_Connected;
            }
            return (connected);
        }



        public void ThreadProc()
        {
            // Incoming data from the client.    
            byte[] command = new byte[1];
            byte[] bytes = new byte[1024];  

            SetConnected(true);

            try
            {

                    while (GetExit() == false)
                    {
                       
                        int bytesRec = handler.Receive(command);

                        if (command[0] == 1) // nastavit pozici avatara
                        {
                            bytesRec = handler.Receive(command);
                            int id = command[0];

                            bytesRec = handler.Receive(command);
                            int x = command[0];

                            bytesRec = handler.Receive(command);
                            int y = command[0];

                            bool result = m_Form.SetAvatarPos(id, x, y);

                            if (result) command[0] = 1; //nastavime zda byl presun uspesny nebo ne
                            else command[0] = 0;
                            handler.Send(command); //posleme vyslednou hodnotu nazpet

                            //byte[] msg = Encoding.ASCII.GetBytes(txt);
                            //handler.Send(msg);
                            continue;
                        }

                        if (command[0] == 2) //vrati hodnotu bunky bludiste na zadane pozici
                        {
                            bytesRec = handler.Receive(command);
                            int id = command[0];

                            bytesRec = handler.Receive(command);
                            int x = command[0];

                            bytesRec = handler.Receive(command);
                            int y = command[0];

                            int value = m_Form.GetMazeItem(id ,x, y);

                            command[0] = Convert.ToByte(value);
                            handler.Send(command); //posleme hodnotu nazpet
                            continue;

                        }

                        if (command[0] == 0) // registrace avatara
                        {
                            bytesRec = handler.Receive(bytes);
                            string name = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            int id = m_Form.RegisterAvatar(name); // zaregistrujeme jmeno a ziskame ID

                            command[0] = Convert.ToByte(id);
                            handler.Send(command); //posleme ID nazpet
                            continue;
                        }

                    if (command[0] == 3) // zjisti adresu cile
                    {
                        Point bod = m_Form.GetDestinationPoint(); // zaregistrujeme jmeno a ziskame ID

                        command[0] = Convert.ToByte(bod.X);
                        handler.Send(command); //posleme X nazpet
                        command[0] = Convert.ToByte(bod.Y);
                        handler.Send(command); //posleme Y nazpet

                        continue;

                    }

                    if (command[0] == 99) // konec black klienta
                        {
                            bytesRec = handler.Receive(command);
                            int id = command[0];

                            m_Form.UnregisterAvatar(id);

                            command[0] = 1; // ok
                            handler.Send(command); //posleme nazpet
                            break;
                        }
                    }

                    SetConnected(false);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
