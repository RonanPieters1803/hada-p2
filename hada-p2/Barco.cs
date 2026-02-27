using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{   
    public class Barco
    {
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

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
                    coor = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna+i);
                }
                else
                {
                    coor = new Coordenada(coordenadaInicio.Fila+i, coordenadaInicio.Columna);
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
                    eventoTocado?.Invoke(this, new TocadoArgs(Nombre, c));
                    NumDanyos++;
                    if (hundido())
                    {
                        eventoHundido?.Invoke(this, new HundidoArgs(Nombre));
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

        public override string ToString()
        {
            string result = $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: {hundido()} - COORDENADAS: ";
                foreach (KeyValuePair<Coordenada, String> par in CoordenadasBarco)
                {
                    result += $"[{par.Key.ToString()} :{par.Value}] ";
                } 
            return result;
        }
    }
}
