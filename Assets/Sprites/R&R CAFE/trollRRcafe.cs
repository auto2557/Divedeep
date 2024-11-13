using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollRRcafe : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject eme3;
    public Animator anim;
    private int Case = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        int randomCase = Random.Range(1, 3);
        Case = randomCase;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Case == 1) {
            if (collision.CompareTag("hitblock") || collision.CompareTag("slash"))
            {
                StartCoroutine(alert());
            }
        }
        else if (Case == 2)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
            {

                StartCoroutine(alert());
            }
        }
    }

    IEnumerator alert()
    {
        anim.SetBool("alert1", true);
        yield return new WaitForSeconds(2f);
        Instantiate(eme3, spawnPoint.transform.position, Quaternion.identity);
        anim.SetBool("alert2", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
