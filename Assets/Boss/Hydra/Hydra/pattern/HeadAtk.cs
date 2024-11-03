using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadAtk : MonoBehaviour
{
    public GameObject redZone;
    public GameObject atkHitblock;
    public float cooldownTime;
    protected Animator hydra;

    void Start()
    {
        cooldownTime = 2f;
        hydra = GetComponent<Animator>();
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public virtual IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(3f);
        redZone.SetActive(true);
          StartCoroutine(attack());
    }

    public virtual IEnumerator attack()
    {
        yield return new WaitForSeconds(3f);
        redZone.SetActive(false);
          StartCoroutine(attackhitblock());
        hydra.SetTrigger("attack");
        StartCoroutine(cooldown());
       
    }

    public virtual IEnumerator attackhitblock()
    {
         yield return new WaitForSeconds(1.5f);
            atkHitblock.SetActive(true);
    }

    public virtual IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }
}
