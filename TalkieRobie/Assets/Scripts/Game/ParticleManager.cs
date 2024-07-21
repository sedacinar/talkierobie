using Sienar.Unity.Core.Broker.Core;
using System.Collections;
using UniRx;
using UnityEngine;
namespace Sienar.TalkieRobie.Game
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] Success;
        [SerializeField]
        GameObject[] Fail;

        CompositeDisposable disposables;
        void Start () 
        {
            disposables = new CompositeDisposable ();
            SienarMessageBus.OnEvent<AnswerEvent>().Subscribe(ev => 
            {
                StartEffect(ev.IsCorrect);
            }).AddTo(disposables);
        }
        private void OnDestroy()
        {
            disposables?.Dispose ();
        }

        void StartEffect(bool IsCorrect)
        {
            if(IsCorrect) 
            {
                foreach(var item in Success) 
                {
                    item.SetActive(true);
                }
            }
            else 
            {
                foreach (var item in Fail)
                {
                    item.SetActive(true);
                }
            }
            StartCoroutine(CloseEffect());
        }

        IEnumerator CloseEffect()
        {
            yield return new WaitForSeconds(3);
            foreach (var item in Success) 
            {
                item.SetActive(false);
            }
            foreach (var item in Fail)
            {
                item.SetActive(false);
            }
        }
    }
}