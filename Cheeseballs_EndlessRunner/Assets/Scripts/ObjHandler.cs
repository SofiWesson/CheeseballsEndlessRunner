using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHandler : MonoBehaviour
{
    // ================================== GENERAL ==================================
    //public
    public List<GameObject> objs = new List<GameObject>();

    // private

    // ================================== SPAWNING ==================================
    // public
    public Transform spawnLocation;
    public Transform deSpawnLocation;
    
    // private
    private float m_timer = 0;
    [SerializeField] private float m_bottomSpawnTime = 1;
    [SerializeField] private float m_topSpawnTime = 5;

    private GameObject m_GOBeingSpawn;

    // ================================== SCROLLING ==================================
    // public
    public float speed = 1;

    // private
    private List<Transform> m_backgroundObjects = new List<Transform>();
    private List<Transform> m_foregroundObjects = new List<Transform>();

    // ================================== ROOMS ==================================
    // public
    // 1 story
    public static GameObject H1S_Hall;
    public static GameObject H1S_InDoor;
    public static GameObject H1S_InWin;
    // story changers
    public static GameObject H1S_To2S;
    public static GameObject H2S_To1S;
    // 2 story
    public static GameObject H2S_Hall;
    public static GameObject H2S_InDoor;
    public static GameObject H2S_InWin;

    // private
    // 1 story
    private static GameObject[] H1S_Hall_Connectors = { H1S_Hall, H1S_To2S };
    private static GameObject[] H1S_InDoor_Connectors = { H1S_Hall, H1S_To2S };
    private static GameObject[] H1S_InWin_Connectors = { H1S_Hall, H1S_To2S };
    // story changes
    private static GameObject[] H1S_To2S_Connectors = { H2S_Hall, H2S_To1S };
    private static GameObject[] H2S_To1S_Connectors = { H1S_Hall, H1S_To2S };
    // 2 story
    private static GameObject[] H2S_Hall_Connectors = { H2S_Hall, H2S_To1S };
    private static GameObject[] H2S_InDoor_Connectors = { H2S_Hall, H2S_To1S };
    private static GameObject[] H2S_InWin_Connectors = { H2S_Hall, H2S_To1S };

    // TODO
    // each piece needs a list of other pieces that can go after it 

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_timer <= 0)
        {
            if (objs.Count == 0)
                m_GOBeingSpawn = objs[0]; // if only one thing use 1st index
            else
                m_GOBeingSpawn = objs[Random.Range(0, objs.Count - 1)]; // gets random object from list of possible objects

            GameObject go = Instantiate(m_GOBeingSpawn);
            go.transform.position = spawnLocation.position; // set position to right side of screen
            MovingObj moving = go.GetComponent<MovingObj>();
            moving.SetStartAndEndPosition(spawnLocation.position, deSpawnLocation.position);
            m_timer = Random.Range(m_bottomSpawnTime, m_topSpawnTime); // sets new time until 
        }
        else if (m_timer > 0)
            m_timer -= Time.fixedDeltaTime;
    }
}