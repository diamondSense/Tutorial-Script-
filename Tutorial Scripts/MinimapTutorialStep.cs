using System.Collections;
using SubmarineHunt.Core;
using UnityEngine;

public class MinimapTutorialStep : AEvent
{
    [Header("References")]
    public UIController ui;

    [Header("Configuration")]
    public string instructionMessage;
    public AudioClip voiceOver;
    public string taskGoal;
    public AudioClip collectionSound;

    private bool collected = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySoundEffect(collectionSound);
            ui.RevealCompleteMinimap();
            collected = true;
        }
    }

    public override bool CanRun()
    {
        return true;
    }

    public override IEnumerator Run()
    {
        // ensure correct state
        collected = false;
        gameObject.SetActive(true);
        ui.AcitvateMinimap();

        ui.DisplayInfoText(instructionMessage, voiceOver, null, true, false);
        ui.DisplayLevelText(taskGoal);

        while (!collected)
        {
            yield return null;
        }
    }
}