using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Alo
{
    // MonoBehaviorは継承しない（シーン遷移時にこのスクリプトのインスタンスが破棄されるのを防ぐため）
    public class SaveSystem
    {
        #region シングルトンにする

        // このスクリプトのインスタンスを生成して_instanceに格納
        private static SaveSystem _instance = new SaveSystem();

        // プロパティにする。（_instanceは読み取り専用）
        public static SaveSystem Instance => _instance;

        #endregion

        /* プライベートコンストラクタ（起動時にロードする）によって，
         * このスクリプトの外部からSaveSystemのインスタンスが生成されることを防ぐ。（バグ防止のため） */
        private SaveSystem() { Load(); }

        /* セーブデータのファイルを置く場所を指定する。
         * セーブデータのファイルはAssetsフォルダに置く。（Application.dataPathはAssetsフォルダの親ディレクトリ）
         * Pathプロパティにセーブデータファイルの完全なパスを入れている。（読み取り専用） */
        public string Path => Application.dataPath + "/data.json";

        /* UserDataプロパティの実装（読み取り専用）
         * （最初にコンストラクタによってロードされたときに，UserDataスクリプトのインスタンスが生成されこの中に入る） */
        public UserData UserData { get; private set; }

        /// <summary>
        /// データをセーブする
        /// </summary>
        public void Save()
        {
            // _userDataを文字列に変換して，jsonData（このメソッドが終われば破棄される）に格納
            string jsonData = JsonUtility.ToJson(UserData);

            /* 上書き保存するよう設定したうえで，StreamWriterのインスタンスを生成 */
            StreamWriter writer = new StreamWriter(Path, false);

            // jsonData（文字列に変換されたセーブデータ）をPathに保存
            writer.WriteLine(jsonData);

            // 書き残しがあれば強制的に書き出す
            writer.Flush();

            // ファイルを閉じる
            writer.Close();
        }

        /// <summary>
        /// データをロードする
        /// </summary>
        public void Load()
        {
            // もしセーブファイルが存在しないのなら，初回起動であると判断して処理を行う
            if (!File.Exists(Path))
            {
                Debug.Log("セーブファイルが見つからないので，初回起動処理を行います");

                // UserDataスクリプトのインスタンスを生成してUserDataに格納
                UserData = new UserData();

                // セーブすることでファイルを作成する
                Save();

                return;
            }

            // StreamReaderのインスタンスを生成
            StreamReader reader = new StreamReader(Path);

            // セーブファイルを読み込んで，jsonData（このメソッドが終われば破棄される）に格納
            string jsonData = reader.ReadToEnd();

            // jsonDataの中の文字列を変換して_userDataに渡す
            UserData = JsonUtility.FromJson<UserData>(jsonData);

            // ファイルを閉じる
            reader.Close();
        }
    }
}

