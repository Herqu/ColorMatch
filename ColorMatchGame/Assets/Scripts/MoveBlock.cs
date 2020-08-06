using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class MoveBlock : MonoBehaviour
{
    public SpriteRenderer m_bodyColor;
    public Sprite m_NormalBlock;
    public Sprite m_EmptyBlock;
    public MoveBlockType m_Type = MoveBlockType.Empty;
    public GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_bodyColor = transform.GetChild(0).GetComponent<SpriteRenderer>();

        m_source = GetComponent<AudioSource>();
        m_Partialsystem = GetComponent<ParticleSystem>();

        if (m_Type == MoveBlockType.End)
        {
            m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            m_GameManager.RegistEnd();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {



        ///如果是空块。
        if (collision.tag == "Player" && m_Type == MoveBlockType.Empty)
        {
            //collision.GetComponent<CharaBlock>().AddColor(GetComponent<SpriteRenderer>().color);
            //Destroy(gameObject);
            m_bodyColor.enabled = true;
            m_bodyColor.color = collision.GetComponent<SpriteRenderer>().color;
            PlayMusic(SoundType.normal);
            GetComponent<SpriteRenderer>().sprite = m_NormalBlock;
            PlayParticle();
        }

        ///如果是可变块。
        if (collision.tag == "Player" && m_Type == MoveBlockType.Changeable)
        {
            if(collision.GetComponent<SpriteRenderer>().color == m_bodyColor.color)
            {
                PlayMusic(SoundType.normal);
            }
            else
            {
                collision.GetComponent<CharaBlock>().AddColor(m_bodyColor.color);
                PlayMusic(SoundType.change);
                PlayParticle();

            }
            //Destroy(gameObject);

        }

        ///如果是维持块。
        if (collision.tag == "Player" && m_Type == MoveBlockType.Constant)
        {
            collision.GetComponent<CharaBlock>().AddColor(m_bodyColor.color);
            PlayMusic(SoundType.change);
            PlayParticle();
            //Destroy(gameObject);
        }

        ///如果是终止块
        if (collision.tag == "Player" && m_Type == MoveBlockType.End)
        {
            if (ColorCheck(collision.GetComponent<CharaBlock>()))
            {
                m_GameManager.SubEnd();
                m_Type = MoveBlockType.Changeable;
                GetComponent<SpriteRenderer>().sprite = m_NormalBlock;
                PlayMusic(SoundType.end);
                PlayParticle();
            }
            else
            {
                PlayMusic(SoundType.normal);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag== "Player" && m_Type == MoveBlockType.Empty)
        {
            m_bodyColor.color = collision.GetComponent<SpriteRenderer>().color;
            m_Type = MoveBlockType.Changeable;

        }
        if (collision.tag == "Player" && m_Type == MoveBlockType.Changeable)
        {
            m_bodyColor.color = collision.GetComponent<SpriteRenderer>().color;

        }

        if (collision.tag == "Player" && m_Type == MoveBlockType.Empty)
        {
            m_bodyColor.color = collision.GetComponent<SpriteRenderer>().color;
            m_Type = MoveBlockType.Changeable;

        }

        
    }

    /// <summary>
    /// 检车两者的颜色是否完全一致。
    /// </summary>
    /// <param name="CB"></param>
    /// <returns></returns>
    public bool ColorCheck(CharaBlock CB)
    {
        if (CB.m_CurrentColor == m_bodyColor.color)
            return true;
        else
            return false;
    }


    [Header("音乐部分")]
    public AudioSource m_source;
    public AudioClip m_NormalClip;
    public AudioClip m_ChangeColorClip;
    public AudioClip m_GetEndClip;
    public GameObject m_AudioSource;
    public ParticleSystem m_Partialsystem;

    public void PlayMusic(SoundType st)
    {
        switch (st)
        {
            case SoundType.change:
                m_source.clip = m_ChangeColorClip;
                m_source.Play();
                break;
            case SoundType.normal:

                m_source.clip = m_NormalClip;
                m_source.Play();
                break;
            case SoundType.end:
                m_source.clip = m_GetEndClip;
                m_source.Play();
                break;
        }

    }

    public void PlayParticle()
    {
        m_Partialsystem.startColor= m_bodyColor.color;
        m_Partialsystem.Play();
    }










    /// <summary>
    /// 编辑器颜色绘制。
    /// </summary>

    public Color BasicColor = Color.red; 
    public Color ChangeColor = Color.red; 
    public Color ConstColor = Color.red; 
    public Color EndColor = Color.red;



    void OnDrawGizmos()
    {
#if UNITY_EDITOR        
        if(m_Type == MoveBlockType.Empty)
        {
            Gizmos.color = BasicColor;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 0.2f);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 0.2f);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.2f);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 0.2f);
        }
        else if(m_Type == MoveBlockType.Changeable)
        {
            Gizmos.color = ChangeColor;
            Gizmos.DrawLine( transform.position + Vector3.up * 0.2f, transform.position + Vector3.right * 0.2f);
            Gizmos.DrawLine( transform.position + Vector3.up * 0.2f, transform.position + Vector3.left* 0.2f);
            Gizmos.DrawLine(transform.position + Vector3.left * 0.2f, transform.position + Vector3.right * 0.2f);
        }
        else if(m_Type == MoveBlockType.Constant)
        {
            Gizmos.color = ConstColor;
            Gizmos.DrawLine(transform.position + Vector3.down* 0.2f, transform.position + Vector3.right * 0.2f);
            Gizmos.DrawLine(transform.position + Vector3.down * 0.2f, transform.position + Vector3.left * 0.2f);
            Gizmos.DrawLine(transform.position + Vector3.left * 0.2f, transform.position + Vector3.right * 0.2f);

        }
        else if(m_Type == MoveBlockType.End)
        {
            Gizmos.color = EndColor;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.2f+ Vector3.right* 0.2f, transform.position + Vector3.up * 0.2f + Vector3.left* 0.2f);
            Gizmos.DrawLine(transform.position + Vector3.down* 0.2f + Vector3.right * 0.2f, transform.position + Vector3.down * 0.2f + Vector3.left * 0.2f);

        }

#endif

    }

}

public enum MoveBlockType
{
    Empty,
    Changeable,
    Constant,
    End,

}
