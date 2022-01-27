using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collingWithPlayer; //check if colliding with player, if not then chase, if true then keep collide and stop chase
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Debug.Log(Vector3.Distance(playerTransform.position, startingPosition));
        // Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true; // chasing will be true if player in trigger length and vice
            if (chasing)
            {
                if (!collingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized); // chase player
                    // Debug.Log("chasing");
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position); // return to starting position
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlaps
        collingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) { continue; }
            if (hits[i].tag == "Fighter" && hits[i].name == "Player") { collingWithPlayer = true; }

            // The array is not cleaned up, so we clean it up ourselves
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+ " + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
