# Week03BCSD

고박사의 유니티 C# **영상 제목 #10~#18** 실습입니다. 재생목록의 순번은 별도 문제 영상 때문에 강의 번호와 다릅니다.

| 강의 | 스크립트 | 실습 내용 |
| --- | --- | --- |
| [10](https://www.youtube.com/watch?v=o0Pklkow-AA) | `Lesson10_method.cs` | 반환값, 값/ref/out 전달, 오버로딩, params, 명명/선택적 매개변수 |
| [11](https://www.youtube.com/watch?v=wbzF4BNw5Zs) | `Lesson11_class.cs` | 멤버, 접근 지정자, 생성자/소멸자, 참조/깊은 복사, this |
| [12](https://www.youtube.com/watch?v=Y4mc6C7mYK0) | `Lesson12_oop.cs` | 상속, 캡슐화, 다형성, is/as, 추상 클래스, 인터페이스 |
| [13](https://www.youtube.com/watch?v=2B16TSUyQPU) | `Lesson13_extension.cs` | new/sealed, 중첩/분할 클래스, 구조체, 튜플, static, 확장 메소드, namespace |
| [14](https://www.youtube.com/watch?v=u0iXSe5IEV8) | `Lesson14_property.cs` | 프로퍼티, 자동 구현, 읽기/쓰기 전용, 초기화, 추상 프로퍼티 |
| [15](https://www.youtube.com/watch?v=8WRh8TJkHBc) | `Lesson15_collection.cs` | ArrayList, Queue, Stack, Hashtable |
| [16](https://www.youtube.com/watch?v=ab83gVHMZNg) | `Lesson16_generic.cs` | 제네릭 메소드/클래스, where, List/Queue/Stack/Dictionary |
| [17](https://www.youtube.com/watch?v=argeDbqzUdE) | `Lesson17_file.cs` | File, FileInfo, Directory, DirectoryInfo |
| [18](https://www.youtube.com/watch?v=SvUYPh6lnqo) | `Lesson18_stream.cs` | FileStream, 이진/텍스트 입출력, 객체 직렬화 |

## 실행

Unity 프로젝트의 `Assets/script`에 스크립트를 넣고 빈 GameObject에 원하는 강의의 컴포넌트를 추가한 다음 Play를 누르면 Console에 결과가 출력됩니다. 컴포넌트 메뉴의 `Run Examples`로도 실행할 수 있습니다. 각 스크립트는 독립적으로 실행됩니다.

이 저장소는 실습 스크립트 모음이며 씬, Packages, ProjectSettings를 포함하는 전체 Unity 프로젝트는 아닙니다. 대상 프로젝트 버전은 Unity **6000.3.24f1**, C# 9.0입니다.

## 원본과의 차이

- 10~16강은 강의 대본과 확인한 화면 예제를 바탕으로 실행 가능한 형태로 재구성했습니다. 모든 화면의 코드를 한 글자씩 옮긴 전사본은 아니며 일부 출력문과 실행용 코드를 추가했습니다.
- 17~18강은 강사 제공 소스를 바탕으로 강의별 한 파일로 통합했습니다. 원본: [17강](https://drive.google.com/file/d/1e8EMj95QUL58t0hb4gRfdiE03si_jf9w/view), [18강](https://drive.google.com/file/d/1OdVue-PeovhixocHdPXY1nBewIgAGIzI/view).
- 반복되는 Player/Enemy 등의 클래스는 강의 컴포넌트 내부 또는 별도 namespace에 배치해 이름 충돌을 방지했습니다. 파일명과 MonoBehaviour 클래스명은 같습니다.
- 여러 독립 예제는 메소드로 분리하여 Awake에서 순차 호출합니다. 컴파일 오류를 설명하는 잘못된 코드는 실행 코드에 넣지 않았습니다.
- 파일 실습은 `Application.persistentDataPath/BCSD/Lesson번호/실행별ID`에서 실행합니다. 스트림은 using으로 닫고, FileStream 읽기는 실제 int 크기와 부분 읽기를 처리합니다.
- 18강의 BinaryFormatter는 영상과의 대응을 위해 남긴 레거시 학습 코드입니다. 직접 만든 데이터만 즉시 읽으며 외부 파일 입력은 받지 않습니다. 최신 .NET에서는 지원하지 않을 수 있으므로 일반 .NET 콘솔 앱이 아닌 대상 Unity 런타임에서 사용하세요.

과제 안내: [BCSD 3차시 문서](https://docs.google.com/document/d/13Z1H-gxWT4MvaeNnjkR6U992HY1JM2Gj-hx_w5ox1ak/edit)

## 검증

- Unity 6000.3.24f1의 실제 `netstandard.dll`, `UnityEngine.CoreModule.dll`을 참조하고 C# 9.0, 경고를 오류로 처리하는 설정으로 9개 스크립트 전체 컴파일 통과.
- 별도 테스트용 Unity API 대체 코드와 Unity에 포함된 Mono 런타임으로 9개 예제를 두 번씩 실행하여 총 18회 통과.
- 값/ref/out 전달, 복사 객체 독립성, 프로퍼티 범위 처리, 파일·이진·텍스트 왕복 입출력, NonSerialized 필드 제외를 확인.
- 실제 Unity Editor 씬에서의 Play Mode 테스트는 수행하지 않았습니다.
