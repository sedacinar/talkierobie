using System;
using UnityEngine;
namespace Sienar.TalkieRobie.DataCenter
{
    [Serializable]
    public class CatalogModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string VoiceUrl { get; set; }
        public string Hint { get; set; }
        public AudioClip AudioClip { get; set; }

        public Sprite   Image { get; set; }
    }
}