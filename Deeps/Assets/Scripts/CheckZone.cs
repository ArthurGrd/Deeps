using UnityEngine;

public class CheckZone : MonoBehaviour
{
    public ProgressBar progressBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            progressBar.SetProgress((int)progressBar.slider.value+1);
        }
    }
}
