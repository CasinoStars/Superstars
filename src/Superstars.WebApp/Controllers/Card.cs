namespace Superstars.WebApp.Controllers
{
    public class Card
    {
		public Card(string Sym, int Val)
        {
            Symbol = Sym;
            Value = Val;
        }
        public string Symbol { get; set; }

        public int Value { get; set; }
    }
}
