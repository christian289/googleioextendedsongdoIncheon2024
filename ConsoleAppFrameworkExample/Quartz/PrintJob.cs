namespace ConsoleAppFrameworkExample.Quartz;

public class PrintJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("�ݺ������� ��µǴ� ����");
        return Task.CompletedTask;
    }
}