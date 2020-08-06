using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentManager : MonoBehaviour
{
    public List<Transform> allLevelButton = new List<Transform>();
    void Start()
    {
        allLevelButton .AddRange( GetComponentsInChildren<Transform>());
        allLevelButton.Remove(transform);
        int i = 1;
        int levelNum = SaveSystem.LoadLevel();
        foreach(Transform t in allLevelButton)
        {
            if (t.GetComponent<Button>())
            {

                t.name = i.ToString();
                t.GetChild(0).GetComponent<Text>().text = t.name;
                if(i<= levelNum)
                {
                    t.GetComponent<LevelButton>().Open();
                }
                else
                {
                    t.GetComponent<LevelButton>().Close();
                }
                i++;

            }
        }
    }


}
