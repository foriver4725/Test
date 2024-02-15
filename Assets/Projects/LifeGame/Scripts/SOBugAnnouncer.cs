using UnityEngine;
using UnityEditor;

namespace LifeGame
{
    [InitializeOnLoad]
    public class AssetSelectionChecker
    {
        static bool errorShown = false; // エラーメッセージを表示したかどうかのフラグ
        static string targetAssetPath = "Assets/Projects/LifeGame/Resources/LifeGame/LifeGameSO.asset"; // 検知したいアセットのパス

        static AssetSelectionChecker()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode && !errorShown)
            {
                // 選択されているアセットを取得
                Object[] selectedAssets = Selection.objects;

                // 選択されたアセットをチェック
                foreach (Object selectedAsset in selectedAssets)
                {
                    // 選択されているアセットのパスを取得
                    string assetPath = AssetDatabase.GetAssetPath(selectedAsset);

                    // 検知したいアセットかどうかを確認
                    if (assetPath == targetAssetPath)
                    {
                        Debug.LogWarning("<color=yellow>Scriptable Object を選択した状態でゲームを実行すると、エラーが出る場合があります。\nこれは現状Unity2022以降で発生しているバグであり、自分が確認する限りでは実害はありません。</color>");
                        Debug.LogWarning("<color=yellow>参考記事：https://www.create-forever.games/unity2022-3-value-cannot-be-null-_unity_self/</color>");
                        errorShown = true;
                        break;
                    }
                }
            }
        }
    }
}
