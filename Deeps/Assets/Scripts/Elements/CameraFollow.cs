using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffSet;
    public Vector3 positionOffSet;
    public Camera cam;
    public float[] camPos = new float[8];

    private Vector3 _velocity;
    private bool _isPlaced;
    private float _originalZoom;
    
    private void Start()
    {
        _isPlaced = false;
        _originalZoom = cam.orthographicSize;
    }

    private void Update()
    {
        if (!_isPlaced)
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, player.transform.position + positionOffSet, ref _velocity, timeOffSet);   
        }
    }

    public void CamPlacement(bool isPlaced)
    {
        if (isPlaced)
        {
            cam.orthographicSize = _originalZoom * 3;
            _isPlaced = true;
        }
        else
        {
            cam.orthographicSize = _originalZoom;
            _isPlaced = false;
        }
    }
}
