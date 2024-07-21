using Sienar.Unity.Core.Broker.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sienar.TalkieRobie.Game
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField]
        TMP_Text txt;

        Button btn;
        OptionModel optionModel;
        private void Start()
        {
            btn = GetComponent<Button>();
            txt = btn.GetComponentInChildren<TMP_Text>();

            btn.onClick.AddListener(ButtonEvent);
        }

        public void SetData(OptionModel model)
        {
            optionModel = model;
            SetTxt(optionModel.CatalogModel.Name);
        }
        void SetTxt(string name) 
        {
            txt.text = name;
            txt.color = Color.white;
        }
        void ButtonEvent()
        {
            if(optionModel.IsCorrect) 
            {
                txt.color = Color.green;
                SienarMessageBus.Publish(new AnswerEvent { IsCorrect = true });
            }
            else 
            {
                txt.color = Color.red;
                SienarMessageBus.Publish(new AnswerEvent { IsCorrect = false });
            }
        }
    }
}