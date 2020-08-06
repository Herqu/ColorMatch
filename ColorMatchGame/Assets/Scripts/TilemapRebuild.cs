using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRebuild : MonoBehaviour
{
    public Tilemap m_tilemap;
    public Tile m_StopMoveBlock;

    public List<GameObject> m_allBlock =  new List<GameObject>();

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] allBlock= GameObject.FindGameObjectsWithTag("Block");

        m_allBlock.AddRange(allBlock);
        m_tilemap = GetComponent<Tilemap>();

        Vector3Int originPoint = new Vector3Int(-15, -17, 0);

        m_tilemap.FloodFill(Vector3Int.zero, m_StopMoveBlock);
        //m_tilemap.BoxFill(originPoint, m_StopMoveBlock,0,0,30,40);


        foreach (GameObject obj in m_allBlock)
        {
            Vector3Int v3i = m_tilemap.WorldToCell(obj.transform.position);
            m_tilemap.SetTile(v3i, null);
        }
    }


    void Start()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }



}
