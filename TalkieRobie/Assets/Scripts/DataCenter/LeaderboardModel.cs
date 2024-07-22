using System;
using UnityEngine;
namespace Sienar.TalkieRobie.DataCenter
{
    [Serializable]
    public class LeaderboardModel 
    {
        [SerializeField]
        public int Position { get; set; }
       
        [SerializeField]
        public string Name { get; set; }
        
        [SerializeField]
        public int Skor { get; set; }
    }
}