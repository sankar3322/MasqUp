using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class VisualController : MonoBehaviour
{
    public float delay = 0.5f;
    [SerializeField] float openDuration = 0.33f, closeDuration = 0.1f, punchScale = 0;
    [SerializeField] Ease openEase = Ease.OutBack, closeEase = Ease.Linear;
    [SerializeField] bool activeAtStart, disableOnClick, isPulsing;
    [SerializeField] List<AudioClip> appearSFX, pressSFX;

    public delegate void PressedEvent(VisualController vc);
    public PressedEvent OnPressed;

    public bool isUnlocked, isClicked;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource audioSource;
    
    Button button;
    Vector3 activeScale;
    bool isShowing = false;
    public bool isEnabled { get; set; } = true;

    private void Awake()
    {
        if (GetComponent<Button>())
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Clicked);
        }
        activeScale = transform.localScale;
    }

    private void Start()
    {
        if (audioSource == null) audioSource = GameObject.FindGameObjectWithTag("FeedbackController").GetComponent<AudioSource>();
        transform.localScale = activeAtStart ? activeScale : Vector3.zero;
        if (activeAtStart) Show();
    }

    Coroutine showCR = null;

    public void Show()
    {
        if (isShowing) return;
        isShowing = true;
        isEnabled = true;
        if (showCR != null) StopCoroutine(showCR);
        transform.localScale = Vector3.zero;
        showCR = StartCoroutine(ShowCR());
    }

    IEnumerator ShowCR()
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        transform.DOScale(activeScale, openDuration).SetEase(openEase);
        showCR = null;
        if (isPulsing) Pulse();
    }

    public void Hide()
    {
        if (!isShowing) return;
        isShowing = false;
        transform.DOScale(Vector3.zero, closeDuration).SetEase(closeEase);
    }

    private void OnMouseDown()
    {
        if(!isClicked && isUnlocked) Clicked();
    }

    public void Clicked()
    {
        if (!isEnabled) return;
        if (punchScale != 0) DOTween.Sequence().Append(transform.DOPunchScale(Vector3.one * punchScale, 0.3f)).AppendCallback(() => isClicked = true);
        if (disableOnClick) isEnabled = false;
        OnPressed?.Invoke(this);
    }

    public void Pulse()
    {
        transform.DOScale(activeScale, closeDuration).SetEase(closeEase);

        DOTween.Sequence()
            .Append(transform.DOPunchScale(Vector3.one * punchScale * 3, 1f, vibrato: 1, elasticity: 5f))
            .AppendInterval(0.1f).SetLoops(-1);
    }
}
