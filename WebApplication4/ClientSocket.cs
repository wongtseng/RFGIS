using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WebApplication4
{
    class ClientSocket
    {
        UdpClient clientSocket=null;
        public ClientSocket() {
            
        }
        public void checkCapacity(String ip,int port,int compacity,int compacityLeft,bool isAlert)
        {
            try
            {
                //开始连接
               
                clientSocket = new UdpClient();
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                byte[] sendBytes = this.sendBytes(compacity, compacityLeft, isAlert);
                Console.WriteLine(sendBytes[0]+"   "+sendBytes[11]);
                
                clientSocket.Send(sendBytes, sendBytes.Length, iep);
                byte[] bytes = clientSocket.Receive(ref iep);

                string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                string message = "来自" + iep.ToString() + "的消息";
                Console.WriteLine("message is:"+message);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally {
                if (clientSocket != null) {
                    clientSocket.Close();
                }
                
            }
        }

        private byte[] sendBytes(int compacityValue,int compacityLeftValue,bool AlertFlag) {
            byte[] sendBytes = new byte[12];
            byte[] preByte = new byte[3];//消息头
            byte[] compacity = new byte[4];//库容
            byte[] compacityLeft = new byte[4];//剩余库容
            byte[] kurongInfo = new byte[8];//库容byte
            byte[] baojingInfo = new byte[9];//库容+报警
            byte[] baojing = new byte[1];

            preByte[0] = 0x55;
            preByte[1] = 0x55;
            preByte[2] = 0x55;

            compacity = System.BitConverter.GetBytes(compacityValue);
            compacityLeft = System.BitConverter.GetBytes(compacityLeftValue);
            kurongInfo = byteAdd(compacity, compacityLeft);

            if (AlertFlag)
            {
                baojing[0] = 0x11;
            }
            else {
                baojing[0] = 0x00;
            }

            baojingInfo = byteAdd(kurongInfo, baojing);

            sendBytes = byteAdd(preByte, baojingInfo);//拼接头+库容
            //4个字节库容，4个字节剩余库容，1个字节报警 00不报警 01报警
            return sendBytes;
        }

        private byte[] byteAdd(byte[] pre, byte[] data)
        {
            byte[] tempdata = new byte[pre.Length + data.Length];
            Array.Copy(pre, 0, tempdata, 0, pre.Length);
            Array.Copy(data, 0, tempdata, pre.Length, data.Length);
            return tempdata;
        }



    }
}
