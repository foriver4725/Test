using UnityEngine;
using UnityEditor;

namespace LifeGame
{
    [InitializeOnLoad]
    public class AssetSelectionChecker
    {
        static bool errorShown = false; // �G���[���b�Z�[�W��\���������ǂ����̃t���O
        static string targetAssetPath = "Assets/Projects/LifeGame/Resources/LifeGame/LifeGameSO.asset"; // ���m�������A�Z�b�g�̃p�X

        static AssetSelectionChecker()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode && !errorShown)
            {
                // �I������Ă���A�Z�b�g���擾
                Object[] selectedAssets = Selection.objects;

                // �I�����ꂽ�A�Z�b�g���`�F�b�N
                foreach (Object selectedAsset in selectedAssets)
                {
                    // �I������Ă���A�Z�b�g�̃p�X���擾
                    string assetPath = AssetDatabase.GetAssetPath(selectedAsset);

                    // ���m�������A�Z�b�g���ǂ������m�F
                    if (assetPath == targetAssetPath)
                    {
                        Debug.LogWarning("<color=yellow>Scriptable Object ��I��������ԂŃQ�[�������s����ƁA�G���[���o��ꍇ������܂��B\n����͌���Unity2022�ȍ~�Ŕ������Ă���o�O�ł���A�������m�F�������ł͎��Q�͂���܂���B</color>");
                        Debug.LogWarning("<color=yellow>�Q�l�L���Fhttps://www.create-forever.games/unity2022-3-value-cannot-be-null-_unity_self/</color>");
                        errorShown = true;
                        break;
                    }
                }
            }
        }
    }
}
