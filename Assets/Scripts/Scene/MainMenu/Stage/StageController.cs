using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] RectTransform _pathObject;
    [SerializeField] private StageButtonController[] _stages;

    public void Setup()
    {
        for (int i = 0; i < _stages.Length; i++)
        {
            _stages[i].Setup(i + 1);

            if (i > 0)
                GeneratePath(_stages[i - 1].transform.position, _stages[i].transform.position);
        }
    }

    private void GeneratePath(Vector2 startPos, Vector2 endPos)
    {
        while (startPos != endPos)
        {
            RectTransform obj = Instantiate(_pathObject, startPos, Quaternion.identity, transform.GetChild(0));

            startPos = Vector2.Lerp(startPos, endPos,
                (obj.rect.width / 2) / Vector2.Distance(startPos, endPos));
        }
    }
}
