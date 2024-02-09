using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Alo
{
    // MonoBehavior�͌p�����Ȃ��i�V�[���J�ڎ��ɂ��̃X�N���v�g�̃C���X�^���X���j�������̂�h�����߁j
    public class SaveSystem
    {
        #region �V���O���g���ɂ���

        // ���̃X�N���v�g�̃C���X�^���X�𐶐�����_instance�Ɋi�[
        private static SaveSystem _instance = new SaveSystem();

        // �v���p�e�B�ɂ���B�i_instance�͓ǂݎ���p�j
        public static SaveSystem Instance => _instance;

        #endregion

        /* �v���C�x�[�g�R���X�g���N�^�i�N�����Ƀ��[�h����j�ɂ���āC
         * ���̃X�N���v�g�̊O������SaveSystem�̃C���X�^���X����������邱�Ƃ�h���B�i�o�O�h�~�̂��߁j */
        private SaveSystem() { Load(); }

        /* �Z�[�u�f�[�^�̃t�@�C����u���ꏊ���w�肷��B
         * �Z�[�u�f�[�^�̃t�@�C����Assets�t�H���_�ɒu���B�iApplication.dataPath��Assets�t�H���_�̐e�f�B���N�g���j
         * Path�v���p�e�B�ɃZ�[�u�f�[�^�t�@�C���̊��S�ȃp�X�����Ă���B�i�ǂݎ���p�j */
        public string Path => Application.dataPath + "/data.json";

        /* UserData�v���p�e�B�̎����i�ǂݎ���p�j
         * �i�ŏ��ɃR���X�g���N�^�ɂ���ă��[�h���ꂽ�Ƃ��ɁCUserData�X�N���v�g�̃C���X�^���X���������ꂱ�̒��ɓ���j */
        public UserData UserData { get; private set; }

        /// <summary>
        /// �f�[�^���Z�[�u����
        /// </summary>
        public void Save()
        {
            // _userData�𕶎���ɕϊ����āCjsonData�i���̃��\�b�h���I���Δj�������j�Ɋi�[
            string jsonData = JsonUtility.ToJson(UserData);

            /* �㏑���ۑ�����悤�ݒ肵�������ŁCStreamWriter�̃C���X�^���X�𐶐� */
            StreamWriter writer = new StreamWriter(Path, false);

            // jsonData�i������ɕϊ����ꂽ�Z�[�u�f�[�^�j��Path�ɕۑ�
            writer.WriteLine(jsonData);

            // �����c��������΋����I�ɏ����o��
            writer.Flush();

            // �t�@�C�������
            writer.Close();
        }

        /// <summary>
        /// �f�[�^�����[�h����
        /// </summary>
        public void Load()
        {
            // �����Z�[�u�t�@�C�������݂��Ȃ��̂Ȃ�C����N���ł���Ɣ��f���ď������s��
            if (!File.Exists(Path))
            {
                Debug.Log("�Z�[�u�t�@�C����������Ȃ��̂ŁC����N���������s���܂�");

                // UserData�X�N���v�g�̃C���X�^���X�𐶐�����UserData�Ɋi�[
                UserData = new UserData();

                // �Z�[�u���邱�ƂŃt�@�C�����쐬����
                Save();

                return;
            }

            // StreamReader�̃C���X�^���X�𐶐�
            StreamReader reader = new StreamReader(Path);

            // �Z�[�u�t�@�C����ǂݍ���ŁCjsonData�i���̃��\�b�h���I���Δj�������j�Ɋi�[
            string jsonData = reader.ReadToEnd();

            // jsonData�̒��̕������ϊ�����_userData�ɓn��
            UserData = JsonUtility.FromJson<UserData>(jsonData);

            // �t�@�C�������
            reader.Close();
        }
    }
}

