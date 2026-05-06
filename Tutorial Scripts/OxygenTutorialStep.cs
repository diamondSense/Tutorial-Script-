using System.Collections;
using UnityEngine;

public class OxygenTutorialStep : AEvent
{
    [Header("References")]
    public UIController ui;
    public PlayerController player;

    [Header("Configuration")]
    public string startMessage;
    public AudioClip startVoiceOver;
    public string endMessage;
    public AudioClip endVoiceOver;

    private bool msgDelivered = false;

    public override bool CanRun()
    {
        return true;
    }

    public override IEnumerator Run()
    {
        ui.HideLevelTextBox();
        ui.DisplayInfoText(startMessage, startVoiceOver, MsgDeliveredCB, false, false);

        player.SetOxygenTime(startVoiceOver.length * 1.35f);
        ui.ActivateOxygenBar();
        player.ActivateOxygenDrain();

        while (player.OxygenStats > 0.2f || !msgDelivered)
            yield return null;

        msgDelivered = false;
        ui.DisplayInfoText(endMessage, endVoiceOver, MsgDeliveredCB, true, false);

        while (!msgDelivered)
            yield return null;
        ui.DeactivateOxygenBar();
    }

    private IEnumerator MsgDeliveredCB()
    {
        msgDelivered = true;
        yield return null;
    }
}