
using Sienar.TalkieRobie.DataCenter;
using Sienar.TalkieRobie.Managers;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField]
    AudioSource source;
   
    void Start()
    {
        GameManager.Instance.BindOnLoadData(PlayVoice);
    }
    private void OnDestroy()
    {
        GameManager.Instance.UnBindOnLoadData(PlayVoice);
    }

    void PlayVoice(List<CatalogModel> models)
    {
        //source.clip = models[0].AudioClip;
        //source.Play();
    }
}
