using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.Networking;

namespace GameServices
{
    public class GameServer
    {
        static private string BaseUrl = "http://localhost:5000";
        static private string POST_REGISTER_SERVER = BaseUrl + "/server/register";
        static private string UPDATE_SERVER_STATUS = BaseUrl + "/server/status";

        public static Server server;
        public async static Task<bool> RegisterServer()
        {
            string ip = GetIPAddress();
            
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("IpAddress", ip);

            string response = await ServerRequest.PostRequest(POST_REGISTER_SERVER, data);
            server = JsonUtility.FromJson<Server>(response);
            
            Console.WriteLine($"Server registered with backend.");
            Console.WriteLine($"Id: {server.Id}");
            Console.WriteLine($"Ip: {server.IpAddress}");
            return true;
        }
        
        public async static Task<bool> ReadyForPlayers()
        {
            string ip = GetIPAddress();
            
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Id", GameServer.server.Id);
            data.Add("Status", "WAITING_FOR_PLAYERS");

            string response = await ServerRequest.PostRequest(UPDATE_SERVER_STATUS, data);
            server = JsonUtility.FromJson<Server>(response);
            
            return true;
        }
        
        static string GetIPAddress()
        {
            foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }  
            }

            return null;
        }
    }
}