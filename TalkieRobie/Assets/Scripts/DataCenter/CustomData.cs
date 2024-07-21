using System;
using UnityEngine;
namespace Sienar.TalkieRobie.DataCenter
{
    [Serializable]
    public class CustomData 
    {
        [SerializeField]
        public string Hint { get; set; }
        [SerializeField]
        public string Voice {  get; set; }
    }
}