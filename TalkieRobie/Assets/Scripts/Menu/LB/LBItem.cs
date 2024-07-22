using Sienar.TalkieRobie.DataCenter;
using TMPro;
using UnityEngine;
namespace Sienar.TalkieRobie.Menu.LB
{
    public class LBItem : MonoBehaviour
    {
        [SerializeField]
        TMP_Text Position;

        [SerializeField]
        TMP_Text Name;

        [SerializeField]
        TMP_Text Skor;

        public void SetLB(LeaderboardModel model)
        {
            SetName(model.Name);
            SetSkor(model.Skor);
            SetPos(model.Position);
        }
        void SetPos(int pos) 
        {
            pos++;
            Position.text = pos.ToString();
        }
        void SetName(string name) 
        {
            Name.text = name;
        }
        void SetSkor(int skor) 
        {
           
            Skor.text = skor.ToString();
        }

    }
}