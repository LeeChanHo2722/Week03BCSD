using UnityEngine;

// #12: 정보 은닉, 상속, 업캐스팅, 오버라이딩, 추상 클래스, 인터페이스.
public class Lesson12_oop : MonoBehaviour
{
    public class Entity
    {
        public string ID = "Entity";
        protected int currentHP = 100;
        public void RecoveryHP(int amount) { currentHP += amount; }
        public virtual void Attack() { Debug.Log("Entity 공격"); }
        public virtual void TakeDamage(int damage) { currentHP -= damage; }
        public int GetCurrentHP() { return currentHP; }
        public void SetCurrentHP(int hp) { currentHP = hp; }
    }

    public class Goblin : Entity, IMovingEntity
    {
        public override void Attack() { Debug.Log("고블린의 몽둥이 스매시"); }
        public void MoveTo() { Debug.Log("고블린 이동"); }
    }

    public class Slime : Entity, IMovingEntity
    {
        public override void Attack() { Debug.Log("슬라임의 몸통 박치기 공격"); }
        public void MoveTo() { Debug.Log("슬라임 이동"); }
    }

    public class Dragon : Entity
    {
        public override void Attack() { Debug.Log("드래곤 공격"); }
    }

    public class Player : Entity, IPerson, ICombatEntity
    {
        public new string ID = "Player";
        public void PrintID() { Debug.Log($"this.ID={this.ID}, base.ID={base.ID}"); }
        public void Hit(Entity enemy) { enemy.TakeDamage(10); }
        public void MoveTo() { Debug.Log("플레이어 이동"); }
        public void Talk() { Debug.Log("플레이어 대화"); }
        public override void Attack() { Debug.Log("플레이어 공격"); }
    }

    public abstract class AbstractEntity
    {
        protected int damage = 10;
        protected int currentHP = 100;
        public void TakeDamage(int amount) { currentHP -= amount; }
        public abstract void Attack();
    }

    public class AbstractGoblin : AbstractEntity
    {
        public override void Attack() { Debug.Log($"고블린 공격력: {damage}"); }
    }

    public interface IMovingEntity { void MoveTo(); }
    public interface IPerson : IMovingEntity { void Talk(); }
    public interface ICombatEntity { void Attack(); }
    public class Butterfly : IMovingEntity
    {
        public void MoveTo() { Debug.Log("나비 이동"); }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        Player player = new Player();
        player.SetCurrentHP(100);
        player.RecoveryHP(10);
        Debug.Log(player.GetCurrentHP());
        player.PrintID();

        Entity[] enemies = { new Goblin(), new Slime(), new Dragon() };
        foreach (Entity enemy in enemies)
        {
            player.Hit(enemy);
            enemy.Attack(); // 참조 타입은 Entity지만 실제 객체의 override가 실행된다.
            Debug.Log(enemy.GetCurrentHP());
        }

        Entity entity = new Slime();
        if (entity is Slime) Debug.Log("Slime 형식입니다.");
        Goblin goblin = entity as Goblin;
        if (goblin == null) Debug.Log("Goblin으로 변환할 수 없습니다.");
        Entity upcast = new Goblin();
        Goblin downcast = (Goblin)upcast;
        downcast.MoveTo();

        AbstractEntity abstractEntity = new AbstractGoblin();
        abstractEntity.Attack();
        // new AbstractEntity()는 불가능하다.
        IMovingEntity[] movers = { new Goblin(), new Slime(), new Butterfly(), player };
        foreach (IMovingEntity mover in movers) MoveEntity(mover);
        CreateMovingEntity().MoveTo();
        IPerson person = player;
        person.MoveTo();
        person.Talk();
        ICombatEntity combat = player;
        combat.Attack();
    }

    private void MoveEntity(IMovingEntity entity) { entity.MoveTo(); }
    private IMovingEntity CreateMovingEntity() { return new Butterfly(); }
}
