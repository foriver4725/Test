using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurve : MonoBehaviour
{
    [SerializeField] AnimationCurve curve; // まずInspectorにカーブを表示させる
    [SerializeField] float amplitude = 3f;

    void Update()
    {
        // Evaluateでカーブの数値を取得できる
        // 引数はグラフの時間軸
        // Time.timeSinceLevelLoad（Time.timeと似ているが、シーン切り替えしたら0に戻る）
        transform.position = Vector3.up * amplitude * (curve.Evaluate(Time.timeSinceLevelLoad));
    }
}
