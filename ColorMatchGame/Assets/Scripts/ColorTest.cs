using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{

    public SpriteRenderer m_sp;

    public float r ;
    public float g ;
    public float b ;

    public float min ;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        m_sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Color c = m_sp.color;
            r = c.r;
            g = c.g;
            b = c.b;

            min = r;
            if (g <= min)
            {
                min = g;
            }
            if (b <= min)
            {
                min = b;
            }

            c.r -= min;
            c.g -= min;
            c.b -= min;

            m_sp.color = c;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Color c = m_sp.color;
            r = c.r;
            g = c.g;
            b = c.b;


            max = r;
            if (g >= min)
            {
                min = g;
            }
            if (b >= min)
            {
                min = b;
            }

            float ratio = 1 / min;

            c.r *= 2;
            c.g *= 2;
            c.b *= 2;

            m_sp.color = c;
        }
    }
}
