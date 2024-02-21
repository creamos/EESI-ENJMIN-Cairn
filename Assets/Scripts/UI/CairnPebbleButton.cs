using System;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class CairnPebbleButton : MonoBehaviour
{
    private static readonly int IsActivePropertyID = Animator.StringToHash("IsActive");
    
    [field:SerializeField]
    public Rock Data { get; private set; }
    
    private Button button;
    private Animator animator;
    private AnimatorOverrideController animatorOverride;
    private AudioSource audioSource;

    public UnityEvent<Rock> OnTriggered;
    public UnityEvent<Rock> OnActivated, OnDeactivated;
    
    public RockEvent OnPebbleSpawned;
    public RockEvent OnTriggeredEvent, OnActivatedEvent, OnDeactivatedEvent;

    

    public bool IsActivated { get; private set; } = false;

    private void OnEnable()
    {
        button ??= GetComponent<Button>();
        animator ??= GetComponent<Animator>();
        animatorOverride ??= animator.runtimeAnimatorController as AnimatorOverrideController;
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
        transform.localScale = new Vector3(data.rockWidth,1,1);
        
        IsActivated = activeState;
        animatorOverride["Pebble_Standard"] = Data.RockAnimClip;
        animator.SetBool(IsActivePropertyID, IsActivated);
        audioSource.clip = Data.RockAudioCLip;

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
        OnActivated?.Invoke(Data);
        OnActivatedEvent?.Raise(Data);
        animator.SetBool(IsActivePropertyID, true);
        audioSource.Play();
    }

    private void Deactivate()
    {
        OnDeactivated?.Invoke(Data);
        OnDeactivatedEvent?.Raise(Data);
        animator.SetBool(IsActivePropertyID, false);
        audioSource.Stop();
    }
}
