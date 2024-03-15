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
            cts?.Cancel(); // null�������Z�q���g�p
            Debug.Log("A,B,C���L�����Z��������");
        }

        async UniTask A(CancellationToken token)
        {
            transform.position = -5 * Vector3.right;
            await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: token); // �C�ӂ̎��ԁA������ҋ@����
            transform.position = 5 * Vector3.right;
            Debug.Log("A�F�w�莞�ԑ҂�����");
        }

        async UniTask B(CancellationToken token)
        {
            await UniTask.WaitUntil(() => 1 + 1 == 2, cancellationToken: token); // �����𖞂����܂őҋ@����i�����Ȃ��E�߂�l����̊֐��������Ɏ��B�j
            Debug.Log("B:�����𖞂����܂ő҂�����");
        }

        async UniTask C(UniTask[] tasks)
        {
            await UniTask.WhenAll(tasks); // �����̏������S�ďI������܂őҋ@����itasks�z����̑SUniTask�̏������I������܂őҋ@�j
            Debug.Log("C:A��B�̏������I�������");
        }
    }
}
