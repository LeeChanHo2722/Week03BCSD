using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// #18: 강사가 제공한 FileStream, Binary/Stream Reader/Writer, 직렬화 예제 통합.
public class Lesson18_stream : MonoBehaviour
{
    [Serializable]
    public class Player
    {
        public string name;
        public int age;
        [NonSerialized] public string address;

        public Player(string name, int age, string address)
        {
            this.name = name;
            this.age = age;
            this.address = address;
        }

        public void PrintData() { Debug.Log($"{name}: 나이({age}), 주소({address})"); }
    }

    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        string root = Path.Combine(Application.persistentDataPath, "BCSD", "Lesson18", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        Debug.Log($"실습 경로: {root}");
        FileStreamExample(Path.Combine(root, "FileStream.dat"));
        BinaryExample(Path.Combine(root, "BinaryFileStream.dat"));
        TextExample(Path.Combine(root, "TextFileStream.dat"));
        SerializationExample(Path.Combine(root, "BinaryFormatter.dat"));
    }

    private void FileStreamExample(string path)
    {
        int writeData = 35;
        byte[] writeBytes = BitConverter.GetBytes(writeData);
        using (Stream writeStream = new FileStream(path, FileMode.CreateNew))
        {
            writeStream.Write(writeBytes, 0, writeBytes.Length);
        }

        // int는 4바이트. Read가 요청한 바이트를 한 번에 전부 읽는다고 가정하지 않는다.
        byte[] readBytes = new byte[sizeof(int)];
        using (Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            int offset = 0;
            while (offset < readBytes.Length)
            {
                int count = readStream.Read(readBytes, offset, readBytes.Length - offset);
                if (count == 0) throw new EndOfStreamException("정수 데이터가 부족합니다.");
                offset += count;
            }
        }
        Debug.Log($"파일에 저장되어 있던 데이터: {BitConverter.ToInt32(readBytes, 0)}");
    }

    private void BinaryExample(string path)
    {
        using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.CreateNew)))
        {
            writer.Write(35);
            writer.Write(12.34f);
            writer.Write("여러분 안녕하세요. 고박사입니다.");
        }
        using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
        {
            Debug.Log($"File Size: {reader.BaseStream.Length} Bytes");
            // 저장 순서와 형식이 일치해야 한다.
            Debug.Log(reader.ReadInt32());
            Debug.Log(reader.ReadSingle());
            Debug.Log(reader.ReadString());
        }
    }

    private void TextExample(string path)
    {
        using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.CreateNew)))
        {
            writer.Write(36);
            writer.WriteLine(12.34f);
            writer.WriteLine("여러분 안녕하세요. 고박사입니다.");
        }
        using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
        {
            while (!reader.EndOfStream) Debug.Log(reader.ReadLine());
        }
        // 끝까지 읽은 스트림은 EOF에 있으므로 새 스트림에서 ReadToEnd 실습.
        using (StreamReader reader = new StreamReader(path)) Debug.Log(reader.ReadToEnd());
    }

    private void SerializationExample(string path)
    {
        // 영상의 레거시 API를 보존한 학습 예제. 외부에서 받은 파일에 사용하지 않는다.
        // 이번 호출에서 직접 직렬화한 데이터만 같은 독점 스트림으로 역직렬화한다.
        // 실제 게임의 저장 시스템에는 JSON 등의 다른 직렬화 방식을 사용한다.
#pragma warning disable SYSLIB0011
        BinaryFormatter formatter = new BinaryFormatter();
        Player writePlayer = new Player("고박사", 35, "고박사의 유니티 노트");
        using (Stream stream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None))
        {
            formatter.Serialize(stream, writePlayer);
            stream.Position = 0;
            Player readPlayer = (Player)formatter.Deserialize(stream);
            readPlayer.PrintData(); // address는 [NonSerialized]이므로 null.
        }
#pragma warning restore SYSLIB0011
    }
}
