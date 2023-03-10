using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private PlayerGameplay _playerGameplay;
    private void Awake()
    {
        _playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        _playerGameplay.setActualSpawn(gameObject);
        _playerGameplay.transform.position = transform.position;
    }
}
