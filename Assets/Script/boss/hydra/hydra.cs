using UnityEngine;

public class Hydra : MonoBehaviour
{
    public enum AttackType { Melee, Spit, TailSwipe, Summon, Beam };
    public HydraBeamAttack beamAttack;     // อ้างอิงไปยังสคริปต์ beam
    public GameObject spitProjectilePrefab;
    public Transform[] firePoints;         // จุดที่จะยิง projectiles ออกมา
    public GameObject minionPrefab;
    public Transform summonPoint;

    private AttackType currentAttack;
    private float attackTimer;
    public float attackCooldown = 5f;      // เวลาระหว่างการโจมตีแต่ละครั้ง

    void Start()
    {
        attackTimer = attackCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            currentAttack = (AttackType)Random.Range(0, 5);  // สุ่มเลือกการโจมตี
            ExecuteAttack(currentAttack);
            attackTimer = attackCooldown;  // รีเซ็ตการโจมตีใหม่
        }
    }

    void ExecuteAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Melee:
                PerformMeleeAttack();
                break;
            case AttackType.Spit:
                PerformSpitAttack();
                break;
            case AttackType.TailSwipe:
                PerformTailSwipeAttack();
                break;
            case AttackType.Summon:
                PerformSummonAttack();
                break;
            case AttackType.Beam:
                beamAttack.StartBeamAttack();
                break;
        }
    }

    void PerformMeleeAttack()
    {
        // โจมตีประชิด
        Debug.Log("Hydra performs a melee attack!");
        // เพิ่ม logic การโจมตีประชิดที่นี่
    }

    void PerformSpitAttack()
    {
        Debug.Log("Hydra spits projectiles!");
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(spitProjectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    void PerformTailSwipeAttack()
    {
        Debug.Log("Hydra does a tail swipe!");
        // เพิ่ม logic การโจมตีด้วยหางที่นี่
    }

    void PerformSummonAttack()
    {
        Debug.Log("Hydra summons minions!");
        Instantiate(minionPrefab, summonPoint.position, summonPoint.rotation);
    }
}
