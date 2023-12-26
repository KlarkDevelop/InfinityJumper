using UnityEngine;

public class Chank : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public Transform WollPoint;

    // 0 - чанк из бонусов(Самый редкий), 1 - очень легко, 2 - легко,
    // 3 - средне, 4 - выше среднего, 5 - сложно, 6 - очень сложно
    public int difficult;
}
