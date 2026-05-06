using System.Collections;
using UnityEngine;

public class LearnMovementStep : AEvent
{
    [Header("References")]
    public UIController ui;
    public PlayerController player;

    [Header("Configuration")]
    public string stepGoal;
    public string instructionText;
    public AudioClip voiceOver;
    public float movementMagnitude = 3;

    // privately scoped attributes
    private Vector2 currentPosition;
    private float currentMovement;
    private bool msgDelivered = false;

    public override bool CanRun()
    {
        return true;
    }

    public override IEnumerator Run()
    {
        // start instruction
        ui.DisplayInfoText(instructionText, voiceOver, MsgDeliveredCB, true, false);
        ui.DisplayLevelText(stepGoal);

        while (!msgDelivered)
        {
            // wait until the message is delivered
            yield return null;
        }

        // save current position
        currentPosition = player.Position;
        currentMovement = 0.0f;

        while (currentMovement < movementMagnitude)
        {
            Vector2 newPosition = player.Position;
            currentMovement += Vector2.Distance(currentPosition, newPosition);
            currentPosition = newPosition;
            yield return null;
        }
    }

    private IEnumerator MsgDeliveredCB()
    {
        msgDelivered = true;
        yield return null;
    }
}