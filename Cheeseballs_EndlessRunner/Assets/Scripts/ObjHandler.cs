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

    // ================================== ARRAYS ==================================

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