using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private bool isAttacking = false;

    private const float fistStretch = 2 / 12f;
    private const float fistRemain = 4 / 12f;
    private const float fistBack = 6 / 12f;
    private const float attackEnd = 7 / 12f;
    private const float range = 1.03f;

    private GameObject fist;
    private Collider2D fistCol;
    private Transform fistTr;
    private Animator anim;
    private SpriteRenderer sprite;

    private IEnumerator attack;

    public bool IsAttacking => isAttacking;

    private void Awake()
    {
        fist = transform.Find("Fist").gameObject;
        fistCol = fist.GetComponent<Collider2D>();
        fistTr = fist.transform;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        Vector3 fistPos = fistTr.localPosition;
        fistPos.x = 0;
        fistTr.localPosition = fistPos;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            attack = Attack();
            StartCoroutine(attack);
        }
    }

    private WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    private IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Marisa_Attack");

        float time = 0;

        fistCol.enabled = true;
        if (sprite.flipX)
        {
            while (time < attackEnd)
            {
                yield return fixedUpdate;

                time += Time.deltaTime;
                Vector3 fistPos = fistTr.localPosition;

                if (time < fistStretch)
                    fistPos.x = -range * time / fistStretch;
                else if (time < fistRemain)
                    fistPos.x = -range;
                else if (time < fistBack)
                    fistPos.x = -range * (1 - (time - fistRemain) / (fistBack - fistRemain));

                fistTr.localPosition = fistPos;
            }
        }
        else
        {
            while (time < attackEnd)
            {
                yield return fixedUpdate;

                time += Time.deltaTime;
                Vector3 fistPos = fistTr.localPosition;

                if (time < fistStretch)
                    fistPos.x = range * time / fistStretch;
                else if (time < fistRemain)
                    fistPos.x = range;
                else if (time < fistBack)
                    fistPos.x = range * (1 - (time - fistRemain) / (fistBack - fistRemain));

                fistTr.localPosition = fistPos;
            }
        }

        isAttacking = false;
        anim.SetBool("isAttacking", false);
        Vector3 fistPosition = fistTr.localPosition;
        fistPosition.x = 0;
        fistTr.localPosition = fistPosition;
    }
}
