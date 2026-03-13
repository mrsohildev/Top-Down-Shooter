using UnityEngine;

public class enmmyController : MonoBehaviour
{

    public float Health;
    public float damge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {            
            Die();
        }
    }
    public void takeDamge()
    {
        Health -= damge;
    }
    void Die()
    {
        
        Destroy(gameObject);
    }


}
