using Sienar.Unity.Core.Scene.Interface;
using Sienar.Unity.Core.Zenject.Core;
using System.Collections;
using TMPro;
using UnityEngine;
namespace Sienar.TalkieRobie.Game
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        TMP_Text CorrectSkor;

        [SerializeField]
        TMP_Text WrongSkor;

        [SerializeField]
        GameObject GameOverPanel;

        ISceneLoader sceneLoader;

        private void Start()
        {
            sceneLoader = DependencyContext.Get<ISceneLoader>();
        }

        public void FinishStats(int correct, int wrong)
        {
            StartCoroutine(Finish());
            CorrectSkor.text = correct.ToString();
            WrongSkor.text = wrong.ToString();
            GameOverPanel.SetActive(true);
        }

        IEnumerator Finish()
        {
            yield return new WaitForSeconds(5);
            sceneLoader.LoadSceneAsync("Menu");
        }
    }
}