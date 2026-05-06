using System.Collections;
using UnityEngine;

public class HealthTutorialStep : AEvent
{
    [Header("References")]
    public UIController ui;
    public PlayerController player;

    [Header("Configuration")]
    public string startMessage;
    public AudioClip startVoiceOver;
    public string taskGoal;
    public string endMessage;
    public AudioClip endVoiceOver;
    public float healthPoints;

    private bool msgDelivered = false;

    public override bool CanRun()
    {
        return true;
    }

    public override IEnumerator Run()
    {
        ui.DisplayInfoText(startMessage, startVoiceOver, MsgDeliveredCB, true, false);
        ui.DisplayLevelText(taskGoal);

        player.SetHealthPoints(healthPoints);
        player.ActivateDamage();
        ui.ActivateHealthBar();

        // wait for the player to take damage
        while (player.HealthStats >= 100f || !msgDelivered)
        {
            yield return null;
        }

        msgDelivered = false;
        ui.DisplayInfoText(endMessage, endVoiceOver, MsgDeliveredCB, true, false);

        while (!msgDelivered)
            yield return null;

        ui.DeactivateHealthBar();
        player.DeactivateDamage();
    }

    private IEnumerator MsgDeliveredCB()
    {
        msgDelivered = true;
        yield return null;
    }
}