using Hada;
using System;

namespace Hada
{

    public class TocadoArgs : EventArgs
    {
        public string Nombre {  get; set; }
        public Coordenada CoordenadaImpacto { get; private set; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            Nombre = nombre;
            CoordenadaImpacto = coordenadaImpacto;
        }
    }
    public class HundidoArgs : EventArgs
    {
        public string Nombre { get; private set; }
        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }
}