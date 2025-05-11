using System.Net;

namespace BrainZap_Server.CLASSES
{
    public class ClJugador
    {
        public string Nickname { get; set; }
        public IPEndPoint EndPoint { get; set; }
        public int Puntos { get; set; }
        public int UltimaRespuesta { get; set; } = -1;
        public bool YaRespondio { get; set; }

        public ClJugador(string nickname, IPEndPoint endpoint)
        {
            Nickname = nickname;
            EndPoint = endpoint;
            Puntos = 0;
        }

        public void SumarPuntos(int cantidad)
        {
            Puntos += cantidad;
        }

        public void ReiniciarParaNuevaPregunta()
        {
            UltimaRespuesta = -1;
            YaRespondio = false;
        }

        public override string ToString()
        {
            return $"{Nickname} - {Puntos} pts";
        }
    }
}
