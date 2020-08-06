using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button m_Button;
    public GameObject m_LockImage;

    private void Start()
    {
        m_Button = GetComponent<Button>();
    }

    public void Close()
    {
        m_Button.enabled = false;
        m_LockImage.SetActive(true);
        ;
    }


    public void Open()
    {
        ;
        m_Button.enabled = true;
        m_LockImage.SetActive(false);
    }
}
