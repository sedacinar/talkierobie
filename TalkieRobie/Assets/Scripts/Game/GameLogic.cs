using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sienar.TalkieRobie.Managers;
using Sienar.TalkieRobie.DataCenter;
namespace Sienar.TalkieRobie.Game
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField]
        RawImage AnswerImage;

        CatalogModel answer;   
        List<CatalogModel> options;
        List<CatalogModel> allData;

        int index = 0;
        private void Start()
        {
            options = new List<CatalogModel>();
            allData = GameManager.Instance.GetCatalog();
            CreateQuestion();
        }

        void CreateQuestion()
        {
            answer = allData[index];
            options.Add(answer);

            for(int i = 0; i < 3; i++) 
            {
                var wrongAnswerIndex = RandomIndex();
                options.Add(allData[wrongAnswerIndex]);
            }

            AnswerImage.texture = answer.Image;
        }

        int RandomIndex()
        {
            var index = Random.Range(0, allData.Count);
            if(index == this.index)
            {
                RandomIndex();
            }
            return index;
        }
    }
}