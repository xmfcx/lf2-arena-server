namespace lf2_arena_server
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      var arena = new Arena();
      arena.Listen(30100);
    }
  }
}