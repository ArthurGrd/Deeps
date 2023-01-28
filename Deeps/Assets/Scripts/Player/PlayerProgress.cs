using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public enum ProgressZones
    {
        Spawn,
        Parkour1,
        Parkour2,
        Parkour3,
        Parkour4
    }
    
    private float _currentProgress;
    private ProgressZones _currentProgressZone;
    private readonly int[] _barPoints = {0, 5, 27, 51, 74, 100};
    private GameObject[] _borders;
    private PlayerGameplay _playerGameplay;
    private ProgressBar _progressBar;


    //-------------GETTERS-SETTERS-------------
    public float GetProgress() { return _currentProgress; }
    public void SetProgress(float value) { _currentProgress = value; }
    public ProgressZones GetCurrentProgressZone() { return _currentProgressZone; }
    public void SetCurrentProgressZone(ProgressZones value) { _currentProgressZone = value; }
    public int[] GetBarPoints() { return _barPoints; }
    public GameObject[] GetBorders() { return _borders; }
    public void SetBorders(GameObject[] gameObjects) { _borders = gameObjects; }
    //----------------------------------------
    
    
    private void Start()
    {
        _progressBar = GameObject.Find("SliderProgress").GetComponent(typeof(ProgressBar)) as ProgressBar;
        _playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        _currentProgress = 0;
        _currentProgressZone = ProgressZones.Spawn;
    }
}
