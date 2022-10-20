using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ObstacleManager : MonoBehaviour
{
    public Obstacle m_obstaclePrefab;
    public float m_cooldownTime = 2;

    public GamePlayManager m_manager { get; private set; }
    List<Obstacle> m_pooledObsList;
    List<Obstacle> m_activeObsList;
    List<GameObject> m_allObsBackupList;
    float m_timer;

    PhotonView m_view;

    void CreateObstacle()
    {
        GameObject m_obstacle = PhotonNetwork.Instantiate(m_obstaclePrefab.name, new Vector3(0, -4.5f, 0), Quaternion.identity);
        Obstacle obs = m_obstacle.GetComponent<Obstacle>();
        obs.Initialize(this);
        m_pooledObsList.Add(obs);
        m_allObsBackupList.Add(m_obstacle);
    }

    Obstacle GetObstacleFromPool()
    {
        if (m_pooledObsList.Count == 0)
        {
            CreateObstacle();
        }
        Obstacle retVal = m_pooledObsList[0];
        m_pooledObsList.RemoveAt(0);
        return retVal;
    }

    public void PoolObstacle(Obstacle obj)
    {
        m_activeObsList.Remove(obj);
        m_pooledObsList.Add(obj);
    }

    public void Initialize(GamePlayManager manager)
    {
        m_view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            m_manager = manager;
            m_pooledObsList = new List<Obstacle>();
            m_activeObsList = new List<Obstacle>();
            m_allObsBackupList = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                CreateObstacle();
            }
            m_timer = m_cooldownTime;
        }
    }

    public void BeforeDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < m_pooledObsList.Count; i++)
            {
                Destroy(m_allObsBackupList[i]);
            }
            m_allObsBackupList.Clear();
            m_pooledObsList.Clear();
        }
    }

    public void CustomUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_timer -= Time.deltaTime;
            if (m_timer < 0)
            {
                m_timer = m_cooldownTime;
                Obstacle obs = GetObstacleFromPool();
                obs.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6, -1f);
                obs.gameObject.SetActive(true);
                obs.Activate();
                m_activeObsList.Add(obs);
            }
            for (int i = 0; i < m_activeObsList.Count; i++)
            {
                m_activeObsList[i].CustomUpdate();
            }
        }
    }

    //[PunRPC]

}