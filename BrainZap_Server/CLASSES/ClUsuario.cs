using System.Net.Sockets;
using System.Net;
using System;

public class ClUsuario
{
    public string Nickname { get; set; }
    public string IP { get; set; }
    public int Puerto { get; set; }

    public int Puntos { get; set; } = 0;

    public ClUsuario(string nickname, string ip, int puerto)
    {
        Nickname = nickname;
        IP = ip;
        Puerto = puerto;
    }
}

