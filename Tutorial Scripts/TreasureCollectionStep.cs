using System.Collections;
using SubmarineHunt.Core;
using UnityEngine;

public class TreasureCollectionStep : AEvent
{
    [Header("References")]
    public UIController ui;

    [Header("Configuration")]
    public string startMessage;
    public AudioClip startVoiceOver;
    public string taskGoal;
    public AudioClip collectionSound;

    private bool collected = false;
    private const string treasureTag = "Treasure";

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
            collected = true;
            ui.ClearHighlightedPositionOnMinimap(treasureTag);
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
        ui.HighlightPositionOnMinimap(treasureTag, gameObject.transform.position, Color.red);

        ui.DisplayInfoText(startMessage, startVoiceOver, null, true, false);
        ui.DisplayLevelText(taskGoal);

        while (!collected)
        {
            yield return null;
        }
    }
}