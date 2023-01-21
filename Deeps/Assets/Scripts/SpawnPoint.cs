using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private PlayerGameplay playerGameplay;
    private void Awake()
    {
        playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        playerGameplay.setActualSpawn(gameObject);
        playerGameplay.transform.position = transform.position;
    }
}
