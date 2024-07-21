using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Sienar.TalkieRobie.Managers;
using Sienar.TalkieRobie.DataCenter;
using Sienar.Unity.Core.Broker.Core;
namespace Sienar.TalkieRobie.Game
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField]
        RawImage AnswerImage;

        AudioSource source;
        OptionModel answer;   
        Options optionComponent;

        List<OptionModel> options;
        List<CatalogModel> allData;
        CompositeDisposable disposables;

        int index = 0;
        private void Start()
        {
            options = new List<OptionModel>();
            disposables = new CompositeDisposable();

            optionComponent = FindObjectOfType<Options>();
            source = GetComponentInChildren<AudioSource>();
            
            allData = GameManager.Instance.GetCatalog();
            SienarMessageBus.OnEvent<AnswerEvent>().Subscribe(ev => 
            {
                if(index < allData.Count) 
                {
                    StartCoroutine(WaitedCreation());
                }
                else 
                {
                    FinishGame();
                }
              
            }).AddTo(disposables);

            CreateQuestion();
        }
        private void OnDestroy()
        {
            disposables?.Dispose();
        }
        void CreateQuestion()
        {
            List<CatalogModel> creationList = new List<CatalogModel>();
            foreach(var item in allData) 
            {
                creationList.Add(item);
            }

            answer = new OptionModel();
            answer.CatalogModel = allData[index];
            answer.IsCorrect = true;

            options.Add(answer);
            creationList.RemoveAt(index);

            for(int i = 0; i < 3; i++) 
            {
                var wrongAnswerIndex = RandomIndex(creationList.Count);

                OptionModel optionModel = new OptionModel();
                optionModel.CatalogModel = creationList[wrongAnswerIndex];
                optionModel.IsCorrect = false;
                
                options.Add(optionModel);
                creationList.RemoveAt(wrongAnswerIndex);
            }

            optionComponent.SetOptions(options);

            AnswerImage.texture = answer.CatalogModel.Image;
            source.clip = answer.CatalogModel.AudioClip;
            source.Play();
            index ++;
        }
        IEnumerator WaitedCreation()
        {
            yield return new WaitForSeconds(3);
            CreateQuestion();
        }
        int RandomIndex(int count)
        {
            var index = Random.Range(0, count);
            return index;
        }

        void FinishGame()
        {
            Debug.LogError("FinishGame");
        }
    }
}