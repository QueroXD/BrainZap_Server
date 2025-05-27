using System.Net.Sockets;
using System.Net;
using System;
using System.Net.Security;

public class ClUsuario
{
    public string Nickname { get; set; }
    public string IP { get; set; }
    public int Puerto { get; set; }
    public int Puntos { get; set; } = 0;

    public SslStream Stream { get; set; }
    public TcpClient Cliente { get; set; }


    public ClUsuario() { }

    public ClUsuario(string nickname, string ip, int puerto, SslStream stream, TcpClient cliente)
    {
        Nickname = nickname;
        IP = ip;
        Puerto = puerto;
        Stream = stream;
        Cliente = cliente;
    }
}

