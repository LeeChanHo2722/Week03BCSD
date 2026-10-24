using System.Collections.Generic;
using UnityEngine;

// #16: 일반화 메소드/클래스, where 제약, 일반화 컬렉션.
public class Lesson16_generic : MonoBehaviour
{
    public class Player<T> { public T value; }
    public class ReferenceOnly<T> where T : class { public T value; }
    public class ValueOnly<T> where T : struct { public T value; }
    public class Factory<T> where T : new()
    {
        public T Create() { return new T(); }
    }
    public interface IEnemy { void TakeDamage(int damage); }
    public class Slime : IEnemy
    {
        public int HP = 100;
        public void TakeDamage(int damage) { HP -= damage; }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        DataInformation<int>(36);
        DataInformation<float>(12.3f);
        DataInformation<string>("안녕하세요. 고박사입니다.");
        Player<int> player01 = new Player<int> { value = 10 };
        Player<string> player02 = new Player<string> { value = "안녕하세요. 고박사입니다." };
        Debug.Log(player01.value);
        Debug.Log(player02.value);
        Slime slime = new Factory<Slime>().Create();
        Attack(slime);
        Debug.Log(slime.HP);
        Debug.Log(new ReferenceOnly<string> { value = "class 제약" }.value);
        Debug.Log(new ValueOnly<int> { value = 100 }.value);
        ListExample();
        QueueExample();
        StackExample();
        DictionaryExample();
    }

    public void DataInformation<T>(T data) { Debug.Log($"{data}, {typeof(T)}"); }
    public void Attack<T>(T enemy) where T : IEnemy { enemy.TakeDamage(10); }

    private void ListExample()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 5; i++) list.Add(i);
        PrintList(list);
        list.Insert(0, 10000);
        PrintList(list);
        list.RemoveAt(1);
        PrintList(list);
        Debug.Log(list.Count); // 5
        list.Clear();
        Debug.Log(list.Count); // 0
    }

    public void PrintList<T>(List<T> list)
    {
        foreach (T value in list) Debug.Log(value);
    }

    private void QueueExample()
    {
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < 5; i++) queue.Enqueue(i);
        Debug.Log(queue.Count);
        while (queue.Count > 0) Debug.Log(queue.Dequeue()); // 0,1,2,3,4
        Debug.Log(queue.Count);
    }

    private void StackExample()
    {
        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < 5; i++) stack.Push(i);
        Debug.Log(stack.Count);
        while (stack.Count > 0) Debug.Log(stack.Pop()); // 4,3,2,1,0
        Debug.Log(stack.Count);
    }

    private void DictionaryExample()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["1"] = "First";
        dictionary["2"] = "Second";
        dictionary["3"] = "Third";
        PrintDictionary(dictionary);
        dictionary.Add("사과", "Apple");
        PrintDictionary(dictionary);
        dictionary.Remove("1");
        PrintDictionary(dictionary);
    }

    public void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        Debug.Log($"Count: {dictionary.Count}");
        foreach (TKey key in dictionary.Keys) Debug.Log($"{key}: {dictionary[key]}");
    }
}
