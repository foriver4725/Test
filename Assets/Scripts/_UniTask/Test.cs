using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace _UniTask
{
    public class Test : MonoBehaviour
    {
        CancellationTokenSource cts;

        async void Start()
        {
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            UniTask a = /*await */A(token);
            UniTask b = /*await */B(token);
            UniTask[] tasks = { a, b };
            UniTask c = C(tasks);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            cts?.Cancel(); // null条件演算子を使用
            Debug.Log("A,B,Cをキャンセルしたよ");
        }

        async UniTask A(CancellationToken token)
        {
            transform.position = -5 * Vector3.right;
            await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: token); // 任意の時間、処理を待機する
            transform.position = 5 * Vector3.right;
            Debug.Log("A：指定時間待ったよ");
        }

        async UniTask B(CancellationToken token)
        {
            await UniTask.WaitUntil(() => 1 + 1 == 2, cancellationToken: token); // 条件を満たすまで待機する（引数なし・戻り値ありの関数を引数に取る。）
            Debug.Log("B:条件を満たすまで待ったよ");
        }

        async UniTask C(UniTask[] tasks)
        {
            await UniTask.WhenAll(tasks); // 複数の処理が全て終了するまで待機する（tasks配列内の全UniTaskの処理が終了するまで待機）
            Debug.Log("C:AとBの処理が終わったよ");
        }
    }
}
