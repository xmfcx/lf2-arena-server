using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace lf2_arena_server
{
  public class Arena
  {
    public void Listen(int port)
    {
      var clientListener = new TcpListener(IPAddress.Any, 30100);

      clientListener.Start();
      while (true)
      {
        try
        {
          // blocking listening operation
          var client = clientListener.AcceptTcpClient();

          // start an async communication channel with client

          var threadClient = new Thread(ThreadClient);
          threadClient.Start(client);
          threadClient.IsBackground = true;
        }
        catch (Exception)
        {
          // ignored
        }
      }
    }

    private void ThreadClient(object o)
    {
      var client = (TcpClient) o;
      var streamLf2 = client.GetStream();
      var ipEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
      Console.WriteLine("hello " + ipEndPoint.Address
                                 + ":" + ipEndPoint.Port);

      Player player;
      while (true)
      {
        // register or login with username and password
        var messageStr = MessageArena.ReceiveString(streamLf2);
        Console.WriteLine(messageStr);
        var pieces = messageStr.Split('\t');

        string registResult;
        if (pieces[0] == "register")
        {
          registResult = Authentication.Register(pieces[1], pieces[2]);
        }
        else
        {
          registResult = Authentication.Login(pieces[1], pieces[2]);
        }

        MessageArena.Send(streamLf2, registResult);
        if (registResult != "success")
          continue;
        player = new Player(client, pieces[1]);
        break;
      }
      
      // send list of rooms or players event based
      // until user decides to join or create a room
      

      MessageArena.Send(streamLf2, "wow şğüİçö");

//      using(var db = new LiteDatabase(@"MyData.db"))
//      {
//        // Get customer collection
//        var customers = db.GetCollection<Customer>("customers");
//
//        // Create your new customer instance
//        var customer = new Customer
//        { 
//          Name = "John Doe", 
//          Phones = new string[] { "8000-0000", "9000-0000" }, 
//          IsActive = true
//        };
//
//        // Insert new customer document (Id will be auto-incremented)
//        customers.Insert(customer);
//
//        // Update a document inside a collection
//        customer.Name = "Joana Doe";
//
//        customers.Update(customer);
//
//        // Index document using a document property
//        customers.EnsureIndex(x => x.Name);
//
//        // Use Linq to query documents
//        var results = customers.Find(x => x.Name.StartsWith("Jo"));
//      }


      client.Close();
      Console.WriteLine("bye " + ipEndPoint.Address
                               + ":" + ipEndPoint.Port);
    }
  }
}