using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefabinizi atayýn
    public Transform bulletSpawnPoint; // Bullet'un doðduðu pozisyonu belirtin
    public GameObject gunObject;
    public LayerMask obstacleLayer; // Engel layer'ýný belirtin
    public float fireRate = 2f; // Ateþ hýzý (saniyede kaç kere ateþ edeceði)

    private GameObject player;
    private bool canShoot = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player bulunamadý!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (canShoot && !HasObstacleBetween())
        {
            Shoot();
        }
        if (!HasObstacleBetween())
        {
            RotateGunTowardsPlayer();
        }
    }
    private void RotateGunTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - gunObject.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private bool HasObstacleBetween()
    {
        Vector3 direction = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, obstacleLayer);
        return hit.collider != null;
    }

    private void Shoot()
    {
        // Ateþ edildiðinde çaðrýlacak fonksiyon
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (player.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);
        bullet.GetComponent<Bullet>().speed = 10;

        canShoot = false;
        Invoke("ResetShoot", fireRate);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
