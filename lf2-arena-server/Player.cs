using System.Net.Sockets;

namespace lf2_arena_server
{
  public class Player
  {
    private TcpClient _client;
    private string _name;

    public Player(TcpClient client, string name)
    {
      _client = client;
      _name = name;
    }
  }
}