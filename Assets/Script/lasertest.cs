using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Transform target;

    public float maxLaserLength = 10f;

    public float laserGrowthSpeed = 5f;

    private float currentLaserLength = 0f;

    private bool isLaserShooting = false;

    private Vector2 directionToTarget;

    public float extraDistanceAfterHit = 20f;
    public int laserDamage;

    private bool hasDealtDamage = false; 

    private void Start()
    {
        int Randomdmg = Random.Range(2, 6);
        laserDamage = Randomdmg;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isLaserShooting)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                ShootLaser();
            }
        }
    }

    private void ShootLaser()
    {
        if (target == null) return;

        directionToTarget = (target.position - transform.position).normalized;

        float distanceToTarget = Vector2.Distance(transform.position, target.position) + extraDistanceAfterHit;
        currentLaserLength = 0f;
        isLaserShooting = true;
        hasDealtDamage = false; 

        StartCoroutine(ExpandLaser(distanceToTarget));
    }

    private IEnumerator ExpandLaser(float distanceToTarget)
    {
        while (currentLaserLength < distanceToTarget)
        {
            currentLaserLength += laserGrowthSpeed * Time.deltaTime;
            currentLaserLength = Mathf.Min(currentLaserLength, distanceToTarget);

            lineRenderer.SetPosition(0, transform.position);
            Vector2 laserEndPoint = (Vector2)transform.position + directionToTarget * currentLaserLength;
            lineRenderer.SetPosition(1, laserEndPoint);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionToTarget, currentLaserLength);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Player") && !hasDealtDamage)
                {
                    PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(laserDamage);
                        hasDealtDamage = true; 
                    }
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);
        lineRenderer.enabled = false;
        isLaserShooting = false;
    }
}
