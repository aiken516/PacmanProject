using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public int CurrentPosition = -15;

    public void ProgressCurrentPosition()
    {
        CurrentPosition += 15;
    }

    public void CreateStage()
    {
        //16ĭ ������ 1 ����������

        //�ܰ���
        for (int i = 0; i < 16; i++)
        {
            GameManager.Instance.Field.CreateBlock(new Vector2(-1, CurrentPosition + i), BlockType.Metal);
            GameManager.Instance.Field.CreateBlock(new Vector2(11, CurrentPosition + i), BlockType.Metal);
        }

        //���� ����
        for (int i = 1; i < 11; i += 2)
        {
            for (int j = 0; j < 14; j += 2)
            {
                GameManager.Instance.Field.CreateBlock(new Vector2(i, j + CurrentPosition), BlockType.Metal);
            }
        }

        //���� ���
        for (int i = 0; i < 48; i++)
        {
            if (!GameManager.Instance.Field.CreateBlock(RandomPosition(CurrentPosition)))
            {
                i--;
            }
        }

        //������ ����
        for (int i = 0; i < 6; i++)
        {
            int randomNum = Random.Range(0, 3);
            ItemType randomItem = (ItemType)randomNum;
            if (!GameManager.Instance.Field.CreateItem(RandomPosition(CurrentPosition), randomItem))
            {
                i--;
            }
        }

        for (int i = 0; i < 3; i++)
        { 
            if (!GameManager.Instance.Field.CreateEnemy(RandomPosition(CurrentPosition)))
            {
                i--;
            }
        }
    }

    private Vector2 RandomPosition(int currentPosition)
    {
        return new Vector2(Random.Range(0, 11), currentPosition + Random.Range(0, 16));
    }

}
