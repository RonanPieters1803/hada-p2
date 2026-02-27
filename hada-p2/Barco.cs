using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class CoordenadaEventArgs : EventArgs
    {
        public Coordenada Coordenada { get; private set; }

        public CoordenadaEventArgs(Coordenada coordenada)
        {
            Coordenada = coordenada;
        }
    }
    internal class Barco
    {
        public event EventHandler<CoordenadaEventArgs> Tocado;
        public event EventHandler Hundido; 

        public Dictionary<Coordenada, String> CoordenadasBarco {  get; private set; }
        public string Nombre { get; private set; }
        public int  NumDanyos { get; private set; }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio) 
        {
            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, String>(); 
            for (int i = 0; i < longitud; i++)
            {
                Coordenada coor;
                if (orientacion == 'v')
                {
                    coor = new Coordenada(coordenadaInicio.Fila+1, coordenadaInicio.Columna);
                    
                }
                else
                {
                    coor = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                CoordenadasBarco[coor] = nombre;
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c))
            {
                if (!CoordenadasBarco[c].EndsWith("_T"))
                {
                    CoordenadasBarco[c] += "_T";
                    Tocado?.Invoke(this, new CoordenadaEventArgs(c));
                    NumDanyos++;
                    if (hundido())
                    {
                        Hundido?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool hundido()
        {
            foreach (string etiqueta in CoordenadasBarco.Values)
            {
                if (etiqueta == Nombre) 
                    return false;
            }
            return true;
        }
    }
}
