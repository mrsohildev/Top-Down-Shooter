using UnityEngine;

public class enemmyBullet : MonoBehaviour
{   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth playerHealth = collision.collider.GetComponent<playerHealth>();
            if (playerHealth != null)
            {
                playerHealth.takeDamge();
            }
        }
        Destroy(this.gameObject);
    }
}

