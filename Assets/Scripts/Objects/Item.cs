using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private KeyCode useItem = KeyCode.LeftShift;// Bad Code
    [SerializeField] private bool isReuseable = false;
    [SerializeField] private float itemUsageTime = 2f;           // Use time ,combine with itemCooldown time to determine how long it takes to be able to use again
    [SerializeField] private float itemCooldownTime = 0.5f;      // Cooldown time for using the item
    [SerializeField] private int itemAmount = 3;                 // Amount of the item left to use (for the items that are not reuseable)
    private bool isItemOnCooldown = false;
    private bool isUsing = false;

    protected Character player;
    private Animator chrAnim;

    public void Initialize(Character player)
    {
        this.player = player;
        chrAnim = this.player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useItem) && !isItemOnCooldown && player.Controllable && itemAmount > 0)
        {
            if (!isReuseable)
                itemAmount--;
            isUsing = true;
            UseItem();
            StartCoroutine(StopItem());
            StartCoroutine(onItemCooldown());
        }
    }

    private IEnumerator StopItem()
    {
        yield return new WaitForSeconds(itemUsageTime);
        isUsing = false;
    }

    protected abstract void UseItem();

    private IEnumerator onItemCooldown()
    {
        yield return new WaitForSeconds(itemUsageTime + itemCooldownTime);
        isItemOnCooldown = false;
    }
}
