using UnityEngine;
using UnityEngine.UI;
namespace Sienar.TalkieRobie.Game
{
    public class ListenBtn : MonoBehaviour
    {
        [SerializeField]
        AudioSource AnswerAudioSource;

        Button btn;

        private void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(PlaySound);
        }
        void PlaySound()
        {
           AnswerAudioSource.Play();
        }
    }
}