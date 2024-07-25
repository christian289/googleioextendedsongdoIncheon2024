namespace ConsoleAppFrameworkExample.Quartz;

public class PrintJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("반복적으로 출력되는 글자");
        return Task.CompletedTask;
    }
}