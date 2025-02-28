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
        //16칸 단위를 1 스테이지로

        //외곽선
        for (int i = 0; i < 16; i++)
        {
            GameManager.Instance.Field.CreateBlock(new Vector2(-1, CurrentPosition + i), BlockType.Metal);
            GameManager.Instance.Field.CreateBlock(new Vector2(11, CurrentPosition + i), BlockType.Metal);
        }

        //내부 격자
        for (int i = 1; i < 11; i += 2)
        {
            for (int j = 0; j < 14; j += 2)
            {
                GameManager.Instance.Field.CreateBlock(new Vector2(i, j + CurrentPosition), BlockType.Metal);
            }
        }

        //내부 블록
        for (int i = 0; i < 48; i++)
        {
            if (!GameManager.Instance.Field.CreateBlock(RandomPosition(CurrentPosition)))
            {
                i--;
            }
        }

        //아이템 생성
        for (int i = 0; i < 8; i++)
        {
            int randomNum = Random.Range(0, 3);

            if (randomNum == 0)
            {
                GameManager.Instance.Field.CreateItem(RandomPosition(CurrentPosition), ItemType.ExtraBoom);
            }
            else if (randomNum == 1)
            {
                GameManager.Instance.Field.CreateItem(RandomPosition(CurrentPosition), ItemType.ExplosionPower);
            }
            else
            {
                GameManager.Instance.Field.CreateItem(RandomPosition(CurrentPosition), ItemType.Score);
            }
        }

        for (int i = 0; i < 3; i++)
        { 
            GameManager.Instance.Field.CreateEnemy(RandomPosition(CurrentPosition));
        }
    }

    private Vector2 RandomPosition(int currentPosition)
    {
        return new Vector2(Random.Range(0, 11), currentPosition + Random.Range(0, 16));
    }

}
