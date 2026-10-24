using System.Collections;
using UnityEngine;

// #15: 비제네릭 컬렉션은 object 형식으로 데이터를 보관한다.
public class Lesson15_collection : MonoBehaviour
{
    public class Goblin { }
    public class Slime { }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        ArrayListExample();
        QueueExample();
        StackExample();
        HashtableExample();
    }

    private void ArrayListExample()
    {
        ArrayList list = new ArrayList();
        Debug.Log(list.Add(10)); // 추가된 인덱스: 0
        PrintArrayList(list);
        list.Insert(1, 100);
        PrintArrayList(list);
        ArrayList extra = new ArrayList { 1, 2, 3 };
        list.AddRange(extra);
        PrintArrayList(list);
        list.Sort(); // 비교 가능한 같은 타입(int)끼리 정렬.
        PrintArrayList(list);
        list.Remove(10);
        PrintArrayList(list);
        list.RemoveAt(0);
        PrintArrayList(list);
        list.RemoveRange(0, 2);
        PrintArrayList(list);
        list.Clear();
        Debug.Log($"ArrayList Count: {list.Count}");
    }

    private void PrintArrayList(ArrayList list)
    {
        for (int i = 0; i < list.Count; i++) Debug.Log($"[{i}]: {list[i]}");
    }

    private void QueueExample()
    {
        Queue queue = new Queue();
        for (int i = 0; i < 5; i++) queue.Enqueue(i);
        Debug.Log(queue.Count); // 5
        Debug.Log(queue.Peek()); // 0, 삭제하지 않음
        Debug.Log(queue.Dequeue()); // 0, 삭제함
        Debug.Log(queue.Count); // 4
        queue.Clear();
        Debug.Log(queue.Count); // 0
    }

    private void StackExample()
    {
        Stack stack = new Stack();
        for (int i = 0; i < 5; i++) stack.Push(i);
        Debug.Log(stack.Count); // 5
        Debug.Log(stack.Peek()); // 4
        Debug.Log(stack.Pop()); // 4
        Debug.Log(stack.Count); // 4
        stack.Clear();
        Debug.Log(stack.Count); // 0
    }

    private void HashtableExample()
    {
        Hashtable hash = new Hashtable();
        Goblin goblin = new Goblin();
        Slime slime = new Slime();
        hash["Goblin"] = goblin;
        hash.Add("Slime", slime);
        hash.Add(1, "First");
        hash.Add(2, "Second");
        hash.Add(3, "Third");
        foreach (object key in hash.Keys) Debug.Log($"{key}: {hash[key]}");
        if (hash.ContainsKey("Slime")) Debug.Log("Slime 키가 있습니다.");
        if (hash.ContainsValue(goblin)) Debug.Log("Goblin 인스턴스가 있습니다.");
        Debug.Log(hash.Count); // 5
        hash.Remove("Slime");
        Debug.Log(hash.Count); // 4
        hash.Clear();
        Debug.Log(hash.Count); // 0
        // Hashtable의 열거 순서는 삽입 순서를 보장하지 않는다.
    }
}
