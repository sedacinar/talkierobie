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

        GameOver gameOver;
        HintLogic hintLogic;
        CompositeDisposable disposables;

        int index = 0;
        int wrong = 0;
        int correct = 0;
        private void Start()
        {
            options = new List<OptionModel>();
            disposables = new CompositeDisposable();

            gameOver = FindObjectOfType<GameOver>();
            hintLogic = FindObjectOfType<HintLogic>();
            optionComponent = FindObjectOfType<Options>();
            source = GetComponentInChildren<AudioSource>();

            allData = GameManager.Instance.GetCatalog();
            SienarMessageBus.OnEvent<AnswerEvent>().Subscribe(ev =>
            {
                hintLogic.HideHint();
                if (index < allData.Count)
                {

                    StartCoroutine(WaitedCreation());
                }
                else
                {
                    StartCoroutine(FinishGame());
                }
                if (ev.IsCorrect)
                {
                    correct++;
                }
                else
                {
                    wrong++;
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
            foreach (var item in allData)
            {
                creationList.Add(item);
            }

            answer = new OptionModel();
            answer.CatalogModel = allData[index];
            answer.IsCorrect = true;

            hintLogic.SetHint(answer.CatalogModel.Hint);
           
            options.Add(answer);
            creationList.RemoveAt(index);

            for (int i = 0; i < 3; i++)
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
            index++;
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

        IEnumerator FinishGame()
        {
            hintLogic.StopTimer();
            yield return new WaitForSeconds(3);
            gameOver.FinishStats(correct, wrong);
        }
    }
}