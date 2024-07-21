using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sienar.Unity.Core.Scene;
using Sienar.TalkieRobie.Account;
using Sienar.Unity.Core.Zenject.Core;
using Sienar.Unity.Core.Scene.Interface;
namespace Sienar.TalkieRobie.Loading
{
    public class LoadingProgress : MonoBehaviour
    {
        [SerializeField]
        Image fill;

        ISceneLoader sceneLoader;
        LoginManager loginManager;
        private void OnEnable()
        {
            loginManager = DependencyContext.Get<LoginManager>();
            loginManager?.BindOnLogin(Load);
        }
        private void Start()
        {
            
            sceneLoader = DependencyContext.Get<SienarSceneLoader>();
           
        }
        void OnDisable() 
        {
            loginManager?.UnBindOnLogin(Load);
        }
        void Load()
        {
            StartCoroutine(LoadScene());
        }
        IEnumerator LoadScene()
        {
            var op = sceneLoader.LoadSceneAsync("Menu");
            while(!op.isDone) 
            {
                float progressValue = Mathf.Clamp01(op.progress / 0.9f);
                fill.fillAmount = progressValue;
                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}