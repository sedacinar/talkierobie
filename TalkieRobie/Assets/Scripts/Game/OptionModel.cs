using System;
using UnityEngine;
using Sienar.TalkieRobie.DataCenter;
namespace Sienar.TalkieRobie.Game
{
    [Serializable]
    public class OptionModel 
    {
        [SerializeField]
        public CatalogModel CatalogModel { get; set; }

        [SerializeField]
        public bool IsCorrect { get; set; }
    }
}