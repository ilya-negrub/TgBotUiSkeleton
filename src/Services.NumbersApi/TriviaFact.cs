namespace Services.NumbersApi
{
    public class TriviaFact : TemplateFact
    {
        public TriviaFact(int number) : base(number, CategoryFact.Trivia)
        {
        }
    }
}
