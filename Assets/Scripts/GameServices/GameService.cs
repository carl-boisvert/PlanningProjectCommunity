using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class GameService
{
    static private string BaseUrl = "http://localhost:5000";

    static private string GET_PLAYER = BaseUrl + "/player/steam/{0}";


    public static async Task<Player> GetPlayer(string steamId)
    {
        string response = await GetRequest(String.Format(GET_PLAYER, steamId));
        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }

        
    private static async Task<string> GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            await webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                return null;
            }
            else
            {
                return webRequest.downloadHandler.text;
            }
        }
    }
}
