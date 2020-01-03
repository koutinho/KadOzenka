using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Collections.Generic;

namespace KadOzenka.Dal.WebSocket
{
    public class SocketPool
    {

        private static readonly List<System.Net.WebSockets.WebSocket> Clients = new List<System.Net.WebSockets.WebSocket>();
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        public static void ProgressInitWebSocket(System.Net.WebSockets.WebSocket webSocket)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            buffer = Encoding.ASCII.GetBytes("====>TestTestTest<====");
            System.Net.WebSockets.WebSocket socket = webSocket;
            if (socket.State == WebSocketState.Open)
            {
                socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            Locker.EnterWriteLock();
            try { Clients.Add(socket); }
            finally { Locker.ExitWriteLock(); }
            Console.WriteLine("=================================>SOCKET<=================================>DuplicateProgress<=================================");
            Console.WriteLine($"=================================>{Clients.Count}<=================================");
        }

        public static void ProgressInitWebSocket()
        {

        }

    }
}
