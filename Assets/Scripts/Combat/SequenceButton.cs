using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequenceButton : MonoBehaviour
{
    public string TargetButton;
    public Image KeyboardButtonImage;
    public Image ControllerButtonImage;
    public Animator Animator;

    private SequenceData _activeData;

    private bool _animating;
    public bool Clearing { get { return _animating; } }

    public void SetSequenceData(SequenceData data)
    {
        _activeData = data;
        TargetButton = data.TargetButton;
        KeyboardButtonImage.sprite = data.KeyboardButtonSprite;
        ControllerButtonImage.sprite = data.ControllerButtonSprite;
    }

    public void ClickedCorrect()
    {
        _animating = true;
        Animator.SetTrigger("Hit");
    }

    public void SelfDestroy()
    {
        if (this != null)
            StartCoroutine(WaitForClear());
    }

    private IEnumerator WaitForClear()
    {
        while (Clearing)
            yield return null;
        Destroy(gameObject);
    }

    public void ClickWrong()
    {
        //_animating = true;
        Debug.Log("wrong");
    }

    public void AnimationFinish()
    {
        _animating = false;

    }
}
