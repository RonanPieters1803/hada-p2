using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        private int _tamTablero;
        public int TamTablero
        {
            get => _tamTablero;
            private set
            {
                if (value < 4 || value > 9)
                    throw new ArgumentOutOfRangeException("El tamaño del tablero tiene que estar entre 4 y 9");
                _tamTablero = value;
            }
        }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;

            this.barcos = barcos;
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            foreach(Barco b in barcos){
                b.eventoTocado += cuandoEventoTocado; 
                b.eventoHundido += cuandoEventoHundido;
            }
            inicializarCasillasTablero();
        }

        private void inicializarCasillasTablero()
        {
            for(int fila = 0; fila < TamTablero; fila++)
            {
                for(int col = 0; col < TamTablero; col++)
                {
                    Coordenada c = new Coordenada(fila, col);
                    casillasTablero[c] = "AGUA";
                }
            }
            foreach (Barco b in barcos)
            {
                foreach(var par in b.CoordenadasBarco)
                {
                    casillasTablero[par.Key] = b.Nombre;
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            if(c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordenada ({c.Fila},{c.Columna}) esta fuera de rango");
                return;
            }
            coordenadasDisparadas.Add(c);

            foreach(Barco b in barcos)
            {
                b.Disparo(c);
            }
        }
        public string DibujarTablero() 
        {
            StringBuilder sb = new StringBuilder();

            for (int fila = 0; fila < TamTablero; fila++) {
                for (int col = 0; col < TamTablero; col++) {
                    Coordenada c = new Coordenada(fila, col);
                    string valor = casillasTablero[c];

                    if (valor == "AGUA") 
                        sb.Append(" ~ ");
                    else if 
                        (valor.EndsWith("_T")) sb.Append(" X ");
                    else 
                        sb.Append(" B "); 
                } 
                sb.AppendLine(); 
            } 
            return sb.ToString(); 
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e) 
        { 
            casillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";
            if (!coordenadasTocadas.Contains(e.CoordenadaImpacto)) 
                coordenadasTocadas.Add(e.CoordenadaImpacto);
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] tocado en Coordenada: [{e.CoordenadaImpacto}]"); 
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e) 
        {
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] hundido!!");
            Barco barcoHundido = barcos.Find(b => b.Nombre == e.Nombre);
            if (!barcosEliminados.Contains(barcoHundido)) barcosEliminados.Add(barcoHundido);
            if (barcosEliminados.Count == barcos.Count) eventoFinPartida?.Invoke(this, EventArgs.Empty); 
        }

        public override string ToString() 
        { 
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== ESTADO DEL TABLERO ===");
            sb.AppendLine();
            
            sb.AppendLine("BARCOS:");
            foreach (Barco b in barcos)
                sb.AppendLine(b.ToString());

            sb.AppendLine("\nCOORDENADAS DISPARADAS:");
            foreach (var c in coordenadasDisparadas) 
                sb.Append($"{c} ");

            sb.AppendLine("\n\nCOORDENADAS TOCADAS:");
            foreach (var c in coordenadasTocadas) 
                sb.Append($"{c} ");

            sb.AppendLine("\n\nTABLERO:");
            sb.AppendLine(DibujarTablero());

            return sb.ToString(); 
        }
    }
}
