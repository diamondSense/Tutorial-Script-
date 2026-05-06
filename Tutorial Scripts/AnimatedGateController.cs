using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using SubmarineHunt.Core;
using System.Collections.Generic;

public class AnimatedGateController : AEvent
{
    [Header("References")]
    public Tilemap tilemap;

    [Header("Configuration")]
    public float delayBetweenTiles = 0.3f; // Time delay between tile removal
    public AudioClip gateSound;
    private List<Vector3Int> gatePositions;

    private bool gateActive = false;
    private Coroutine opening;

    void Start()
    {
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        gatePositions = new List<Vector3Int>();
        // add each position to the array
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(pos))
                    gatePositions.Add(pos);
                else
                    Debug.Log($"No tile at {pos}");
            }
        }
    }

    // Use Stay as player might already be in the collider box
    // when the gate gets activated.
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gateActive)
        {
            Debug.Log("Player is in the gate area.");
            if (opening == null)
                opening = StartCoroutine(RemoveTiles());
        }
    }

    public override bool CanRun()
    {
        return true;
    }

    public override IEnumerator Run()
    {
        gateActive = true;
        yield return null;
    }

    private IEnumerator RemoveTiles()
    {
        foreach (Vector3Int pos in gatePositions)
        {
            Debug.Log($"Removing tile at {pos}");
            Debug.Log($"Tile at {pos}: {tilemap.HasTile(pos)}");
            tilemap.SetTile(pos, null); // Remove tile
            tilemap.RefreshTile(pos);
            SoundManager.Instance.PlaySoundEffect(gateSound);
            yield return new WaitForSeconds(delayBetweenTiles);
        }
    }
}