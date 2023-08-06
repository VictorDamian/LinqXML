using System.Xml.Linq;

Console.WriteLine("---");
BookXml();
static void BookXml()
{
    
    var file = @"Model\endorsment.xml";
    XDocument bookXML = XDocument.Load(file);
    var endoso = from i in bookXML.Descendants("VALOR")
              orderby Int32.Parse(i.Attribute("secEm").Value) descending
              select new Endoso
              {
                  Metodo = i.Attribute("metodo").Value,
                  Email = i.Attribute("email").Value,
                  PrioE = i.Attribute("prioEmail").Value,
                  SecE = i.Attribute("secEm").Value
              };

    var ultimo = endoso.FirstOrDefault();

    foreach (var i in endoso)
    {
        if (i.Metodo == 1.ToString())
        {
            var ultimaSecuencia = Int32.Parse(ultimo.SecE);

            if (ultimaSecuencia == Int32.Parse(i.SecE))
            {
                // Si hay secuencia toma el ultimo
                ultimo.Print();
            }
            else if (Int32.Parse(i.SecE) > ultimaSecuencia && i.PrioE == "S")
            {
                // Si no toma el por defecto
                i.Print();
            }
        }
    }
}
public enum EntityState
{
    PorDefecto,
    Secuencia
}

public class Endoso
{
    public string Metodo { get; set; }
    public string Email { get; set; }
    public string PrioE { get; set; }
    public string SecE { get; set; }
    public void Print()
    {
        Console.WriteLine($"Metodo:   {Metodo}\nEmail:    {Email}\nPriori:   {PrioE}\nSec:      {SecE}\n");
    }
}