using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Novabeam : MonoBehaviour
{
    public GameObject redZone;
    public GameObject atkHitblock;
    public float cooldownTime;
    protected Animator novabeam;

    void Start()
    {
        redZone = GameObject.FindGameObjectWithTag("red1");
        atkHitblock = GameObject.FindGameObjectWithTag("block1");
        cooldownTime = 1f;
        novabeam = GetComponent<Animator>();
        redZone.SetActive(false);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }


    public virtual IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(1f);
        redZone.SetActive(true);
        StartCoroutine(attack());
    }

    public virtual IEnumerator attack()
    {
        yield return new WaitForSeconds(1f);
        redZone.SetActive(false);
        novabeam.SetTrigger("attack");
        atkHitblock.SetActive(true);
        StartCoroutine(cooldown());

    }


    public virtual IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        atkHitblock.SetActive(false);
        StartCoroutine(redZonealert());
    }
}
