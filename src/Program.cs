// Exemplo de uso de CancellationToken para cancelar uma operação assíncrona

using var cancellationTokenSource = new CancellationTokenSource();
Console.WriteLine("Pressione Enter para cancelar...");
var task = Task.Run( () =>
{
    for (var i = 0; i < 100; i++)
    {
        if (cancellationTokenSource.Token.IsCancellationRequested)
        {
            Console.WriteLine("Operação cancelada.");
            return;
        }

        // Simula alguma operação demorada
        Thread.Sleep(1000);
        Console.WriteLine($"Iteração {i + 1}");
    }

    Console.WriteLine("Operação concluída com êxito.");
});
Console.ReadLine();
cancellationTokenSource.Cancel();
await task;

// Exemplo de uso de CancellationToken para cancelar uma operação assíncrona após um tempo limite de 10 segundos

using var cancellationTokenSourceTimeOut = new CancellationTokenSource();
cancellationTokenSourceTimeOut.CancelAfter(10000);

await ProcessoDemorado(cancellationTokenSourceTimeOut.Token).ContinueWith(task =>
{
    if (task.IsCanceled)
    {
        Console.WriteLine("Processo cancelado por Timeout...");
        return;
    }
});
    
Console.WriteLine("Fim");
Console.Read();

static async Task ProcessoDemorado(CancellationToken cancellationToken)
{
    var i = 0;
    while(true)
    {
        Console.WriteLine($"{i + 1} segundo{(i == 0 ? "" : "s")}");
        await Task.Delay(1000, cancellationToken);
        i++;
    }
}




















