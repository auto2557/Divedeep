using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadAtk : MonoBehaviour
{
    public GameObject redZone;
    public GameObject atkHitblock;
    public float cooldownTime;
    private Animator hydra;

    void Start()
    {
        cooldownTime = 5f;
        hydra = GetComponent<Animator>();
        redZone.SetActive(false);
        StartCoroutine(redZonealert());
    }


    IEnumerator redZonealert()
    {
        yield return new WaitForSeconds(3f);
          redZone.SetActive(true);
          StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(3f);
        hydra.SetTrigger("attack");
        redZone.SetActive(false);
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        redZone.SetActive(true);
        StartCoroutine(redZonealert());
    }
}
