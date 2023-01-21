using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    private PlayerProgress playerProgress;
    private int[] _barPoints;

    private void Start()
    {
        playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _barPoints = playerProgress.GetBarPoints();
    }

    private void Update()
    {
        slider.value = playerProgress.GetProgress();
    }
    
    public void progressBarUpdateByZone(float leftBorder, float rightBorder, PlayerProgress.ProgressZones progressZone)
    {
        Vector3 playerPosition = playerProgress.transform.position;
        if (playerPosition.x >= leftBorder && playerPosition.x <= rightBorder)
        {
            float ratioProgression = (playerPosition.x - leftBorder) / (rightBorder -leftBorder);
            playerProgress.SetProgress(_barPoints[(int)progressZone] + ratioProgression * (_barPoints[(int)progressZone+1] - _barPoints[(int)progressZone]));
        }
    }
}
