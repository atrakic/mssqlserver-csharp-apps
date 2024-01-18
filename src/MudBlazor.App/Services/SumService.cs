namespace app.Services;

public class SumService
{
    public int A { get; set; }
    public int B { get; set; }

    public int? Result { get; set; }

    public void Sum()
    {
        Result = A + B;
    }
}
