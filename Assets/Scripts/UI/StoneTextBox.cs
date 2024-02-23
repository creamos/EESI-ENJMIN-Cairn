using UnityEngine;
using UnityEngine.UI;
using ScriptableEvents;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class StoneTextBox : MonoBehaviour
{
    // public GameObject[] stonesUI;
    public SelectPebbleButton currentStone;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;

    private AudioSource audioSource;
    
    public GameObject textBox;
    public Button pickRockButton;

    public RockEvent OnPebbleAddedByPlayer;
    public GameEvent OnPanelOpened;
    
    private void OnEnable()
    {
        OnPanelOpened?.Raise();
        audioSource ??= GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // init when selected stone
        pickRockButton.onClick.RemoveAllListeners();
        pickRockButton.onClick.AddListener(() => SendRockData());
    }

    public void OnPebbleSelected(SelectPebbleButton selectedPebble)
    {
        if (textBox.activeSelf == false) textBox.SetActive(true);
        UpdateText(selectedPebble);
        PlayAudio(selectedPebble);
    }

    private void PlayAudio(SelectPebbleButton selectedPebble)
    {
        if (audioSource)
        {
            audioSource.Stop();
            audioSource.clip = selectedPebble.Rock.RockAudioCLip;
            audioSource.Play();
        }
    }

    public void UpdateText(SelectPebbleButton selectedPebble) 
    {
        //update old Select Stone button
        if (currentStone) currentStone._fauxFixController.IsPlaying = false;
        
        //update new Select Stone button
        currentStone = selectedPebble;
        currentStone._fauxFixController.IsPlaying = true;
        
        // update text stone todo: uncomment when we have data text for Pebble
        titleText.text = currentStone.Rock.prefabName;
        bodyText.text = currentStone.Rock.rockDescription;
    }

    public void SendRockData () //datas to sendhere
    {
        // Reset rock selection panel
        if (currentStone) currentStone._fauxFixController.IsPlaying = false;
        gameObject.SetActive(false);
        textBox.SetActive(false);
        audioSource.Stop();
        
        // Debug.Log("Datas sent: "+currentStone.Rock.prefabName);
        OnPebbleAddedByPlayer?.Raise(currentStone.Rock);
    }
}
