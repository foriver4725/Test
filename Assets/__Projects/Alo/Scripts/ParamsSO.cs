using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    // �f�[�^�x�[�X
    [CreateAssetMenu]
    public class ParamsSO : ScriptableObject
    {
        #region ParamsSO��QOL���㏈��
        //ParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "Alo/ParamsSO";

        //ParamsSO�̎���
        private static ParamsSO _entity;
        public static ParamsSO Entity
        {
            get
            {
                //���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<ParamsSO>(PATH);

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

        // �Z�[�u�͂ł��Ȃ��C�C���X�y�N�^�ォ��l��ύX���邱��!!!
        [TextArea(1, 100)] public string[] Stage1;
        [TextArea(1, 100)] public string[] Stage2;
    }
}
