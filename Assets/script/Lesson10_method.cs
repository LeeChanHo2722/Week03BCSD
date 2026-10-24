using UnityEngine;

// 고박사의 유니티 노트 #10: 메소드. 강의의 개별 예제를 한 컴포넌트로 정리.
public class Lesson10_method : MonoBehaviour
{
    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        Multiple(3, 4);
        Multiple(5, 8);
        Multiple(4, 6);
        Debug.Log(Add(10, 20));
        Debug.Log(Add(172.3f, 89.5f));
        int max = Max(10, 20);
        Debug.Log(max);
        Debug.Log(Max(100, 200));
        Max(5, 10); // 반환값을 사용하지 않아도 된다.
        PrintPositive(-1); // return으로 메소드 종료.
        PrintPositive(10);

        int a = 3;
        int b = 4;
        Swap(a, b);
        Debug.Log($"값 전달 후: a={a}, b={b}"); // 3, 4
        Swap(ref a, ref b);
        Debug.Log($"참조 전달 후: a={a}, b={b}"); // 4, 3

        Calculate(10, 3, out int sum, out int product);
        Debug.Log($"합={sum}, 곱={product}");
        Debug.Log(Sum(1, 2, 3, 4, 5));
        Player("고박사", 100);
        Player(id: "고박사", health: 100);
        Player(health: 100, id: "고박사");
        Player("고박사"); // 선택적 매개변수: health=1000
    }

    public int Add(int num1, int num2) { return num1 + num2; }
    public float Add(float num1, float num2) { return num1 + num2; }
    public void Multiple(int num1, int num2) { Debug.Log(num1 * num2); }

    public int Max(int num1, int num2)
    {
        if (num1 > num2) return num1;
        return num2;
    }

    private void PrintPositive(int number)
    {
        if (number <= 0) return;
        Debug.Log(number);
    }

    public void Swap(int num1, int num2)
    {
        int temp = num1;
        num1 = num2;
        num2 = temp;
    }

    public void Swap(ref int num1, ref int num2)
    {
        int temp = num1;
        num1 = num2;
        num2 = temp;
    }

    public void Calculate(int num1, int num2, out int result1, out int result2)
    {
        result1 = num1 + num2;
        result2 = num1 * num2;
    }

    public int Sum(params int[] nums)
    {
        int result = 0;
        for (int i = 0; i < nums.Length; i++) result += nums[i];
        return result;
    }

    private void Player(string id, int health = 1000)
    {
        Debug.Log($"ID: {id}, Health: {health}");
    }
}
