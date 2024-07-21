using UnityEngine;
using System.Collections.Generic;
using Sienar.TalkieRobie.DataCenter;
namespace Sienar.TalkieRobie.Game
{
    public class Options : MonoBehaviour
    {
        [SerializeField]
        AnswerButton []allButtons;

        List<OptionModel> allOptions;
        
        public void SetOptions(List<OptionModel> models)
        {
            allOptions = models;
            for(int i = 0; i < allButtons.Length; i++) 
            {
                var index = RandomIndex();
                allButtons[i].SetData(allOptions[index]);
                allOptions.RemoveAt(index);
            }
        }

        int  RandomIndex()
        {
            var index = Random.Range(0, allOptions.Count);
            return index;
        }
    }
}