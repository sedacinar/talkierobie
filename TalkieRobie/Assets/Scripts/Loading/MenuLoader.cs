using UnityEngine;
using System.Collections.Generic;
using Sienar.TalkieRobie.Managers;
using Sienar.TalkieRobie.DataCenter;
using Sienar.Unity.Core.Zenject.Core;
using Sienar.Unity.Core.Scene.Interface;
namespace Sienar.TalkieRobie.Loading
{
    public class MenuLoader : MonoBehaviour
    {
        ISceneLoader sceneLoader;
        void Start () 
        {
            GameManager.Instance.BindOnLoadData(LoadGameScene);
            sceneLoader = DependencyContext.Get<ISceneLoader>();
        }
        void OnDestroy () 
        {
            GameManager.Instance.UnBindOnLoadData(LoadGameScene);
        }
        void LoadGameScene(List<CatalogModel> models)
        {
            sceneLoader.LoadSceneAsync("Game");
        }
    }
}