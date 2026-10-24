using UnityEngine;
using BCSD.Lesson13;
using MyPlayer = BCSD.Lesson13.MySpace.Player;

// #13: new, sealed, nested, partial, struct, tuple, static, 확장 메소드, namespace.
public class Lesson13_extension : MonoBehaviour
{
    public class Parent
    {
        public void Method01() { Debug.Log("Parent.Method01"); }
        public virtual void TakeDamage() { Debug.Log("Parent.TakeDamage"); }
    }

    public class Child : Parent
    {
        public new void Method01() { Debug.Log("Child.Method01"); }
        public sealed override void TakeDamage() { Debug.Log("Child.TakeDamage"); }
    }
    public class GrandChild : Child { } // TakeDamage는 다시 override할 수 없다.

    public class OuterClass
    {
        private int value = 10;
        public class InnerClass
        {
            public void Print(OuterClass outer) { Debug.Log(outer.value); }
        }
    }

    public struct Person
    {
        public string name;
        public int age;
    }

    public class Enemy
    {
        public int number;
        public static string species = "Goblin";
        public void InstanceRun() { Debug.Log($"Instance: {number}"); }
        public static void StaticRun() { Debug.Log($"Static: {species}"); }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        Child child = new Child();
        child.Method01();
        Parent parent = child;
        parent.Method01(); // new는 override와 달리 참조 타입에 의해 결정된다.
        parent.TakeDamage();
        new OuterClass.InnerClass().Print(new OuterClass());
        PartialPlayer partial = new PartialPlayer();
        partial.MoveTo();
        partial.Attack();

        Person first = new Person { name = "고박사", age = 35 };
        Person second = first;
        second.age = 36;
        Debug.Log($"구조체 복사: {first.age}, {second.age}");
        // struct는 필드를 값 복사한다. 참조형 필드의 대상까지 깊은 복사하지는 않는다.
        var tuple = ("고박사", 35);
        Debug.Log($"{tuple.Item1}, {tuple.Item2}");
        var named = (Name: "고박사", Age: 35);
        var (name, age) = named;
        var (_, onlyAge) = named;
        Debug.Log($"{name}, {age}, {onlyAge}");

        Enemy.StaticRun();
        Enemy enemy01 = new Enemy { number = 1 };
        Enemy enemy02 = new Enemy { number = 2 };
        enemy01.InstanceRun();
        enemy02.InstanceRun();
        Debug.Log(Enemy.species);
        Debug.Log(StaticData.Value);
        string str = "안녕하세요. 고박사입니다.";
        str.PrintData();
        Debug.Log(new MyPlayer().Name);
        Debug.Log(new BCSD.Lesson13.YourSpace.Player().Name);
    }
}

namespace BCSD.Lesson13
{
    // 두 선언을 별도 파일에 놓아도 같은 클래스가 된다. 제출 파일 수를 위해 함께 배치.
    public partial class PartialPlayer
    {
        public void MoveTo() { Debug.Log("PartialPlayer 이동"); }
    }
    public partial class PartialPlayer
    {
        public void Attack() { Debug.Log("PartialPlayer 공격"); }
    }
    public static class StaticData
    {
        public static int Value;
        static StaticData() { Value = 100; }
    }
    public static class StringExtension
    {
        public static void PrintData(this string str) { Debug.Log(str); }
    }
    namespace MySpace { public class Player { public string Name = "MySpace.Player"; } }
    namespace YourSpace { public class Player { public string Name = "YourSpace.Player"; } }
}
