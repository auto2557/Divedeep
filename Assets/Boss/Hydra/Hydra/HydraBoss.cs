using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for using UI elements like Slider

public class HydraBoss : enemyHP
{
    public Transform player; 
    public float speed;
    private Animator animator;
    public Animator[] part;
    private int skillNumber = 0;
    
    public Slider healthSlider;

    public GameObject Dimension;
    public GameObject[] head;
    public GameObject[] hydra;
    public GameObject[] redzone;
    public GameObject[] hitblock;
    public GameObject ULTboss;

    private const int maxHP = 6000;

    public float waitTimes;
     private bool isMoving = false;

    void Start()
    {
        isMoving = false;
        StartCoroutine(startFight());
        
        Dimension.SetActive(false);
        waitTimes = 15f;

        redzone[0].SetActive(false);
        redzone[1].SetActive(false);
        redzone[2].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);

        ULTboss.SetActive(false);

        Dictionary<int, int> bgmSelections = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 1 }
        };

        SoundManager.instance.PlayMultipleBGM(bgmSelections);

        speed = 0.8f;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject Player = GameObject.FindWithTag("Player");
        Playerscript = Player.GetComponent<player>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();

        healthSlider.maxValue = maxHP;
        healthSlider.value = maxHP;

        StartCoroutine(waitTime());
    }

    void Update()
    {
        if (isMoving && player != null) 
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if(hp <= 2000 && hp > 1000)
        {
            Destroy(head[0]);
            part[0].SetTrigger("die");
            Destroy(hydra[0],2f);
        }
        else if(hp <= 1000 && hp > 500)
        {
            Destroy(head[1]);
            part[1].SetTrigger("die");
            Destroy(hydra[1],2f);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead && !isHit)
        {
            isHit = true;

            int damageAmount = Playerscript.damage;
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            UpdateHealthSlider();

            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

            StartCoroutine(ResetHit());
        }
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = Mathf.Clamp(hp, 0, maxHP);
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(waitTimes);

        waitTimes = 15f;
        Dimension.SetActive(false);

        redzone[0].SetActive(false);
        redzone[1].SetActive(false);
        redzone[2].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);

        part[0].SetBool("ult",false);
         part[1].SetBool("ult",false);
          part[2].SetBool("ult",false);
           part[3].SetBool("ult",false);
            part[4].SetBool("ult",false);

         ULTboss.SetActive(false);

        Camera.main.orthographicSize = 2.409138f;

        Head1atk scriptToRemove = hydra[0].GetComponent<Head1atk>();
        if (scriptToRemove != null)
        {
            Destroy(scriptToRemove);
        }

        Head2atk scriptToRemove2 = hydra[1].GetComponent<Head2atk>();
        if (scriptToRemove2 != null)
        {
            Destroy(scriptToRemove2);
        }

        Head3atk scriptToRemove3 = hydra[2].GetComponent<Head3atk>();
        if (scriptToRemove3 != null)
        {
            Destroy(scriptToRemove3);
        }

        LaserSpawner scriptToRemove4 = gameObject.GetComponent<LaserSpawner>();
        if (scriptToRemove4 != null)
        {
            Destroy(scriptToRemove4);
        }

        StartCoroutine(coolDownskill());
    }

    IEnumerator coolDownskill()
    {
        yield return new WaitForSeconds(5f);

            if(hp > 5000)
            {
        int patternSkill = Random.Range(1, 5);
        skillNumber = patternSkill;
            }
            else if(hp <= 5000)
            {
                int patternSkill = Random.Range(1, 6);
        skillNumber = patternSkill;
            }
            else if(hp<= 3000)
            {
                int patternSkill = Random.Range(1, 7);
        skillNumber = patternSkill;
            }
            else if(hp <= 1000)
            {
                int patternSkill = Random.Range(4, 7);
        skillNumber = patternSkill;
            }
        
        patternBoss();
        StartCoroutine(waitTime());
    }

    private void patternBoss()
    {
        switch (skillNumber)
        {
            case 1:
             isMoving = true;
                redzone[0].SetActive(true);
                hitblock[0].SetActive(true);
                hydra[0].AddComponent<Head1atk>();
                Debug.Log("1");
                break;

            case 2:
             isMoving = true;
                redzone[1].SetActive(true);
                hitblock[1].SetActive(true);
                hydra[1].AddComponent<Head2atk>();
                Debug.Log("2");
                break;

            case 4:
             isMoving = true;
                redzone[2].SetActive(true);
                hitblock[2].SetActive(true);
                hydra[2].AddComponent<Head3atk>();
                Debug.Log("3");
                break;

            case 3:
             isMoving = true;
                redzone[0].SetActive(true);
                hitblock[0].SetActive(true);
                redzone[1].SetActive(true);
                hitblock[1].SetActive(true);
                redzone[2].SetActive(true);
                hitblock[2].SetActive(true);

                hydra[0].AddComponent<Head1atk>();
                hydra[1].AddComponent<Head2atk>();
                hydra[2].AddComponent<Head3atk>();
                Debug.Log("4");
                break;

            case 5:
             isMoving = true;
                gameObject.AddComponent<LaserSpawner>();
                break;

                case 6:
                Dimension.SetActive(true);
                 waitTimes = 30f;
                 isMoving = false;
                ULTboss.SetActive(true);
                    part[0].SetBool("ult",true);
                    part[1].SetBool("ult",true);
                    part[2].SetBool("ult",true);
                    part[3].SetBool("ult",true);
                    part[4].SetBool("ult",true);
                break;
        }
    }

    IEnumerator startFight()
    {
        yield return new WaitForSeconds(10f);
        isMoving = true;
    }
}
