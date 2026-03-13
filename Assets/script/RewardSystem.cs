using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public int points = 0;
    public GameObject wintest;
    private void Start()
    {
        wintest.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            points++;
            Destroy(other.gameObject);
            Debug.Log("Points: " + points);
        }
    }
    private void Update()
    {
        if(points >= 5)
        {
            WinGame();
        }
    }
    void WinGame()
    {
        Debug.Log("YOU WIN!");
        wintest.SetActive(true);
    }
}

