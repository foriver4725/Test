using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeGame
{
    [CreateAssetMenu(menuName = "ParamsSO/LifeGame/LifeGame", fileName = "LifeGameSO")]
    public class LifeGameSO : ScriptableObject
    {
        #region QOL���㏈��
        // ParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "LifeGame/LifeGameSO";

        // ParamsSO�̎���
        private static LifeGameSO _entity = null;
        public static LifeGameSO Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<LifeGameSO>(PATH);

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

        [Header("�����z�u�̓��́F\n�����Ă���Z���̍��W���A\n���S��(0, 0)�A�Ԋu��1�Ƃ��ē��͂���")] public Vector2[] FirstStateInput;
        [Space(25)]
        [Header("�ȉ��̏����l�ɂ��`����A���{�ɏk�����邩")] public int DrawShrinkScale;
        [Header("�c�̃Z�����i��j")] public int H;
        [Header("���̃Z�����i��j")] public int W;
        [Header("�Z����1�ӂ̒���")] public float CellEdgeLength;
        [Space(25)]
        [Header("�V�~�����[�V�����̊Ԋu�i�b�j")] public float SimulationInterval;
        [Header("�Z���B�̐e�̃v���n�u")] public GameObject CellsParent;
        [Header("�����Ă���Z���̃v���n�u")] public GameObject LivingCellPrefab;
        [Header("����ł���Z���̃v���n�u")] public GameObject DiedCellPrefab;
    }
}
