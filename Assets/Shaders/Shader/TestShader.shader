// シェーダーの情報
Shader "Custom/Shader/TestShader"
{
    // Unity上でやり取りをするプロパティ情報
    // マテリアルのInspectorウィンドウ上に表示され、スクリプト上からも設定できる
    Properties
    {
        // Color プロパティー (デフォルトは白)
        // ①第一引数「Main Color」という名称で、
        // ②第二引数「Color型」のデータを取得し、
        // ③のちの変数定義箇所でしている_Color変数へ代入している。「_」は必須！
        _Color ("Main Color", Color) = (1, 1, 1, 1)

        // グレースケールにするかどうか(0はOff、1はOn)。インスペクタからチェックボックスで設定できる。
        [MaterialToggle] _T ("Is Gray Scale", float) = 0
    }

    // サブシェーダー
    // シェーダーの主な処理はこの中に記述する
    // サブシェーダーは複数書くことも可能だが、基本は一つ
    SubShader
    {
        // シェーダーの設定
        Tags { "RenderType"="Transparent"  "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        // パス
        // 1つのオブジェクトの1度の描画で行う処理をここに書く
        // これも基本一つだが、複雑な描画をするときは複数書くことも可能
        Pass
        {
            ZWrite ON
            ColorMask 0
        }

        Pass
        {
            ZWrite OFF
            Ztest LEqual

            CGPROGRAM // プログラムを書き始めるという宣言。CGPROGRAM以下がHLSL（シェーダー言語）であるという定義

            // 関数宣言
            // "vert" 関数を頂点シェーダー使用する宣言。「頂点(vertex)シェーダーでは、vertという関数を呼び出してね!」
            #pragma vertex vert
            // "frag" 関数をフラグメントシェーダーと使用する宣言。「フラグメント(fragment)シェーダーでは、fragという関数を呼び出してね!」
            #pragma fragment frag

            // 変数宣言
            // Properties{} 内参照
            // Unityから取得してきたデータが入る
            fixed4 _Color; // マテリアルからのカラー
            float _T; // グレースケールにするかどうか

            // 頂点シェーダー。頂点情報を操作する
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            // フラグメントシェーダー。ピクセル情報を操作する
            fixed4 frag () : SV_Target
            {
                // なるべく、if文などは使わないように！
                float gray = 0.2126f * _Color.r + 0.7152f * _Color.g + 0.0722f * _Color.b;
                float r = (1 - _T) * _Color.r + _T * gray;
                float g = (1 - _T) * _Color.g + _T * gray;
                float b = (1 - _T) * _Color.b + _T * gray;
                float a = _Color.a;
                return fixed4(r, g, b, a);
            }

            ENDCG // プログラムを書き終わるという宣言
        }
    }
}