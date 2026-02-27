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
                if (_fila > 0 && _fila < 9)
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
                if (_columna > 0 && _columna < 9)
                {
                    _columna = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Columna entre 0 y 9");
                }
            }
        }
    }
}
