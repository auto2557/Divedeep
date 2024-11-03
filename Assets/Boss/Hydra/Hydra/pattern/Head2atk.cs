using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head2atk : HeadAtk
{
    void Start()
    {
        redZone = GameObject.FindGameObjectWithTag("red2");
        atkHitblock = GameObject.FindGameObjectWithTag("block2");
        cooldownTime = 0.5f;
        hydra = GetComponent<Animator>();
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public override IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(2f);
        redZone.SetActive(true);
          StartCoroutine(attack());
    }

    public override IEnumerator attack()
    {
        yield return new WaitForSeconds(3f);
        redZone.SetActive(false);
          StartCoroutine(attackhitblock());
        hydra.SetTrigger("attack");
        StartCoroutine(cooldown());
       
    }

    public override IEnumerator attackhitblock()
    {
         yield return new WaitForSeconds(0.4f);
            atkHitblock.SetActive(true);
    }

    public override IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }
}
