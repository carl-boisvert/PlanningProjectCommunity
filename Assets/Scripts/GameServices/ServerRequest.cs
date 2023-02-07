using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GameServices
{
    public class ServerRequest
    {
        public static async Task<string> GetRequest(string uri)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                await request.SendWebRequest();
     
                if (request.isNetworkError)
                {
                    Debug.Log("Error: " + request.error);
                    return null;
                }
                else
                {
                    return request.downloadHandler.text;
                }
            }
        }
     
        public async static Task<string> PostRequest(string url, Dictionary<string, string> data)
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, data))
            {
                await request.SendWebRequest();
     
                if (request.isNetworkError)
                {
                    Debug.Log("Error: " + request.error);
                    return null;
                }
                else
                {
                    return request.downloadHandler.text;
                }
            }
        }
    }
}