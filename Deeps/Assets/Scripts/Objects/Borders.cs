using UnityEngine;

public class Borders : MonoBehaviour
{
    private PlayerProgress playerProgress;
    private PlayerGameplay playerGameplay;
    private ProgressBar progressBar;
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
        playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        progressBar = GameObject.Find("Slider").GetComponent(typeof(ProgressBar)) as ProgressBar;

        _borders = childrens;
        playerProgress.SetBorders(childrens);
    }


    private void FixedUpdate()
    {
        if (playerGameplay.getActualScene().ToString() is "Spawn" or "Parkour1" or "Parkour2" or "Parkour3"
            or "Parkour4")
        {
            progressBar.progressBarUpdateByZone(_borders[0].transform.position.x,_borders[1].transform.position.x,playerProgress.GetCurrentProgressZone());
        }
    }
}
