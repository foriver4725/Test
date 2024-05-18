using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    [CreateAssetMenu(menuName = "ParamsSO/RandomMaze/Prefab", fileName = "PrefabParamsSO")]
    public class PrefabParamsSO : ScriptableObject
    {
        #region QOL���㏈��
        // ParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "RandomMaze/PrefabParamsSO";

        // ParamsSO�̎���
        private static PrefabParamsSO _entity = null;
        public static PrefabParamsSO Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<PrefabParamsSO>(PATH);

                    //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("��L�̐e�I�u�W�F�N�g")] public GameObject Room;
        [Header("�X�^�[�g/�S�[��")] public GameObject[] StartAndGoal;
        [Header("���̕���")] public GameObject[] CornerRooms;
        [Header("�ӂ̕���")] public GameObject[] SideRooms;
        [Header("�����̕���")] public GameObject[] CenterRooms;
    }
}
