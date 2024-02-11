using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    // �f�[�^�x�[�X
    [CreateAssetMenu(menuName = "ParamsSO/SO/Test", fileName = "TestParamsSO")]
    public class TestParamsSO : ScriptableObject
    {
        #region ParamsSO��QOL���㏈��
        //ParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO/TestParamsSO";

        //ParamsSO�̎���
        private static TestParamsSO _entity;
        public static TestParamsSO Entity
        {
            get
            {
                //���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<TestParamsSO>(PATH);

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

        public List<Database> DataBases;

        public List<Database> GetData(Database.Type type)
        {
            List<Database> found = DataBases.FindAll(e => e.type == type);
            if (found != null)
            {
                return found;
            }
            else
            {
                Debug.LogError($"type:{type}�̃N���X��������܂���");
                return null;
            }
        }
    }

    [Serializable]
    public class Database
    {
        public enum Type
        {
            SLIME,
            HUMAN,
            DRAGON
        }
        public Type type;

        public string name;
        public int hp;
        public int attack;
        [TextArea(1, 100)] public string text;
    }
}
