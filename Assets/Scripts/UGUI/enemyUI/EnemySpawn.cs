using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int SpawnCount;
    public int Spawnwidth;
    public int Spawnlenght;


    public List<GameObject> Enemylist = new List<GameObject>();
    GameObject EnemyParent;

    private void Start()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            GameObject obj = Instantiate(enemy, transform);
            obj.SetActive(false);
            //obj.transform.parent = EnemyParent.transform;
            Enemylist.Add(obj);
        }
    }
    private void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 StartPos = transform.position;
        float Rnd = Random.Range(-Spawnwidth / 2, Spawnwidth / 2);

        Vector3 SpawnPos = new Vector3(StartPos.x + Rnd, StartPos.y, StartPos.z + Rnd);

        for (int i = 0; i < Enemylist.Count; i++)
        {
            if (!Enemylist[i].activeSelf)
            {
                Enemylist[i].SetActive(true);
                Enemylist[i].transform.position = SpawnPos;
                return;
            }
            else if (Enemylist[i].GetComponent<enemyController>().currentHP <= 0 && !Enemylist[i].GetComponent<enemyController>().skinRen[0])
            {
                Enemylist[i].transform.position = SpawnPos;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(50, 50, 50, 50);
        Gizmos.DrawCube(transform.position, new Vector3(Spawnwidth, 1, Spawnlenght));
    }
}