using UnityEngine;
using UnityEngine.UI;

public class SequenceButton : MonoBehaviour
{
    public string TargetButton;
    public Image KeyboardButtonImage;
    public Image ControllerButtonImage;

    private SequenceData _activeData;


    public void SetSequenceData(SequenceData data)
    {
        _activeData = data;
        TargetButton = data.TargetButton;
        KeyboardButtonImage.sprite = data.KeyboardButtonSprite;
        ControllerButtonImage.sprite = data.ControllerButtonSprite;
    }

    public void ClickedCorrect()
    {
        Debug.Log("correct");
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);

    }

    public void ClickWrong()
    {

        Debug.Log("wrong");
    }
}
