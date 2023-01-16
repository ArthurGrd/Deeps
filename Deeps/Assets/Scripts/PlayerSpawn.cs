using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }

    public void Revive()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
