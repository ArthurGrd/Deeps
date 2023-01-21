using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffSet;
    public Vector3 positionOffSet;
    
    private Vector3 _velocity;
    
    
    private void Update()
    {
        transform.position =
            Vector3.SmoothDamp(transform.position, player.transform.position + positionOffSet, ref _velocity, timeOffSet);
    }
}
