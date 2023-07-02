using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet h�z�

    private Vector2 direction; // Bullet y�n�
    private void Start()
    {
        Destroy(this.gameObject, 8f);
    }
    void Update()
    {
        // Bullet'u belirlenen y�nde hareket ettirme
        transform.Translate(direction * speed * Time.deltaTime);

    }

    public void SetDirection(Vector2 dir)
    {
        // Bullet'un y�n�n� ayarlama
        direction = dir;
    }


}
