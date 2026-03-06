using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida;
        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            Barco b1 = new Barco("THOR", 1, 'h', new Coordenada(0, 0));
            Barco b2 = new Barco("LOKI", 2, 'v', new Coordenada(1, 2));
            Barco b3 = new Barco("MAYA", 3, 'h', new Coordenada(3, 1));

            List<Barco> barcos = new List<Barco>() { b1, b2, b3 };

            Tablero tablero = new Tablero(9, barcos);

            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {
                Console.WriteLine("Introduce una coordenada (NUMERO,NUMERO) o 's' para salir:");
                string input = Console.ReadLine();

                if (input.ToLower() == "s")
                {
                    finPartida = true;
                    Console.WriteLine("PARTIDA FINALIZADA!!");
                    break;
                }

                string[] partes = input.Split(',');

                if (partes.Length != 2 ||
                    !int.TryParse(partes[0], out int fila) ||
                    !int.TryParse(partes[1], out int columna))
                {
                    Console.WriteLine("Formato incorrecto. Ejemplo correcto: 0,3");
                    continue;
                }

                Coordenada coordenada = new Coordenada(fila, columna);
                tablero.Disparar(coordenada);
            }
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
