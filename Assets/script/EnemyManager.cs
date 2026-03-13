using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int totalEnemies;
    public GameObject wintest;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Scene me jitne bhi enemies hain, unko count karega
        totalEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        Debug.Log("Total Enemies: " + totalEnemies);
        wintest.SetActive(false);
    }

    public void EnemyKilled()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
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
