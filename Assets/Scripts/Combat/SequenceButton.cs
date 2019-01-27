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

    public void SelfDestroyAnimation()
    {
        if (this != null)
            StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        while (Clearing)
            yield return null;

        _animating = true;
        Animator.SetTrigger("Die");

    }

    public void ClickWrong()
    {
        _animating = true;
        Animator.SetTrigger("Miss");
    }

    public void AnimationFinish()
    {
        _animating = false;

    }
}
