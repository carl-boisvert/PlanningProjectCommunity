using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public struct Player
    {
        public string Id;
        public string SteamId;
        public string Gamertag;
        public List<string> Skins;
    }   
}
