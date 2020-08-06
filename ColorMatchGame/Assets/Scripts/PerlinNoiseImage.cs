using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoiseImage : MonoBehaviour
{
    public int pictureWidth = 100;
    public int pictrueHeight = 100;
    private float xoffest = .0f;
    private float yoffset = .0f;

    public float noisescale = 20.0f;
    public float scalespeed = 0.1f;
    public float scrollspeed = 1;
    public Gradient g;
    private Texture2D noiseTex;
    private Color[] pix;

    // Use this for initialization
    void Start()
    {
        noiseTex = new Texture2D(pictureWidth, pictrueHeight);
        GetComponent<MeshRenderer>().material.SetTexture("_MainTex", noiseTex);
        pix = new Color[pictureWidth * pictrueHeight];
        xoffest = pictureWidth / 2;
        yoffset = pictureWidth / 2;


        //GradientColorKey ck1 = new GradientColorKey(Color.red,0);
        //GradientColorKey ck2 = new GradientColorKey(Color.blue,1);
        //GradientColorKey[] colorkey = { ck1, ck2};
        //g.SetKeys(colorkey,g.alphaKeys);

    }

    // Update is called once per frame
    void Update()
    {
        CalcNoise();
        xoffest += Time.deltaTime * scrollspeed;
        yoffset += Time.deltaTime * scrollspeed;
    }

    private void CalcNoise()
    {
        float y = .0f;
        while (y < pictrueHeight)
        {
            float x = .0f;
            while (x < pictureWidth)
            {
                float xCoord = xoffest + x / pictureWidth * noisescale * (1 + Mathf.Sin(Time.time * scalespeed) * 0.1f);
                float yCoord = yoffset + y / pictrueHeight * noisescale * (1 + Mathf.Cos(Time.time * scalespeed) * 0.1f);
                Color color = g.Evaluate(Mathf.PerlinNoise(xCoord, yCoord));
                pix[(int)(y * pictureWidth + x)] = color;
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
}