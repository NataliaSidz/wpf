using System;

namespace Zadanie2
{
    internal class Order
    {
        public int Count { get; set; } = 0;
        public string Format { get; set; } = "A5";
        public string Grammary { get; set; } = "80 g/m²";
        public string PrintType { get; set; } = "Normalny";
        public string PaperColor { get; set; } = "Biały";
        public string LeadTime { get; set; } = "Standard";
        public double Discount
        {
            get
            {
                return 1 - Math.Floor((double)Count / 100) / 100;
            }
            private set { }
        }
        public double FinalPrice
        {
            get
            {
                return Count * BasePrice * Discount * GrammageChangeMultiplier * BonusOptionsMultiplier + LeadTimeAddedCost;
            }
            private set { }
        }
        public double BasePrice { get; set; } = 0.2;
        public double GrammageChangeMultiplier { get; set; } = 1;
        public double BonusOptionsMultiplier { get; set; } = 1;
        public int LeadTimeAddedCost { get; set; } = 0;

        public override string ToString()
        {
            return "Przedmiot zamówienia: " + Count + "szt." + ", Format: " + Format + ", Gramatura: " + Grammary + "\n"
                + "Czas realizacji: " + LeadTime + ", Papier: " + PaperColor + "\n"
                + "Cena przed rabatem: " + BasePrice + "zł\n"
                + "Naliczony rabat: " + Math.Floor((1 - Discount) * 100) + "%\n"
                + "Cena po rabacie: " + Math.Round(FinalPrice, 2).ToString("0.00") + "zł";
        }
    }
}
