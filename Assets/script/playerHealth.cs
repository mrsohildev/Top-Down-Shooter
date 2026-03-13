using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public GameObject gameoverpannel;
    public float Health;
    public float damge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameoverpannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("playerdie");  
            gameoverpannel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void takeDamge()
    {
        Health -= damge;
    }
}
