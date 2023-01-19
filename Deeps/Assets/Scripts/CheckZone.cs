using UnityEngine;

public class CheckZone : MonoBehaviour
{
    public ProgressBar progressBar;
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.name == "Door1Opening" && collision.CompareTag("Player"))
        {
            door.SetActive(false);
            progressBar.SetProgress(1);
        }
        else if (transform.name == "Door1Closing" && collision.CompareTag("Player"))
        {
            door.SetActive(true);
        }
    }
}
