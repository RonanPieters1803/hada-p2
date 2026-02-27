using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        private int _fila, _columna;

        public int Fila
        {
            get { return _fila; }

            set
            {
                if (value >= 0 && value <= 9)
                {
                    _fila = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Fila entre 0 y 9");
                }
            }
        }
        public int Columna
        {
            get { return _columna; }

            set
            {
                if (value >= 0 && value <= 9)
                {
                    _columna = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Columna entre 0 y 9");
                }
            }
        }
        public Coordenada() {
            _fila = 0;
            _columna = 0;
        }

        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);
        }

        public Coordenada(Coordenada coordenada) 
        { 
            Fila = coordenada.Fila;
            Columna = coordenada.Columna;
        }

        public override string ToString()
        {
            return $"({_fila}, {_columna})";
        }

        public override int GetHashCode() {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Coordenada nueva = (Coordenada)obj;
            return this.Fila == nueva.Fila && this.Columna == nueva.Columna;
        }

        public bool Equals(Coordenada coordenada)
        {
            return this.Fila == coordenada.Fila && this.Columna == coordenada.Columna;        }
    }
}
