using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Player player = await GameService.GetPlayer("steam_id");
        Debug.Log("Player Gamertag: " + player.Gamertag);
        Debug.Log("Player SteamId: " + player.SteamId);
    }
}
