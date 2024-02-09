// �V�F�[�_�[�̏��
Shader "Custom/Shader/TestShader"
{
    // Unity��ł���������v���p�e�B���
    // �}�e���A����Inspector�E�B���h�E��ɕ\������A�X�N���v�g�ォ����ݒ�ł���
    Properties
    {
        // Color �v���p�e�B�[ (�f�t�H���g�͔�)
        // �@�������uMain Color�v�Ƃ������̂ŁA
        // �A�������uColor�^�v�̃f�[�^���擾���A
        // �B�̂��̕ϐ���`�ӏ��ł��Ă���_Color�ϐ��֑�����Ă���B�u_�v�͕K�{�I
        _Color ("Main Color", Color) = (1, 1, 1, 1)

        // �O���[�X�P�[���ɂ��邩�ǂ���(0��Off�A1��On)�B�C���X�y�N�^����`�F�b�N�{�b�N�X�Őݒ�ł���B
        [MaterialToggle] _T ("Is Gray Scale", float) = 0
    }

    // �T�u�V�F�[�_�[
    // �V�F�[�_�[�̎�ȏ����͂��̒��ɋL�q����
    // �T�u�V�F�[�_�[�͕����������Ƃ��\�����A��{�͈��
    SubShader
    {
        // �V�F�[�_�[�̐ݒ�
        Tags { "RenderType"="Transparent"  "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        // �p�X
        // 1�̃I�u�W�F�N�g��1�x�̕`��ōs�������������ɏ���
        // �������{������A���G�ȕ`�������Ƃ��͕����������Ƃ��\
        Pass
        {
            ZWrite ON
            ColorMask 0
        }

        Pass
        {
            ZWrite OFF
            Ztest LEqual

            CGPROGRAM // �v���O�����������n�߂�Ƃ����錾�BCGPROGRAM�ȉ���HLSL�i�V�F�[�_�[����j�ł���Ƃ�����`

            // �֐��錾
            // "vert" �֐��𒸓_�V�F�[�_�[�g�p����錾�B�u���_(vertex)�V�F�[�_�[�ł́Avert�Ƃ����֐����Ăяo���Ă�!�v
            #pragma vertex vert
            // "frag" �֐����t���O�����g�V�F�[�_�[�Ǝg�p����錾�B�u�t���O�����g(fragment)�V�F�[�_�[�ł́Afrag�Ƃ����֐����Ăяo���Ă�!�v
            #pragma fragment frag

            // �ϐ��錾
            // Properties{} ���Q��
            // Unity����擾���Ă����f�[�^������
            fixed4 _Color; // �}�e���A������̃J���[
            float _T; // �O���[�X�P�[���ɂ��邩�ǂ���

            // ���_�V�F�[�_�[�B���_���𑀍삷��
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            // �t���O�����g�V�F�[�_�[�B�s�N�Z�����𑀍삷��
            fixed4 frag () : SV_Target
            {
                // �Ȃ�ׂ��Aif���Ȃǂ͎g��Ȃ��悤�ɁI
                float gray = 0.2126f * _Color.r + 0.7152f * _Color.g + 0.0722f * _Color.b;
                float r = (1 - _T) * _Color.r + _T * gray;
                float g = (1 - _T) * _Color.g + _T * gray;
                float b = (1 - _T) * _Color.b + _T * gray;
                float a = _Color.a;
                return fixed4(r, g, b, a);
            }

            ENDCG // �v���O�����������I���Ƃ����錾
        }
    }
}