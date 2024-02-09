using PedraPaperTisores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PedraPaperTisores.Model;
using PedraPaperTisores.Tools;
using System.Windows;

namespace PedraPaperTisores.ViewModel
{
    public class wndPPTViewModel:ObservableBase
    {
        Partida partida;
        Dictionary<EstatPartida, string> missatges;
        string missatge = "";
        Random randNum = new();
        string[] nomsRival = { "Agaue","Agni","Solaire","Oscar","Rukimi","Andre","Leo","Elis"};
        string[] cognomsRival = {"Amon Ra","Zal","Azrael","Neith","Thaleia","Meyer","Yermolay","Of Astora"};


        //visibilitat panells
        Visibility visibilitatConfiguracio;
        Visibility visibilitatEnJoc;
        Visibility visibilitatResultats;

        public wndPPTViewModel()
        {
            partida = new Partida();
            partida.Jugador = new Jugador
            {
                Nom="Ivan",
                Cognom="Torrente"
            };
            partida.Robot = new Jugador
            {
                Nom = TriaNom(),
                Cognom = TriaCognom(),
            };
            InicialitzaMissatges();
            CreaCommands();
        }

        private string TriaCognom()
        {
            return cognomsRival[randNum.Next(0, cognomsRival.Length)];
        }
        private string TriaNom()
        {
            return nomsRival[randNum.Next(0, nomsRival.Length)];
        }

        private void InicialitzaMissatges()
        {
            missatges = new Dictionary<EstatPartida, string>();
            missatges.Add(EstatPartida.Configuracio,
                "Entra el teu nom i número de rondes");
            missatges.Add(EstatPartida.EnJoc,
                "Guanya "+ ((Partida.NRondes/2)+1) +" rondes per acabar amb el rival");
            missatges.Add(EstatPartida.Resultats,
                "El resultat de la partida és:");
            CanviaEstat(EstatPartida.Configuracio);
        }

        private void CanviaEstat(EstatPartida estat)
        {
            Partida.Estat = estat;
            Missatge = missatges[estat];
            if (estat == EstatPartida.Configuracio)
            {
                VisibilitatConfiguracio = Visibility.Visible;
                VisibilitatEnJoc = Visibility.Collapsed;
                VisibilitatResultats = Visibility.Collapsed;
            }
            else if (estat == EstatPartida.EnJoc)
            {
                VisibilitatConfiguracio = Visibility.Collapsed;
                VisibilitatEnJoc = Visibility.Visible;
                VisibilitatResultats = Visibility.Collapsed;
            }
            else if (estat == EstatPartida.Resultats)
            {
                VisibilitatConfiguracio = Visibility.Collapsed;
                VisibilitatEnJoc = Visibility.Collapsed;
                VisibilitatResultats = Visibility.Visible;
            }
        }

        public ICommand JugaCommand { get; private set; }
        public ICommand PedraCommand { get; private set; }
        public ICommand PaperCommand { get; private set; }
        public ICommand TisoresCommand { get; private set; }
        public ICommand LlangardaixCommand { get; private set; }
        public ICommand SpockCommand { get; private set; }
        public ICommand TornaAlIniciCommand { get; private set; }
        public ICommand AcabaPartidaCommand { get; private set; }

        public Partida Partida { get => partida; set => partida = value; }
        public string Missatge
        {
            get => missatge;
            set => SetProperty(ref missatge, value);
        }
        public Visibility VisibilitatConfiguracio 
        { get => visibilitatConfiguracio; set => SetProperty(ref visibilitatConfiguracio, value); }
        public Visibility VisibilitatEnJoc 
        { get => visibilitatEnJoc; set => SetProperty(ref visibilitatEnJoc, value); }
        public Visibility VisibilitatResultats 
        { get => visibilitatResultats; set => SetProperty(ref visibilitatResultats, value); }

        private void CreaCommands()
        {
            JugaCommand = new RelayCommand(Juga, PotJugar);

            PedraCommand = new RelayCommand(Pedra, PotTriarOpcio);
            PaperCommand = new RelayCommand(Paper, PotTriarOpcio);
            TisoresCommand = new RelayCommand(Tisores, PotTriarOpcio);
            LlangardaixCommand = new RelayCommand(Llangardaix, PotTriarOpcio);
            SpockCommand = new RelayCommand(Spock, PotTriarOpcio);

            TornaAlIniciCommand = new RelayCommand(TornaAlInici, PotTornaAlInici);
            AcabaPartidaCommand = new RelayCommand(AcabaPartida, PotAcabaPartida);
        }

        private bool PotAcabaPartida(object obj)
        {
            return Partida.Jugador.NPerdudes == ((Partida.NRondes / 2) + 1);
        }
        private void AcabaPartida(object obj)
        {
            CanviaEstat(EstatPartida.Resultats);
        }
        private bool PotTornaAlInici(object obj)
        {
            return true;
        }
        private void TornaAlInici(object obj)
        {
            ResetStats();
            CanviaEstat(EstatPartida.Configuracio);
        }
        private void ResetStats()
        {
            Partida.Jugador.NGuanyades = 0;
            Partida.Jugador.NPerdudes = 0;
            Partida.Jugador.NContrincantsEliminats = 0;
            Partida.Robot.NGuanyades = 0;
            Partida.Robot.NPerdudes = 0;
            Partida.Robot.NContrincantsEliminats = 0;
            Partida.Robot.Nom = TriaNom();
            Partida.Robot.Cognom = TriaCognom();
        }

        private bool PotTriarOpcio(object obj)
        {
            return Partida.Jugador.NPerdudes < ((Partida.NRondes / 2) + 1);
        }
        private void Pedra(object obj)
        {
            Partida.Jugador.OpcioTriada = Opcio.Pedra;
            ComprovaResultat(Opcio.Pedra);
        }
        private void Paper(object obj)
        {
            Partida.Jugador.OpcioTriada = Opcio.Paper;
            ComprovaResultat(Opcio.Paper);
        }
        private void Tisores(object obj)
        {
            Partida.Jugador.OpcioTriada = Opcio.Tisores;
            ComprovaResultat(Opcio.Tisores);
        }
        private void Llangardaix(object obj)
        {
            Partida.Jugador.OpcioTriada = Opcio.Llangardaix;
            ComprovaResultat(Opcio.Llangardaix);
        }
        private void Spock(object obj)
        {
            Partida.Jugador.OpcioTriada = Opcio.Spock;
            ComprovaResultat(Opcio.Spock);
        }
        private void ComprovaResultat(Opcio opJugador)
        {
            Opcio opRobot = (Opcio)randNum.Next(0, 5);
            Partida.Robot.OpcioTriada = opRobot;
            switch (opJugador)
            {
                case Opcio.Pedra:
                    if (opRobot==Opcio.Tisores||opRobot==Opcio.Llangardaix)
                    {
                        Partida.Jugador.NGuanyades++; Partida.Jugador.NGuanyadesTotal++;
                        Partida.Robot.NPerdudes++;
                    }
                    else if (opRobot==Opcio.Paper||opRobot==Opcio.Spock)
                    {
                        Partida.Jugador.NPerdudes++; Partida.Jugador.NPerdudesTotal++;
                        Partida.Robot.NGuanyades++;
                    }
                    break;
                case Opcio.Paper:
                    if (opRobot == Opcio.Pedra || opRobot == Opcio.Spock)
                    {
                        Partida.Jugador.NGuanyades++; Partida.Jugador.NGuanyadesTotal++;
                        Partida.Robot.NPerdudes++;
                    }
                    else if (opRobot == Opcio.Tisores || opRobot == Opcio.Llangardaix)
                    {
                        Partida.Jugador.NPerdudes++; Partida.Jugador.NPerdudesTotal++;
                        Partida.Robot.NGuanyades++;
                    }
                    break;
                case Opcio.Tisores:
                    if (opRobot == Opcio.Paper || opRobot == Opcio.Llangardaix)
                    {
                        Partida.Jugador.NGuanyades++; Partida.Jugador.NGuanyadesTotal++;
                        Partida.Robot.NPerdudes++;
                    }
                    else if (opRobot == Opcio.Pedra || opRobot == Opcio.Spock)
                    {
                        Partida.Jugador.NPerdudes++; Partida.Jugador.NPerdudesTotal++;
                        Partida.Robot.NGuanyades++;
                    }
                    break;
                case Opcio.Llangardaix:
                    if (opRobot == Opcio.Paper || opRobot == Opcio.Spock)
                    {
                        Partida.Jugador.NGuanyades++; Partida.Jugador.NGuanyadesTotal++;
                        Partida.Robot.NPerdudes++;
                    }
                    else if (opRobot == Opcio.Tisores || opRobot == Opcio.Pedra)
                    {
                        Partida.Jugador.NPerdudes++; Partida.Jugador.NPerdudesTotal++;
                        Partida.Robot.NGuanyades++;
                    }
                    break;
                case Opcio.Spock:
                    if (opRobot == Opcio.Pedra || opRobot == Opcio.Tisores)
                    {
                        Partida.Jugador.NGuanyades++; Partida.Jugador.NGuanyadesTotal++;
                        Partida.Robot.NPerdudes++;
                    }
                    else if (opRobot == Opcio.Paper || opRobot == Opcio.Llangardaix)
                    {
                        Partida.Jugador.NPerdudes++; Partida.Jugador.NPerdudesTotal++;
                        Partida.Robot.NGuanyades++;
                    }
                    break;
            }
            if (Partida.Robot.NPerdudes == ((Partida.NRondes / 2) + 1))
            {
                Partida.Robot.Nom = TriaNom();
                Partida.Robot.Cognom = TriaCognom();
                Partida.Robot.NGuanyades = 0;
                Partida.Robot.NPerdudes = 0;
                Partida.Jugador.NGuanyades = 0;
                Partida.Jugador.NPerdudes = 0;
                Partida.Jugador.NContrincantsEliminats++;
            }
        }

        private bool PotJugar(object obj)
        {
            return Partida.Jugador.EsValid;
        }
        private void Juga(object obj)
        {
            CanviaEstat(EstatPartida.EnJoc);
        }
    }
}
