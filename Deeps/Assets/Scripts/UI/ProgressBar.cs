using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Sprite originalBar;
    
    private Slider _slider;
    private PlayerProgress _playerProgress;
    private int[] _barPoints;
    private Image _progressBarBack;

    private void Start()
    {
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _slider = GameObject.Find("SliderProgress").GetComponent(typeof(Slider)) as Slider;
        _barPoints = _playerProgress.GetBarPoints();
        _progressBarBack = GameObject.Find("ProgressBarBack").GetComponent(typeof(Image)) as Image;
        _progressBarBack.sprite = originalBar;
    }

    private void Update()
    {
        _slider.value = _playerProgress.GetProgress();
    }
    
    public void progressBarUpdateByZone(float leftBorder, float rightBorder, PlayerProgress.ProgressZones progressZone)
    {
        Vector3 playerPosition = _playerProgress.transform.position;
        if (playerPosition.x >= leftBorder && playerPosition.x <= rightBorder)
        {
            float ratioProgression = (playerPosition.x - leftBorder) / (rightBorder -leftBorder);
            _playerProgress.SetProgress(_barPoints[(int)progressZone] + ratioProgression * (_barPoints[(int)progressZone+1] - _barPoints[(int)progressZone]));
        }
    }
}
