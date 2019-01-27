using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Combat.Sequences
{
    public class RefreshBar : MonoBehaviour
    {
        public Image RefreshBarImage;
        public Color NormalColor;
        public Color ErrorColor;

        public void Refresh()
        {

            RefreshBarImage.color = NormalColor;
        }

        public void TriggerError()
        {
            RefreshBarImage.color = ErrorColor;

        }

        public void SetState(float current, float max)
        {
            if (max == 0)
                RefreshBarImage.fillAmount = 0;
            else
                RefreshBarImage.fillAmount = current / max;
        }
    }
}
