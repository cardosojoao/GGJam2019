using UnityEngine;

namespace Assets.Scripts.Util
{
    public class ObjectToggler : MonoBehaviour
    {
        public Transform PanelTransform;

        public void OpenPanel()
        {
            PanelTransform.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            PanelTransform.gameObject.SetActive(false);
        }

        public void ToggleObject()
        {
            PanelTransform.gameObject.SetActive(!PanelTransform.gameObject.activeSelf);
        }
    }
}
