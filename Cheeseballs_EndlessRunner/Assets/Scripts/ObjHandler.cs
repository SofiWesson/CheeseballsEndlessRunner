using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHandler : MonoBehaviour
{
    // ================================== GENERAL ==================================
    //public
    [Header("General")]
    public List<GameObject> objs = new List<GameObject>();

    // private

    // ================================== SPAWNING ==================================
    // public
    [Space(10)]
    [Header("Spawning")]
    public Transform spawnLocation;
    public Transform deSpawnLocation;
    
    // private
    private float m_timer = 0;
    [SerializeField] private float m_backgroundSpawnTime = 1;
    [SerializeField] private float m_foregroundSpawnTime = 5; // 0.2725

    private List<GameObject> m_objSpawnNext = new List<GameObject>();

    // private GameObject m_GOBeingSpawn;

    // ================================== SCROLLING ==================================
    // public
    [Space(10)]
    [Header("Scrolling")]
    public float speed = 1;

    // private
    private List<Transform> m_backgroundObjects = new List<Transform>();
    private List<Transform> m_foregroundObjects = new List<Transform>();

    // ================================== ROOMS ==================================

    // private
    // 1 story
    // private GameObject[] H1S_Hall_Connectors = { H1S_Hall, H1S_To2S };
    // private GameObject[] H1S_InDoor_Connectors = { H1S_Hall, H1S_To2S };
    // private GameObject[] H1S_InWin_Connectors = { H1S_Hall, H1S_To2S };
    // // story changes
    // private GameObject[] H1S_To2S_Connectors = { H2S_Hall, H2S_To1S };
    // private GameObject[] H2S_To1S_Connectors = { H1S_Hall, H1S_To2S };
    // // 2 story
    // private GameObject[] H2S_Hall_Connectors = { H2S_Hall, H2S_To1S };
    // private GameObject[] H2S_InDoor_Connectors = { H2S_Hall, H2S_To1S };
    // private GameObject[] H2S_InWin_Connectors = { H2S_Hall, H2S_To1S };

    // TODO
    // each piece needs a list of other pieces that can go after it 

    private void Start()
    {
        // cant have empty list
        m_objSpawnNext.Add(new GameObject());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer <= 0)
        {
            m_objSpawnNext[0] = objs[Random.Range(0, objs.Count - 1)]; // gets random object from list of possible objects

            GameObject go = Instantiate(m_objSpawnNext[0]);
            go.transform.position = spawnLocation.position; // set position to right side of screen
            MovingObj moving = go.GetComponent<MovingObj>();
            moving.SetStartAndEndPosition(spawnLocation.position, deSpawnLocation.position);
            m_timer = m_foregroundSpawnTime; // Random.Range(m_backgroundSpawnTime, m_foregroundSpawnTime); // sets new time until 
        }
        else if (m_timer > 0)
            m_timer -= Time.deltaTime;
    }
}