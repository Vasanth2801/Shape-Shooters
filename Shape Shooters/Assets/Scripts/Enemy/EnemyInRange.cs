using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float lineOfSite = 5f;
    [SerializeField] private float shootingRange;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private ObjectPooler pooler;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float nextFireRate;

    void Start()
    {
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        pooler = FindAnyObjectByType<ObjectPooler>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);
        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if(distanceFromPlayer <= shootingRange && nextFireRate < Time.time)
        {
            GameObject enemyBullet = pooler.SpawnFromPools("EnemyBullet",firePoint.position,firePoint.rotation);
            Rigidbody2D bulletRb = enemyBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            nextFireRate = Time.time + fireRate;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,shootingRange);
    }
}
