using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedraPaperTisores.Model;
using PedraPaperTisores.Tools;

namespace PedraPaperTisores.Model
{
    public class Partida:ObservableBase
    {
        Jugador jugador;
        Jugador robot;
        int nRondes = 5;
        EstatPartida estat;

        public Jugador Jugador { get => jugador; set => jugador = value; }
        public Jugador Robot { get => robot; set => robot = value; }
        public int NRondes 
        { get => nRondes; set => SetProperty(ref nRondes, value); }
        public EstatPartida Estat 
        { get => estat; set => SetProperty(ref estat, value); }
    }
}
