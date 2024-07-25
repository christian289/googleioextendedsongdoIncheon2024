namespace ConsoleAppFrameworkExample.Options;

// 보통 옵션은 Runtime에서 Loading 한 뒤 불변으로 사용하므로 record로 선언
// Generic Host에서 값 확인을 위해 binding 시에는 생성자의 인자로 사용할 수 없으므로 파라미터 형태로 사용.
internal record SampleOption
{
    internal string? Name { get; init; }
    internal int Age { get; init; }
    internal string? Description { get; init; }
}
