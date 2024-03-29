﻿using System.Collections;
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
    public List<int> unBlockList1;
    public List<int> unBlockList2;
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
        string name = gameObject.name;
        GameObject go = GameObject.Find("TilesController");
        TilesController tilesController = go.GetComponent<TilesController>();
        unBlockList1 = tilesController.unBlockList1;
        unBlockList2 = tilesController.unBlockList2;
        if (!tilesController.isPopup)
        {
            if (!isClicked && isUnlocked)
                Clicked();
            else
            {
                if (unBlockList1.Contains(int.Parse(name)) || unBlockList2.Contains(int.Parse(name)))
                {
                    audioSource.clip = audioClips[4];
                    audioSource.Play();
                }
                Debug.Log("Name....." + name + "  Unblock1.........." + unBlockList1.Count + "     Unblock2" + unBlockList2.Count);
            }
        }
    }

    public void Clicked()
    {
        if (!isEnabled) return;
        if (punchScale != 0) DOTween.Sequence().Append(transform.DOPunchScale(Vector3.one * punchScale, 0.3f));
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
