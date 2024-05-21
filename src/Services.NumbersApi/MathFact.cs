namespace Services.NumbersApi
{
    public class MathFact : TemplateFact
    {
        public MathFact(int number) : base(number, CategoryFact.Year)
        {
        }
    }
}
