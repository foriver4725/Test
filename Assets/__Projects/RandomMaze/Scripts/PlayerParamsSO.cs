using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    [CreateAssetMenu(menuName = "ParamsSO/RandomMaze/Player", fileName = "PlayerParamsSO")]
    public class PlayerParamsSO : ScriptableObject
    {
        #region QOL���㏈��
        // ParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "RandomMaze/PlayerParamsSO";

        // ParamsSO�̎���
        private static PlayerParamsSO _entity = null;
        public static PlayerParamsSO Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<PlayerParamsSO>(PATH);

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

        [Header("����")] public float Speed = 100f;
        [Header("���_���x(x,y)")] public float[] Sensitivity = new float[] { 3f, 3f };
        [Header("�������_�̐���(�I�C���[�p�Fmin,max)")] public float[] CameraRotXLimit = new float[] { -90, 90 };
        [Header("�_�b�V�����ɁA���������{�ɂȂ邩")] public float SpeedCoef = 2f;
        [Header("�U��Ԃ��Ă��鎞�ɁA���������{�ɂȂ邩")] public float OnLookingBackSpeedCoef = 1f;
    }
}
