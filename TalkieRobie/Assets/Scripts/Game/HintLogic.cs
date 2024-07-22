using UnityEngine;
namespace Sienar.TalkieRobie.Game
{
    public class HintLogic : MonoBehaviour
    {
        [SerializeField]
        HintPanel hintPanel;

        float timer = 0;
        bool isStart = false;
        
        public void SetHint(string message)
        {
            hintPanel.SetHint(message);
            StartTimer();
        }

        void Update()
        {
            if (isStart)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    isStart = false;
                    hintPanel.Show();
                }
            }
        }

        void StartTimer()
        {
            timer = 5;
            isStart = true;
        }
        public void HideHint()
        {
            StopTimer();
            hintPanel.HardHide();
        }
        public void StopTimer()
        {
            timer = 0;
            isStart = false;
        }
    }
}