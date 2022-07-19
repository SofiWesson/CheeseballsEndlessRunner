using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHandler : MonoBehaviour
{
    // ================================== GENERAL ==================================
    //public
    [Header("General")]
    public List<GameObject> objs = new List<GameObject>();
    public GameObject startingRoom;

    // private

    // ================================== SPAWNING ==================================
    // public
    [Space(10)]
    [Header("Spawning")]
    public Transform spawnLocation;
    public Transform spawnNextIndicator;
    public Transform deSpawnLocation;

    private GameObject m_objSpawnNext;

    // private GameObject m_GOBeingSpawn;

    // ================================== SCROLLING ==================================
    // public
    [Space(10)]
    [Header("Scrolling")]
    public float speed = 1;

    // ================================== ROOMS ==================================

    // camera heights:
    // 1 level - 2
    // 2 levels - 3.65

    private void Start()
    {
        SpawnObject(startingRoom);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_objSpawnNext.transform.position.x < spawnNextIndicator.position.x)
        {
            MovingObj obj = m_objSpawnNext.GetComponent<MovingObj>();
            m_objSpawnNext = obj.compatibleRooms[Random.Range(0, obj.compatibleRooms.Count)]; // gets random object from list of possible objects
            SpawnObject(m_objSpawnNext);
        }
    }

    private void SpawnObject(GameObject a_go)
    {
        GameObject go = Instantiate(a_go);
        MovingObj moving = go.GetComponent<MovingObj>();
        moving.transform.position = spawnLocation.position; // set position to right side of screen
        //moving.transform.position = Vector3.up * (moving.isOneStory ? 3.45f / 2.0f : 6.71f / 2.0f); // 3.45 6.71
        moving.SetStartAndEndPosition(spawnLocation.position, deSpawnLocation.position);
        m_objSpawnNext = go;
    }
}