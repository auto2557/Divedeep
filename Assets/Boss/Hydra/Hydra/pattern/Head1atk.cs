using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head1atk : HeadAtk
{
    
 void Start()
    {
        cooldownTime = 2f;
        hydra = GetComponent<Animator>();
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public override IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(3f);
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
