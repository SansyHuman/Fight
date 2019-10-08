using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFeet : MonoBehaviour
{
    private float collisionCnt = 0;

    public float CollisionCnt => collisionCnt;
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
