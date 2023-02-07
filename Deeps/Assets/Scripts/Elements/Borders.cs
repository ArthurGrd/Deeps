using UnityEngine;

public class Borders : MonoBehaviour
{
    private PlayerProgress _playerProgress;
    private PlayerGameplay _playerGameplay;
    private ProgressBar _progressBar;
    private GameObject[] _borders;

    
    private void Awake()
    {
        GameObject[] childrens = new GameObject[2];
        foreach (Transform child in transform)
        {
            if (child.tag=="BorderL")
            {
                childrens[0] = child.gameObject;
            }
            else if (child.tag == "BorderR")
            {
                childrens[1] = child.gameObject;
            }
        }
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        _progressBar = GameObject.Find("SliderProgress").GetComponent(typeof(ProgressBar)) as ProgressBar;

        _borders = childrens;
        _playerProgress.SetBorders(childrens);
    }


    private void FixedUpdate()
    {
        if (PlayerGameplay._actualScene.ToString() is "Spawn" or "Parkour1" or "Parkour2" or "Parkour3"
            or "Parkour4")
        {
            _progressBar.progressBarUpdateByZone(_borders[0].transform.position.x,_borders[1].transform.position.x,_playerProgress.GetCurrentProgressZone());
        }
    }
}
