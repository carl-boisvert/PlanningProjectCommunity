using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.Networking;

namespace GameServices
{
 public static class GameService
 {
     static private string BaseUrl = "http://localhost:5000";
 
     static private string GET_PLAYER = BaseUrl + "/player/steam/{0}";
     static private string POST_LOGIN_PLAYER = BaseUrl + "/login/steam";
 
     
         public static async Task<Player> GetPlayer(string steamId)
         {
             string response = await ServerRequest.GetRequest(String.Format(GET_PLAYER, steamId));
             Player player = JsonUtility.FromJson<Player>(response);
             return player;
         }
         
         public static async Task<Player> LoginPlayer(string sessionTicket)
         {
             Dictionary<string, string> data = new Dictionary<string, string>();
             data.Add("SessionTicket", sessionTicket);
             string response = await ServerRequest.PostRequest(POST_LOGIN_PLAYER, data);
             Player player = JsonUtility.FromJson<Player>(response);
             return player;
         }
     
     
             
         
     }   
}
