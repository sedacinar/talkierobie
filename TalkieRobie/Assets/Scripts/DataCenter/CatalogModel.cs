using System;
using UnityEngine;
using UnityEngine.UI;
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

        public Texture2D   Image { get; set; }
    }
}