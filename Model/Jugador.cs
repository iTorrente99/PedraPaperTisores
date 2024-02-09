using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedraPaperTisores.Model;
using PedraPaperTisores.Tools;

namespace PedraPaperTisores.Model
{
    public class Jugador:ObservableBase
    {
        string nom;
        string cognom;
        bool esValid;
        int nGuanyades = 0;
        int nPerdudes = 0;
        int nGuanyadesTotal = 0;
        int nPerdudesTotal = 0;
        int nContrincantsEliminats=0;
        Opcio opcioTriada;

        public string Nom 
        { 
            get => nom; 
            set
            {
                SetProperty(ref nom, value);
                NotifyPropertyChanged(nameof(NomComplet));
                ValidaJugador();
            }
        }
        public string Cognom
        {
            get => cognom;
            set
            {
                SetProperty(ref cognom, value);
                NotifyPropertyChanged(nameof(NomComplet));
                ValidaJugador();
            }
        }
        public string NomComplet { get => nom + " " + cognom; }
        public bool EsValid 
        { get => esValid; set => SetProperty(ref esValid, value); }
        public int NGuanyades 
        { 
            get => nGuanyades;
            set
            {
                SetProperty(ref nGuanyades, value);
                NotifyPropertyChanged(nameof(NGuanyadesTotal));
            }
        }
        public int NPerdudes 
        { 
            get => nPerdudes; set
            {
                SetProperty(ref nPerdudes, value);
                NotifyPropertyChanged(nameof(NPerdudesTotal));
            }
        }
        public int NContrincantsEliminats 
        { get => nContrincantsEliminats; set => SetProperty(ref nContrincantsEliminats, value); }
        public Opcio OpcioTriada 
        { get => opcioTriada; set => SetProperty(ref opcioTriada, value); }
        public int NGuanyadesTotal 
        { get => nGuanyadesTotal; set => SetProperty(ref nGuanyadesTotal, value); }
        public int NPerdudesTotal 
        { get => nPerdudesTotal; set => SetProperty(ref nPerdudesTotal, value); }

        private void ValidaJugador()
        {
            EsValid = !(String.IsNullOrEmpty(Nom) || String.IsNullOrEmpty(Cognom));
        }
    }
}
