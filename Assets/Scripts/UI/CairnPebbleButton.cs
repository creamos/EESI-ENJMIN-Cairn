using System;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(FauxFix))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class CairnPebbleButton : MonoBehaviour
{
    private static readonly int IsActivePropertyID = Animator.StringToHash("IsActive");
    
    [field:SerializeField]
    public Rock Data { get; private set; }
    
    private FauxFix fauxFix;
    private Button button;
    private AudioSource audioSource;

    public UnityEvent<Rock> OnTriggered;
    public UnityEvent<Rock> OnActivated, OnDeactivated;
    
    public RockEvent OnPebbleSpawned;
    public RockEvent OnTriggeredEvent, OnActivatedEvent, OnDeactivatedEvent;

    

    public bool IsActivated { get; private set; } = false;

    private void OnEnable()
    {
        fauxFix ??= GetComponent<FauxFix>();
        button ??= GetComponent<Button>();
        audioSource ??= GetComponent<AudioSource>();

        if (button)
        {
            button.onClick.RemoveListener(Trigger);
            button.onClick.AddListener(Trigger);
        }
    }

    private void OnDisable()
    {
        if (button)
        {
            button.onClick.RemoveListener(Trigger);
        }
    }

    private void Start()
    {
        if (Data) Initialize(Data, false);
    }

    public void Initialize(Rock data, bool activeState = false)
    {
        Data = data;
        RectTransform rectTr = transform as RectTransform;
        rectTr.sizeDelta = new Vector2(data.rockWidth * 107, rectTr.rect.height);
        fauxFix?.Initialize(data);
        IsActivated = activeState;
        audioSource.clip = Data.RockAudioCLip;
        
        if (fauxFix) fauxFix.IsPlaying = activeState;
        if (activeState) audioSource.Play();

        OnPebbleSpawned?.Raise(Data);
    }

    private void Trigger()
    {
        IsActivated = !IsActivated;

        OnTriggered?.Invoke(Data);
        OnTriggeredEvent?.Raise(Data);

        if (IsActivated) Activate();
        else Deactivate();
    }

    private void Activate()
    {
        if (fauxFix) fauxFix.IsPlaying = true;
        audioSource.Play();
        OnActivated?.Invoke(Data);
        OnActivatedEvent?.Raise(Data);
    }

    private void Deactivate()
    {
        if (fauxFix) fauxFix.IsPlaying = false;
        audioSource.Stop();
        OnDeactivated?.Invoke(Data);
        OnDeactivatedEvent?.Raise(Data);
    }
}