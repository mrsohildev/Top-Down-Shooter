using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enmmyController enemmyScript = collision.collider.GetComponent<enmmyController>();
            if (enemmyScript != null)
            {
                enemmyScript.takeDamge();
            }
        }
        Destroy(this.gameObject);
    }
}
