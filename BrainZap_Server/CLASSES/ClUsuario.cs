using System.Net;

namespace BrainZap_Server.CLASSES
{
    public class ClUsuario
    {
        public string Nickname { get; set; }
        public IPEndPoint IP { get; set; }
        public int Puntos { get; set; } = 0;

        public ClUsuario(string nickname, string ip, int puerto = 5555)
        {
            Nickname = nickname;
            IP = new IPEndPoint(IPAddress.Parse(ip), puerto);
        }
    }
}
