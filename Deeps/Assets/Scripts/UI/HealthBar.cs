using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image refHearth;

    private Image[] hearths;
    private PlayerHealth playerHealth;


    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        hearths = new Image[playerHealth.maxHealth];
        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i] = Instantiate(refHearth, transform.position, Quaternion.identity);
            hearths[i].transform.SetParent(transform);
            hearths[i].transform.localScale = new Vector3(1, 1, 1);
            hearths[i].transform.localPosition = new Vector3(transform.position.x + (i * 70), transform.position.y,transform.position.z);
            hearths[i].name = "Hearth N" + i;
        }
    }

    private void Update()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < playerHealth.GetCurrentHealth())
                hearths[i].enabled = true;
            else
                hearths[i].enabled = false;
        }    
    }
}
