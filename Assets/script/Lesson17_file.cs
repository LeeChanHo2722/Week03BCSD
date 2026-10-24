using System;
using System.IO;
using UnityEngine;

// #17: 강사가 제공한 File/FileInfo/Directory/DirectoryInfo 예제를 통합.
public class Lesson17_file : MonoBehaviour
{
    private void Awake() { RunExamples(); }

    [ContextMenu("Run Examples")]
    public void RunExamples()
    {
        // 실행마다 새 폴더를 사용하므로 기존 파일을 덮어쓰거나 이동하지 않는다.
        string root = Path.Combine(Application.persistentDataPath, "BCSD", "Lesson17", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        Debug.Log($"실습 경로: {root}");
        FileExample(root);
        FileInfoExample(root);
        DirectoryExample(root);
        DirectoryInfoExample(root);
    }

    private void FileExample(string root)
    {
        string a = Path.Combine(root, "a.dat");
        string b = Path.Combine(root, "b.dat");
        string c = Path.Combine(root, "c.dat");
        // using은 예외가 발생해도 스트림을 닫는다.
        using (FileStream file = File.Create(a)) { }
        File.Copy(a, b);
        File.Move(a, c);
        Debug.Log($"c.dat 파일 존재 여부: {File.Exists(c)}");
        Debug.Log($"c.dat 파일 속성: {File.GetAttributes(c)}");
        File.Delete(c);
        File.Delete(b);
    }

    private void FileInfoExample(string root)
    {
        FileInfo fileInfo = new FileInfo(Path.Combine(root, "d.dat"));
        using (FileStream file = fileInfo.Create()) { }
        fileInfo.CopyTo(Path.Combine(root, "e.dat"));
        fileInfo.MoveTo(Path.Combine(root, "f.dat"));
        fileInfo.Refresh();
        Debug.Log($"{fileInfo.Name} 파일 존재 여부: {fileInfo.Exists}");
        Debug.Log($"{fileInfo.Name} 파일 속성: {fileInfo.Attributes}");
        fileInfo.Delete();
    }

    private void DirectoryExample(string root)
    {
        string source = Path.Combine(root, "Exercise");
        string target = Path.Combine(root, "Exercise2");
        Directory.CreateDirectory(source);
        Directory.Move(source, target);
        Debug.Log($"Exercise2 폴더 존재 여부: {Directory.Exists(target)}");
        Debug.Log($"하위 폴더 개수: {Directory.GetDirectories(target).Length}");
        Debug.Log($"하위 파일 개수: {Directory.GetFiles(target).Length}");
        Directory.Delete(target); // 이 예제에서 만든 빈 폴더만 삭제.
    }

    private void DirectoryInfoExample(string root)
    {
        // 영상의 Application.dataPath 대신 이번 실행의 실습 폴더를 사용한다.
        DirectoryInfo currentDirectory = new DirectoryInfo(root);
        currentDirectory.CreateSubdirectory("TestA");
        Debug.Log($"전체경로 + 폴더이름: {currentDirectory.FullName}");
        Debug.Log($"폴더이름: {currentDirectory.Name}");
        Debug.Log($"폴더 존재 여부: {currentDirectory.Exists}");
        Debug.Log($"폴더 속성: {currentDirectory.Attributes}");
        foreach (DirectoryInfo directory in currentDirectory.GetDirectories()) Debug.Log(directory.Name);
        foreach (FileInfo file in currentDirectory.GetFiles()) Debug.Log(file.Name);
    }
}
