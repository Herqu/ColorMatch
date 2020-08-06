using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 FingerMove = Vector2.zero;
    public Vector2 BeginFingerPosition;
    public Vector2 EndFingerPosition;
    public Camera m_Camera;
    public MoveDirection m_Movedirection = MoveDirection.None;


    [Header("角色目标")]
    public CharaBlock m_Chara;

    private void Start()
    {
        //string path = Application.persistentDataPath + "/player.fun";
        //FileStream stream = new FileStream(path, FileMode.Open);
        ////CurrentLevelData data = formatter.Deserialize(stream) as CurrentLevelData;
        //stream.Close();


        if (GameObject.FindGameObjectWithTag("Player"))
            m_Chara = GameObject.FindGameObjectWithTag("Player").GetComponent<CharaBlock>();
        else
            m_Chara = null;
        m_Camera = Camera.main;



    }

    private void Update()
    {
        if (m_Chara)
        {
            InputHandle();

        }
    }

    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginFingerPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        }


        if (Input.GetMouseButton(0))
        {

            EndFingerPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            FingerMove = EndFingerPosition - BeginFingerPosition;


            if (m_Movedirection == MoveDirection.None)
            {
                float x = Mathf.Abs(FingerMove.x);
                float y = Mathf.Abs(FingerMove.y);
                if (x > y)
                {
                    m_Movedirection = MoveDirection.Hor;
                }
                else if (x < y)
                {
                    m_Movedirection = MoveDirection.Ver;
                }
                else
                {
                    m_Movedirection = MoveDirection.None;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_Movedirection = MoveDirection.None;
            FingerMove = Vector2.zero;
        }



    }


    private void FixedUpdate()
    {
        if (m_Chara)
        {
            MoveChara();

        }

    }

    /// <summary>
    /// 移动角色。
    /// </summary>
    private void MoveChara()
    {
        Vector2 oneDLocation = Vector2.zero;

        if (m_Movedirection == MoveDirection.Hor)
        {
            oneDLocation = FingerMove.x * Vector2.right ;
            m_Chara.Move(oneDLocation, true);

        }
        else if(m_Movedirection == MoveDirection.Ver)
        {
            oneDLocation = FingerMove * Vector2.up;
            m_Chara.Move(oneDLocation, true);
        }
        else
        {
            m_Chara.Move(oneDLocation, false);

        }


    }





    public void OutGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void StartGameByMenu(GameObject obj)
    {
       
        SceneManager.LoadScene(obj.name);
    }

    public GameObject m_LevelMenu;
    public List<GameObject> m_levelbuttons;
    public void OpenLevelMenu()
    {
        m_LevelMenu.SetActive(true);
        m_levelbuttons.AddRange(GameObject.FindGameObjectsWithTag("LevelButton"));
        int i = SaveSystem.LoadLevel();
        for(int y =0; y <i;y++)
        {
            m_levelbuttons[y].GetComponent<Button>().enabled= true;
        }
    }

    public void CloseLevelMenu()
    {
        m_LevelMenu.SetActive(false);
    }




    //public MoveBlock 




    public int m_endBlockNum = 0;
    public GameObject m_WinUI;

    public void RegistEnd()
    {
        m_endBlockNum += 1;
    }

    public void SubEnd()
    {
        m_endBlockNum--;
        if(m_endBlockNum<= 0)
        {
            ///UI 里面出来一个重要的东西
            m_WinUI.SetActive(true);
        }
    }


    public void Nextlevel()
    {
        int i = int.Parse(SceneManager.GetActiveScene().name);
        i++;
        if (i <= SceneManager.sceneCountInBuildSettings- 2)
        {
            SceneManager.LoadScene(i.ToString());
            SaveSystem.SaveLevel(i);
        }
        else
        {
            SceneManager.LoadScene("LastScene");

        }


    }
}


public enum MoveDirection
{
    None,
    Hor,
    Ver,
}
