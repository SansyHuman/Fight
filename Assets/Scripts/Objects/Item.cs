using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private KeyCode useItem = KeyCode.LeftShift;  // BAD CODE
    [SerializeField] private bool isReuseable = false;
    [SerializeField] private float useCooldownTime = 0.5f;       // Cooldown time (for the item that is reuseable)
    [SerializeField] private int itemAmount = 1;                 // Amount of the item left to use (for the item that is not reuseable)
    private bool isItemOnCooldown = false;

    private Character player;
    private Animator chrAnim;

    public void Initialize(Character player)
    {
        this.player = player;
        chrAnim = this.player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useItem) && !isItemOnCooldown && player.Controllable)
        {
            UseItem();
            StartCoroutine(onShotCooldown());
        }
    }

    protected void UseItem()
    {

    }
}
