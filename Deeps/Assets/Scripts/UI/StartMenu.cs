using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


public class StartMenu : MonoBehaviour
{
    public GameObject startMenuUI;

    private void Start()
    {
        foreach (Object obj in GameObject.FindObjectsOfType(typeof(MonoBehaviour)))
        {
            GameObject objet;
            string parentName;
            if (obj.GameObject().transform.parent != null)
            {
                if (obj.GameObject().transform.parent.parent != null)
                {
                    if (obj.GameObject().transform.parent.parent.parent != null)
                    {
                        objet = obj.GameObject();
                        parentName = obj.GameObject().transform.parent.parent.parent.name;
                    }
                    else
                    {
                        objet = obj.GameObject();
                        parentName = obj.GameObject().transform.parent.parent.name;
                    }
                }
                else
                {
                    objet = obj.GameObject();
                    parentName = obj.GameObject().transform.parent.name;
                }
            }
            else
            {
                objet = obj.GameObject();
                parentName = obj.name;
            }

            if (parentName!="SpawnScreen")
            {
                Destroy(objet);
            }

        }
    }
    

    public void StartSoloGame()
    {
        SceneManager.LoadScene("Spawn");
    }

    public void StartMultiplayerGame()
    {
        Debug.Log("a");
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }
}