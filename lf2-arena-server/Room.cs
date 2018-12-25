using System.Collections.Generic;
using System.Net.Sockets;

namespace lf2_arena_server
{
  public class Room
  {
    private List<Player> _players;

    public void AddPlayer(Player player)
    {
      _players.Add(player);
    }
    
    
  }
}