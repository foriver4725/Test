using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeGame
{
    public class Handler : MonoBehaviour
    {
        int[,] field; // �����p���ď������s���B0�����A1����
        GameObject[,] cellsList; // �v���n�u�̃C���X�^���X�i�[�p�z��
        GameObject cellsParent; // �Z���B�̐e
        int h, w; // �c���̃Z����
        float cellEdgeLength; // �Z����1�ӂ̒���

        void Start()
        {
            // SO����l���擾���A�k���̌v�Z���s��
            h = LifeGameSO.Entity.H * LifeGameSO.Entity.DrawShrinkScale;
            if (h % 2 == 0) h--;
            w = LifeGameSO.Entity.W * LifeGameSO.Entity.DrawShrinkScale;
            if (w % 2 == 0) w--;
            cellEdgeLength = LifeGameSO.Entity.CellEdgeLength / LifeGameSO.Entity.DrawShrinkScale;

            // �t�B�[���h�쐬�A�C���X�^���X�i�[�p�z��̍쐬
            field = new int[h, w];
            cellsList = new GameObject[h, w];

            // �Z���B�̐e�𐶐�
            cellsParent = Instantiate(LifeGameSO.Entity.CellsParent);

            // �����z�u��ǂݍ���
            foreach (Vector2 pos in LifeGameSO.Entity.FirstStateInput)
            {
                field[-(int)pos.y + (h - 1) / 2, (int)pos.x + (w - 1) / 2] = 1;
            }

            // �`��
            FirstDraw(field);

            // �V�~�����[�V�����J�n
            StartCoroutine(Simulate());
        }

        IEnumerator Simulate()
        {
            while (true)
            {
                yield return new WaitForSeconds(LifeGameSO.Entity.SimulationInterval);

                List<Vector2> memoToLive = new List<Vector2>(); // �����p�F�ω������ӏ������L�^����B��=>��
                List<Vector2> memoToDie = new List<Vector2>();  // �����p�F�ω������ӏ������L�^����B��=>��
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        #region �S�ẴZ���ɂ��āA���̏�Ԃ����肵�A�������Ă����B
                        if (field[i, j] == 0)
                        {
                            // ����ł���Z���i�����̃Z���j�ɗאڂ��鐶�����Z���̐����J�E���g
                            int dieCount = 0;
                            #region �t�B�[���h�̊O�ɂ͉����Ȃ����̂Ƃ��A�J�E���g�𑝂₳�Ȃ��B
                            if (i >= 1     && j >= 1     && field[i - 1, j - 1] == 1) dieCount++;
                            if (i >= 1                   && field[i - 1, j    ] == 1) dieCount++;
                            if (i >= 1     && j <= w - 2 && field[i - 1, j + 1] == 1) dieCount++;
                            if (              j >= 1     && field[i    , j - 1] == 1) dieCount++;
                            if (              j <= w - 2 && field[i    , j + 1] == 1) dieCount++;
                            if (i <= h - 2 && j >= 1     && field[i + 1, j - 1] == 1) dieCount++;
                            if (i <= h - 2               && field[i + 1, j    ] == 1) dieCount++;
                            if (i <= h - 2 && j <= w - 2 && field[i + 1, j + 1] == 1) dieCount++;
                            #endregion

                            // �a���F����ł���Z���ɗאڂ��鐶�����Z�������傤��3����΁A���̐��オ�a������B
                            if (dieCount == 3)
                            {
                                memoToLive.Add(new Vector2(i, j));
                            }
                        }
                        else
                        {
                            // �����Ă���Z���i�����̃Z���j�ɗאڂ��鐶�����Z���̐����J�E���g
                            int liveCount = 0;
                            #region �t�B�[���h�̊O�ɂ͉����Ȃ����̂Ƃ��A�J�E���g�𑝂₳�Ȃ��B
                            if (i >= 1     && j >= 1     && field[i - 1, j - 1] == 1) liveCount++;
                            if (i >= 1                   && field[i - 1, j    ] == 1) liveCount++;
                            if (i >= 1     && j <= w - 2 && field[i - 1, j + 1] == 1) liveCount++;
                            if (              j >= 1     && field[i    , j - 1] == 1) liveCount++;
                            if (              j <= w - 2 && field[i    , j + 1] == 1) liveCount++;
                            if (i <= h - 2 && j >= 1     && field[i + 1, j - 1] == 1) liveCount++;
                            if (i <= h - 2               && field[i + 1, j    ] == 1) liveCount++;
                            if (i <= h - 2 && j <= w - 2 && field[i + 1, j + 1] == 1) liveCount++;
                            #endregion

                            // �ߑa�F�����Ă���Z���ɗאڂ��鐶�����Z����1�ȉ��Ȃ�΁A�ߑa�ɂ�莀�ł���B
                            // �ߖ��F�����Ă���Z���ɗאڂ��鐶�����Z����4�ȏ�Ȃ�΁A�ߖ��ɂ�莀�ł���B
                            // �����F�����Ă���Z���ɗאڂ��鐶�����Z����2��3�Ȃ�΁A���̐���ł���������B
                            if (liveCount <= 1 || liveCount >= 4)
                            {
                                memoToDie.Add(new Vector2(i, j));
                            }
                        }
                        #endregion
                    }
                }

                // �`��A�y��field�̍X�V
                Draw(memoToLive, memoToDie);
            }
        }

        // field�̏������ɁA�V�[���ɃZ���B��`�悷��B�������A������
        void FirstDraw(int[,] field)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);

                    if (field[i, j] == 0)
                    {
                        GameObject newCell = Instantiate(LifeGameSO.Entity.DiedCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                        newCell.transform.localScale = Vector3.one * cellEdgeLength;
                        cellsList[i, j] = newCell;
                    }
                    else
                    {
                        GameObject newCell = Instantiate(LifeGameSO.Entity.LivingCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                        newCell.transform.localScale = Vector3.one * cellEdgeLength;
                        cellsList[i, j] = newCell;
                    }
                }
            }
        }

        // �ω������ꏊ���󂯎��A�V�[���ɃZ���B��`�悷��B�������A������
        void Draw(List<Vector2> memoToLive, List<Vector2> memoToDie)
        {
            foreach(Vector2 memoPos in memoToLive)
            {
                int i = (int)memoPos.x;
                int j = (int)memoPos.y;

                Destroy(cellsList[i, j]);

                Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);
                GameObject newCell = Instantiate(LifeGameSO.Entity.LivingCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                newCell.transform.localScale = Vector3.one * cellEdgeLength;
                cellsList[i, j] =  newCell;

                field[i, j] = 1;
            }

            foreach (Vector2 memoPos in memoToDie)
            {
                int i = (int)memoPos.x;
                int j = (int)memoPos.y;

                Destroy(cellsList[i, j]);

                Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);
                GameObject newCell = Instantiate(LifeGameSO.Entity.DiedCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                newCell.transform.localScale = Vector3.one * cellEdgeLength;
                cellsList[i, j] = newCell;

                field[i, j] = 0;
            }
        }
    }
}
