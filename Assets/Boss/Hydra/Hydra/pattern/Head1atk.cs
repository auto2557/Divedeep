using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head1atk : HeadAtk
{
    
 void Start()
    {
        redZone = GameObject.FindGameObjectWithTag("red1");
        atkHitblock = GameObject.FindGameObjectWithTag("block1");
        cooldownTime = 2f;
        hydra = GetComponent<Animator>();
        atkHitblock.SetActive(false);
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public override IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(1f);
        redZone.SetActive(true);
          StartCoroutine(attack());
    }

    public override IEnumerator attack()
    {
        yield return new WaitForSeconds(2f);
        redZone.SetActive(false);
          StartCoroutine(attackhitblock());
        hydra.SetTrigger("attack");
        StartCoroutine(cooldown());
       
    }

    public override IEnumerator attackhitblock()
    {
         yield return new WaitForSeconds(1.4f);
            atkHitblock.SetActive(true);
    }

    public override IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }
}
