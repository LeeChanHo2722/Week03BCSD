using UnityEngine;

// #11: 멤버, 접근 지정자, 생성자, 복사, this.
public class Lesson11_class : MonoBehaviour
{
    public class Player
    {
        public string ID;
        private int currentHP;

        public Player() : this(1000) { }
        public Player(int hp) : this("고박사", hp) { }
        public Player(string ID, int currentHP)
        {
            this.ID = ID;
            this.currentHP = currentHP;
        }

        public void TakeDamage(int damage)
        {
            if (currentHP > damage) currentHP -= damage;
        }

        public int GetCurrentHP() { return currentHP; }

        public Player DeepCopy()
        {
            Player clone = new Player();
            clone.ID = ID;
            clone.currentHP = currentHP;
            return clone;
        }

        // 소멸자 문법 예시. 호출 시점은 GC가 결정하며 Unity API를 호출하지 않는다.
        ~Player() { }
    }

    public class Enemy
    {
        public void AttackToTarget(Player target) { target.TakeDamage(100); }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        Player player01 = new Player();
        Player player02 = new Player("유니티", 10);
        new Enemy().AttackToTarget(player01);
        Debug.Log($"{player01.ID}: {player01.GetCurrentHP()}");
        Debug.Log($"{player02.ID}: {player02.GetCurrentHP()}");

        // 참조 대입: 두 변수가 같은 객체를 가리킨다.
        player02 = player01;
        player02.ID = "유니티";
        Debug.Log($"참조 복사: {player01.ID}, {player02.ID}");

        player01.ID = "고박사";
        player02 = player01.DeepCopy();
        player02.ID = "유니티";
        Debug.Log($"깊은 복사: {player01.ID}, {player02.ID}");
        // 이 예제는 string과 int만 포함한다. 참조형 필드를 추가하면 별도 복사가 필요하다.
    }
}
