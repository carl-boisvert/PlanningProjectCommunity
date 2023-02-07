using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using GameServer = GameServices.GameServer;

public class ServerNetworkManager : NetworkManager
{

    protected Callback<SteamServersConnected_t> OnSteamServerConnectedCallback;
    private async void OnEnable()
    {
        //Initialize Steam
        OnSteamServerConnectedCallback = Callback<SteamServersConnected_t>.Create(OnSteamServerConnected);

        InitializeSteam();
        
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        OnServerStarted += OnServerStartedCallback;
        OnClientConnectedCallback += OnClientConnected;
        OnClientDisconnectCallback += OnClientDisconnect;
        StartServer();

        await GameServer.RegisterServer();
        /*SteamGameServer.SetDedicatedServer(true);*/
        await GameServer.ReadyForPlayers();
        
        Console.WriteLine(GameServer.server.Status);
    }

    private void OnSteamServerConnected(SteamServersConnected_t param)
    {
        Console.WriteLine("Connected to steam server");
    }

    private void InitializeSteam()
    {
        SteamAPI.Init();
            
        if (!DllCheck.Test()) {
            Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
            return;
        }
            
        string authKey = "C53869B669E0F6A894741A7CE84B5DA4";
        SteamGameServer.LogOn(authKey);// Replace this with your authentication key
        SteamGameServer.SetProduct("Projet-Community-Server");
        SteamGameServer.SetDedicatedServer(true);
        SteamAPI.RunCallbacks();
    }

    private void OnClientDisconnect(ulong obj)
    {
        Console.WriteLine("Client Disconnected");
    }

    private void OnClientConnected(ulong obj)
    {
        Console.WriteLine("Client Connected");
    }

    private void OnServerStartedCallback()
    {
        Console.WriteLine("Server ready Single line");
    }
}
