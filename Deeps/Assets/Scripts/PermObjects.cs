using UnityEngine;

public class PermObjects : MonoBehaviour
{
    public GameObject[] objectsToKeep;
    
    
    private void Awake()
    {
        foreach (GameObject gameObject in objectsToKeep)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
