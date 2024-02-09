using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GoHome
{
    public class GoalLight : MonoBehaviour
    {
        Material mt;
        int firstA = 100; // �ŏ���A�l
        float showRange = 20; // �S�[���̌������n�߂鋗��
        float goalRange = 2; // �S�[������ɂȂ�C�S�[���Ƃ̋���

        private void Start()
        {
            mt = GetComponent<Renderer>().material;
        }

        void Update()
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            float distance = (transform.position - player.transform.position).magnitude;
            if (distance <= goalRange) // �v���C���[���S�[���ɂ���
            {
                GameManager.Instance.IsClear = true;
                GameObject.Find("ClearText").GetComponent<TextMeshProUGUI>().enabled = true;
                Destroy(gameObject);
            }
            else if (distance <= showRange) // �v���C���[�Ƃ̋����ɉ����ē����x��ω�
            {
                float ratio = (distance - showRange) / (goalRange - showRange);
                float a = firstA / 256f * ratio;
                mt.color = new Color(0, 0, 0, a);
            }
            else // �v���C���[�Ɨ��ꂷ���Ă���Ȃ��\��
            {
                mt.color = new Color(0, 0, 0, 0);
            }
        }
    }
}
