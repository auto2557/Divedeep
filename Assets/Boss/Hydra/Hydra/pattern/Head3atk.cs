using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head3atk : HeadAtk
{
  void Start()
    {
      redZone = GameObject.FindGameObjectWithTag("red3");
        atkHitblock = GameObject.FindGameObjectWithTag("block3");
        cooldownTime = 1f;
        hydra = GetComponent<Animator>();
        atkHitblock.SetActive(false);
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public override IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySFX("boss", 1, 1);
        redZone.SetActive(true);
          StartCoroutine(attack());
    }

    public override IEnumerator attack()
    {
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySFX("boss", 2, 1);
        redZone.SetActive(false);
          StartCoroutine(attackhitblock());
        hydra.SetTrigger("attack");
        StartCoroutine(cooldown());
       
    }

    public override IEnumerator attackhitblock()
    {
         yield return new WaitForSeconds(0.5f);
        
        atkHitblock.SetActive(true);
    }

    public override IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }
}
