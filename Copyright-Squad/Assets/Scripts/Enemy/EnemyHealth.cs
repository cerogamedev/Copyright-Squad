using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int _health, _maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHealth(int health)
    {
        _health += health;
    }
    public int GetHealth()
    {
        return _health;
    }
}
