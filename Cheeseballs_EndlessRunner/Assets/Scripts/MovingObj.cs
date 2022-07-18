using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    // public 
    public float speed = 1;

    // private
    private Vector3 m_spawnPosition;
    private Vector3 m_deSpawnPosition;

    private float m_tValue = 0;

    private void Update()
    {
        if (m_tValue < 1)
            m_tValue += Time.fixedDeltaTime * speed;
        else if (m_tValue >= 1)
            Destroy(gameObject);

        transform.position = Vector3.Lerp(m_spawnPosition, m_deSpawnPosition, m_tValue);
    }

    public void SetStartAndEndPosition(Vector3 a_spawnPosition, Vector3 a_deSpawnPosition)
    {
        m_spawnPosition = a_spawnPosition;
        m_deSpawnPosition = a_deSpawnPosition;
    }
}