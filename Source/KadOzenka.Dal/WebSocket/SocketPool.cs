using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace KadOzenka.Dal.WebSocket
{

    public class SocketPool
    {

        private static readonly List<System.Net.WebSockets.WebSocket> Clients = new List<System.Net.WebSockets.WebSocket>();
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        public async Task AddSocket(System.Net.WebSockets.WebSocket webSocket)
        {
            Locker.EnterWriteLock();
            try { Clients.Add(webSocket); }
            finally { Locker.ExitWriteLock(); }
            GC();
            await ListenSocketAsync(webSocket);
        }

        public async Task SendMessage(System.Net.WebSockets.WebSocket webSocket, string message) => await webSocket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);

        public async Task BroadCastMessage(string message)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                System.Net.WebSockets.WebSocket client = Clients[i];
                try { if (client.State == WebSocketState.Open) await client.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None); }
                catch (Exception) { RemoveSocket(client, i); }
            }
        }

        private void RemoveSocket(System.Net.WebSockets.WebSocket webSocket, int index)
        {
            Locker.EnterWriteLock();
            try
            {
                Clients.Remove(webSocket);
                index--;
            }
            finally { Locker.ExitWriteLock(); }
        }

        private async Task ListenSocketAsync(System.Net.WebSockets.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open) { WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None); }
        }

        private void GC()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                System.Net.WebSockets.WebSocket client = Clients[i];
                if (client.State != WebSocketState.Open) RemoveSocket(client, i);
            }
        }

    }

}