using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Coorth {


    public class RemoteDomain : ActorDomain, IAwake<IPEndPoint> {
        
        private IPEndPoint address;

        private readonly System.Net.Sockets.TcpClient client = new TcpClient();

        
        public void OnAwake(IPEndPoint p) {
            this.address = p;
        }

        public async Task Startup() {
            await client.ConnectAsync(address.Address, address.Port);
        }

        public Task Shutdown() {
            client.Close();
            return Task.CompletedTask;
        }
        
        public override ActorRef GetRef() {
            
            return default;
        }


    }
}