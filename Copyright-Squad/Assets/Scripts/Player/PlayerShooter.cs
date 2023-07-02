using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefabinizi atay�n
    public Transform bulletSpawnPoint; // Bullet'un do�du�u pozisyonu belirtin
    public float fireRate = 0.2f; // Ate� h�z� (saniyede ka� kere ate� edece�i)
    public GameObject gunObject; // Gun objesini atay�n

    private bool canShoot = true;

    private void Update()
    {
        RotateGunTowardsMouse();

        if (Input.GetMouseButton(0) && canShoot)
        {
            Shoot();
        }
    }

    private void RotateGunTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - gunObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        // Ate� edildi�inde �a�r�lacak fonksiyon
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        canShoot = false;
        Invoke("ResetShoot", fireRate);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
