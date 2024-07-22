using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Android;
namespace Sienar.TalkieRobie.Game
{
    public class HintPanel : MonoBehaviour
    {
        [SerializeField]
        RectTransform Hint;

        [SerializeField]
        TMP_Text HintTxt;

        Sequence sequence;
        public void Show()
        {
            Hint.gameObject.SetActive(transform);
            sequence = DOTween.Sequence();
            sequence.Append(Hint.DOAnchorPosX(0, 1));
            sequence.AppendInterval(3).OnComplete(() => { Hide(); });
        }
        public void SetHint(string hint)
        {
            HardHide();
            HintTxt.text = hint;
        }

        public void HardHide()
        {
            sequence.Kill();
            Hint.DOAnchorPosX(Hint.sizeDelta.x,0.01f);
            Hint.gameObject.gameObject.SetActive(false);
        }
        void Hide()
        {
            Hint.DOAnchorPosX(Hint.sizeDelta.x , 1).OnComplete(() => 
            {
                Hint.gameObject.SetActive(false);
            });    
        }
    }
}