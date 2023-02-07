using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private GameObject _serverGO;
    [SerializeField] private GameObject _clientGO;
    void Start()
    {
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
        {
            Console.WriteLine("Server Build");
            Instantiate(_serverGO);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Client Build");
            Instantiate(_clientGO);
            SceneManager.LoadSceneAsync("MainMenu");
            Destroy(gameObject);
        }
        
    }
}
