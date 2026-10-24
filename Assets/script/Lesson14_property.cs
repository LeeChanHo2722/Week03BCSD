using UnityEngine;

// #14: get/set, 자동 구현, 읽기/쓰기 전용, 객체 초기화, 인터페이스/추상 프로퍼티.
public class Lesson14_property : MonoBehaviour
{
    public class Player
    {
        private int currentHP;
        public int CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value < 0 ? 0 : value; }
        }
        public string ID { get; set; }
        public int Level { get; private set; } = 1;
        public int ReadOnlyHP { get { return currentHP; } }
        public int WriteOnlyHP { set { CurrentHP = value; } }
        public void LevelUp() { Level++; }
    }

    public class AutoPlayer
    {
        public string ID { get; set; }
        public int CurrentHP { get; set; }
    }

    public interface IEntity { string ID { get; set; } }
    public abstract class BaseEntity
    {
        public abstract int Shield { get; set; }
        public int Defense { get; set; }
    }
    public class ShieldPlayer : BaseEntity, IEntity
    {
        public string ID { get; set; }
        public override int Shield { get; set; }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        Player player = new Player { ID = "고박사", CurrentHP = 1000 };
        Debug.Log($"{player.ID}: {player.CurrentHP}");
        player.CurrentHP = -100;
        Debug.Log(player.ReadOnlyHP); // 0
        player.WriteOnlyHP = 100;
        player.LevelUp();
        Debug.Log($"HP={player.CurrentHP}, Level={player.Level}");

        AutoPlayer autoPlayer = new AutoPlayer { ID = "유니티", CurrentHP = 100 };
        Debug.Log($"{autoPlayer.ID}: {autoPlayer.CurrentHP}");
        ShieldPlayer shieldPlayer = new ShieldPlayer { ID = "고박사", Shield = 50, Defense = 10 };
        IEntity entity = shieldPlayer;
        Debug.Log($"{entity.ID}, Shield={shieldPlayer.Shield}, Defense={shieldPlayer.Defense}");
    }
}
