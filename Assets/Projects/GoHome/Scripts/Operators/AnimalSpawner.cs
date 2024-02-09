using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class AnimalSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] animalPrfbs;
        [Header("�X�|�[��xz�̌��E\n(X,Z : �ŏ��C�ő�)")][SerializeField] Vector2[] spawnXZLimit;
        [Header("�X�|�[��y")][SerializeField] float spawnY;
        int spawnNum = 10; // 1��ޓ����艽�̃X�|�[�������邩

        void Start()
        {
            // �������C�z��Ɋi�[

            GameManager.Instance.AnimalList = new List<GameObject>();

            foreach (GameObject animal in animalPrfbs)
            {
                for (int i = 0; i < spawnNum; i++)
                {
                    Vector3 spawnPos = new Vector3(Random.Range(spawnXZLimit[0].x, spawnXZLimit[0].y), spawnY, Random.Range(spawnXZLimit[1].x, spawnXZLimit[1].y));
                    GameManager.Instance.AnimalList.Add(Instantiate(animal, spawnPos, Quaternion.identity));
                }
            }
        }
    }
}
