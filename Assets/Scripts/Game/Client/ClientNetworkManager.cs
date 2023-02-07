using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameServices;
using Model;
using Steamworks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ClientNetworkManager : NetworkManager
{
    private async void Start()
    {
        Player? player = await ConnectToBackend();
        ConnectToServer();
    }

    private async Task<Player?> ConnectToBackend()
    {
        if (SteamManager.Initialized)
        {
            Byte[] ticket = new byte[1024];
            uint sessionTicketSize;
            HAuthTicket ticketRequest = SteamUser.GetAuthSessionTicket(ticket, 1024, out sessionTicketSize);
            Array.Resize(ref ticket, (int)sessionTicketSize);
			
            string hex = BitConverter.ToString(ticket).Replace("-", "");
            return await GameService.LoginPlayer(hex);
        }

        return null;
    }

    // Start is called before the first frame update
    void ConnectToServer()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;
        StartClient();
        Debug.Log("Client ready");
    }
}
