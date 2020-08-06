
using UnityEngine;

public class CharaBlock : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody2d;




    private void Start()
    {
        m_Rigidbody2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        m_CurrentColor = GetComponent<SpriteRenderer>().color;
    }


    private void RetifyPosition()
    {
        if (m_currentTileBlock)
        {
            transform.position =  Vector2.Lerp(transform.position, m_currentTileBlock.position, Time.fixedDeltaTime* m_retifySpeed);
            m_LastPosition = m_currentTileBlock.position;
        }
    }


    public void Move(Vector2 newDirection,bool isMove)
    {
        if(isMove)
        {
            Vector2 targetLocation= m_LastPosition + newDirection;
            Vector2 direction = targetLocation - m_LastPosition;
            m_Rigidbody2d.MovePosition(targetLocation);
        }
        else
        {
            RetifyPosition();

        }
    }

    public float m_retifySpeed = 0.5f;
    public float m_FingerForce = 1f;
    public Transform  m_currentTileBlock;
    public Vector2 m_LastPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_currentTileBlock = collision.transform; 

    }



    public Color m_CurrentColor = Color.white;


    /// <summary>
    /// 这里要开始 学习混色模式了。
    /// </summary>
    /// <param name="newColor"></param>
    public void AddColor(Color blockColor)
    {
        Color newColor = (blockColor + m_CurrentColor)/2;
        GetComponent<SpriteRenderer>().color = newColor;
    }
}


public enum SoundType
{
    normal,
    change,
    end,
}