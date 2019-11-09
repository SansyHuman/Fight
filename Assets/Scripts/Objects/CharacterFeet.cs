using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for characters' feet. Used to check if the character is landing.
/// </summary>
public class CharacterFeet : MonoBehaviour
{
    private float collisionCnt = 0;

    /// <value>Gets the number of physical objects that is colliding with the feet.</value>
    public float CollisionCnt => collisionCnt;

    /// <value>Gets whether the character is landing.</value>
    public bool IsLanding => collisionCnt > 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionCnt++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionCnt--;
    }
}
