namespace ConsoleAppFrameworkExample.Options;

// 보통 옵션은 Runtime에서 Loading 한 뒤 불변으로 사용하므로 record로 선언
// Generic Host에서 값 확인을 위해 binding 시에는 생성자의 인자로 사용할 수 없으므로 파라미터 형태로 사용.
// Option으로 정의되는 Class와 내부 Property는 반드시 public으로 선언해야 한다.
// 옵션 타입을 정의할 때는 public class, public property, get; set; 으로 정의해도 되지만
// 옵션이 불변성을 지닌다고 생각하면 record와 get; init; 으로 선언할 수도 있다. (선택)
// https://learn.microsoft.com/ko-kr/dotnet/core/extensions/options
public sealed record SampleOption
{
    public string? Name { get; init; }
    public int Age { get; init; }
    public string? Description { get; init; }
}
