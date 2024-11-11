using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Novabeam2 : MonoBehaviour
{
    public GameObject redZone;
    public GameObject atkHitblock;
    public float cooldownTime;
    protected Animator novabeam;

    void Start()
    {
        redZone = GameObject.FindGameObjectWithTag("red2");
        atkHitblock = GameObject.FindGameObjectWithTag("block2");
        cooldownTime = 1f;
        novabeam = GetComponent<Animator>();
        redZone.SetActive(false);
        atkHitblock.SetActive(false);
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
        yield return new WaitForSeconds(4f);
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