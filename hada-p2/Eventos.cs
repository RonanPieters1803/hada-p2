using Hada;
using System;

public class TocadoArgs : EventArgs
{
    public Coordenada Coordenada { get; private set; }

    public TocadoArgs(Coordenada coordenada)
    {
        Coordenada = coordenada;
    }
}
public class HundidoArgs : EventArgs
{
    public string Nombre { get; private set; }
    public HundidoArgs (string nombre)
    {
        Nombre = nombre;
    }
}