using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHandler : MonoBehaviour
{
    // ================================== GENERAL ==================================
    //public
    [Header("General")]
    public GameObject startingRoom;
    public GameObject hallRoom;
    public float speed = 0.1f;

    // private
    private bool isGamePlaying = false;

    // ================================== SPAWNING ==================================
    // public
    [Space(10)]
    [Header("Spawning")]
    public Transform spawnLocation;
    public Transform spawnNextIndicator;
    public Transform deSpawnLocation;

    private GameObject m_objSpawnNext;
    private GameObject m_objSpawnNextObstacle;

    // ================================== ROOMS ==================================

    // camera heights:
    // 1 level - 2
    // 2 levels - 3.65

    private void Start()
    {
        m_objSpawnNext = SpawnObject(startingRoom);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_objSpawnNext.transform.position.x < spawnNextIndicator.position.x)
        {
            if (!isGamePlaying)
            {
                m_objSpawnNext = hallRoom;
                m_objSpawnNext = SpawnObject(m_objSpawnNext);
            }
            else
            {
                // room
                MovingObj obj = m_objSpawnNext.GetComponent<MovingObj>();
                if (obj.compatibleRooms.Count == 0)
                    return;

                m_objSpawnNext = obj.compatibleRooms[Random.Range(0, obj.compatibleRooms.Count)]; // gets random object from list of possible objects
                m_objSpawnNext = SpawnObject(m_objSpawnNext);

                // obstacles
                if (obj.compatibleObstacles.Count == 0)
                    return;

                m_objSpawnNextObstacle = obj.compatibleObstacles[Random.Range(0, obj.compatibleObstacles.Count)]; // gets random obstacle layout for room
                m_objSpawnNextObstacle = SpawnObject(m_objSpawnNextObstacle);
            }
        }
    }

    private GameObject SpawnObject(GameObject a_go)
    {
        GameObject go = Instantiate(a_go);
        MovingObj moving = go.GetComponent<MovingObj>();
        moving.transform.position = spawnLocation.position; // set position to right side of screen
        moving.SetSpeed(speed);
        moving.SetStartAndEndPosition(spawnLocation.position, deSpawnLocation.position);
        return go;
    }

    public void PlayGame()
    {
        isGamePlaying = true;
    }
}