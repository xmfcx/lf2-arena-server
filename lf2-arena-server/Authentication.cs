namespace lf2_arena_server
{
  public class Authentication
  {
    public static string Register(string username, string password)
    {

      return "username cannot contain ?";
      return "too short";
      return "too long";
      return "success";
    }
    
    public static string Login(string username, string password)
    {

      return "user doesn't exist";
      return "wrong pass";
      return "success";
    }
  }
}