using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet hýzý

    private Vector2 direction; // Bullet yönü
    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }
    void Update()
    {
        // Bullet'u belirlenen yönde hareket ettirme
        transform.Translate(direction * speed * Time.deltaTime);

    }

    public void SetDirection(Vector2 dir)
    {
        // Bullet'un yönünü ayarlama
        direction = dir;
    }


}
