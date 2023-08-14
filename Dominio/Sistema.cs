﻿using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Dominio
{
    public class Sistema
    {
        #region Listas
        private List<Resenia> _resenias = new List<Resenia>();
        private List<Periodista> _periodistas = new List<Periodista>();
        private List<Jugador> _jugadores = new List<Jugador>();
        private List<Pais> _paises = new List<Pais>();
        private List<Seleccion> _selecciones = new List<Seleccion>();
        private List<Partido> _partidos = new List<Partido>();
        private List<Operador> _operadores = new List<Operador>();
        private List<Funcionario> _funcionarios = new List<Funcionario>();
        #endregion

        #region Singleton
        //Parte 1
        //Crear instancia privada static en null
        private static Sistema instancia = null;

        //Parte 2
        //Constructor privado
        private Sistema()
        {
            PrecargarPaises();
            PrecargarJugadores();
            PrecargarSelecciones();
            PrecargarPartidos();
            PrecargarPeriodistas();
            PrecargarOperadores();
            PrecargarResenias();
        }

        //Parte 3
        //Creamos metodo GetInstancia solo nos deja crear una instancia nueva de sistema si esta null sino
        //devuelve la misma instancia
        public static Sistema GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }
        #endregion

        #region Métodos Get Listas
        public List<Resenia> GetResenias()
        {
            return _resenias;
        }
        public List<Periodista> GetPeriodistas()
        {
            _periodistas.Sort();
            return _periodistas;
        }
        public List<Jugador> GetJugadores()
        {
            return _jugadores;
        }
        public List<Pais> GetPaises()
        {
            return _paises;
        }
        public List<Seleccion> GetSelecciones()
        {
            _selecciones.Sort();
            return _selecciones;
        }
        public List<Partido> GetPartidos()
        {
            return _partidos;
        }
        public List<Operador> GetOperadores()
        {
            return _operadores;
        }

        #region Filtros para los partidos (Fase de grupos y Fase Eliminatoria)
        public List<FaseDeGrupos> GetPartidosFaseGrupos()
        {
            List<FaseDeGrupos> ret = new List<FaseDeGrupos>();

            foreach (Partido pfg in _partidos)
            {
                if (pfg is FaseDeGrupos)
                {
                    ret.Add((FaseDeGrupos)pfg);
                }
            }
            return ret;
        }


        public List<FaseEliminatoria> GetPartidosFaseEliminatoria()
        {
            List<FaseEliminatoria> ret = new List<FaseEliminatoria>();

            foreach (Partido pfe in _partidos)
            {
                if (pfe is FaseEliminatoria)
                {
                    ret.Add((FaseEliminatoria)pfe);
                }
            }
            return ret;
        }
        #endregion

        #endregion

        #region Get Objeto
        private Pais GetPais(string nombre)
        {
            foreach (Pais p in _paises)
            {
                if (p.Nombre.Equals(nombre))
                {
                    return p;
                }
            }
            return null;
        }

        private Jugador GetJugador(int id)
        {
            foreach (Jugador j in _jugadores)
            {
                if (j.Id.Equals(id))
                {
                    return j;
                }
            }
            return null;
        }

        private Seleccion GetSeleccion(Pais pais)
        {

            foreach (Seleccion sel in _selecciones)
            {
                if (sel.Pais.Equals(pais))
                {
                    return sel;
                }
            }
            return null;
        }

        public Seleccion GetSelecciones(string nombrePais)
        {
            foreach (Seleccion s in _selecciones)
            {

                if (s.Pais.Nombre.Equals(nombrePais))
                {
                    return s;
                }

            }
            return null;
        }

        public Periodista GetPeriodista(int? id)
        {
            foreach (Funcionario f in _funcionarios)
            {
                if (f is Periodista)
                {
                    if (f.Id.Equals(id))
                    {
                        return f as Periodista;
                    }
                }
            }
            return null;
        }

        public Partido GetPartido(int id)
        {
            foreach (Partido p in _partidos)
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }

        public int GetCantidadGolesDeSeleccion (Seleccion sel)
        {
            int cantGoles = 0;

            foreach (Partido p in _partidos)
            {
                if (p.SeleccionesEnfrentadas[0].Pais.Nombre == sel.Pais.Nombre || p.SeleccionesEnfrentadas[1].Pais.Nombre == sel.Pais.Nombre)
                {
                    foreach (Incidencia inci in p.Incidencias)
                    {
                        if (inci.TipoIncidencia == TipoDeIncidencia.golConvertido && inci.MinutoOcurrido != -1)
                        {
                            cantGoles++;
                        }
                    }
                }
            }
            return cantGoles;
        }
        #endregion

        #region Get Listas Nuestras
        public List<Resenia> GetReseniasDePeriodista(int? id)
        {
            List<Resenia> ret = new List<Resenia>();
            foreach (Resenia r in _resenias)
            {
                if (r.Periodista.Id.Equals(id))
                {
                    ret.Add(r);
                }
            }
            ret.Sort();
            return ret;
        }

        public List<Resenia> GetReseniasDePeriodistaPorMail(string mail)
        {
            List<Resenia> ret = new List<Resenia>();
            foreach (Resenia r in _resenias)
            {
                if (r.Periodista.Email.Equals(mail))
                {
                    ret.Add(r);
                }
            }
            return ret;
        }

        public List<Partido> GetPartidosFinalizados()
        {
            List<Partido> ret = new List<Partido>();
            foreach (Partido p in _partidos)
            {
                if (p.Finalizado)
                {
                    ret.Add(p);
                }
            }
            return ret;
        }

        public List<Partido> GetPartidosEntreFechas(DateTime f1, DateTime f2)
        {
            DateTime fechaAux;
            if (f1 > f2)
            {
                fechaAux = f1;
                f1 = f2;
                f2 = fechaAux;
            }

            List<Partido> ret = new List<Partido>();

            foreach (Partido p in GetPartidosFinalizados())
            {
                if (p.Fecha > f1 && p.Fecha < f2)
                {
                    ret.Add(p);
                }
            }

            if (ret.Count == 0)
            {
                throw new Exception("No hay partidos cerrados en esas fechas");
            }
            return ret;
        }

        public List<Seleccion> GetSeleccionesGoleadoras()
        {
            List<Seleccion> ret = new List<Seleccion>();
            int cantAux = 0;
            foreach (Seleccion sel in _selecciones)
            {
                if (GetCantidadGolesDeSeleccion(sel) > cantAux)
                {
                    if (GetCantidadGolesDeSeleccion(sel) != cantAux)
                    {
                        ret.Clear();
                    }

                    cantAux = GetCantidadGolesDeSeleccion(sel);
                    ret.Add(sel);
                }
            }
            return ret;
        }

        public List<Partido> GetPartidosDePeriodista(string email)
        {
            List<Partido> ret = new List<Partido>();
            List<Resenia> reseniasP = GetReseniasDePeriodistaPorMail(email);

            foreach (Resenia r in reseniasP)
            {
                foreach (Incidencia i in r.Partido.Incidencias)
                {
                    if (i.TipoIncidencia == TipoDeIncidencia.expulsion)
                    {
                        if (!ret.Contains(r.Partido))
                        {
                            ret.Add(r.Partido);
                        }
                    }
                }

            }
            return ret;
        }
        #endregion

        #region Métodos Altas
        public void AltaResenia(Resenia r)
        {
            try
            {
                if (!_resenias.Contains(r))
                {
                    r.EsValido();
                    _resenias.Add(r);
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void AltaFuncionario(Funcionario fun)
        {
            try
            {
                if (!_funcionarios.Contains(fun))
                {
                    fun.EsValido();
                    _funcionarios.Add(fun);

                    if (fun is Periodista)
                    {
                        _periodistas.Add((Periodista)fun);
                    }
                    else
                    {
                        _operadores.Add((Operador)fun);
                    }
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void AltaJugador(Jugador j)
        {
            try
            {
                if (!_jugadores.Contains(j))
                {
                    j.EsValido();
                    _jugadores.Add(j);
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void AltaPais(Pais pais)
        {
            try
            {
                if (!_paises.Contains(pais))
                {
                    pais.EsValido();
                    _paises.Add(pais);
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void AltaSeleccion(Seleccion sel)
        {
            try
            {
                if (!_selecciones.Contains(sel))
                {
                    sel.EsValido();
                    _selecciones.Add(sel);
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void AltaPartidoFG(FaseDeGrupos pfg)
        {
            try
            {
                if (!_partidos.Contains(pfg))
                {
                    pfg.EsValido();
                    _partidos.Add(pfg);
                }
                else
                {
                    throw new Exception("Ya existe partido");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void AltaPartidoFE(FaseEliminatoria pfe)
        {
            try
            {
                if (!_partidos.Contains(pfe))
                {
                    pfe.EsValido();
                    _partidos.Add(pfe);
                }
                else
                {
                    throw new Exception("Ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Precarga Datos

        #region Paises
        private void PrecargarPaises()
        {
            try
            {
                AltaPais(new Pais("Catar", "QAT"));
                AltaPais(new Pais("Alemania", "DEU"));
                AltaPais(new Pais("Dinamarca", "DNK"));
                AltaPais(new Pais("Brasil", "BRA"));
                AltaPais(new Pais("Francia", "FRA"));
                AltaPais(new Pais("Bélgica", "BEL"));
                AltaPais(new Pais("Croacia", "HRV"));
                AltaPais(new Pais("España", "ESP"));
                AltaPais(new Pais("Serbia", "SRB"));
                AltaPais(new Pais("Inglaterra", "GBR"));
                AltaPais(new Pais("Suiza", "CHE"));
                AltaPais(new Pais("Países Bajos", "NLD"));
                AltaPais(new Pais("Argentina", "ARG"));
                AltaPais(new Pais("Irán", "IRN"));
                AltaPais(new Pais("Corea del Sur", "KOR"));
                AltaPais(new Pais("Japón", "JPN"));
                AltaPais(new Pais("Arabia Saudita", "SAU"));
                AltaPais(new Pais("Ecuador", "ECU"));
                AltaPais(new Pais("Uruguay", "URY"));
                AltaPais(new Pais("Canadá", "CAN"));
                AltaPais(new Pais("Ghana", "GHA"));
                AltaPais(new Pais("Senegal", "SEN"));
                AltaPais(new Pais("Marruecos", "MAR"));
                AltaPais(new Pais("Túnez", "TUN"));
                AltaPais(new Pais("Portugal", "PRT"));
                AltaPais(new Pais("Polonia", "POL"));
                AltaPais(new Pais("Camerún", "CMR"));
                AltaPais(new Pais("México", "MEX"));
                AltaPais(new Pais("Estados Unidos", "USA"));
                AltaPais(new Pais("Gales", "WLS"));
                AltaPais(new Pais("Australia", "AUS"));
                AltaPais(new Pais("Costa Rica", "CRI"));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Jugadores
        private void PrecargarJugadores()
        {
            try
            {
                AltaJugador(new Jugador(1, "23", "Emiliano Martínez", DateTime.Parse("1992-09-02"), 1.95, "derecho", 28000000, "EUR", GetPais("Argentina"), "Portero"));
                AltaJugador(new Jugador(2, "12", "Gerónimo Rulli", DateTime.Parse("1992-05-20"), 1.89, "derecho", 6000000, "EUR", GetPais("Argentina"), "Portero"));
                AltaJugador(new Jugador(3, "1", "Franco Armani", DateTime.Parse("1986-10-16"), 1.89, "derecho", 3500000, "EUR", GetPais("Argentina"), "Portero"));
                AltaJugador(new Jugador(4, "13", "Cristian Romero", DateTime.Parse("1998-04-27"), 1.85, "derecho", 48000000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(5, "16", "Lisandro Martínez", DateTime.Parse("1998-01-18"), 1.75, "izquierdo", 32000000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(6, "25", "Marcos Senesi", DateTime.Parse("1997-05-10"), 1.85, "izquierdo", 17000000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(7, "114", "Lucas Martínez Quarta", DateTime.Parse("1996-05-10"), 1.83, "derecho", 12000000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(8, "6", "Germán Pezzella", DateTime.Parse("1991-06-27"), 1.87, "derecho", 5000000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(9, "19", "Nicolás Otamendi", DateTime.Parse("1988-02-12"), 1.83, "derecho", 3500000, "EUR", GetPais("Argentina"), "Defensa central"));
                AltaJugador(new Jugador(10, "8", "Marcos Acuña", DateTime.Parse("1991-10-28"), 1.72, "izquierdo", 18000000, "EUR", GetPais("Argentina"), "Lateral izquierdo"));
                AltaJugador(new Jugador(11, "3", "Nicolás Tagliafico", DateTime.Parse("1992-08-31"), 1.72, "izquierdo", 11000000, "EUR", GetPais("Argentina"), "Lateral izquierdo"));
                AltaJugador(new Jugador(12, "2", "Juan Foyth", DateTime.Parse("1998-01-12"), 1.87, "derecho", 25000000, "EUR", GetPais("Argentina"), "Lateral derecho"));
                AltaJugador(new Jugador(13, "26", "Nahuel Molina", DateTime.Parse("1998-04-06"), 1.75, "derecho", 20000000, "EUR", GetPais("Argentina"), "Lateral derecho"));
                AltaJugador(new Jugador(14, "4", "Gonzalo Montiel", DateTime.Parse("1997-01-01"), 1.76, "derecho", 14000000, "EUR", GetPais("Argentina"), "Lateral derecho"));
                AltaJugador(new Jugador(15, "18", "Guido Rodríguez", DateTime.Parse("1994-04-12"), 1.85, "derecho", 25000000, "EUR", GetPais("Argentina"), "Pivote"));
                AltaJugador(new Jugador(16, "NDF", "Leandro Paredes", DateTime.Parse("1994-06-29"), 1.8, "derecho", 17000000, "EUR", GetPais("Argentina"), "Pivote"));
                AltaJugador(new Jugador(17, "7", "Rodrigo de Paul", DateTime.Parse("1994-05-24"), 1.8, "derecho", 40000000, "EUR", GetPais("Argentina"), "Mediocentro"));
                AltaJugador(new Jugador(18, "20", "Giovani Lo Celso", DateTime.Parse("1996-04-09"), 1.77, "izquierdo", 22000000, "EUR", GetPais("Argentina"), "Mediocentro"));
                AltaJugador(new Jugador(19, "14", "Exequiel Palacios", DateTime.Parse("1998-10-05"), 1.77, "derecho", 15000000, "EUR", GetPais("Argentina"), "Mediocentro"));
                AltaJugador(new Jugador(20, "5", "Alexis Mac Allister", DateTime.Parse("1998-12-24"), 1.74, "derecho", 16000000, "EUR", GetPais("Argentina"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(21, "17", "Papu Gómez", DateTime.Parse("1988-02-15"), 1.67, "derecho", 6000000, "EUR", GetPais("Argentina"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(22, "15", "Nicolás González", DateTime.Parse("1998-04-06"), 1.8, "izquierdo", 25000000, "EUR", GetPais("Argentina"), "Extremo izquierdo"));
                AltaJugador(new Jugador(23, "10", "Lionel Messi", DateTime.Parse("1987-06-24"), 1.7, "izquierdo", 50000000, "EUR", GetPais("Argentina"), "Extremo derecho"));
                AltaJugador(new Jugador(24, "11", "Ángel Di María", DateTime.Parse("1988-02-14"), 1.8, "izquierdo", 12000000, "EUR", GetPais("Argentina"), "Extremo derecho"));
                AltaJugador(new Jugador(25, "21", "Paulo Dybala", DateTime.Parse("1993-11-15"), 1.77, "izquierdo", 35000000, "EUR", GetPais("Argentina"), "Mediapunta"));
                AltaJugador(new Jugador(26, "19", "Joaquín Correa", DateTime.Parse("1994-08-13"), 1.88, "derecho", 23000000, "EUR", GetPais("Argentina"), "Mediapunta"));
                AltaJugador(new Jugador(27, "22", "Lautaro Martínez", DateTime.Parse("1997-08-22"), 1.74, "derecho", 75000000, "EUR", GetPais("Argentina"), "Delantero centro"));
                AltaJugador(new Jugador(28, "9", "Julián Álvarez", DateTime.Parse("2000-01-31"), 1.7, "derecho", 23000000, "EUR", GetPais("Argentina"), "Delantero centro"));
                AltaJugador(new Jugador(29, "22", "Meshaal Barsham", DateTime.Parse("1998-02-14"), 1.8, "derecho", 450000, "EUR", GetPais("Catar"), "Portero"));
                AltaJugador(new Jugador(30, "1", "Saad Al-Sheeb", DateTime.Parse("1990-02-19"), 1.84, "derecho", 200000, "EUR", GetPais("Catar"), "Portero"));
                AltaJugador(new Jugador(31, "21", "Yousef Hassan", DateTime.Parse("1996-05-24"), 0, "NDF", 150000, "EUR", GetPais("Catar"), "Portero"));
                AltaJugador(new Jugador(32, "5", "Tarek Salman", DateTime.Parse("1997-12-05"), 1.79, "derecho", 550000, "EUR", GetPais("Catar"), "Defensa central"));
                AltaJugador(new Jugador(33, "2", "Pedro Miguel", DateTime.Parse("1990-08-06"), 1.82, "derecho", 500000, "EUR", GetPais("Catar"), "Defensa central"));
                AltaJugador(new Jugador(34, "15", "Bassam Al-Rawi", DateTime.Parse("1997-12-16"), 1.75, "derecho", 500000, "EUR", GetPais("Catar"), "Defensa central"));
                AltaJugador(new Jugador(35, "16", "Boualem Khoukhi", DateTime.Parse("1990-07-09"), 1.8, "derecho", 500000, "EUR", GetPais("Catar"), "Defensa central"));
                AltaJugador(new Jugador(36, "3", "Abdelkarim Hassan", DateTime.Parse("1993-08-28"), 1.86, "izquierdo", 775000, "EUR", GetPais("Catar"), "Lateral izquierdo"));
                AltaJugador(new Jugador(37, "14", "Homam Ahmed", DateTime.Parse("1999-08-25"), 1.86, "izquierdo", 500000, "EUR", GetPais("Catar"), "Lateral izquierdo"));
                AltaJugador(new Jugador(38, "13", "Musab Khoder", DateTime.Parse("1993-01-01"), 1.74, "derecho", 325000, "EUR", GetPais("Catar"), "Lateral derecho"));
                AltaJugador(new Jugador(39, "12", "Karim Boudiaf", DateTime.Parse("1990-09-16"), 1.87, "derecho", 450000, "EUR", GetPais("Catar"), "Pivote"));
                AltaJugador(new Jugador(40, "23", "Assim Madibo", DateTime.Parse("1996-10-22"), 1.68, "derecho", 300000, "EUR", GetPais("Catar"), "Pivote"));
                AltaJugador(new Jugador(41, "115", "Salem Al-Hajri", DateTime.Parse("1996-04-10"), 1.83, "derecho", 250000, "EUR", GetPais("Catar"), "Pivote"));
                AltaJugador(new Jugador(42, "167", "Ahmed Fadel", DateTime.Parse("1993-04-07"), 0, "NDF", 175000, "EUR", GetPais("Catar"), "Pivote"));
                AltaJugador(new Jugador(43, "154", "Abdulaziz Hatem", DateTime.Parse("1990-10-28"), 1.83, "izquierdo", 500000, "EUR", GetPais("Catar"), "Mediocentro"));
                AltaJugador(new Jugador(44, "4", "Mohammed Waad", DateTime.Parse("1999-09-18"), 1.83, "izquierdo", 300000, "EUR", GetPais("Catar"), "Mediocentro"));
                AltaJugador(new Jugador(45, "20", "Abdullah Al-Ahrak", DateTime.Parse("1997-05-10"), 1.75, "derecho", 500000, "EUR", GetPais("Catar"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(46, "8", "Ali Asad", DateTime.Parse("1993-01-19"), 1.75, "derecho", 250000, "EUR", GetPais("Catar"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(47, "11", "Akram Afif", DateTime.Parse("1996-11-18"), 1.77, "derecho", 5500000, "EUR", GetPais("Catar"), "Extremo izquierdo"));
                AltaJugador(new Jugador(48, "NDF", "Abdelrahman Moustafa", DateTime.Parse("1997-04-05"), 1.68, "derecho", 375000, "EUR", GetPais("Catar"), "Extremo izquierdo"));
                AltaJugador(new Jugador(49, "18", "Khalid Muneer Mazeed", DateTime.Parse("1998-02-24"), 1.74, "NDF", 350000, "EUR", GetPais("Catar"), "Extremo izquierdo"));
                AltaJugador(new Jugador(50, "41", "Hasan Al-Haydos", DateTime.Parse("1990-12-11"), 1.74, "derecho", 1300000, "EUR", GetPais("Catar"), "Extremo derecho"));
                AltaJugador(new Jugador(51, "NDF", "Hazem Ahmed", DateTime.Parse("1998-02-02"), 0, "izquierdo", 400000, "EUR", GetPais("Catar"), "Extremo derecho"));
                AltaJugador(new Jugador(52, "17", "Ismaeel Mohammed", DateTime.Parse("1990-04-05"), 1.68, "derecho", 250000, "EUR", GetPais("Catar"), "Extremo derecho"));
                AltaJugador(new Jugador(53, "19", "Almoez Ali", DateTime.Parse("1996-08-19"), 1.8, "derecho", 2800000, "EUR", GetPais("Catar"), "Delantero centro"));
                AltaJugador(new Jugador(54, "7", "Ahmed Alaaeldin", DateTime.Parse("1993-01-31"), 1.77, "derecho", 700000, "EUR", GetPais("Catar"), "Delantero centro"));
                AltaJugador(new Jugador(55, "NDF", "Mohammed Muntari", DateTime.Parse("1993-12-20"), 1.94, "derecho", 350000, "EUR", GetPais("Catar"), "Delantero centro"));
                AltaJugador(new Jugador(56, "1", "Manuel Neuer", DateTime.Parse("1986-03-27"), 1.93, "derecho", 15000000, "EUR", GetPais("Alemania"), "Portero"));
                AltaJugador(new Jugador(57, "12", "Kevin Trapp", DateTime.Parse("1990-07-08"), 1.89, "derecho", 7000000, "EUR", GetPais("Alemania"), "Portero"));
                AltaJugador(new Jugador(58, "22", "Oliver Baumann", DateTime.Parse("1990-06-02"), 1.87, "derecho", 4500000, "EUR", GetPais("Alemania"), "Portero"));
                AltaJugador(new Jugador(59, "2", "Antonio Rüdiger", DateTime.Parse("1993-03-03"), 1.9, "derecho", 40000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(60, "15", "Niklas Süle", DateTime.Parse("1995-09-03"), 1.95, "derecho", 35000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(61, "23", "Nico Schlotterbeck", DateTime.Parse("1999-12-01"), 1.91, "izquierdo", 33000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(62, "4", "Jonathan Tah", DateTime.Parse("1996-02-11"), 1.95, "derecho", 25000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(63, "5", "Thilo Kehrer", DateTime.Parse("1996-09-21"), 1.86, "derecho", 22000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(64, "16", "Lukas Klostermann", DateTime.Parse("1996-06-03"), 1.89, "derecho", 18000000, "EUR", GetPais("Alemania"), "Defensa central"));
                AltaJugador(new Jugador(65, "3", "David Raum", DateTime.Parse("1998-04-22"), 1.8, "izquierdo", 20000000, "EUR", GetPais("Alemania"), "Lateral izquierdo"));
                AltaJugador(new Jugador(66, "17", "Benjamin Henrichs", DateTime.Parse("1997-02-23"), 1.85, "derecho", 14000000, "EUR", GetPais("Alemania"), "Lateral derecho"));
                AltaJugador(new Jugador(67, "6", "Joshua Kimmich", DateTime.Parse("1995-02-08"), 1.77, "derecho", 80000000, "EUR", GetPais("Alemania"), "Pivote"));
                AltaJugador(new Jugador(68, "2", "Anton Stach", DateTime.Parse("1998-11-15"), 1.94, "ambidiestro", 13000000, "EUR", GetPais("Alemania"), "Pivote"));
                AltaJugador(new Jugador(69, "8", "Leon Goretzka ", DateTime.Parse("1995-02-06"), 1.89, "derecho", 65000000, "EUR", GetPais("Alemania"), "Mediocentro"));
                AltaJugador(new Jugador(70, "21", "Ilkay Gündogan", DateTime.Parse("1990-10-24"), 1.8, "derecho", 25000000, "EUR", GetPais("Alemania"), "Mediocentro"));
                AltaJugador(new Jugador(71, "7", "Kai Havertz", DateTime.Parse("1999-06-11"), 1.9, "izquierdo", 70000000, "EUR", GetPais("Alemania"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(72, "14", "Jamal Musiala", DateTime.Parse("2003-02-26"), 1.83, "derecho", 65000000, "EUR", GetPais("Alemania"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(73, "20", "Julian Brandt", DateTime.Parse("1996-05-02"), 1.85, "derecho", 25000000, "EUR", GetPais("Alemania"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(74, "18", "Jonas Hofmann", DateTime.Parse("1992-07-14"), 1.76, "derecho", 13000000, "EUR", GetPais("Alemania"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(75, "19", "Leroy Sané", DateTime.Parse("1996-01-11"), 1.83, "izquierdo", 60000000, "EUR", GetPais("Alemania"), "Extremo izquierdo"));
                AltaJugador(new Jugador(76, "10", "Serge Gnabry", DateTime.Parse("1995-07-14"), 1.76, "derecho", 65000000, "EUR", GetPais("Alemania"), "Extremo derecho"));
                AltaJugador(new Jugador(77, "13", "Thomas Müller", DateTime.Parse("1989-09-13"), 1.85, "derecho", 22000000, "EUR", GetPais("Alemania"), "Mediapunta"));
                AltaJugador(new Jugador(78, "9", "Timo Werner", DateTime.Parse("1996-03-06"), 1.8, "derecho", 35000000, "EUR", GetPais("Alemania"), "Delantero centro"));
                AltaJugador(new Jugador(79, "10", "Karim Adeyemi", DateTime.Parse("2002-01-18"), 1.8, "izquierdo", 35000000, "EUR", GetPais("Alemania"), "Delantero centro"));
                AltaJugador(new Jugador(80, "11", "Lukas Nmecha", DateTime.Parse("1998-12-14"), 1.85, "derecho", 18000000, "EUR", GetPais("Alemania"), "Delantero centro"));
                AltaJugador(new Jugador(81, "1", "Kasper Schmeichel", DateTime.Parse("1986-11-05"), 1.89, "derecho", 4000000, "EUR", GetPais("Dinamarca"), "Portero"));
                AltaJugador(new Jugador(82, "16", "Daniel Iversen", DateTime.Parse("1997-07-19"), 1.91, "NDF", 2000000, "EUR", GetPais("Dinamarca"), "Portero"));
                AltaJugador(new Jugador(83, "22", "Peter Vindahl", DateTime.Parse("1998-02-16"), 1.95, "derecho", 900000, "EUR", GetPais("Dinamarca"), "Portero"));
                AltaJugador(new Jugador(84, "6", "Andreas Christensen", DateTime.Parse("1996-04-10"), 1.87, "derecho", 35000000, "EUR", GetPais("Dinamarca"), "Defensa central"));
                AltaJugador(new Jugador(85, "2", "Joachim Andersen", DateTime.Parse("1996-05-31"), 1.92, "derecho", 25000000, "EUR", GetPais("Dinamarca"), "Defensa central"));
                AltaJugador(new Jugador(86, "3", "Jannik Vestergaard", DateTime.Parse("1992-08-03"), 1.99, "ambidiestro", 13000000, "EUR", GetPais("Dinamarca"), "Defensa central"));
                AltaJugador(new Jugador(87, "4", "Victor Nelsson", DateTime.Parse("1998-10-14"), 1.85, "derecho", 12000000, "EUR", GetPais("Dinamarca"), "Defensa central"));
                AltaJugador(new Jugador(88, "20", "Nicolai Boilesen", DateTime.Parse("1992-02-16"), 1.86, "izquierdo", 1000000, "EUR", GetPais("Dinamarca"), "Defensa central"));
                AltaJugador(new Jugador(89, "13", "Rasmus Kristensen", DateTime.Parse("1997-07-11"), 1.87, "derecho", 15000000, "EUR", GetPais("Dinamarca"), "Lateral derecho"));
                AltaJugador(new Jugador(90, "5", "Joakim Maehle", DateTime.Parse("1997-05-20"), 1.85, "derecho", 14000000, "EUR", GetPais("Dinamarca"), "Lateral derecho"));
                AltaJugador(new Jugador(91, "17", "Jens Stryger Larsen", DateTime.Parse("1991-02-21"), 1.8, "ambidiestro", 2000000, "EUR", GetPais("Dinamarca"), "Lateral derecho"));
                AltaJugador(new Jugador(92, "8", "Morten Hjulmand", DateTime.Parse("1999-06-25"), 1.85, "derecho", 6500000, "EUR", GetPais("Dinamarca"), "Pivote"));
                AltaJugador(new Jugador(93, "23", "Pierre-Emile Höjbjerg", DateTime.Parse("1995-08-05"), 1.85, "derecho", 40000000, "EUR", GetPais("Dinamarca"), "Mediocentro"));
                AltaJugador(new Jugador(94, "15", "Philip Billing", DateTime.Parse("1996-06-11"), 1.93, "izquierdo", 18000000, "EUR", GetPais("Dinamarca"), "Mediocentro"));
                AltaJugador(new Jugador(95, "7", "Mathias Jensen", DateTime.Parse("1996-01-01"), 1.8, "derecho", 10000000, "EUR", GetPais("Dinamarca"), "Mediocentro"));
                AltaJugador(new Jugador(96, "18", "Daniel Wass", DateTime.Parse("1989-05-31"), 1.78, "derecho", 6000000, "EUR", GetPais("Dinamarca"), "Mediocentro"));
                AltaJugador(new Jugador(97, "11", "Andreas Skov Olsen", DateTime.Parse("1999-12-29"), 1.87, "izquierdo", 12000000, "EUR", GetPais("Dinamarca"), "Interior derecho"));
                AltaJugador(new Jugador(98, "10", "Christian Eriksen", DateTime.Parse("1992-02-14"), 1.82, "derecho", 20000000, "EUR", GetPais("Dinamarca"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(99, "14", "Mikkel Damsgaard", DateTime.Parse("2000-07-03"), 1.8, "derecho", 15000000, "EUR", GetPais("Dinamarca"), "Extremo izquierdo"));
                AltaJugador(new Jugador(100, "12", "Robert Skov", DateTime.Parse("1996-05-20"), 1.85, "izquierdo", 7000000, "EUR", GetPais("Dinamarca"), "Extremo derecho"));
                AltaJugador(new Jugador(101, "19", "Jonas Wind", DateTime.Parse("1999-02-07"), 1.9, "derecho", 14000000, "EUR", GetPais("Dinamarca"), "Delantero centro"));
                AltaJugador(new Jugador(102, "21", "Andreas Cornelius", DateTime.Parse("1993-03-16"), 1.93, "izquierdo", 9000000, "EUR", GetPais("Dinamarca"), "Delantero centro"));
                AltaJugador(new Jugador(103, "9", "Martin Braithwaite", DateTime.Parse("1991-06-05"), 1.77, "derecho", 6000000, "EUR", GetPais("Dinamarca"), "Delantero centro"));
                AltaJugador(new Jugador(104, "1", "Alisson", DateTime.Parse("1992-10-02"), 1.91, "derecho", 50000000, "EUR", GetPais("Brasil"), "Portero"));
                AltaJugador(new Jugador(105, "23", "Ederson", DateTime.Parse("1993-08-17"), 1.88, "izquierdo", 45000000, "EUR", GetPais("Brasil"), "Portero"));
                AltaJugador(new Jugador(106, "12", "Weverton", DateTime.Parse("1987-12-13"), 1.89, "derecho", 4500000, "EUR", GetPais("Brasil"), "Portero"));
                AltaJugador(new Jugador(107, "4", "Marquinhos", DateTime.Parse("1994-05-14"), 1.83, "derecho", 70000000, "EUR", GetPais("Brasil"), "Defensa central"));
                AltaJugador(new Jugador(108, "2", "Éder Militão", DateTime.Parse("1998-01-18"), 1.86, "derecho", 60000000, "EUR", GetPais("Brasil"), "Defensa central"));
                AltaJugador(new Jugador(109, "22", "Gabriel Magalhães", DateTime.Parse("1997-12-19"), 1.9, "izquierdo", 38000000, "EUR", GetPais("Brasil"), "Defensa central"));
                AltaJugador(new Jugador(110, "NDF", "Léo Ortiz", DateTime.Parse("1996-01-03"), 1.85, "derecho", 7500000, "EUR", GetPais("Brasil"), "Defensa central"));
                AltaJugador(new Jugador(111, "3", "Thiago Silva", DateTime.Parse("1984-09-22"), 1.81, "derecho", 2500000, "EUR", GetPais("Brasil"), "Defensa central"));
                AltaJugador(new Jugador(112, "16", "Alex Telles", DateTime.Parse("1992-12-15"), 1.81, "izquierdo", 18000000, "EUR", GetPais("Brasil"), "Lateral izquierdo"));
                AltaJugador(new Jugador(113, "14", "Guilherme Arana", DateTime.Parse("1997-04-14"), 1.76, "izquierdo", 14000000, "EUR", GetPais("Brasil"), "Lateral izquierdo"));
                AltaJugador(new Jugador(114, "6", "Alex Sandro", DateTime.Parse("1991-01-26"), 1.81, "izquierdo", 6000000, "EUR", GetPais("Brasil"), "Lateral izquierdo"));
                AltaJugador(new Jugador(115, "2", "Danilo", DateTime.Parse("1991-07-15"), 1.84, "derecho", 13000000, "EUR", GetPais("Brasil"), "Lateral derecho"));
                AltaJugador(new Jugador(116, "13", "Dani Alves", DateTime.Parse("1983-05-06"), 1.72, "derecho", 1000000, "EUR", GetPais("Brasil"), "Lateral derecho"));
                AltaJugador(new Jugador(117, "15", "Fabinho", DateTime.Parse("1993-10-23"), 1.88, "derecho", 60000000, "EUR", GetPais("Brasil"), "Pivote"));
                AltaJugador(new Jugador(118, "5", "Casemiro", DateTime.Parse("1992-02-23"), 1.85, "derecho", 40000000, "EUR", GetPais("Brasil"), "Pivote"));
                AltaJugador(new Jugador(119, "17", "Bruno Guimarães", DateTime.Parse("1997-11-16"), 1.82, "derecho", 40000000, "EUR", GetPais("Brasil"), "Pivote"));
                AltaJugador(new Jugador(120, "14", "Danilo", DateTime.Parse("2001-04-29"), 1.77, "izquierdo", 22000000, "EUR", GetPais("Brasil"), "Pivote"));
                AltaJugador(new Jugador(121, "8", "Fred", DateTime.Parse("1993-03-05"), 1.69, "izquierdo", 20000000, "EUR", GetPais("Brasil"), "Mediocentro"));
                AltaJugador(new Jugador(122, "7", "Lucas Paquetá", DateTime.Parse("1997-08-27"), 1.8, "izquierdo", 35000000, "EUR", GetPais("Brasil"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(123, "20", "Vinicius Junior", DateTime.Parse("2000-07-12"), 1.76, "derecho", 100000000, "EUR", GetPais("Brasil"), "Extremo izquierdo"));
                AltaJugador(new Jugador(124, "10", "Neymar", DateTime.Parse("1992-02-05"), 1.75, "derecho", 75000000, "EUR", GetPais("Brasil"), "Extremo izquierdo"));
                AltaJugador(new Jugador(125, "22", "Gabriel Martinelli", DateTime.Parse("2001-06-18"), 1.76, "derecho", 40000000, "EUR", GetPais("Brasil"), "Extremo izquierdo"));
                AltaJugador(new Jugador(126, "11", "Philippe Coutinho", DateTime.Parse("1992-06-12"), 1.72, "derecho", 20000000, "EUR", GetPais("Brasil"), "Extremo izquierdo"));
                AltaJugador(new Jugador(127, "23", "Rodrygo", DateTime.Parse("2001-01-09"), 1.74, "derecho", 60000000, "EUR", GetPais("Brasil"), "Extremo derecho"));
                AltaJugador(new Jugador(128, "19", "Raphinha", DateTime.Parse("1996-12-14"), 1.76, "izquierdo", 45000000, "EUR", GetPais("Brasil"), "Extremo derecho"));
                AltaJugador(new Jugador(129, "18", "Gabriel Jesus", DateTime.Parse("1997-04-03"), 1.75, "derecho", 50000000, "EUR", GetPais("Brasil"), "Delantero centro"));
                AltaJugador(new Jugador(130, "9", "Richarlison", DateTime.Parse("1997-05-10"), 1.84, "derecho", 48000000, "EUR", GetPais("Brasil"), "Delantero centro"));
                AltaJugador(new Jugador(131, "21", "Matheus Cunha", DateTime.Parse("1999-05-27"), 1.84, "derecho", 30000000, "EUR", GetPais("Brasil"), "Delantero centro"));
                AltaJugador(new Jugador(132, "16", "Mike Maignan", DateTime.Parse("1995-07-03"), 1.91, "derecho", 35000000, "EUR", GetPais("Francia"), "Portero"));
                AltaJugador(new Jugador(133, "23", "Alphonse Areola", DateTime.Parse("1993-02-27"), 1.95, "derecho", 8000000, "EUR", GetPais("Francia"), "Portero"));
                AltaJugador(new Jugador(134, "1", "Hugo Lloris", DateTime.Parse("1986-12-26"), 1.88, "izquierdo", 7000000, "EUR", GetPais("Francia"), "Portero"));
                AltaJugador(new Jugador(135, "5", "Jules Koundé", DateTime.Parse("1998-11-12"), 1.78, "derecho", 60000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(136, "21", "Lucas Hernández", DateTime.Parse("1996-02-14"), 1.84, "izquierdo", 50000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(137, "4", "Raphaël Varane", DateTime.Parse("1993-04-25"), 1.91, "derecho", 48000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(138, "3", "Presnel Kimpembe", DateTime.Parse("1995-08-13"), 1.83, "izquierdo", 40000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(139, "4", "Ibrahima Konaté", DateTime.Parse("1999-05-25"), 1.94, "derecho", 40000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(140, "17", "William Saliba", DateTime.Parse("2001-03-24"), 1.92, "derecho", 30000000, "EUR", GetPais("Francia"), "Defensa central"));
                AltaJugador(new Jugador(141, "22", "Theo Hernández", DateTime.Parse("1997-10-06"), 1.84, "izquierdo", 55000000, "EUR", GetPais("Francia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(142, "18", "Lucas Digne", DateTime.Parse("1993-07-20"), 1.78, "izquierdo", 25000000, "EUR", GetPais("Francia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(143, "2", "Benjamin Pavard", DateTime.Parse("1996-03-28"), 1.86, "derecho", 30000000, "EUR", GetPais("Francia"), "Lateral derecho"));
                AltaJugador(new Jugador(144, "15", "Jonathan Clauss", DateTime.Parse("1992-09-25"), 1.78, "derecho", 15000000, "EUR", GetPais("Francia"), "Lateral derecho"));
                AltaJugador(new Jugador(145, "8", "Aurélien Tchouameni", DateTime.Parse("2000-01-27"), 1.88, "derecho", 60000000, "EUR", GetPais("Francia"), "Pivote"));
                AltaJugador(new Jugador(146, "13", "N'Golo Kanté ", DateTime.Parse("1991-03-29"), 1.68, "derecho", 40000000, "EUR", GetPais("Francia"), "Pivote"));
                AltaJugador(new Jugador(147, "13", "Boubacar Kamara", DateTime.Parse("1999-11-23"), 1.84, "derecho", 25000000, "EUR", GetPais("Francia"), "Pivote"));
                AltaJugador(new Jugador(148, "6", "Mattéo Guendouzi", DateTime.Parse("1999-04-14"), 1.85, "derecho", 25000000, "EUR", GetPais("Francia"), "Mediocentro"));
                AltaJugador(new Jugador(149, "14", "Adrien Rabiot", DateTime.Parse("1995-04-03"), 1.88, "izquierdo", 17000000, "EUR", GetPais("Francia"), "Mediocentro"));
                AltaJugador(new Jugador(150, "20", "Moussa Diaby", DateTime.Parse("1999-07-07"), 1.7, "izquierdo", 60000000, "EUR", GetPais("Francia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(151, "11", "Kingsley Coman ", DateTime.Parse("1996-06-13"), 1.8, "derecho", 60000000, "EUR", GetPais("Francia"), "Extremo derecho"));
                AltaJugador(new Jugador(152, "12", "Christopher Nkunku", DateTime.Parse("1997-11-14"), 1.75, "derecho", 80000000, "EUR", GetPais("Francia"), "Mediapunta"));
                AltaJugador(new Jugador(153, "7", "Antoine Griezmann", DateTime.Parse("1991-03-21"), 1.76, "izquierdo", 35000000, "EUR", GetPais("Francia"), "Mediapunta"));
                AltaJugador(new Jugador(154, "10", "Kylian Mbappé", DateTime.Parse("1998-12-20"), 1.78, "derecho", 160000000, "EUR", GetPais("Francia"), "Delantero centro"));
                AltaJugador(new Jugador(155, "19", "Karim Benzema", DateTime.Parse("1987-12-19"), 1.85, "derecho", 30000000, "EUR", GetPais("Francia"), "Delantero centro"));
                AltaJugador(new Jugador(156, "9", "Wissam Ben Yedder", DateTime.Parse("1990-08-12"), 1.7, "ambidiestro", 25000000, "EUR", GetPais("Francia"), "Delantero centro"));
                AltaJugador(new Jugador(157, "13", "Koen Casteels", DateTime.Parse("1992-06-25"), 1.97, "izquierdo", 11000000, "EUR", GetPais("Bélgica"), "Portero"));
                AltaJugador(new Jugador(158, "1", "Matz Sels", DateTime.Parse("1992-02-26"), 1.88, "derecho", 8000000, "EUR", GetPais("Bélgica"), "Portero"));
                AltaJugador(new Jugador(159, "12", "Simon Mignolet", DateTime.Parse("1988-03-06"), 1.93, "derecho", 3500000, "EUR", GetPais("Bélgica"), "Portero"));
                AltaJugador(new Jugador(160, "3", "Arthur Theate", DateTime.Parse("2000-05-25"), 1.86, "izquierdo", 15000000, "EUR", GetPais("Bélgica"), "Defensa central"));
                AltaJugador(new Jugador(161, "NDF", "Wout Faes", DateTime.Parse("1998-04-03"), 1.87, "derecho", 10000000, "EUR", GetPais("Bélgica"), "Defensa central"));
                AltaJugador(new Jugador(162, "2", "Toby Alderweireld ", DateTime.Parse("1989-03-02"), 1.87, "derecho", 7000000, "EUR", GetPais("Bélgica"), "Defensa central"));
                AltaJugador(new Jugador(163, "4", "Brandon Mechele", DateTime.Parse("1993-01-28"), 1.9, "derecho", 6000000, "EUR", GetPais("Bélgica"), "Defensa central"));
                AltaJugador(new Jugador(164, "5", "Jan Vertonghen", DateTime.Parse("1987-04-24"), 1.89, "izquierdo", 2000000, "EUR", GetPais("Bélgica"), "Defensa central"));
                AltaJugador(new Jugador(165, "21", "Timothy Castagne", DateTime.Parse("1995-12-05"), 1.85, "derecho", 28000000, "EUR", GetPais("Bélgica"), "Lateral derecho"));
                AltaJugador(new Jugador(166, "15", "Thomas Foket", DateTime.Parse("1994-09-25"), 1.77, "derecho", 5000000, "EUR", GetPais("Bélgica"), "Lateral derecho"));
                AltaJugador(new Jugador(167, "19", "Leander Dendoncker", DateTime.Parse("1995-04-15"), 1.88, "derecho", 28000000, "EUR", GetPais("Bélgica"), "Pivote"));
                AltaJugador(new Jugador(168, "6", "Axel Witsel", DateTime.Parse("1989-01-12"), 1.86, "derecho", 4000000, "EUR", GetPais("Bélgica"), "Pivote"));
                AltaJugador(new Jugador(169, "8", "Youri Tielemans", DateTime.Parse("1997-05-07"), 1.76, "derecho", 55000000, "EUR", GetPais("Bélgica"), "Mediocentro"));
                AltaJugador(new Jugador(170, "7", "Dennis Praet", DateTime.Parse("1994-05-14"), 1.81, "derecho", 13000000, "EUR", GetPais("Bélgica"), "Mediocentro"));
                AltaJugador(new Jugador(171, "11", "Charles De Ketelaere", DateTime.Parse("2001-03-10"), 1.92, "izquierdo", 30000000, "EUR", GetPais("Bélgica"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(172, "20", "Hans Vanaken", DateTime.Parse("1992-08-24"), 1.95, "derecho", 16000000, "EUR", GetPais("Bélgica"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(173, "17", "Leandro Trossard", DateTime.Parse("1994-12-04"), 1.72, "derecho", 20000000, "EUR", GetPais("Bélgica"), "Extremo izquierdo"));
                AltaJugador(new Jugador(174, "16", "Thorgan Hazard", DateTime.Parse("1993-03-29"), 1.75, "derecho", 15000000, "EUR", GetPais("Bélgica"), "Extremo izquierdo"));
                AltaJugador(new Jugador(175, "10", "Eden Hazard", DateTime.Parse("1991-01-07"), 1.75, "derecho", 12000000, "EUR", GetPais("Bélgica"), "Extremo izquierdo"));
                AltaJugador(new Jugador(176, "22", "Alexis Saelemaekers", DateTime.Parse("1999-06-27"), 1.8, "derecho", 17000000, "EUR", GetPais("Bélgica"), "Extremo derecho"));
                AltaJugador(new Jugador(177, "18", "Adnan Januzaj", DateTime.Parse("1995-02-05"), 1.86, "izquierdo", 12000000, "EUR", GetPais("Bélgica"), "Extremo derecho"));
                AltaJugador(new Jugador(178, "23", "Michy Batshuayi", DateTime.Parse("1993-10-02"), 1.85, "derecho", 10000000, "EUR", GetPais("Bélgica"), "Delantero centro"));
                AltaJugador(new Jugador(179, "9", "Loïs Openda", DateTime.Parse("2000-02-16"), 1.75, "derecho", 7500000, "EUR", GetPais("Bélgica"), "Delantero centro"));
                AltaJugador(new Jugador(180, "14", "Dries Mertens", DateTime.Parse("1987-05-06"), 1.69, "derecho", 4000000, "EUR", GetPais("Bélgica"), "Delantero centro"));
                AltaJugador(new Jugador(181, "1", "Dominik Livakovic", DateTime.Parse("1995-01-09"), 1.88, "derecho", 8500000, "EUR", GetPais("Croacia"), "Portero"));
                AltaJugador(new Jugador(182, "23", "Ivica Ivusic", DateTime.Parse("1995-02-01"), 1.95, "derecho", 4500000, "EUR", GetPais("Croacia"), "Portero"));
                AltaJugador(new Jugador(183, "12", "Nediljko Labrovic", DateTime.Parse("1999-10-10"), 1.96, "derecho", 2000000, "EUR", GetPais("Croacia"), "Portero"));
                AltaJugador(new Jugador(184, "5", "Duje Caleta-Car", DateTime.Parse("1996-09-17"), 1.92, "derecho", 16000000, "EUR", GetPais("Croacia"), "Defensa central"));
                AltaJugador(new Jugador(185, "6", "Josip Sutalo", DateTime.Parse("2000-02-28"), 1.88, "derecho", 8000000, "EUR", GetPais("Croacia"), "Defensa central"));
                AltaJugador(new Jugador(186, "2", "Marin Pongracic", DateTime.Parse("1997-09-11"), 1.93, "derecho", 6000000, "EUR", GetPais("Croacia"), "Defensa central"));
                AltaJugador(new Jugador(187, "20", "Martin Erlic", DateTime.Parse("1998-01-24"), 1.93, "derecho", 5500000, "EUR", GetPais("Croacia"), "Defensa central"));
                AltaJugador(new Jugador(188, "21", "Domagoj Vida", DateTime.Parse("1989-04-29"), 1.84, "derecho", 1900000, "EUR", GetPais("Croacia"), "Defensa central"));
                AltaJugador(new Jugador(189, "19", "Borna Sosa", DateTime.Parse("1998-01-21"), 1.87, "izquierdo", 23000000, "EUR", GetPais("Croacia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(190, "3", "Borna Barisic", DateTime.Parse("1992-11-10"), 1.86, "izquierdo", 6000000, "EUR", GetPais("Croacia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(191, "22", "Josip Juranovic", DateTime.Parse("1995-08-16"), 1.73, "derecho", 7000000, "EUR", GetPais("Croacia"), "Lateral derecho"));
                AltaJugador(new Jugador(192, "2", "Sime Vrsaljko ", DateTime.Parse("1992-01-10"), 1.81, "derecho", 5000000, "EUR", GetPais("Croacia"), "Lateral derecho"));
                AltaJugador(new Jugador(193, "3", "Josip Stanisic", DateTime.Parse("2000-04-02"), 1.87, "ambidiestro", 4000000, "EUR", GetPais("Croacia"), "Lateral derecho"));
                AltaJugador(new Jugador(194, "11", "Marcelo Brozovic", DateTime.Parse("1992-11-16"), 1.81, "ambidiestro", 40000000, "EUR", GetPais("Croacia"), "Pivote"));
                AltaJugador(new Jugador(195, "16", "Kristijan Jakic", DateTime.Parse("1997-05-14"), 1.81, "derecho", 9000000, "EUR", GetPais("Croacia"), "Pivote"));
                AltaJugador(new Jugador(196, "8", "Mateo Kovacic", DateTime.Parse("1994-05-06"), 1.76, "derecho", 42000000, "EUR", GetPais("Croacia"), "Mediocentro"));
                AltaJugador(new Jugador(197, "14", "Luka Sucic", DateTime.Parse("2002-09-08"), 1.85, "izquierdo", 20000000, "EUR", GetPais("Croacia"), "Mediocentro"));
                AltaJugador(new Jugador(198, "10", "Luka Modric", DateTime.Parse("1985-09-09"), 1.72, "ambidiestro", 10000000, "EUR", GetPais("Croacia"), "Mediocentro"));
                AltaJugador(new Jugador(199, "15", "Mario Pasalic", DateTime.Parse("1995-02-09"), 1.88, "derecho", 28000000, "EUR", GetPais("Croacia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(200, "13", "Nikola Vlasic", DateTime.Parse("1997-10-04"), 1.78, "derecho", 22000000, "EUR", GetPais("Croacia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(201, "4", "Lovro Majer", DateTime.Parse("1998-01-17"), 1.76, "izquierdo", 20000000, "EUR", GetPais("Croacia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(202, "19", "Luka Ivanusec", DateTime.Parse("1998-11-26"), 1.75, "derecho", 12500000, "EUR", GetPais("Croacia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(203, "7", "Josip Brekalo", DateTime.Parse("1998-06-23"), 1.75, "derecho", 15000000, "EUR", GetPais("Croacia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(204, "18", "Mislav Orsic", DateTime.Parse("1992-12-29"), 1.79, "ambidiestro", 10000000, "EUR", GetPais("Croacia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(205, "9", "Andrej Kramaric", DateTime.Parse("1991-06-19"), 1.77, "derecho", 12000000, "EUR", GetPais("Croacia"), "Delantero centro"));
                AltaJugador(new Jugador(206, "NDF", "Marko Livaja", DateTime.Parse("1993-08-26"), 1.84, "derecho", 7500000, "EUR", GetPais("Croacia"), "Delantero centro"));
                AltaJugador(new Jugador(207, "17", "Ante Budimir", DateTime.Parse("1991-07-22"), 1.9, "izquierdo", 6000000, "EUR", GetPais("Croacia"), "Delantero centro"));
                AltaJugador(new Jugador(208, "NDF", "Petar Musa", DateTime.Parse("1998-03-04"), 1.9, "derecho", 5000000, "EUR", GetPais("Croacia"), "Delantero centro"));
                AltaJugador(new Jugador(209, "23", "Unai Simón ", DateTime.Parse("1997-06-11"), 1.9, "derecho", 25000000, "EUR", GetPais("España"), "Portero"));
                AltaJugador(new Jugador(210, "13", "David Raya", DateTime.Parse("1995-09-15"), 1.83, "derecho", 22000000, "EUR", GetPais("España"), "Portero"));
                AltaJugador(new Jugador(211, "1", "Robert Sánchez", DateTime.Parse("1997-11-18"), 1.97, "derecho", 16000000, "EUR", GetPais("España"), "Portero"));
                AltaJugador(new Jugador(212, "4", "Pau Torres", DateTime.Parse("1997-01-16"), 1.91, "izquierdo", 50000000, "EUR", GetPais("España"), "Defensa central"));
                AltaJugador(new Jugador(213, "3", "Iñigo Martínez", DateTime.Parse("1991-05-17"), 1.82, "izquierdo", 25000000, "EUR", GetPais("España"), "Defensa central"));
                AltaJugador(new Jugador(214, "14", "Eric García", DateTime.Parse("2001-01-09"), 1.82, "derecho", 18000000, "EUR", GetPais("España"), "Defensa central"));
                AltaJugador(new Jugador(215, "15", "Diego Llorente", DateTime.Parse("1993-08-16"), 1.86, "derecho", 18000000, "EUR", GetPais("España"), "Defensa central"));
                AltaJugador(new Jugador(216, "17", "Marcos Alonso", DateTime.Parse("1990-12-28"), 1.89, "izquierdo", 12000000, "EUR", GetPais("España"), "Lateral izquierdo"));
                AltaJugador(new Jugador(217, "18", "Jordi Alba", DateTime.Parse("1989-03-21"), 1.7, "izquierdo", 9000000, "EUR", GetPais("España"), "Lateral izquierdo"));
                AltaJugador(new Jugador(218, "20", "Daniel Carvajal", DateTime.Parse("1992-01-11"), 1.73, "derecho", 18000000, "EUR", GetPais("España"), "Lateral derecho"));
                AltaJugador(new Jugador(219, "2", "César Azpilicueta", DateTime.Parse("1989-08-28"), 1.78, "derecho", 9000000, "EUR", GetPais("España"), "Lateral derecho"));
                AltaJugador(new Jugador(220, "16", "Rodri", DateTime.Parse("1996-06-22"), 1.91, "derecho", 80000000, "EUR", GetPais("España"), "Pivote"));
                AltaJugador(new Jugador(221, "5", "Sergio Busquets", DateTime.Parse("1988-07-16"), 1.89, "derecho", 9000000, "EUR", GetPais("España"), "Pivote"));
                AltaJugador(new Jugador(222, "9", "Gavi", DateTime.Parse("2004-08-05"), 1.73, "derecho", 60000000, "EUR", GetPais("España"), "Mediocentro"));
                AltaJugador(new Jugador(223, "19", "Carlos Soler", DateTime.Parse("1997-01-02"), 1.8, "derecho", 50000000, "EUR", GetPais("España"), "Mediocentro"));
                AltaJugador(new Jugador(224, "6", "Marcos Llorente", DateTime.Parse("1995-01-30"), 1.84, "derecho", 45000000, "EUR", GetPais("España"), "Mediocentro"));
                AltaJugador(new Jugador(225, "8", "Koke", DateTime.Parse("1992-01-08"), 1.76, "derecho", 25000000, "EUR", GetPais("España"), "Mediocentro"));
                AltaJugador(new Jugador(226, "21", "Dani Olmo", DateTime.Parse("1998-05-07"), 1.79, "derecho", 40000000, "EUR", GetPais("España"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(227, "12", "Ansu Fati", DateTime.Parse("2002-10-31"), 1.78, "derecho", 60000000, "EUR", GetPais("España"), "Extremo izquierdo"));
                AltaJugador(new Jugador(228, "11", "Ferran Torres", DateTime.Parse("2000-02-29"), 1.84, "derecho", 45000000, "EUR", GetPais("España"), "Extremo izquierdo"));
                AltaJugador(new Jugador(229, "10", "Marco Asensio", DateTime.Parse("1996-01-21"), 1.82, "izquierdo", 40000000, "EUR", GetPais("España"), "Extremo derecho"));
                AltaJugador(new Jugador(230, "22", "Pablo Sarabia", DateTime.Parse("1992-05-11"), 1.74, "izquierdo", 25000000, "EUR", GetPais("España"), "Extremo derecho"));
                AltaJugador(new Jugador(231, "7", "Álvaro Morata", DateTime.Parse("1992-10-23"), 1.87, "derecho", 25000000, "EUR", GetPais("España"), "Delantero centro"));
                AltaJugador(new Jugador(232, "14", "Raúl de Tomás", DateTime.Parse("1994-10-17"), 1.8, "derecho", 25000000, "EUR", GetPais("España"), "Delantero centro"));
                AltaJugador(new Jugador(233, "12", "Predrag Rajković ", DateTime.Parse("1995-10-31"), 1.92, "derecho", 9000000, "EUR", GetPais("Serbia"), "Portero"));
                AltaJugador(new Jugador(234, "1", "Marko Dmitrović", DateTime.Parse("1992-01-24"), 1.94, "izquierdo", 5000000, "EUR", GetPais("Serbia"), "Portero"));
                AltaJugador(new Jugador(235, "23", "Vanja Milinković-Savić", DateTime.Parse("1997-02-20"), 2.02, "derecho", 2500000, "EUR", GetPais("Serbia"), "Portero"));
                AltaJugador(new Jugador(236, "4", "Nikola Milenković", DateTime.Parse("1997-10-12"), 1.95, "derecho", 20000000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(237, "2", "Strahinja Pavlovic", DateTime.Parse("2001-05-24"), 1.94, "izquierdo", 5000000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(238, "5", "Strahinja Erakovic", DateTime.Parse("2001-01-22"), 1.86, "derecho", 5000000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(239, "15", "Milos Veljkovic", DateTime.Parse("1995-09-26"), 1.88, "derecho", 4000000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(240, "3", "Erhan Masovic", DateTime.Parse("1998-11-22"), 1.89, "ambidiestro", 2500000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(241, "13", "Stefan Mitrović", DateTime.Parse("1990-05-22"), 1.89, "ambidiestro", 1800000, "EUR", GetPais("Serbia"), "Defensa central"));
                AltaJugador(new Jugador(242, "19", "Mihailo Ristic", DateTime.Parse("1995-10-31"), 1.8, "izquierdo", 3500000, "EUR", GetPais("Serbia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(243, "14", "Aleksa Terzić", DateTime.Parse("1999-08-17"), 1.84, "izquierdo", 1500000, "EUR", GetPais("Serbia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(244, "3", "Filip Mladenovic", DateTime.Parse("1991-08-15"), 1.8, "izquierdo", 1200000, "EUR", GetPais("Serbia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(245, "6", "Nemanja Maksimović", DateTime.Parse("1995-01-26"), 1.89, "derecho", 14000000, "EUR", GetPais("Serbia"), "Pivote"));
                AltaJugador(new Jugador(246, "18", "Uroš Račić", DateTime.Parse("1998-03-17"), 1.93, "ambidiestro", 7000000, "EUR", GetPais("Serbia"), "Pivote"));
                AltaJugador(new Jugador(247, "8", "Nemanja Gudelj", DateTime.Parse("1991-11-16"), 1.87, "derecho", 4000000, "EUR", GetPais("Serbia"), "Pivote"));
                AltaJugador(new Jugador(248, "20", "Sergej Milinković-Savić", DateTime.Parse("1995-02-27"), 1.91, "derecho", 70000000, "EUR", GetPais("Serbia"), "Mediocentro"));
                AltaJugador(new Jugador(249, "16", "Saša Lukić", DateTime.Parse("1996-08-13"), 1.83, "derecho", 15000000, "EUR", GetPais("Serbia"), "Mediocentro"));
                AltaJugador(new Jugador(250, "6", "Ivan Ilić", DateTime.Parse("2001-03-17"), 1.85, "izquierdo", 14000000, "EUR", GetPais("Serbia"), "Mediocentro"));
                AltaJugador(new Jugador(251, "22", "Marko Grujic", DateTime.Parse("1996-04-13"), 1.91, "derecho", 12000000, "EUR", GetPais("Serbia"), "Mediocentro"));
                AltaJugador(new Jugador(252, "17", "Filip Kostic", DateTime.Parse("1992-11-01"), 1.84, "izquierdo", 24000000, "EUR", GetPais("Serbia"), "Interior izquierdo"));
                AltaJugador(new Jugador(253, "22", "Darko Lazović", DateTime.Parse("1990-09-15"), 1.81, "derecho", 4000000, "EUR", GetPais("Serbia"), "Interior izquierdo"));
                AltaJugador(new Jugador(254, "21", "Filip Djuricic", DateTime.Parse("1992-01-30"), 1.81, "derecho", 4000000, "EUR", GetPais("Serbia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(255, "10", "Dusan Tadić", DateTime.Parse("1988-11-20"), 1.81, "izquierdo", 12000000, "EUR", GetPais("Serbia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(256, "7", "Nemanja Radonjić", DateTime.Parse("1996-02-15"), 1.85, "ambidiestro", 4000000, "EUR", GetPais("Serbia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(257, "14", "Andrija Zivkovic", DateTime.Parse("1996-07-11"), 1.69, "izquierdo", 6000000, "EUR", GetPais("Serbia"), "Extremo derecho"));
                AltaJugador(new Jugador(258, "18", "Dušan Vlahović", DateTime.Parse("2000-01-28"), 1.9, "izquierdo", 85000000, "EUR", GetPais("Serbia"), "Delantero centro"));
                AltaJugador(new Jugador(259, "9", "Aleksandar Mitrović", DateTime.Parse("1994-09-16"), 1.89, "derecho", 20000000, "EUR", GetPais("Serbia"), "Delantero centro"));
                AltaJugador(new Jugador(260, "11", "Luka Jović", DateTime.Parse("1997-12-23"), 1.82, "ambidiestro", 16000000, "EUR", GetPais("Serbia"), "Delantero centro"));
                AltaJugador(new Jugador(261, "9", "Djordje Jovanovic", DateTime.Parse("1999-02-15"), 1.87, "derecho", 2500000, "EUR", GetPais("Serbia"), "Delantero centro"));
                AltaJugador(new Jugador(262, "NDF", "Aaron Ramsdale", DateTime.Parse("1998-05-14"), 1.9, "derecho", 28000000, "EUR", GetPais("Inglaterra"), "Portero"));
                AltaJugador(new Jugador(263, "NDF", "Jordan Pickford", DateTime.Parse("1994-03-07"), 1.85, "izquierdo", 20000000, "EUR", GetPais("Inglaterra"), "Portero"));
                AltaJugador(new Jugador(264, "NDF", "Nick Pope", DateTime.Parse("1992-04-19"), 1.98, "derecho", 15000000, "EUR", GetPais("Inglaterra"), "Portero"));
                AltaJugador(new Jugador(265, "NDF", "Fikayo Tomori", DateTime.Parse("1997-12-19"), 1.85, "derecho", 50000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(266, "NDF", "Ben White", DateTime.Parse("1997-10-08"), 1.86, "derecho", 40000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(267, "NDF", "Harry Maguire", DateTime.Parse("1993-03-05"), 1.94, "derecho", 38000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(268, "NDF", "Marc Guéhi", DateTime.Parse("2000-07-13"), 1.82, "derecho", 32000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(269, "NDF", "John Stones ", DateTime.Parse("1994-05-28"), 1.88, "derecho", 28000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(270, "NDF", "Conor Coady", DateTime.Parse("1993-02-25"), 1.85, "derecho", 25000000, "EUR", GetPais("Inglaterra"), "Defensa central"));
                AltaJugador(new Jugador(271, "NDF", "Trent Alexander-Arnold", DateTime.Parse("1998-10-07"), 1.8, "derecho", 80000000, "EUR", GetPais("Inglaterra"), "Lateral derecho"));
                AltaJugador(new Jugador(272, "NDF", "Reece James", DateTime.Parse("1999-12-08"), 1.8, "derecho", 60000000, "EUR", GetPais("Inglaterra"), "Lateral derecho"));
                AltaJugador(new Jugador(273, "NDF", "James Justin", DateTime.Parse("1998-02-23"), 1.83, "derecho", 25000000, "EUR", GetPais("Inglaterra"), "Lateral derecho"));
                AltaJugador(new Jugador(274, "NDF", "Kyle Walker", DateTime.Parse("1990-05-28"), 1.83, "derecho", 18000000, "EUR", GetPais("Inglaterra"), "Lateral derecho"));
                AltaJugador(new Jugador(275, "NDF", "Kieran Trippier", DateTime.Parse("1990-09-19"), 1.73, "derecho", 15000000, "EUR", GetPais("Inglaterra"), "Lateral derecho"));
                AltaJugador(new Jugador(276, "NDF", "Declan Rice ", DateTime.Parse("1999-01-14"), 1.88, "derecho", 80000000, "EUR", GetPais("Inglaterra"), "Pivote"));
                AltaJugador(new Jugador(277, "NDF", "Kalvin Phillips", DateTime.Parse("1995-12-02"), 1.79, "derecho", 50000000, "EUR", GetPais("Inglaterra"), "Pivote"));
                AltaJugador(new Jugador(278, "NDF", "Phil Foden", DateTime.Parse("2000-05-28"), 1.71, "izquierdo", 90000000, "EUR", GetPais("Inglaterra"), "Mediocentro"));
                AltaJugador(new Jugador(279, "NDF", "Jude Bellingham", DateTime.Parse("2003-06-29"), 1.86, "derecho", 80000000, "EUR", GetPais("Inglaterra"), "Mediocentro"));
                AltaJugador(new Jugador(280, "NDF", "James Ward-Prowse", DateTime.Parse("1994-11-01"), 1.77, "derecho", 32000000, "EUR", GetPais("Inglaterra"), "Mediocentro"));
                AltaJugador(new Jugador(281, "NDF", "Conor Gallagher", DateTime.Parse("2000-02-06"), 1.82, "derecho", 25000000, "EUR", GetPais("Inglaterra"), "Mediocentro"));
                AltaJugador(new Jugador(282, "NDF", "Bukayo Saka", DateTime.Parse("2001-09-05"), 1.78, "izquierdo", 65000000, "EUR", GetPais("Inglaterra"), "Interior derecho"));
                AltaJugador(new Jugador(283, "NDF", "Mason Mount", DateTime.Parse("1999-01-10"), 1.8, "derecho", 75000000, "EUR", GetPais("Inglaterra"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(284, "NDF", "Raheem Sterling", DateTime.Parse("1994-12-08"), 1.7, "derecho", 70000000, "EUR", GetPais("Inglaterra"), "Extremo izquierdo"));
                AltaJugador(new Jugador(285, "NDF", "Jack Grealish", DateTime.Parse("1995-09-10"), 1.8, "derecho", 70000000, "EUR", GetPais("Inglaterra"), "Extremo izquierdo"));
                AltaJugador(new Jugador(286, "NDF", "Jarrod Bowen", DateTime.Parse("1996-12-20"), 1.82, "izquierdo", 42000000, "EUR", GetPais("Inglaterra"), "Extremo derecho"));
                AltaJugador(new Jugador(287, "NDF", "Harry Kane", DateTime.Parse("1993-07-28"), 1.88, "derecho", 90000000, "EUR", GetPais("Inglaterra"), "Delantero centro"));
                AltaJugador(new Jugador(288, "NDF", "Tammy Abraham", DateTime.Parse("1997-10-02"), 1.94, "derecho", 50000000, "EUR", GetPais("Inglaterra"), "Delantero centro"));
                AltaJugador(new Jugador(289, "21", "Gregor Kobel", DateTime.Parse("1997-12-06"), 1.94, "derecho", 22000000, "EUR", GetPais("Suiza"), "Portero"));
                AltaJugador(new Jugador(290, "12", "Jonas Omlin", DateTime.Parse("1994-01-10"), 1.9, "derecho", 8000000, "EUR", GetPais("Suiza"), "Portero"));
                AltaJugador(new Jugador(291, "1", "Yann Sommer", DateTime.Parse("1988-12-17"), 1.83, "derecho", 5000000, "EUR", GetPais("Suiza"), "Portero"));
                AltaJugador(new Jugador(292, "12", "Yvon Mvogo", DateTime.Parse("1994-06-06"), 1.89, "derecho", 2800000, "EUR", GetPais("Suiza"), "Portero"));
                AltaJugador(new Jugador(293, "5", "Manuel Akanji", DateTime.Parse("1995-07-19"), 1.88, "derecho", 30000000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(294, "4", "Nico Elvedi", DateTime.Parse("1996-09-30"), 1.89, "derecho", 18000000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(295, "22", "Fabian Schär", DateTime.Parse("1991-12-20"), 1.86, "derecho", 7000000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(296, "NDF", "Leonidas Stergiou", DateTime.Parse("2002-03-03"), 1.81, "derecho", 6000000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(297, "13", "Ricardo Rodríguez", DateTime.Parse("1992-08-25"), 1.8, "izquierdo", 3400000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(298, "18", "Eray Cömert", DateTime.Parse("1998-02-04"), 1.83, "derecho", 1500000, "EUR", GetPais("Suiza"), "Defensa central"));
                AltaJugador(new Jugador(299, "2", "Kevin Mbabu", DateTime.Parse("1995-04-19"), 1.84, "derecho", 9000000, "EUR", GetPais("Suiza"), "Lateral derecho"));
                AltaJugador(new Jugador(300, "16", "Jordan Lotomba", DateTime.Parse("1998-09-29"), 1.77, "derecho", 7000000, "EUR", GetPais("Suiza"), "Lateral derecho"));
                AltaJugador(new Jugador(301, "3", "Silvan Widmer", DateTime.Parse("1993-03-05"), 1.83, "derecho", 5000000, "EUR", GetPais("Suiza"), "Lateral derecho"));
                AltaJugador(new Jugador(302, "10", "Granit Xhaka", DateTime.Parse("1992-09-27"), 1.86, "izquierdo", 20000000, "EUR", GetPais("Suiza"), "Pivote"));
                AltaJugador(new Jugador(303, "6", "Fabian Frei", DateTime.Parse("1989-01-08"), 1.83, "derecho", 500000, "EUR", GetPais("Suiza"), "Pivote"));
                AltaJugador(new Jugador(304, "15", "Djibril Sow", DateTime.Parse("1997-02-06"), 1.84, "derecho", 22000000, "EUR", GetPais("Suiza"), "Mediocentro"));
                AltaJugador(new Jugador(305, "8", "Remo Freuler", DateTime.Parse("1992-04-15"), 1.8, "derecho", 20000000, "EUR", GetPais("Suiza"), "Mediocentro"));
                AltaJugador(new Jugador(306, "20", "Michel Aebischer", DateTime.Parse("1997-01-06"), 1.83, "derecho", 3500000, "EUR", GetPais("Suiza"), "Mediocentro"));
                AltaJugador(new Jugador(307, "14", "Mattia Bottani", DateTime.Parse("1991-05-24"), 1.7, "derecho", 500000, "EUR", GetPais("Suiza"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(308, "17", "Rubén Vargas ", DateTime.Parse("1998-08-05"), 1.79, "derecho", 8000000, "EUR", GetPais("Suiza"), "Extremo izquierdo"));
                AltaJugador(new Jugador(309, "14", "Steven Zuber", DateTime.Parse("1991-08-17"), 1.82, "derecho", 2500000, "EUR", GetPais("Suiza"), "Extremo izquierdo"));
                AltaJugador(new Jugador(310, "11", "Renato Steffen", DateTime.Parse("1991-11-03"), 1.7, "izquierdo", 2000000, "EUR", GetPais("Suiza"), "Extremo izquierdo"));
                AltaJugador(new Jugador(311, "23", "Xherdan Shaqiri", DateTime.Parse("1991-10-10"), 1.69, "izquierdo", 7000000, "EUR", GetPais("Suiza"), "Extremo derecho"));
                AltaJugador(new Jugador(312, "5", "Noah Okafor", DateTime.Parse("2000-05-24"), 1.85, "derecho", 18000000, "EUR", GetPais("Suiza"), "Delantero centro"));
                AltaJugador(new Jugador(313, "7", "Breel Embolo", DateTime.Parse("1997-02-14"), 1.87, "derecho", 15000000, "EUR", GetPais("Suiza"), "Delantero centro"));
                AltaJugador(new Jugador(314, "9", "Haris Seferovic", DateTime.Parse("1992-02-22"), 1.89, "izquierdo", 8000000, "EUR", GetPais("Suiza"), "Delantero centro"));
                AltaJugador(new Jugador(315, "NDF", "Zeki Amdouni", DateTime.Parse("2000-12-04"), 1.85, "NDF", 2000000, "EUR", GetPais("Suiza"), "Delantero centro"));
                AltaJugador(new Jugador(316, "19", "Mario Gavranovic", DateTime.Parse("1989-11-24"), 1.75, "derecho", 1500000, "EUR", GetPais("Suiza"), "Delantero centro"));
                AltaJugador(new Jugador(317, "13", "Mark Flekken", DateTime.Parse("1993-06-13"), 1.94, "ambidiestro", 7000000, "EUR", GetPais("Países Bajos"), "Portero"));
                AltaJugador(new Jugador(318, "1", "Jasper Cillessen", DateTime.Parse("1989-04-22"), 1.87, "derecho", 3000000, "EUR", GetPais("Países Bajos"), "Portero"));
                AltaJugador(new Jugador(319, "23", "Kjell Scherpen", DateTime.Parse("2000-01-23"), 2.04, "derecho", 2200000, "EUR", GetPais("Países Bajos"), "Portero"));
                AltaJugador(new Jugador(320, "3", "Matthijs de Ligt", DateTime.Parse("1999-08-12"), 1.89, "derecho", 70000000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(321, "NDF", "Jurrien Timber", DateTime.Parse("2001-06-17"), 1.82, "derecho", 35000000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(322, "6", "Stefan de Vrij", DateTime.Parse("1992-02-05"), 1.89, "derecho", 28000000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(323, "NDF", "Nathan Aké", DateTime.Parse("1995-02-18"), 1.8, "izquierdo", 25000000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(324, "2", "Jordan Teze", DateTime.Parse("1999-09-30"), 1.83, "derecho", 8000000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(325, "4", "Bruno Martins Indi", DateTime.Parse("1992-02-08"), 1.85, "izquierdo", 2200000, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(326, "NDF", "Ernest Faber", DateTime.Parse("1971-08-27"), 1.84, "derecho", 0, "EUR", GetPais("Países Bajos"), "Defensa central"));
                AltaJugador(new Jugador(327, "5", "Tyrell Malacia", DateTime.Parse("1999-08-17"), 1.69, "izquierdo", 17000000, "EUR", GetPais("Países Bajos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(328, "17", "Daley Blind", DateTime.Parse("1990-03-09"), 1.8, "izquierdo", 9000000, "EUR", GetPais("Países Bajos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(329, "15", "Hans Hateboer", DateTime.Parse("1994-01-09"), 1.85, "derecho", 13000000, "EUR", GetPais("Países Bajos"), "Lateral derecho"));
                AltaJugador(new Jugador(330, "20", "Teun Koopmeiners", DateTime.Parse("1998-02-28"), 1.83, "izquierdo", 25000000, "EUR", GetPais("Países Bajos"), "Pivote"));
                AltaJugador(new Jugador(331, "18", "Jerdy Schouten", DateTime.Parse("1997-01-12"), 1.85, "derecho", 9000000, "EUR", GetPais("Países Bajos"), "Pivote"));
                AltaJugador(new Jugador(332, "21", "Frenkie de Jong", DateTime.Parse("1997-05-12"), 1.8, "derecho", 60000000, "EUR", GetPais("Países Bajos"), "Mediocentro"));
                AltaJugador(new Jugador(333, "22", "Denzel Dumfries", DateTime.Parse("1996-04-18"), 1.88, "derecho", 25000000, "EUR", GetPais("Países Bajos"), "Interior derecho"));
                AltaJugador(new Jugador(334, "11", "Steven Berghuis", DateTime.Parse("1991-12-19"), 1.82, "izquierdo", 14000000, "EUR", GetPais("Países Bajos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(335, "14", "Davy Klaassen", DateTime.Parse("1993-02-21"), 1.79, "derecho", 14000000, "EUR", GetPais("Países Bajos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(336, "8", "Guus Til", DateTime.Parse("1997-12-22"), 1.86, "derecho", 7500000, "EUR", GetPais("Países Bajos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(337, "9", "Cody Gakpo", DateTime.Parse("1999-05-07"), 1.89, "derecho", 30000000, "EUR", GetPais("Países Bajos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(338, "12", "Noa Lang", DateTime.Parse("1999-06-17"), 1.73, "derecho", 22000000, "EUR", GetPais("Países Bajos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(339, "7", "Steven Bergwijn", DateTime.Parse("1997-10-08"), 1.78, "derecho", 18000000, "EUR", GetPais("Países Bajos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(340, "10", "Memphis Depay", DateTime.Parse("1994-02-13"), 1.76, "derecho", 35000000, "EUR", GetPais("Países Bajos"), "Delantero centro"));
                AltaJugador(new Jugador(341, "19", "Wout Weghorst", DateTime.Parse("1992-08-07"), 1.97, "derecho", 14000000, "EUR", GetPais("Países Bajos"), "Delantero centro"));
                AltaJugador(new Jugador(342, "16", "Vincent Janssen", DateTime.Parse("1994-06-15"), 1.84, "izquierdo", 2500000, "EUR", GetPais("Países Bajos"), "Delantero centro"));
                AltaJugador(new Jugador(343, "22", "Amir Abedzadeh", DateTime.Parse("1993-04-26"), 1.87, "derecho", 1500000, "EUR", GetPais("Irán"), "Portero"));
                AltaJugador(new Jugador(344, "1", "Alireza Beiranvand", DateTime.Parse("1992-09-21"), 1.94, "derecho", 1000000, "EUR", GetPais("Irán"), "Portero"));
                AltaJugador(new Jugador(345, "30", "Payam Niazmand", DateTime.Parse("1995-04-06"), 1.94, "derecho", 500000, "EUR", GetPais("Irán"), "Portero"));
                AltaJugador(new Jugador(346, "12", "Hossein Hosseini", DateTime.Parse("1992-06-30"), 1.89, "derecho", 450000, "EUR", GetPais("Irán"), "Portero"));
                AltaJugador(new Jugador(347, "19", "Majid Hosseini", DateTime.Parse("1996-06-20"), 1.87, "derecho", 3000000, "EUR", GetPais("Irán"), "Defensa central"));
                AltaJugador(new Jugador(348, "4", "Aref Gholami", DateTime.Parse("1997-04-19"), 1.83, "derecho", 600000, "EUR", GetPais("Irán"), "Defensa central"));
                AltaJugador(new Jugador(349, "29", "Armin Sohrabian", DateTime.Parse("1995-07-26"), 1.81, "izquierdo", 550000, "EUR", GetPais("Irán"), "Defensa central"));
                AltaJugador(new Jugador(350, "13", "Aref Aghasi", DateTime.Parse("1997-01-02"), 1.84, "izquierdo", 450000, "EUR", GetPais("Irán"), "Defensa central"));
                AltaJugador(new Jugador(351, "21", "Omid Noorafkan", DateTime.Parse("1997-04-09"), 1.82, "izquierdo", 1200000, "EUR", GetPais("Irán"), "Lateral izquierdo"));
                AltaJugador(new Jugador(352, "3", "Ehsan Hajsafi", DateTime.Parse("1990-02-25"), 1.78, "izquierdo", 800000, "EUR", GetPais("Irán"), "Lateral izquierdo"));
                AltaJugador(new Jugador(353, "16", "Abolfazl Jalali", DateTime.Parse("1998-06-26"), 1.77, "izquierdo", 250000, "EUR", GetPais("Irán"), "Lateral izquierdo"));
                AltaJugador(new Jugador(354, "2", "Sadegh Moharrami", DateTime.Parse("1996-03-01"), 1.74, "derecho", 1500000, "EUR", GetPais("Irán"), "Lateral derecho"));
                AltaJugador(new Jugador(355, "5", "Saleh Hardani", DateTime.Parse("1998-09-14"), 1.76, "derecho", 700000, "EUR", GetPais("Irán"), "Lateral derecho"));
                AltaJugador(new Jugador(356, "26", "Mehdi Shiri", DateTime.Parse("1991-01-31"), 1.76, "derecho", 250000, "EUR", GetPais("Irán"), "Lateral derecho"));
                AltaJugador(new Jugador(357, "18", "Milad Sarlak", DateTime.Parse("1995-03-26"), 1.81, "derecho", 900000, "EUR", GetPais("Irán"), "Pivote"));
                AltaJugador(new Jugador(358, "6", "Saeid Ezatolahi", DateTime.Parse("1996-10-01"), 1.9, "derecho", 800000, "EUR", GetPais("Irán"), "Pivote"));
                AltaJugador(new Jugador(359, "14", "Omid Ebrahimi", DateTime.Parse("1987-09-16"), 1.78, "derecho", 325000, "EUR", GetPais("Irán"), "Pivote"));
                AltaJugador(new Jugador(360, "8", "Ahmad Nourollahi", DateTime.Parse("1993-02-01"), 1.85, "derecho", 1500000, "EUR", GetPais("Irán"), "Mediocentro"));
                AltaJugador(new Jugador(361, "23", "Mehdi Mehdipour", DateTime.Parse("1994-10-25"), 1.82, "derecho", 775000, "EUR", GetPais("Irán"), "Mediocentro"));
                AltaJugador(new Jugador(362, "28", "Seyed Mehdi Hosseini", DateTime.Parse("1993-09-16"), 1.72, "derecho", 350000, "EUR", GetPais("Irán"), "Mediocentro"));
                AltaJugador(new Jugador(363, "15", "Amirhossein Hosseinzadeh", DateTime.Parse("2000-10-30"), 1.78, "derecho", 850000, "EUR", GetPais("Irán"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(364, "24", "Soroosh Rafiei", DateTime.Parse("1990-03-24"), 1.75, "derecho", 500000, "EUR", GetPais("Irán"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(365, "27", "Mehdi Torabi", DateTime.Parse("1994-09-10"), 1.85, "derecho", 1000000, "EUR", GetPais("Irán"), "Extremo izquierdo"));
                AltaJugador(new Jugador(366, "7", "Alireza Jahanbakhsh", DateTime.Parse("1993-08-11"), 0, "NDF", 3500000, "EUR", GetPais("Irán"), "Extremo derecho"));
                AltaJugador(new Jugador(367, "17", "Ali Gholizadeh", DateTime.Parse("1996-03-10"), 1.76, "izquierdo", 3000000, "EUR", GetPais("Irán"), "Extremo derecho"));
                AltaJugador(new Jugador(368, "25", "Saeid Sadeghi", DateTime.Parse("1994-04-25"), 1.8, "derecho", 900000, "EUR", GetPais("Irán"), "Extremo derecho"));
                AltaJugador(new Jugador(369, "20", "Sardar Azmoun", DateTime.Parse("1995-01-01"), 1.86, "ambidiestro", 22000000, "EUR", GetPais("Irán"), "Delantero centro"));
                AltaJugador(new Jugador(370, "9", "Mehdi Taremi", DateTime.Parse("1992-07-18"), 1.87, "derecho", 20000000, "EUR", GetPais("Irán"), "Delantero centro"));
                AltaJugador(new Jugador(371, "10", "Allahyar Sayyadmanesh", DateTime.Parse("2001-06-29"), 1.82, "derecho", 2500000, "EUR", GetPais("Irán"), "Delantero centro"));
                AltaJugador(new Jugador(372, "11", "Ali Alipour", DateTime.Parse("1995-11-11"), 1.81, "ambidiestro", 1500000, "EUR", GetPais("Irán"), "Delantero centro"));
                AltaJugador(new Jugador(373, "21", "Hyeon-woo Jo", DateTime.Parse("1991-09-25"), 1.89, "derecho", 1400000, "EUR", GetPais("Corea del Sur"), "Portero"));
                AltaJugador(new Jugador(374, "12", "Bum-keun Song", DateTime.Parse("1997-10-15"), 1.94, "NDF", 950000, "EUR", GetPais("Corea del Sur"), "Portero"));
                AltaJugador(new Jugador(375, "1", "Dong-jun Kim", DateTime.Parse("1994-12-19"), 1.89, "NDF", 550000, "EUR", GetPais("Corea del Sur"), "Portero"));
                AltaJugador(new Jugador(376, "18", "Ji-soo Park", DateTime.Parse("1994-06-13"), 1.87, "derecho", 750000, "EUR", GetPais("Corea del Sur"), "Defensa central"));
                AltaJugador(new Jugador(377, "20", "Kyung-won Kwon", DateTime.Parse("1992-01-31"), 1.89, "izquierdo", 750000, "EUR", GetPais("Corea del Sur"), "Defensa central"));
                AltaJugador(new Jugador(378, "4", "Yu-min Cho", DateTime.Parse("1996-11-17"), 1.84, "derecho", 625000, "EUR", GetPais("Corea del Sur"), "Defensa central"));
                AltaJugador(new Jugador(379, "19", "Young-gwon Kim", DateTime.Parse("1990-02-27"), 1.86, "izquierdo", 600000, "EUR", GetPais("Corea del Sur"), "Defensa central"));
                AltaJugador(new Jugador(380, "24", "Ju-sung Kim ", DateTime.Parse("2000-12-12"), 1.88, "izquierdo", 500000, "EUR", GetPais("Corea del Sur"), "Defensa central"));
                AltaJugador(new Jugador(381, "3", "Jin-su Kim", DateTime.Parse("1992-06-13"), 1.77, "izquierdo", 600000, "EUR", GetPais("Corea del Sur"), "Lateral izquierdo"));
                AltaJugador(new Jugador(382, "14", "Chul Hong", DateTime.Parse("1990-09-17"), 1.76, "izquierdo", 525000, "EUR", GetPais("Corea del Sur"), "Lateral izquierdo"));
                AltaJugador(new Jugador(383, "15", "Moon-hwan Kim", DateTime.Parse("1995-08-01"), 1.73, "derecho", 950000, "EUR", GetPais("Corea del Sur"), "Lateral derecho"));
                AltaJugador(new Jugador(384, "2", "Jong-gyu Yoon", DateTime.Parse("1998-03-20"), 1.73, "derecho", 600000, "EUR", GetPais("Corea del Sur"), "Lateral derecho"));
                AltaJugador(new Jugador(385, "16", "Dong-hyeon Kim", DateTime.Parse("1997-06-11"), 1.82, "derecho", 700000, "EUR", GetPais("Corea del Sur"), "Pivote"));
                AltaJugador(new Jugador(386, "6", "In-beom Hwang ", DateTime.Parse("1996-09-20"), 1.77, "NDF", 3000000, "EUR", GetPais("Corea del Sur"), "Mediocentro"));
                AltaJugador(new Jugador(387, "8", "Seung-ho Paik", DateTime.Parse("1997-03-17"), 1.82, "NDF", 925000, "EUR", GetPais("Corea del Sur"), "Mediocentro"));
                AltaJugador(new Jugador(388, "5", "Yeong-jae Lee", DateTime.Parse("1994-09-13"), 1.74, "izquierdo", 850000, "EUR", GetPais("Corea del Sur"), "Mediocentro"));
                AltaJugador(new Jugador(389, "10", "Jin-gyu Kim", DateTime.Parse("1997-02-24"), 1.76, "derecho", 600000, "EUR", GetPais("Corea del Sur"), "Mediocentro"));
                AltaJugador(new Jugador(390, "25", "Gi-hyuk Lee", DateTime.Parse("2000-07-07"), 1.84, "NDF", 200000, "EUR", GetPais("Corea del Sur"), "Mediocentro"));
                AltaJugador(new Jugador(391, "23", "Young-jun Goh", DateTime.Parse("2001-07-09"), 1.68, "derecho", 500000, "EUR", GetPais("Corea del Sur"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(392, "13", "Min-kyu Song", DateTime.Parse("1999-09-12"), 1.81, "derecho", 1300000, "EUR", GetPais("Corea del Sur"), "Extremo izquierdo"));
                AltaJugador(new Jugador(393, "7", "Sang-ho Na", DateTime.Parse("1996-08-12"), 1.73, "derecho", 900000, "EUR", GetPais("Corea del Sur"), "Extremo izquierdo"));
                AltaJugador(new Jugador(394, "22", "Chang-hoon Kwon", DateTime.Parse("1994-06-30"), 1.73, "izquierdo", 1500000, "EUR", GetPais("Corea del Sur"), "Extremo derecho"));
                AltaJugador(new Jugador(395, "11", "Won-sang Um", DateTime.Parse("1999-01-06"), 1.71, "derecho", 1000000, "EUR", GetPais("Corea del Sur"), "Extremo derecho"));
                AltaJugador(new Jugador(396, "17", "Young-wook Cho", DateTime.Parse("1999-02-05"), 1.78, "derecho", 700000, "EUR", GetPais("Corea del Sur"), "Extremo derecho"));
                AltaJugador(new Jugador(397, "26", "Seong-jin Kang", DateTime.Parse("2003-03-26"), 1.8, "derecho", 425000, "EUR", GetPais("Corea del Sur"), "Extremo derecho"));
                AltaJugador(new Jugador(398, "9", "Gue-sung Cho ", DateTime.Parse("1998-01-25"), 1.88, "derecho", 800000, "EUR", GetPais("Corea del Sur"), "Delantero centro"));
                AltaJugador(new Jugador(399, "1", "Keisuke Osako", DateTime.Parse("1999-07-28"), 1.87, "derecho", 850000, "EUR", GetPais("Japón"), "Portero"));
                AltaJugador(new Jugador(400, "12", "Kosei Tani", DateTime.Parse("2000-11-22"), 1.9, "derecho", 800000, "EUR", GetPais("Japón"), "Portero"));
                AltaJugador(new Jugador(401, "23", "Zion Suzuki", DateTime.Parse("2002-08-21"), 1.9, "derecho", 300000, "EUR", GetPais("Japón"), "Portero"));
                AltaJugador(new Jugador(402, "3", "Shogo Taniguchi", DateTime.Parse("1991-07-15"), 1.83, "derecho", 1700000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(403, "4", "Shinnosuke Nakatani", DateTime.Parse("1996-03-24"), 1.84, "derecho", 1300000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(404, "5", "Shinnosuke Hatanaka", DateTime.Parse("1995-08-25"), 1.84, "ambidiestro", 1000000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(405, "6", "Tomoki Iwata", DateTime.Parse("1997-04-07"), 1.76, "derecho", 1000000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(406, "19", "Sho Sasaki", DateTime.Parse("1989-10-02"), 1.76, "derecho", 900000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(407, "22", "Hayato Araki", DateTime.Parse("1996-08-07"), 1.86, "derecho", 900000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(408, "24", "Takuma Ominami", DateTime.Parse("1997-12-13"), 1.84, "derecho", 800000, "EUR", GetPais("Japón"), "Defensa central"));
                AltaJugador(new Jugador(409, "13", "Daiki Sugioka", DateTime.Parse("1998-09-08"), 1.82, "izquierdo", 550000, "EUR", GetPais("Japón"), "Lateral izquierdo"));
                AltaJugador(new Jugador(410, "2", "Miki Yamane", DateTime.Parse("1993-12-22"), 1.78, "derecho", 1300000, "EUR", GetPais("Japón"), "Lateral derecho"));
                AltaJugador(new Jugador(411, "25", "Ryuta Koike", DateTime.Parse("1995-08-29"), 1.69, "derecho", 900000, "EUR", GetPais("Japón"), "Lateral derecho"));
                AltaJugador(new Jugador(412, "26", "Joel Chima Fujita", DateTime.Parse("2002-02-16"), 1.73, "derecho", 700000, "EUR", GetPais("Japón"), "Pivote"));
                AltaJugador(new Jugador(413, "15", "Kento Hashimoto", DateTime.Parse("1993-08-16"), 1.83, "derecho", 2200000, "EUR", GetPais("Japón"), "Mediocentro"));
                AltaJugador(new Jugador(414, "18", "Kota Mizunuma", DateTime.Parse("1990-02-22"), 1.76, "derecho", 850000, "EUR", GetPais("Japón"), "Interior derecho"));
                AltaJugador(new Jugador(415, "8", "Tsukasa Morishima", DateTime.Parse("1997-04-25"), 1.73, "derecho", 1300000, "EUR", GetPais("Japón"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(416, "14", "Yasuto Wakizaka", DateTime.Parse("1995-06-11"), 1.72, "derecho", 1000000, "EUR", GetPais("Japón"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(417, "7", "Gakuto Notsuda", DateTime.Parse("1994-06-06"), 1.75, "izquierdo", 700000, "EUR", GetPais("Japón"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(418, "16", "Yuki Soma", DateTime.Parse("1997-02-25"), 1.66, "derecho", 900000, "EUR", GetPais("Japón"), "Extremo izquierdo"));
                AltaJugador(new Jugador(419, "10", "Yuto Iwasaki", DateTime.Parse("1998-06-11"), 1.72, "derecho", 500000, "EUR", GetPais("Japón"), "Extremo izquierdo"));
                AltaJugador(new Jugador(420, "21", "Makoto Mitsuta", DateTime.Parse("1999-07-20"), 1.7, "derecho", 450000, "EUR", GetPais("Japón"), "Extremo derecho"));
                AltaJugador(new Jugador(421, "17", "Ryo Miyaichi ", DateTime.Parse("1992-12-14"), 1.83, "derecho", 400000, "EUR", GetPais("Japón"), "Extremo derecho"));
                AltaJugador(new Jugador(422, "9", "Takuma Nishimura", DateTime.Parse("1996-10-22"), 1.78, "derecho", 800000, "EUR", GetPais("Japón"), "Mediapunta"));
                AltaJugador(new Jugador(423, "11", "Shuto Machino", DateTime.Parse("1999-09-30"), 1.84, "ambidiestro", 700000, "EUR", GetPais("Japón"), "Delantero centro"));
                AltaJugador(new Jugador(424, "20", "Mao Hosoya", DateTime.Parse("2001-09-07"), 1.77, "derecho", 550000, "EUR", GetPais("Japón"), "Delantero centro"));
                AltaJugador(new Jugador(425, "NDF", "Mohammed Al-Rubaie", DateTime.Parse("1997-08-14"), 1.9, "derecho", 1200000, "EUR", GetPais("Arabia Saudita"), "Portero"));
                AltaJugador(new Jugador(426, "NDF", "Mohammed Al-Owais", DateTime.Parse("1991-10-10"), 1.85, "izquierdo", 900000, "EUR", GetPais("Arabia Saudita"), "Portero"));
                AltaJugador(new Jugador(427, "NDF", "Fawaz Al-Qarni", DateTime.Parse("1992-04-02"), 1.85, "derecho", 350000, "EUR", GetPais("Arabia Saudita"), "Portero"));
                AltaJugador(new Jugador(428, "NDF", "Amin Al-Bukhari", DateTime.Parse("1997-05-02"), 1.94, "derecho", 175000, "EUR", GetPais("Arabia Saudita"), "Portero"));
                AltaJugador(new Jugador(429, "NDF", "Ziyad Al-Sahafi", DateTime.Parse("1994-10-17"), 1.82, "derecho", 1500000, "EUR", GetPais("Arabia Saudita"), "Defensa central"));
                AltaJugador(new Jugador(430, "NDF", "Abdulelah Al-Amri", DateTime.Parse("1997-01-15"), 1.83, "derecho", 1000000, "EUR", GetPais("Arabia Saudita"), "Defensa central"));
                AltaJugador(new Jugador(431, "NDF", "Ali Al-Oujami", DateTime.Parse("1996-04-25"), 1.75, "derecho", 750000, "EUR", GetPais("Arabia Saudita"), "Defensa central"));
                AltaJugador(new Jugador(432, "12", "Hassan Tambakti", DateTime.Parse("1999-02-09"), 1.82, "derecho", 600000, "EUR", GetPais("Arabia Saudita"), "Defensa central"));
                AltaJugador(new Jugador(433, "NDF", "Ali Al-Boleahi", DateTime.Parse("1989-11-21"), 1.78, "izquierdo", 400000, "EUR", GetPais("Arabia Saudita"), "Defensa central"));
                AltaJugador(new Jugador(434, "NDF", "Yasser Al-Shahrani", DateTime.Parse("1992-05-25"), 1.71, "derecho", 2000000, "EUR", GetPais("Arabia Saudita"), "Lateral izquierdo"));
                AltaJugador(new Jugador(435, "NDF", "Sultan Al-Ghannam", DateTime.Parse("1994-05-06"), 1.73, "derecho", 3500000, "EUR", GetPais("Arabia Saudita"), "Lateral derecho"));
                AltaJugador(new Jugador(436, "NDF", "Mohammed Al-Burayk", DateTime.Parse("1992-09-15"), 1.7, "derecho", 2000000, "EUR", GetPais("Arabia Saudita"), "Lateral derecho"));
                AltaJugador(new Jugador(437, "NDF", "Nasser Al-Dawsari", DateTime.Parse("1998-12-19"), 1.78, "izquierdo", 350000, "EUR", GetPais("Arabia Saudita"), "Pivote"));
                AltaJugador(new Jugador(438, "NDF", "Ali Al-Hassan", DateTime.Parse("1997-03-04"), 1.76, "derecho", 300000, "EUR", GetPais("Arabia Saudita"), "Pivote"));
                AltaJugador(new Jugador(439, "NDF", "Abdullah Otayf", DateTime.Parse("1992-08-03"), 1.77, "derecho", 300000, "EUR", GetPais("Arabia Saudita"), "Pivote"));
                AltaJugador(new Jugador(440, "NDF", "Mohamed Kanno", DateTime.Parse("1994-09-22"), 1.92, "derecho", 1800000, "EUR", GetPais("Arabia Saudita"), "Mediocentro"));
                AltaJugador(new Jugador(441, "NDF", "Salman Al-Faraj", DateTime.Parse("1989-08-01"), 1.8, "izquierdo", 750000, "EUR", GetPais("Arabia Saudita"), "Mediocentro"));
                AltaJugador(new Jugador(442, "NDF", "Sami Al-Najei", DateTime.Parse("1997-02-07"), 1.81, "izquierdo", 900000, "EUR", GetPais("Arabia Saudita"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(443, "NDF", "Abdulrahman Ghareeb", DateTime.Parse("1997-03-31"), 1.64, "derecho", 3000000, "EUR", GetPais("Arabia Saudita"), "Extremo izquierdo"));
                AltaJugador(new Jugador(444, "NDF", "Salem Al-Dawsari", DateTime.Parse("1991-08-19"), 1.71, "derecho", 2800000, "EUR", GetPais("Arabia Saudita"), "Extremo izquierdo"));
                AltaJugador(new Jugador(445, "NDF", "Khalid Al-Ghannam", DateTime.Parse("2000-11-08"), 1.71, "derecho", 1000000, "EUR", GetPais("Arabia Saudita"), "Extremo izquierdo"));
                AltaJugador(new Jugador(446, "NDF", "Abdulaziz Al-Bishi", DateTime.Parse("1994-03-11"), 1.72, "derecho", 2500000, "EUR", GetPais("Arabia Saudita"), "Extremo derecho"));
                AltaJugador(new Jugador(447, "20", "Abdulrahman Al-Obood", DateTime.Parse("1995-06-10"), 1.74, "derecho", 1500000, "EUR", GetPais("Arabia Saudita"), "Extremo derecho"));
                AltaJugador(new Jugador(448, "NDF", "Hattan Bahebri", DateTime.Parse("1992-07-16"), 1.7, "derecho", 900000, "EUR", GetPais("Arabia Saudita"), "Extremo derecho"));
                AltaJugador(new Jugador(449, "NDF", "Firas Al-Buraikan", DateTime.Parse("2000-05-14"), 1.81, "izquierdo", 1200000, "EUR", GetPais("Arabia Saudita"), "Delantero centro"));
                AltaJugador(new Jugador(450, "NDF", "Abdullah Al-Hamdan", DateTime.Parse("1999-09-12"), 1.84, "ambidiestro", 375000, "EUR", GetPais("Arabia Saudita"), "Delantero centro"));
                AltaJugador(new Jugador(451, "12", "Moisés Ramírez", DateTime.Parse("2000-09-09"), 1.85, "derecho", 800000, "EUR", GetPais("Ecuador"), "Portero"));
                AltaJugador(new Jugador(452, "1", "Hernán Galíndez", DateTime.Parse("1987-03-30"), 1.88, "derecho", 600000, "EUR", GetPais("Ecuador"), "Portero"));
                AltaJugador(new Jugador(453, "22", "Alexander Domínguez", DateTime.Parse("1987-06-05"), 1.96, "derecho", 350000, "EUR", GetPais("Ecuador"), "Portero"));
                AltaJugador(new Jugador(454, "3", "Piero Hincapié", DateTime.Parse("2002-01-09"), 1.84, "izquierdo", 17000000, "EUR", GetPais("Ecuador"), "Defensa central"));
                AltaJugador(new Jugador(455, "2", "Félix Torres", DateTime.Parse("1997-01-11"), 1.87, "derecho", 5000000, "EUR", GetPais("Ecuador"), "Defensa central"));
                AltaJugador(new Jugador(456, "14", "Jackson Porozo", DateTime.Parse("2000-08-04"), 1.92, "derecho", 3000000, "EUR", GetPais("Ecuador"), "Defensa central"));
                AltaJugador(new Jugador(457, "4", "Robert Arboleda ", DateTime.Parse("1991-10-22"), 1.87, "derecho", 2000000, "EUR", GetPais("Ecuador"), "Defensa central"));
                AltaJugador(new Jugador(458, "14", "Xavier Arreaga", DateTime.Parse("1994-09-28"), 1.83, "NDF", 1500000, "EUR", GetPais("Ecuador"), "Defensa central"));
                AltaJugador(new Jugador(459, "7", "Pervis Estupiñán", DateTime.Parse("1998-01-21"), 1.75, "izquierdo", 20000000, "EUR", GetPais("Ecuador"), "Lateral izquierdo"));
                AltaJugador(new Jugador(460, "18", "Diego Palacios", DateTime.Parse("1999-07-12"), 1.69, "izquierdo", 3500000, "EUR", GetPais("Ecuador"), "Lateral izquierdo"));
                AltaJugador(new Jugador(461, "6", "Byron Castillo", DateTime.Parse("1998-11-10"), 1.67, "derecho", 2800000, "EUR", GetPais("Ecuador"), "Lateral derecho"));
                AltaJugador(new Jugador(462, "17", "Angelo Preciado", DateTime.Parse("1998-02-18"), 1.73, "derecho", 2500000, "EUR", GetPais("Ecuador"), "Lateral derecho"));
                AltaJugador(new Jugador(463, "8", "Carlos Gruezo", DateTime.Parse("1995-04-19"), 1.71, "derecho", 3200000, "EUR", GetPais("Ecuador"), "Pivote"));
                AltaJugador(new Jugador(464, "20", "Jhegson Méndez", DateTime.Parse("1997-04-26"), 1.71, "derecho", 1500000, "EUR", GetPais("Ecuador"), "Pivote"));
                AltaJugador(new Jugador(465, "5", "Dixon Arroyo", DateTime.Parse("1992-06-01"), 1.79, "derecho", 900000, "EUR", GetPais("Ecuador"), "Pivote"));
                AltaJugador(new Jugador(466, "5", "José Cifuentes", DateTime.Parse("1999-03-12"), 1.75, "derecho", 6500000, "EUR", GetPais("Ecuador"), "Mediocentro"));
                AltaJugador(new Jugador(467, "23", "Moisés Caicedo", DateTime.Parse("2001-11-02"), 1.78, "derecho", 6000000, "EUR", GetPais("Ecuador"), "Mediocentro"));
                AltaJugador(new Jugador(468, "21", "Alan Franco", DateTime.Parse("1998-08-21"), 1.74, "derecho", 2500000, "EUR", GetPais("Ecuador"), "Mediocentro"));
                AltaJugador(new Jugador(469, "16", "Jeremy Sarmiento", DateTime.Parse("2002-06-16"), 1.83, "derecho", 3000000, "EUR", GetPais("Ecuador"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(470, "10", "Romario Ibarra", DateTime.Parse("1994-09-24"), 1.76, "derecho", 1800000, "EUR", GetPais("Ecuador"), "Extremo izquierdo"));
                AltaJugador(new Jugador(471, "5", "Alexander Alvarado ", DateTime.Parse("1999-04-21"), 1.65, "derecho", 1200000, "EUR", GetPais("Ecuador"), "Extremo izquierdo"));
                AltaJugador(new Jugador(472, "19", "Gonzalo Plata", DateTime.Parse("2000-11-01"), 1.78, "izquierdo", 6000000, "EUR", GetPais("Ecuador"), "Extremo derecho"));
                AltaJugador(new Jugador(473, "15", "Ángel Mena", DateTime.Parse("1988-01-21"), 1.68, "izquierdo", 1800000, "EUR", GetPais("Ecuador"), "Extremo derecho"));
                AltaJugador(new Jugador(474, "11", "Michael Estrada", DateTime.Parse("1996-04-07"), 1.88, "derecho", 3000000, "EUR", GetPais("Ecuador"), "Delantero centro"));
                AltaJugador(new Jugador(475, "13", "Enner Valencia", DateTime.Parse("1989-11-04"), 1.77, "derecho", 2400000, "EUR", GetPais("Ecuador"), "Delantero centro"));
                AltaJugador(new Jugador(476, "11", "Jordy Caicedo", DateTime.Parse("1997-11-18"), 1.87, "derecho", 2000000, "EUR", GetPais("Ecuador"), "Delantero centro"));
                AltaJugador(new Jugador(477, "9", "Leonardo Campana", DateTime.Parse("2000-07-24"), 1.88, "izquierdo", 1200000, "EUR", GetPais("Ecuador"), "Delantero centro"));
                AltaJugador(new Jugador(478, "10", "Djorkaeff Reasco", DateTime.Parse("1999-01-18"), 1.72, "derecho", 700000, "EUR", GetPais("Ecuador"), "Delantero centro"));
                AltaJugador(new Jugador(479, "12", "Sergio Rochet", DateTime.Parse("1993-03-23"), 1.9, "derecho", 3000000, "EUR", GetPais("Uruguay"), "Portero"));
                AltaJugador(new Jugador(480, "23", "Sebastián Sosa", DateTime.Parse("1986-08-19"), 1.81, "derecho", 2000000, "EUR", GetPais("Uruguay"), "Portero"));
                AltaJugador(new Jugador(481, "1", "Fernando Muslera", DateTime.Parse("1986-06-16"), 1.9, "derecho", 1800000, "EUR", GetPais("Uruguay"), "Portero"));
                AltaJugador(new Jugador(482, "2", "José María Giménez ", DateTime.Parse("1995-01-20"), 1.85, "derecho", 50000000, "EUR", GetPais("Uruguay"), "Defensa central"));
                AltaJugador(new Jugador(483, "4", "Ronald Araújo", DateTime.Parse("1999-03-07"), 1.88, "derecho", 50000000, "EUR", GetPais("Uruguay"), "Defensa central"));
                AltaJugador(new Jugador(484, "19", "Sebastián Coates", DateTime.Parse("1990-10-07"), 1.96, "derecho", 8000000, "EUR", GetPais("Uruguay"), "Defensa central"));
                AltaJugador(new Jugador(485, "3", "Diego Godín", DateTime.Parse("1986-02-16"), 1.87, "derecho", 1000000, "EUR", GetPais("Uruguay"), "Defensa central"));
                AltaJugador(new Jugador(486, "22", "Martín Cáceres", DateTime.Parse("1987-04-07"), 1.8, "derecho", 900000, "EUR", GetPais("Uruguay"), "Defensa central"));
                AltaJugador(new Jugador(487, "16", "Mathías Olivera", DateTime.Parse("1997-10-31"), 1.85, "izquierdo", 15000000, "EUR", GetPais("Uruguay"), "Lateral izquierdo"));
                AltaJugador(new Jugador(488, "17", "Matías Viña", DateTime.Parse("1997-11-09"), 1.8, "izquierdo", 7500000, "EUR", GetPais("Uruguay"), "Lateral izquierdo"));
                AltaJugador(new Jugador(489, "4", "Guillermo Varela", DateTime.Parse("1993-03-24"), 1.73, "derecho", 2500000, "EUR", GetPais("Uruguay"), "Lateral derecho"));
                AltaJugador(new Jugador(490, "4", "Luis Suárez", DateTime.Parse("1987-01-24"), 1.82, "derecho", 28000000, "EUR", GetPais("Uruguay"), "Delantero centro"));
                AltaJugador(new Jugador(491, "13", "Damián Suárez", DateTime.Parse("1988-04-27"), 1.73, "derecho", 1500000, "EUR", GetPais("Uruguay"), "Lateral derecho"));
                AltaJugador(new Jugador(492, "14", "Lucas Torreira", DateTime.Parse("1996-02-11"), 1.66, "derecho", 20000000, "EUR", GetPais("Uruguay"), "Pivote"));
                AltaJugador(new Jugador(493, "6", "Manuel Ugarte", DateTime.Parse("2001-04-11"), 1.82, "derecho", 15000000, "EUR", GetPais("Uruguay"), "Pivote"));
                AltaJugador(new Jugador(494, "15", "Federico Valverde", DateTime.Parse("1998-07-22"), 1.82, "derecho", 70000000, "EUR", GetPais("Uruguay"), "Mediocentro"));
                AltaJugador(new Jugador(495, "20", "Mauro Arambarri", DateTime.Parse("1995-09-30"), 1.75, "derecho", 15000000, "EUR", GetPais("Uruguay"), "Mediocentro"));
                AltaJugador(new Jugador(496, "7", "Nicolás de la Cruz", DateTime.Parse("1997-06-01"), 1.67, "derecho", 13000000, "EUR", GetPais("Uruguay"), "Mediocentro"));
                AltaJugador(new Jugador(497, "11", "Fernando Gorriarán", DateTime.Parse("1994-11-27"), 1.68, "derecho", 8000000, "EUR", GetPais("Uruguay"), "Mediocentro"));
                AltaJugador(new Jugador(498, "5", "Matías Vecino", DateTime.Parse("1991-08-24"), 1.87, "derecho", 3500000, "EUR", GetPais("Uruguay"), "Mediocentro"));
                AltaJugador(new Jugador(499, "10", "Giorgian de Arrascaeta", DateTime.Parse("1994-06-01"), 1.74, "derecho", 17000000, "EUR", GetPais("Uruguay"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(500, "8", "Diego Rossi", DateTime.Parse("1998-03-05"), 1.7, "derecho", 14500000, "EUR", GetPais("Uruguay"), "Extremo izquierdo"));
                AltaJugador(new Jugador(501, "8", "Facundo Pellistri", DateTime.Parse("2001-12-20"), 1.74, "derecho", 2500000, "EUR", GetPais("Uruguay"), "Extremo derecho"));
                AltaJugador(new Jugador(502, "NDF", "Agustín Canobbio", DateTime.Parse("1998-10-01"), 1.75, "derecho", 2000000, "EUR", GetPais("Uruguay"), "Extremo derecho"));
                AltaJugador(new Jugador(503, "NDF", "Augusto Scarone", DateTime.Parse("2004-06-03"), 1.7, "izquierdo", 300000, "EUR", GetPais("Uruguay"), "Mediapunta"));
                AltaJugador(new Jugador(504, "11", "Darwin Núñez", DateTime.Parse("1999-06-24"), 1.87, "derecho", 55000000, "EUR", GetPais("Uruguay"), "Delantero centro"));
                AltaJugador(new Jugador(505, "18", "Maxi Gómez", DateTime.Parse("1996-08-14"), 1.86, "derecho", 12000000, "EUR", GetPais("Uruguay"), "Delantero centro"));
                AltaJugador(new Jugador(506, "21", "Edinson Cavani", DateTime.Parse("1987-02-14"), 1.84, "derecho", 4000000, "EUR", GetPais("Uruguay"), "Delantero centro"));
                AltaJugador(new Jugador(507, "NDF", "Maxime Crépeau", DateTime.Parse("1994-05-11"), 1.85, "derecho", 2000000, "EUR", GetPais("Canadá"), "Portero"));
                AltaJugador(new Jugador(508, "18", "Milan Borjan", DateTime.Parse("1987-10-23"), 1.96, "derecho", 1500000, "EUR", GetPais("Canadá"), "Portero"));
                AltaJugador(new Jugador(509, "NDF", "Dayne St. Clair", DateTime.Parse("1997-05-09"), 1.91, "izquierdo", 1000000, "EUR", GetPais("Canadá"), "Portero"));
                AltaJugador(new Jugador(510, "NDF", "Kamal Miller", DateTime.Parse("1997-05-16"), 1.83, "izquierdo", 2500000, "EUR", GetPais("Canadá"), "Defensa central"));
                AltaJugador(new Jugador(511, "NDF", "Doneil Henry", DateTime.Parse("1993-04-20"), 1.88, "derecho", 750000, "EUR", GetPais("Canadá"), "Defensa central"));
                AltaJugador(new Jugador(512, "NDF", "Scott Kennedy", DateTime.Parse("1997-03-31"), 1.9, "izquierdo", 600000, "EUR", GetPais("Canadá"), "Defensa central"));
                AltaJugador(new Jugador(513, "NDF", "Steven Vitória", DateTime.Parse("1987-01-11"), 1.95, "derecho", 100000, "EUR", GetPais("Canadá"), "Defensa central"));
                AltaJugador(new Jugador(514, "19", "Alphonso Davies", DateTime.Parse("2000-11-02"), 1.83, "izquierdo", 70000000, "EUR", GetPais("Canadá"), "Lateral izquierdo"));
                AltaJugador(new Jugador(515, "NDF", "Sam Adekugbe", DateTime.Parse("1995-01-16"), 1.76, "izquierdo", 2000000, "EUR", GetPais("Canadá"), "Lateral izquierdo"));
                AltaJugador(new Jugador(516, "NDF", "Raheem Edwards", DateTime.Parse("1995-07-17"), 1.72, "NDF", 450000, "EUR", GetPais("Canadá"), "Lateral izquierdo"));
                AltaJugador(new Jugador(517, "NDF", "Alistair Johnston", DateTime.Parse("1998-10-08"), 1.8, "NDF", 3000000, "EUR", GetPais("Canadá"), "Lateral derecho"));
                AltaJugador(new Jugador(518, "NDF", "Richie Laryea", DateTime.Parse("1995-01-07"), 1.75, "derecho", 2500000, "EUR", GetPais("Canadá"), "Lateral derecho"));
                AltaJugador(new Jugador(519, "NDF", "Stephen Eustaquio", DateTime.Parse("1996-12-21"), 1.77, "derecho", 5000000, "EUR", GetPais("Canadá"), "Pivote"));
                AltaJugador(new Jugador(520, "NDF", "Samuel Piette", DateTime.Parse("1994-11-12"), 1.71, "derecho", 2500000, "EUR", GetPais("Canadá"), "Pivote"));
                AltaJugador(new Jugador(521, "NDF", "Mark-Anthony Kaye", DateTime.Parse("1994-12-02"), 1.85, "izquierdo", 5000000, "EUR", GetPais("Canadá"), "Mediocentro"));
                AltaJugador(new Jugador(522, "NDF", "Jonathan Osorio", DateTime.Parse("1992-06-12"), 1.75, "ambidiestro", 3500000, "EUR", GetPais("Canadá"), "Mediocentro"));
                AltaJugador(new Jugador(523, "13", "Atiba Hutchinson", DateTime.Parse("1983-02-08"), 1.87, "derecho", 275000, "EUR", GetPais("Canadá"), "Mediocentro"));
                AltaJugador(new Jugador(524, "NDF", "Cyle Larin", DateTime.Parse("1995-04-17"), 1.88, "derecho", 10000000, "EUR", GetPais("Canadá"), "Extremo izquierdo"));
                AltaJugador(new Jugador(525, "NDF", "Junior Hoilett", DateTime.Parse("1990-06-05"), 1.74, "derecho", 800000, "EUR", GetPais("Canadá"), "Extremo izquierdo"));
                AltaJugador(new Jugador(526, "NDF", "Luca Koleosho", DateTime.Parse("2004-09-15"), 0, "derecho", 0, "EUR", GetPais("Canadá"), "Extremo izquierdo"));
                AltaJugador(new Jugador(527, "NDF", "Tajon Buchanan ", DateTime.Parse("1999-02-08"), 1.83, "derecho", 9500000, "EUR", GetPais("Canadá"), "Extremo derecho"));
                AltaJugador(new Jugador(528, "NDF", "Jonathan David", DateTime.Parse("2000-01-14"), 1.8, "ambidiestro", 45000000, "EUR", GetPais("Canadá"), "Delantero centro"));
                AltaJugador(new Jugador(529, "NDF", "Iké Ugbo", DateTime.Parse("1998-09-21"), 1.84, "derecho", 5000000, "EUR", GetPais("Canadá"), "Delantero centro"));
                AltaJugador(new Jugador(530, "NDF", "Lucas Cavallini", DateTime.Parse("1992-12-28"), 1.82, "izquierdo", 3000000, "EUR", GetPais("Canadá"), "Delantero centro"));
                AltaJugador(new Jugador(531, "NDF", "Charles-Andreas Brym", DateTime.Parse("1998-08-08"), 1.85, "izquierdo", 325000, "EUR", GetPais("Canadá"), "Delantero centro"));
                AltaJugador(new Jugador(532, "12", "Lawrence Ati Zigi", DateTime.Parse("1996-11-29"), 1.89, "derecho", 2500000, "EUR", GetPais("Ghana"), "Portero"));
                AltaJugador(new Jugador(533, "NDF", "Abdul Nurudeen", DateTime.Parse("1999-02-08"), 1.9, "derecho", 600000, "EUR", GetPais("Ghana"), "Portero"));
                AltaJugador(new Jugador(534, "16", "Joe Wollacott", DateTime.Parse("1996-09-08"), 1.9, "NDF", 300000, "EUR", GetPais("Ghana"), "Portero"));
                AltaJugador(new Jugador(535, "NDF", "Ibrahim Danlad", DateTime.Parse("2002-12-02"), 1.77, "derecho", 100000, "EUR", GetPais("Ghana"), "Portero"));
                AltaJugador(new Jugador(536, "18", "Daniel Amartey", DateTime.Parse("1994-12-21"), 1.86, "derecho", 10000000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(537, "6", "Joseph Aidoo", DateTime.Parse("1995-09-29"), 1.77, "derecho", 7000000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(538, "26", "Abdul Mumin", DateTime.Parse("1998-06-06"), 1.88, "derecho", 3000000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(539, "NDF", "Patric Pfeiffer", DateTime.Parse("1999-08-20"), 1.96, "derecho", 2000000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(540, "NDF", "Alidu Seidu", DateTime.Parse("2000-06-04"), 1.73, "derecho", 2000000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(541, "NDF", "Jonathan Mensah", DateTime.Parse("1990-07-13"), 1.88, "derecho", 800000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(542, "NDF", "Stephan Ambrosius", DateTime.Parse("1998-12-18"), 1.83, "derecho", 800000, "EUR", GetPais("Ghana"), "Defensa central"));
                AltaJugador(new Jugador(543, "14", "Gideon Mensah", DateTime.Parse("1998-07-18"), 1.78, "izquierdo", 2500000, "EUR", GetPais("Ghana"), "Lateral izquierdo"));
                AltaJugador(new Jugador(544, "NDF", "Abdul-Rahman Baba", DateTime.Parse("1994-07-02"), 1.79, "izquierdo", 2200000, "EUR", GetPais("Ghana"), "Lateral izquierdo"));
                AltaJugador(new Jugador(545, "NDF", "Andy Yiadom", DateTime.Parse("1991-12-02"), 1.8, "derecho", 1200000, "EUR", GetPais("Ghana"), "Lateral derecho"));
                AltaJugador(new Jugador(546, "3", "Denis Odoi", DateTime.Parse("1988-05-27"), 1.78, "derecho", 700000, "EUR", GetPais("Ghana"), "Lateral derecho"));
                AltaJugador(new Jugador(547, "10", "Elisha Owusu", DateTime.Parse("1997-11-07"), 1.82, "derecho", 2500000, "EUR", GetPais("Ghana"), "Pivote"));
                AltaJugador(new Jugador(548, "NDF", "Edmund Addo", DateTime.Parse("2000-05-17"), 1.8, "derecho", 1100000, "EUR", GetPais("Ghana"), "Pivote"));
                AltaJugador(new Jugador(549, "20", "Mohammed Kudus", DateTime.Parse("2000-08-02"), 1.77, "izquierdo", 10000000, "EUR", GetPais("Ghana"), "Mediocentro"));
                AltaJugador(new Jugador(550, "21", "Iddrisu Baba", DateTime.Parse("1996-01-22"), 1.82, "derecho", 3500000, "EUR", GetPais("Ghana"), "Mediocentro"));
                AltaJugador(new Jugador(551, "NDF", "Mubarak Wakaso", DateTime.Parse("1990-07-25"), 1.71, "izquierdo", 2000000, "EUR", GetPais("Ghana"), "Mediocentro"));
                AltaJugador(new Jugador(552, "8", "Daniel-Kofi Kyereh", DateTime.Parse("1996-03-08"), 1.79, "derecho", 3000000, "EUR", GetPais("Ghana"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(553, "7", "Issahaku Fatawu", DateTime.Parse("2004-03-08"), 1.77, "izquierdo", 500000, "EUR", GetPais("Ghana"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(554, "NDF", "Augustine Okrah", DateTime.Parse("1993-09-14"), 1.72, "izquierdo", 150000, "EUR", GetPais("Ghana"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(555, "22", "Kamaldeen Sulemana", DateTime.Parse("2002-02-15"), 1.75, "derecho", 18000000, "EUR", GetPais("Ghana"), "Extremo izquierdo"));
                AltaJugador(new Jugador(556, "10", "André Ayew", DateTime.Parse("1989-12-17"), 1.76, "izquierdo", 2500000, "EUR", GetPais("Ghana"), "Extremo izquierdo"));
                AltaJugador(new Jugador(557, "15", "Joseph Paintsil", DateTime.Parse("1998-02-01"), 1.69, "derecho", 2500000, "EUR", GetPais("Ghana"), "Extremo izquierdo"));
                AltaJugador(new Jugador(558, "22", "Christopher Antwi-Adjei", DateTime.Parse("1994-02-07"), 1.74, "derecho", 1000000, "EUR", GetPais("Ghana"), "Extremo izquierdo"));
                AltaJugador(new Jugador(559, "NDF", "Daniel Afriyie", DateTime.Parse("2001-06-26"), 1.76, "NDF", 150000, "EUR", GetPais("Ghana"), "Extremo izquierdo"));
                AltaJugador(new Jugador(560, "11", "Osman Bukari", DateTime.Parse("1998-12-13"), 1.7, "ambidiestro", 3000000, "EUR", GetPais("Ghana"), "Extremo derecho"));
                AltaJugador(new Jugador(561, "19", "Yaw Yeboah", DateTime.Parse("1997-03-28"), 1.75, "izquierdo", 1800000, "EUR", GetPais("Ghana"), "Extremo derecho"));
                AltaJugador(new Jugador(562, "NDF", "Braydon Manu", DateTime.Parse("1997-03-28"), 1.7, "ambidiestro", 550000, "EUR", GetPais("Ghana"), "Extremo derecho"));
                AltaJugador(new Jugador(563, "9", "Jordan Ayew", DateTime.Parse("1991-09-11"), 1.82, "derecho", 6000000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(564, "13", "Felix Afena-Gyan", DateTime.Parse("2003-01-19"), 1.75, "derecho", 6000000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(565, "15", "Antoine Semenyo", DateTime.Parse("2000-01-07"), 1.78, "derecho", 2500000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(566, "NDF", "Benjamin Tetteh", DateTime.Parse("1997-07-10"), 1.93, "derecho", 2300000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(567, "NDF", "Ransford-Yeboah Königsdörffer", DateTime.Parse("2001-09-13"), 1.83, "derecho", 1300000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(568, "17", "Kwasi Wriedt", DateTime.Parse("1994-07-10"), 1.88, "izquierdo", 900000, "EUR", GetPais("Ghana"), "Delantero centro"));
                AltaJugador(new Jugador(569, "16", "Edouard Mendy", DateTime.Parse("1992-03-01"), 1.94, "derecho", 32000000, "EUR", GetPais("Senegal"), "Portero"));
                AltaJugador(new Jugador(570, "NDF", "Alfred Gomis", DateTime.Parse("1993-09-05"), 1.96, "derecho", 175000, "EUR", GetPais("Senegal"), "Portero"));
                AltaJugador(new Jugador(571, "NDF", "Seny Dieng", DateTime.Parse("1994-11-23"), 1.93, "derecho", 1000000, "EUR", GetPais("Senegal"), "Portero"));
                AltaJugador(new Jugador(572, "3", "Kalidou Koulibaly", DateTime.Parse("1991-06-20"), 1.86, "derecho", 35000000, "EUR", GetPais("Senegal"), "Defensa central"));
                AltaJugador(new Jugador(573, "22", "Abdou Diallo", DateTime.Parse("1996-05-04"), 1.87, "izquierdo", 18000000, "EUR", GetPais("Senegal"), "Defensa central"));
                AltaJugador(new Jugador(574, "4", "Pape Abou Cissé", DateTime.Parse("1995-09-14"), 1.98, "derecho", 8000000, "EUR", GetPais("Senegal"), "Defensa central"));
                AltaJugador(new Jugador(575, "14", "Abdoulaye Seck", DateTime.Parse("1992-06-04"), 1.92, "derecho", 3000000, "EUR", GetPais("Senegal"), "Defensa central"));
                AltaJugador(new Jugador(576, "12", "Fodé Ballo-Touré", DateTime.Parse("1997-01-03"), 1.82, "izquierdo", 3500000, "EUR", GetPais("Senegal"), "Lateral izquierdo"));
                AltaJugador(new Jugador(577, "2", "Saliou Ciss", DateTime.Parse("1989-09-15"), 1.74, "izquierdo", 400000, "EUR", GetPais("Senegal"), "Lateral izquierdo"));
                AltaJugador(new Jugador(578, "12", "Youssouf Sabaly", DateTime.Parse("1993-03-05"), 1.73, "derecho", 3000000, "EUR", GetPais("Senegal"), "Lateral derecho"));
                AltaJugador(new Jugador(579, "2", "Alpha Diounkou", DateTime.Parse("2001-10-10"), 1.84, "derecho", 300000, "EUR", GetPais("Senegal"), "Lateral derecho"));
                AltaJugador(new Jugador(580, "26", "Pape Gueye", DateTime.Parse("1999-01-24"), 1.83, "izquierdo", 12000000, "EUR", GetPais("Senegal"), "Pivote"));
                AltaJugador(new Jugador(581, "6", "Nampalys Mendy", DateTime.Parse("1992-06-23"), 1.67, "derecho", 4000000, "EUR", GetPais("Senegal"), "Pivote"));
                AltaJugador(new Jugador(582, "8", "Cheikhou Kouyaté", DateTime.Parse("1989-12-21"), 1.89, "derecho", 4000000, "EUR", GetPais("Senegal"), "Pivote"));
                AltaJugador(new Jugador(583, "25", "Mamadou Loum", DateTime.Parse("1996-12-30"), 1.88, "derecho", 4000000, "EUR", GetPais("Senegal"), "Pivote"));
                AltaJugador(new Jugador(584, "17", "Pape Matar Sarr", DateTime.Parse("2002-09-14"), 1.84, "derecho", 15000000, "EUR", GetPais("Senegal"), "Mediocentro"));
                AltaJugador(new Jugador(585, "5", "Idrissa Gueye", DateTime.Parse("1989-09-26"), 1.74, "derecho", 12000000, "EUR", GetPais("Senegal"), "Mediocentro"));
                AltaJugador(new Jugador(586, "24", "Moustapha Name", DateTime.Parse("1995-05-05"), 1.85, "ambidiestro", 2000000, "EUR", GetPais("Senegal"), "Mediocentro"));
                AltaJugador(new Jugador(587, "NDF", "Iliman Ndiaye", DateTime.Parse("2000-03-06"), 1.8, "NDF", 1500000, "EUR", GetPais("Senegal"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(588, "10", "Sadio Mané", DateTime.Parse("1992-04-10"), 1.74, "derecho", 70000000, "EUR", GetPais("Senegal"), "Extremo izquierdo"));
                AltaJugador(new Jugador(589, "18", "Ismaïla Sarr", DateTime.Parse("1998-02-25"), 1.85, "derecho", 27000000, "EUR", GetPais("Senegal"), "Extremo derecho"));
                AltaJugador(new Jugador(590, "NDF", "Demba Seck", DateTime.Parse("2001-02-10"), 1.9, "izquierdo", 3000000, "EUR", GetPais("Senegal"), "Extremo derecho"));
                AltaJugador(new Jugador(591, "9", "Boulaye Dia", DateTime.Parse("1996-11-16"), 1.8, "derecho", 12000000, "EUR", GetPais("Senegal"), "Delantero centro"));
                AltaJugador(new Jugador(592, "11", "Habib Diallo", DateTime.Parse("1995-06-18"), 1.86, "derecho", 12000000, "EUR", GetPais("Senegal"), "Delantero centro"));
                AltaJugador(new Jugador(593, "19", "Famara Diédhiou", DateTime.Parse("1992-12-15"), 1.89, "derecho", 4000000, "EUR", GetPais("Senegal"), "Delantero centro"));
                AltaJugador(new Jugador(594, "7", "Keita Baldé", DateTime.Parse("1995-03-08"), 1.81, "ambidiestro", 3000000, "EUR", GetPais("Senegal"), "Delantero centro"));
                AltaJugador(new Jugador(595, "22", "Diogo Costa", DateTime.Parse("1999-09-19"), 1.86, "derecho", 25000000, "EUR", GetPais("Portugal"), "Portero"));
                AltaJugador(new Jugador(596, "12", "Rui Silva", DateTime.Parse("1994-02-07"), 1.91, "izquierdo", 15000000, "EUR", GetPais("Portugal"), "Portero"));
                AltaJugador(new Jugador(597, "1", "Rui Patrício", DateTime.Parse("1988-02-15"), 1.9, "izquierdo", 6000000, "EUR", GetPais("Portugal"), "Portero"));
                AltaJugador(new Jugador(598, "5", "David Carmo", DateTime.Parse("1999-07-19"), 1.96, "izquierdo", 15000000, "EUR", GetPais("Portugal"), "Defensa central"));
                AltaJugador(new Jugador(599, "4", "Domingos Duarte", DateTime.Parse("1995-03-10"), 1.92, "derecho", 8000000, "EUR", GetPais("Portugal"), "Defensa central"));
                AltaJugador(new Jugador(600, "3", "Pepe", DateTime.Parse("1983-02-26"), 1.87, "derecho", 1000000, "EUR", GetPais("Portugal"), "Defensa central"));
                AltaJugador(new Jugador(601, "19", "Nuno Mendes", DateTime.Parse("2002-06-19"), 1.76, "izquierdo", 40000000, "EUR", GetPais("Portugal"), "Lateral izquierdo"));
                AltaJugador(new Jugador(602, "20", "João Cancelo ", DateTime.Parse("1994-05-27"), 1.82, "derecho", 65000000, "EUR", GetPais("Portugal"), "Lateral derecho"));
                AltaJugador(new Jugador(603, "2", "Diogo Dalot", DateTime.Parse("1999-03-18"), 1.83, "derecho", 20000000, "EUR", GetPais("Portugal"), "Lateral derecho"));
                AltaJugador(new Jugador(604, "18", "Rúben Neves", DateTime.Parse("1997-03-13"), 1.8, "derecho", 40000000, "EUR", GetPais("Portugal"), "Pivote"));
                AltaJugador(new Jugador(605, "6", "João Palhinha", DateTime.Parse("1995-07-09"), 1.9, "derecho", 25000000, "EUR", GetPais("Portugal"), "Pivote"));
                AltaJugador(new Jugador(606, "14", "William Carvalho", DateTime.Parse("1992-04-07"), 1.87, "derecho", 16000000, "EUR", GetPais("Portugal"), "Pivote"));
                AltaJugador(new Jugador(607, "13", "Danilo Pereira", DateTime.Parse("1991-09-09"), 1.88, "derecho", 14000000, "EUR", GetPais("Portugal"), "Pivote"));
                AltaJugador(new Jugador(608, "23", "Matheus Nunes", DateTime.Parse("1998-08-27"), 1.83, "derecho", 35000000, "EUR", GetPais("Portugal"), "Mediocentro"));
                AltaJugador(new Jugador(609, "11", "Vitinha", DateTime.Parse("2000-02-13"), 1.72, "derecho", 30000000, "EUR", GetPais("Portugal"), "Mediocentro"));
                AltaJugador(new Jugador(610, "16", "Otávio", DateTime.Parse("1995-02-09"), 1.72, "derecho", 30000000, "EUR", GetPais("Portugal"), "Interior derecho"));
                AltaJugador(new Jugador(611, "8", "Bruno Fernandes", DateTime.Parse("1994-09-08"), 1.79, "derecho", 85000000, "EUR", GetPais("Portugal"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(612, "10", "Bernardo Silva", DateTime.Parse("1994-08-10"), 1.73, "izquierdo", 80000000, "EUR", GetPais("Portugal"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(613, "15", "Rafael Leão", DateTime.Parse("1999-06-10"), 1.88, "derecho", 70000000, "EUR", GetPais("Portugal"), "Extremo izquierdo"));
                AltaJugador(new Jugador(614, "21", "Diogo Jota", DateTime.Parse("1996-12-04"), 1.78, "derecho", 60000000, "EUR", GetPais("Portugal"), "Extremo izquierdo"));
                AltaJugador(new Jugador(615, "17", "Gonçalo Guedes", DateTime.Parse("1996-11-29"), 1.79, "derecho", 40000000, "EUR", GetPais("Portugal"), "Extremo izquierdo"));
                AltaJugador(new Jugador(616, "7", "Ricardo Horta", DateTime.Parse("1994-09-15"), 1.73, "derecho", 20000000, "EUR", GetPais("Portugal"), "Extremo izquierdo"));
                AltaJugador(new Jugador(617, "9", "André Silva", DateTime.Parse("1995-11-06"), 1.85, "derecho", 32000000, "EUR", GetPais("Portugal"), "Delantero centro"));
                AltaJugador(new Jugador(618, "1", "Wojciech Szczesny ", DateTime.Parse("1990-04-18"), 1.96, "derecho", 15000000, "EUR", GetPais("Polonia"), "Portero"));
                AltaJugador(new Jugador(619, "22", "Bartlomiej Dragowski", DateTime.Parse("1997-08-19"), 1.91, "derecho", 6000000, "EUR", GetPais("Polonia"), "Portero"));
                AltaJugador(new Jugador(620, "1", "Kamil Grabara ", DateTime.Parse("1999-01-08"), 1.95, "derecho", 4000000, "EUR", GetPais("Polonia"), "Portero"));
                AltaJugador(new Jugador(621, "12", "Lukasz Skorupski", DateTime.Parse("1991-05-05"), 1.87, "derecho", 4000000, "EUR", GetPais("Polonia"), "Portero"));
                AltaJugador(new Jugador(622, "5", "Jan Bednarek", DateTime.Parse("1996-04-12"), 1.89, "derecho", 22000000, "EUR", GetPais("Polonia"), "Defensa central"));
                AltaJugador(new Jugador(623, "3", "Mateusz Wieteska", DateTime.Parse("1997-02-11"), 1.87, "derecho", 1300000, "EUR", GetPais("Polonia"), "Defensa central"));
                AltaJugador(new Jugador(624, "NDF", "Marcin Kaminski", DateTime.Parse("1992-01-15"), 1.92, "izquierdo", 1000000, "EUR", GetPais("Polonia"), "Defensa central"));
                AltaJugador(new Jugador(625, "15", "Kamil Glik", DateTime.Parse("1988-02-03"), 1.9, "derecho", 1000000, "EUR", GetPais("Polonia"), "Defensa central"));
                AltaJugador(new Jugador(626, "19", "Tymoteusz Puchacz ", DateTime.Parse("1999-01-23"), 1.8, "izquierdo", 2300000, "EUR", GetPais("Polonia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(627, "13", "Kamil Pestka", DateTime.Parse("1998-08-22"), 1.89, "izquierdo", 900000, "EUR", GetPais("Polonia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(628, "2", "Matty Cash", DateTime.Parse("1997-08-07"), 1.85, "derecho", 25000000, "EUR", GetPais("Polonia"), "Lateral derecho"));
                AltaJugador(new Jugador(629, "4", "Tomasz Kedziora", DateTime.Parse("1994-06-11"), 1.84, "derecho", 4500000, "EUR", GetPais("Polonia"), "Lateral derecho"));
                AltaJugador(new Jugador(630, "18", "Bartosz Bereszynski", DateTime.Parse("1992-07-12"), 1.83, "derecho", 4000000, "EUR", GetPais("Polonia"), "Lateral derecho"));
                AltaJugador(new Jugador(631, "25", "Robert Gumny", DateTime.Parse("1998-06-04"), 1.82, "derecho", 2500000, "EUR", GetPais("Polonia"), "Lateral derecho"));
                AltaJugador(new Jugador(632, "10", "Grzegorz Krychowiak", DateTime.Parse("1990-01-29"), 1.87, "derecho", 8000000, "EUR", GetPais("Polonia"), "Pivote"));
                AltaJugador(new Jugador(633, "NDF", "Krystian Bielik", DateTime.Parse("1998-01-04"), 1.89, "derecho", 4000000, "EUR", GetPais("Polonia"), "Pivote"));
                AltaJugador(new Jugador(634, "4", "Jakub Kiwior", DateTime.Parse("2000-02-15"), 1.89, "izquierdo", 3000000, "EUR", GetPais("Polonia"), "Pivote"));
                AltaJugador(new Jugador(635, "6", "Jacek Goralski", DateTime.Parse("1992-09-21"), 1.72, "derecho", 1600000, "EUR", GetPais("Polonia"), "Pivote"));
                AltaJugador(new Jugador(636, "8", "Szymon Zurkowski ", DateTime.Parse("1997-09-25"), 1.85, "derecho", 7000000, "EUR", GetPais("Polonia"), "Mediocentro"));
                AltaJugador(new Jugador(637, "8", "Karol Linetty", DateTime.Parse("1995-02-02"), 1.76, "derecho", 3500000, "EUR", GetPais("Polonia"), "Mediocentro"));
                AltaJugador(new Jugador(638, "14", "Mateusz Klich", DateTime.Parse("1990-06-13"), 1.83, "derecho", 3000000, "EUR", GetPais("Polonia"), "Mediocentro"));
                AltaJugador(new Jugador(639, "17", "Damian Szymanski", DateTime.Parse("1995-06-16"), 1.82, "derecho", 1800000, "EUR", GetPais("Polonia"), "Mediocentro"));
                AltaJugador(new Jugador(640, "21", "Nicola Zalewski ", DateTime.Parse("2002-01-23"), 1.75, "ambidiestro", 12000000, "EUR", GetPais("Polonia"), "Interior izquierdo"));
                AltaJugador(new Jugador(641, "19", "Przemyslaw Frankowski", DateTime.Parse("1995-04-12"), 1.76, "derecho", 8000000, "EUR", GetPais("Polonia"), "Interior izquierdo"));
                AltaJugador(new Jugador(642, "NDF", "Patryk Kun", DateTime.Parse("1995-04-20"), 1.65, "ambidiestro", 1200000, "EUR", GetPais("Polonia"), "Interior izquierdo"));
                AltaJugador(new Jugador(643, "20", "Piotr Zielinski", DateTime.Parse("1994-05-20"), 1.8, "ambidiestro", 40000000, "EUR", GetPais("Polonia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(644, "17", "Sebastian Szymanski", DateTime.Parse("1999-05-10"), 1.74, "izquierdo", 14000000, "EUR", GetPais("Polonia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(645, "2", "Jakub Kaminski", DateTime.Parse("2002-06-05"), 1.79, "derecho", 10000000, "EUR", GetPais("Polonia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(646, "NDF", "Kamil Józwiak", DateTime.Parse("1998-04-22"), 1.76, "derecho", 2500000, "EUR", GetPais("Polonia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(647, "NDF", "Przemyslaw Placheta", DateTime.Parse("1998-03-23"), 1.78, "izquierdo", 1800000, "EUR", GetPais("Polonia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(648, "11", "Kamil Grosicki", DateTime.Parse("1988-06-08"), 1.8, "derecho", 800000, "EUR", GetPais("Polonia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(649, "NDF", "Konrad Michalak", DateTime.Parse("1997-09-19"), 1.76, "derecho", 1700000, "EUR", GetPais("Polonia"), "Extremo derecho"));
                AltaJugador(new Jugador(650, "9", "Robert Lewandowski", DateTime.Parse("1988-08-21"), 1.85, "derecho", 45000000, "EUR", GetPais("Polonia"), "Delantero centro"));
                AltaJugador(new Jugador(651, "NDF", "Arkadiusz Milik", DateTime.Parse("1994-02-28"), 1.86, "izquierdo", 16000000, "EUR", GetPais("Polonia"), "Delantero centro"));
                AltaJugador(new Jugador(652, "23", "Krzysztof Piatek", DateTime.Parse("1995-07-01"), 1.83, "derecho", 12000000, "EUR", GetPais("Polonia"), "Delantero centro"));
                AltaJugador(new Jugador(653, "7", "Adam Buksa ", DateTime.Parse("1996-07-12"), 1.93, "izquierdo", 9000000, "EUR", GetPais("Polonia"), "Delantero centro"));
                AltaJugador(new Jugador(654, "16", "Karol Swiderski", DateTime.Parse("1997-01-23"), 1.84, "izquierdo", 5000000, "EUR", GetPais("Polonia"), "Delantero centro"));
                AltaJugador(new Jugador(655, "NDF", "Bechir Ben Said", DateTime.Parse("1994-11-29"), 1.94, "NDF", 850000, "EUR", GetPais("Túnez"), "Portero"));
                AltaJugador(new Jugador(656, "16", "Aymen Dahmen", DateTime.Parse("1997-01-28"), 1.88, "derecho", 850000, "EUR", GetPais("Túnez"), "Portero"));
                AltaJugador(new Jugador(657, "1", "Mohamed Sedki Debchi", DateTime.Parse("1999-10-28"), 1.95, "derecho", 500000, "EUR", GetPais("Túnez"), "Portero"));
                AltaJugador(new Jugador(658, "3", "Montassar Talbi", DateTime.Parse("1998-05-26"), 1.9, "derecho", 1200000, "EUR", GetPais("Túnez"), "Defensa central"));
                AltaJugador(new Jugador(659, "6", "Nader Ghandri", DateTime.Parse("1995-02-18"), 1.96, "derecho", 650000, "EUR", GetPais("Túnez"), "Defensa central"));
                AltaJugador(new Jugador(660, "24", "Alaa Ghram", DateTime.Parse("2001-07-24"), 0, "derecho", 450000, "EUR", GetPais("Túnez"), "Defensa central"));
                AltaJugador(new Jugador(661, "2", "Bilel Ifa", DateTime.Parse("1990-03-09"), 1.85, "derecho", 250000, "EUR", GetPais("Túnez"), "Defensa central"));
                AltaJugador(new Jugador(662, "NDF", "Adam Ben Lamin", DateTime.Parse("2001-06-02"), 1.84, "derecho", 200000, "EUR", GetPais("Túnez"), "Defensa central"));
                AltaJugador(new Jugador(663, "4", "Ali Abdi", DateTime.Parse("1993-12-20"), 1.83, "izquierdo", 1500000, "EUR", GetPais("Túnez"), "Lateral izquierdo"));
                AltaJugador(new Jugador(664, "12", "Ali Maâloul", DateTime.Parse("1990-01-01"), 1.75, "izquierdo", 1500000, "EUR", GetPais("Túnez"), "Lateral izquierdo"));
                AltaJugador(new Jugador(665, "21", "Rami Kaib", DateTime.Parse("1997-05-08"), 1.78, "NDF", 700000, "EUR", GetPais("Túnez"), "Lateral izquierdo"));
                AltaJugador(new Jugador(666, "20", "Mohamed Dräger", DateTime.Parse("1996-06-25"), 1.81, "derecho", 1300000, "EUR", GetPais("Túnez"), "Lateral derecho"));
                AltaJugador(new Jugador(667, "8", "Anis Slimane", DateTime.Parse("2001-03-16"), 1.88, "ambidiestro", 3500000, "EUR", GetPais("Túnez"), "Mediocentro"));
                AltaJugador(new Jugador(668, "14", "Aïssa Laïdouni", DateTime.Parse("1996-12-13"), 1.83, "derecho", 3500000, "EUR", GetPais("Túnez"), "Mediocentro"));
                AltaJugador(new Jugador(669, "15", "Mohamed Ali Ben Romdhane", DateTime.Parse("1999-09-06"), 1.85, "derecho", 2700000, "EUR", GetPais("Túnez"), "Mediocentro"));
                AltaJugador(new Jugador(670, "13", "Ferjani Sassi", DateTime.Parse("1992-03-18"), 1.85, "derecho", 2000000, "EUR", GetPais("Túnez"), "Mediocentro"));
                AltaJugador(new Jugador(671, "10", "Hannibal Mejbri", DateTime.Parse("2003-01-21"), 1.84, "derecho", 6000000, "EUR", GetPais("Túnez"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(672, "18", "Firas Ben Larbi", DateTime.Parse("1996-05-27"), 1.71, "izquierdo", 3000000, "EUR", GetPais("Túnez"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(673, "8", "Mootez Zaddem", DateTime.Parse("2001-01-05"), 1.85, "derecho", 500000, "EUR", GetPais("Túnez"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(674, "23", "Naïm Sliti", DateTime.Parse("1992-07-27"), 1.73, "derecho", 7500000, "EUR", GetPais("Túnez"), "Extremo izquierdo"));
                AltaJugador(new Jugador(675, "7", "Youssef Msakni", DateTime.Parse("1990-10-28"), 1.79, "derecho", 1200000, "EUR", GetPais("Túnez"), "Extremo izquierdo"));
                AltaJugador(new Jugador(676, "NDF", "Elias Achouri", DateTime.Parse("1999-02-10"), 1.77, "derecho", 300000, "EUR", GetPais("Túnez"), "Extremo derecho"));
                AltaJugador(new Jugador(677, "17", "Issam Jebali", DateTime.Parse("1991-12-25"), 1.86, "ambidiestro", 500000, "EUR", GetPais("Túnez"), "Mediapunta"));
                AltaJugador(new Jugador(678, "19", "Seifeddine Jaziri", DateTime.Parse("1993-02-12"), 1.8, "derecho", 1000000, "EUR", GetPais("Túnez"), "Delantero centro"));
                AltaJugador(new Jugador(679, "NDF", "Taha Yassine Khenissi", DateTime.Parse("1992-01-06"), 1.8, "derecho", 500000, "EUR", GetPais("Túnez"), "Delantero centro"));
                AltaJugador(new Jugador(680, "1", "Bono", DateTime.Parse("1991-04-05"), 1.92, "izquierdo", 18000000, "EUR", GetPais("Marruecos"), "Portero"));
                AltaJugador(new Jugador(681, "12", "Munir", DateTime.Parse("1989-05-10"), 1.9, "derecho", 1500000, "EUR", GetPais("Marruecos"), "Portero"));
                AltaJugador(new Jugador(682, "NDF", "Anas Zniti", DateTime.Parse("1988-10-28"), 1.82, "derecho", 1200000, "EUR", GetPais("Marruecos"), "Portero"));
                AltaJugador(new Jugador(683, "5", "Nayef Aguerd ", DateTime.Parse("1996-03-30"), 1.88, "izquierdo", 12000000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(684, "6", "Romain Saïss", DateTime.Parse("1990-03-26"), 1.88, "izquierdo", 8000000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(685, "NDF", "Achraf Dari", DateTime.Parse("1999-05-06"), 1.88, "derecho", 1800000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(686, "24", "Samy Mmaee", DateTime.Parse("1996-09-08"), 1.88, "derecho", 1300000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(687, "NDF", "Jawad El Yamiq", DateTime.Parse("1992-02-29"), 1.93, "derecho", 1200000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(688, "18", "Soufiane Chakla", DateTime.Parse("1993-09-02"), 1.88, "derecho", 800000, "EUR", GetPais("Marruecos"), "Defensa central"));
                AltaJugador(new Jugador(689, "3", "Adam Masina", DateTime.Parse("1994-01-02"), 1.91, "izquierdo", 3000000, "EUR", GetPais("Marruecos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(690, "NDF", "Yahia Attiyat Allah", DateTime.Parse("1995-03-02"), 1.76, "izquierdo", 1000000, "EUR", GetPais("Marruecos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(691, "2", "Achraf Hakimi", DateTime.Parse("1998-11-04"), 1.81, "derecho", 65000000, "EUR", GetPais("Marruecos"), "Lateral derecho"));
                AltaJugador(new Jugador(692, "NDF", "Noussair Mazraoui", DateTime.Parse("1997-11-14"), 1.83, "derecho", 20000000, "EUR", GetPais("Marruecos"), "Lateral derecho"));
                AltaJugador(new Jugador(693, "20", "Sofiane Alakouch", DateTime.Parse("1998-07-29"), 1.75, "derecho", 1500000, "EUR", GetPais("Marruecos"), "Lateral derecho"));
                AltaJugador(new Jugador(694, "NDF", "Mohamed Chibi", DateTime.Parse("1993-01-21"), 1.79, "derecho", 825000, "EUR", GetPais("Marruecos"), "Lateral derecho"));
                AltaJugador(new Jugador(695, "4", "Sofyan Amrabat", DateTime.Parse("1996-08-21"), 1.85, "derecho", 10000000, "EUR", GetPais("Marruecos"), "Pivote"));
                AltaJugador(new Jugador(696, "NDF", "Yahya Jabrane", DateTime.Parse("1991-06-18"), 1.87, "derecho", 1700000, "EUR", GetPais("Marruecos"), "Pivote"));
                AltaJugador(new Jugador(697, "7", "Imrân Louza", DateTime.Parse("1999-05-01"), 1.78, "izquierdo", 9000000, "EUR", GetPais("Marruecos"), "Mediocentro"));
                AltaJugador(new Jugador(698, "8", "Azzedine Ounahi", DateTime.Parse("2000-04-19"), 1.82, "derecho", 3000000, "EUR", GetPais("Marruecos"), "Mediocentro"));
                AltaJugador(new Jugador(699, "NDF", "Adel Taarabt", DateTime.Parse("1989-05-24"), 1.78, "derecho", 2500000, "EUR", GetPais("Marruecos"), "Mediocentro"));
                AltaJugador(new Jugador(700, "11", "Fayçal Fajr", DateTime.Parse("1988-08-01"), 1.79, "derecho", 1800000, "EUR", GetPais("Marruecos"), "Mediocentro"));
                AltaJugador(new Jugador(701, "NDF", "Amine Harit", DateTime.Parse("1997-06-18"), 1.8, "derecho", 10000000, "EUR", GetPais("Marruecos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(702, "15", "Selim Amallah", DateTime.Parse("1996-11-15"), 1.86, "derecho", 5000000, "EUR", GetPais("Marruecos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(703, "13", "Ilias Chair", DateTime.Parse("1997-10-30"), 1.72, "derecho", 4000000, "EUR", GetPais("Marruecos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(704, "NDF", "Aymen Barkok", DateTime.Parse("1998-05-21"), 1.89, "derecho", 1800000, "EUR", GetPais("Marruecos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(705, "28", "Tarik Tissoudali ", DateTime.Parse("1993-04-02"), 1.82, "derecho", 7500000, "EUR", GetPais("Marruecos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(706, "NDF", "Soufiane Rahimi", DateTime.Parse("1996-06-02"), 1.8, "derecho", 4500000, "EUR", GetPais("Marruecos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(707, "NDF", "Zakaria Aboukhlal", DateTime.Parse("2000-02-18"), 1.79, "ambidiestro", 2000000, "EUR", GetPais("Marruecos"), "Extremo derecho"));
                AltaJugador(new Jugador(708, "19", "Youssef En-Nesyri", DateTime.Parse("1997-06-01"), 1.89, "izquierdo", 25000000, "EUR", GetPais("Marruecos"), "Delantero centro"));
                AltaJugador(new Jugador(709, "10", "Munir El Haddadi", DateTime.Parse("1995-09-01"), 1.75, "izquierdo", 8000000, "EUR", GetPais("Marruecos"), "Delantero centro"));
                AltaJugador(new Jugador(710, "9", "Ayoub El Kaabi", DateTime.Parse("1993-06-25"), 1.82, "izquierdo", 5000000, "EUR", GetPais("Marruecos"), "Delantero centro"));
                AltaJugador(new Jugador(711, "24", "André Onana", DateTime.Parse("1996-04-02"), 1.9, "derecho", 12000000, "EUR", GetPais("Camerún"), "Portero"));
                AltaJugador(new Jugador(712, "16", "Devis Epassy", DateTime.Parse("1993-02-02"), 1.89, "derecho", 600000, "EUR", GetPais("Camerún"), "Portero"));
                AltaJugador(new Jugador(713, "23", "Simon Omossola", DateTime.Parse("1998-05-05"), 1.89, "derecho", 400000, "EUR", GetPais("Camerún"), "Portero"));
                AltaJugador(new Jugador(714, "21", "Jean-Charles Castelletto", DateTime.Parse("1995-01-26"), 1.86, "ambidiestro", 4000000, "EUR", GetPais("Camerún"), "Defensa central"));
                AltaJugador(new Jugador(715, "5", "Michael Ngadeu", DateTime.Parse("1990-11-23"), 1.9, "derecho", 3500000, "EUR", GetPais("Camerún"), "Defensa central"));
                AltaJugador(new Jugador(716, "NDF", "Christopher Wooh", DateTime.Parse("2001-09-18"), 1.91, "derecho", 3000000, "EUR", GetPais("Camerún"), "Defensa central"));
                AltaJugador(new Jugador(717, "NDF", "Duplexe Tchamba", DateTime.Parse("1998-07-10"), 1.91, "izquierdo", 400000, "EUR", GetPais("Camerún"), "Defensa central"));
                AltaJugador(new Jugador(718, "25", "Nouhou", DateTime.Parse("1997-06-23"), 1.78, "NDF", 2500000, "EUR", GetPais("Camerún"), "Lateral izquierdo"));
                AltaJugador(new Jugador(719, "6", "Ambroise Oyongo", DateTime.Parse("1991-06-22"), 1.76, "izquierdo", 1800000, "EUR", GetPais("Camerún"), "Lateral izquierdo"));
                AltaJugador(new Jugador(720, "17", "Olivier Mbaizo", DateTime.Parse("1997-08-15"), 1.78, "NDF", 1500000, "EUR", GetPais("Camerún"), "Lateral derecho"));
                AltaJugador(new Jugador(721, "19", "Collins Fai", DateTime.Parse("1992-08-13"), 1.65, "derecho", 900000, "EUR", GetPais("Camerún"), "Lateral derecho"));
                AltaJugador(new Jugador(722, "8", "André Zambo Anguissa", DateTime.Parse("1995-11-16"), 1.84, "derecho", 30000000, "EUR", GetPais("Camerún"), "Pivote"));
                AltaJugador(new Jugador(723, "18", "Martin Hongla", DateTime.Parse("1998-03-16"), 1.81, "derecho", 4000000, "EUR", GetPais("Camerún"), "Pivote"));
                AltaJugador(new Jugador(724, "14", "Samuel Oum Gouet", DateTime.Parse("1997-12-14"), 1.85, "derecho", 2000000, "EUR", GetPais("Camerún"), "Pivote"));
                AltaJugador(new Jugador(725, "8", "Gaël Ondoua", DateTime.Parse("1995-11-04"), 1.85, "izquierdo", 1000000, "EUR", GetPais("Camerún"), "Pivote"));
                AltaJugador(new Jugador(726, "NDF", "Olivier Ntcham", DateTime.Parse("1996-02-09"), 1.8, "derecho", 3000000, "EUR", GetPais("Camerún"), "Mediocentro"));
                AltaJugador(new Jugador(727, "15", "Pierre Kunde", DateTime.Parse("1995-07-26"), 1.8, "derecho", 2000000, "EUR", GetPais("Camerún"), "Mediocentro"));
                AltaJugador(new Jugador(728, "12", "Karl Toko Ekambi", DateTime.Parse("1992-09-14"), 1.85, "derecho", 15000000, "EUR", GetPais("Camerún"), "Extremo izquierdo"));
                AltaJugador(new Jugador(729, "3", "Nicolas Moumi Ngamaleu", DateTime.Parse("1994-07-09"), 1.81, "derecho", 4700000, "EUR", GetPais("Camerún"), "Extremo izquierdo"));
                AltaJugador(new Jugador(730, "NDF", "Georges-Kevin N'Koudou", DateTime.Parse("1995-02-13"), 1.72, "derecho", 4200000, "EUR", GetPais("Camerún"), "Extremo izquierdo"));
                AltaJugador(new Jugador(731, "10", "Vincent Aboubakar", DateTime.Parse("1992-01-22"), 1.84, "derecho", 7000000, "EUR", GetPais("Camerún"), "Delantero centro"));
                AltaJugador(new Jugador(732, "20", "Ignatius Ganago", DateTime.Parse("1999-02-16"), 1.79, "derecho", 5000000, "EUR", GetPais("Camerún"), "Delantero centro"));
                AltaJugador(new Jugador(733, "13", "Eric Maxim Choupo-Moting", DateTime.Parse("1989-03-23"), 1.91, "derecho", 3000000, "EUR", GetPais("Camerún"), "Delantero centro"));
                AltaJugador(new Jugador(734, "NDF", "Léandre Tawamba", DateTime.Parse("1989-12-20"), 1.89, "derecho", 2000000, "EUR", GetPais("Camerún"), "Delantero centro"));
                AltaJugador(new Jugador(735, "9", "Kévin Soni", DateTime.Parse("1998-04-17"), 1.83, "izquierdo", 800000, "EUR", GetPais("Camerún"), "Delantero centro"));
                AltaJugador(new Jugador(736, "1", "Carlos Acevedo", DateTime.Parse("1996-04-19"), 1.85, "derecho", 5000000, "EUR", GetPais("México"), "Portero"));
                AltaJugador(new Jugador(737, "12", "Rodolfo Cota", DateTime.Parse("1987-07-03"), 1.83, "derecho", 1500000, "EUR", GetPais("México"), "Portero"));
                AltaJugador(new Jugador(738, "13", "David Ochoa", DateTime.Parse("2001-01-16"), 1.88, "NDF", 800000, "EUR", GetPais("México"), "Portero"));
                AltaJugador(new Jugador(739, "3", "Jesús Angulo", DateTime.Parse("1998-01-30"), 1.78, "izquierdo", 6000000, "EUR", GetPais("México"), "Defensa central"));
                AltaJugador(new Jugador(740, "15", "Israel Reyes", DateTime.Parse("2000-05-23"), 1.79, "derecho", 3500000, "EUR", GetPais("México"), "Defensa central"));
                AltaJugador(new Jugador(741, "4", "Julio Domínguez", DateTime.Parse("1987-11-08"), 1.77, "derecho", 1400000, "EUR", GetPais("México"), "Defensa central"));
                AltaJugador(new Jugador(742, "19", "Érick Aguirre", DateTime.Parse("1997-02-23"), 1.71, "derecho", 5000000, "EUR", GetPais("México"), "Lateral izquierdo"));
                AltaJugador(new Jugador(743, "23", "Jesús Gallardo", DateTime.Parse("1994-08-15"), 1.76, "izquierdo", 3000000, "EUR", GetPais("México"), "Lateral izquierdo"));
                AltaJugador(new Jugador(744, "2", "Kevin Álvarez", DateTime.Parse("1999-01-15"), 1.76, "derecho", 5000000, "EUR", GetPais("México"), "Lateral derecho"));
                AltaJugador(new Jugador(745, "5", "Julián Araujo", DateTime.Parse("2001-08-13"), 1.75, "derecho", 4500000, "EUR", GetPais("México"), "Lateral derecho"));
                AltaJugador(new Jugador(746, "18", "Luis Chávez", DateTime.Parse("1996-01-15"), 1.78, "izquierdo", 5000000, "EUR", GetPais("México"), "Pivote"));
                AltaJugador(new Jugador(747, "18", "Erik Lira", DateTime.Parse("2000-05-08"), 1.72, "derecho", 4000000, "EUR", GetPais("México"), "Pivote"));
                AltaJugador(new Jugador(748, "6", "Érick Sánchez", DateTime.Parse("1999-09-27"), 1.67, "derecho", 7000000, "EUR", GetPais("México"), "Mediocentro"));
                AltaJugador(new Jugador(749, "7", "Luis Romo", DateTime.Parse("1995-06-05"), 1.84, "derecho", 7000000, "EUR", GetPais("México"), "Mediocentro"));
                AltaJugador(new Jugador(750, "16", "Fernando Beltrán", DateTime.Parse("1998-05-08"), 1.68, "derecho", 4500000, "EUR", GetPais("México"), "Mediocentro"));
                AltaJugador(new Jugador(751, "10", "Orbelín Pineda", DateTime.Parse("1996-03-24"), 1.69, "derecho", 4000000, "EUR", GetPais("México"), "Mediocentro"));
                AltaJugador(new Jugador(752, "8", "Sebastián Córdova", DateTime.Parse("1997-06-12"), 1.74, "NDF", 5000000, "EUR", GetPais("México"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(753, "20", "Rodolfo Pizarro", DateTime.Parse("1994-02-15"), 1.75, "derecho", 4000000, "EUR", GetPais("México"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(754, "17", "Marcelo Flores", DateTime.Parse("2003-10-01"), 1.64, "derecho", 1500000, "EUR", GetPais("México"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(755, "22", "Uriel Antuna", DateTime.Parse("1997-08-21"), 1.74, "derecho", 5000000, "EUR", GetPais("México"), "Extremo derecho"));
                AltaJugador(new Jugador(756, "11", "Diego Lainez", DateTime.Parse("2000-06-09"), 1.68, "izquierdo", 2500000, "EUR", GetPais("México"), "Extremo derecho"));
                AltaJugador(new Jugador(757, "9", "Santiago Giménez", DateTime.Parse("2001-04-18"), 1.83, "izquierdo", 4000000, "EUR", GetPais("México"), "Delantero centro"));
                AltaJugador(new Jugador(758, "21", "Henry Martín", DateTime.Parse("1992-11-18"), 1.77, "derecho", 3500000, "EUR", GetPais("México"), "Delantero centro"));
                AltaJugador(new Jugador(759, "1", "Matt Turner", DateTime.Parse("1994-06-24"), 1.91, "derecho", 5000000, "EUR", GetPais("Estados Unidos"), "Portero"));
                AltaJugador(new Jugador(760, "NDF", "Sean Johnson", DateTime.Parse("1989-05-31"), 1.9, "derecho", 1500000, "EUR", GetPais("Estados Unidos"), "Portero"));
                AltaJugador(new Jugador(761, "18", "Ethan Horvath", DateTime.Parse("1995-06-09"), 1.95, "derecho", 800000, "EUR", GetPais("Estados Unidos"), "Portero"));
                AltaJugador(new Jugador(762, "20", "Cameron Carter-Vickers", DateTime.Parse("1997-12-31"), 1.83, "derecho", 7000000, "EUR", GetPais("Estados Unidos"), "Defensa central"));
                AltaJugador(new Jugador(763, "3", "Walker Zimmerman", DateTime.Parse("1993-05-19"), 1.9, "derecho", 3500000, "EUR", GetPais("Estados Unidos"), "Defensa central"));
                AltaJugador(new Jugador(764, "12", "Erik Palmer-Brown", DateTime.Parse("1997-04-24"), 1.86, "derecho", 3000000, "EUR", GetPais("Estados Unidos"), "Defensa central"));
                AltaJugador(new Jugador(765, "15", "Aaron Long", DateTime.Parse("1992-10-12"), 1.86, "derecho", 3000000, "EUR", GetPais("Estados Unidos"), "Defensa central"));
                AltaJugador(new Jugador(766, "5", "Antonee Robinson", DateTime.Parse("1997-08-08"), 1.83, "izquierdo", 8000000, "EUR", GetPais("Estados Unidos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(767, "24", "George Bello", DateTime.Parse("2002-01-22"), 1.7, "izquierdo", 3000000, "EUR", GetPais("Estados Unidos"), "Lateral izquierdo"));
                AltaJugador(new Jugador(768, "29", "Joe Scally", DateTime.Parse("2002-12-31"), 1.84, "derecho", 8000000, "EUR", GetPais("Estados Unidos"), "Lateral derecho"));
                AltaJugador(new Jugador(769, "22", "Reggie Cannon", DateTime.Parse("1998-06-11"), 1.8, "derecho", 4000000, "EUR", GetPais("Estados Unidos"), "Lateral derecho"));
                AltaJugador(new Jugador(770, "2", "DeAndre Yedlin", DateTime.Parse("1993-07-09"), 1.73, "derecho", 2500000, "EUR", GetPais("Estados Unidos"), "Lateral derecho"));
                AltaJugador(new Jugador(771, "4", "Tyler Adams", DateTime.Parse("1999-02-14"), 1.75, "derecho", 17000000, "EUR", GetPais("Estados Unidos"), "Pivote"));
                AltaJugador(new Jugador(772, "23", "Kellyn Acosta", DateTime.Parse("1995-07-24"), 1.77, "derecho", 3000000, "EUR", GetPais("Estados Unidos"), "Pivote"));
                AltaJugador(new Jugador(773, "8", "Weston McKennie ", DateTime.Parse("1998-08-28"), 1.77, "derecho", 25000000, "EUR", GetPais("Estados Unidos"), "Mediocentro"));
                AltaJugador(new Jugador(774, "6", "Yunus Musah", DateTime.Parse("2002-11-29"), 1.78, "derecho", 15000000, "EUR", GetPais("Estados Unidos"), "Mediocentro"));
                AltaJugador(new Jugador(775, "16", "Cristian Roldán", DateTime.Parse("1995-06-03"), 1.73, "NDF", 5500000, "EUR", GetPais("Estados Unidos"), "Mediocentro"));
                AltaJugador(new Jugador(776, "14", "Luca de la Torre", DateTime.Parse("1998-05-23"), 1.77, "derecho", 850000, "EUR", GetPais("Estados Unidos"), "Mediocentro"));
                AltaJugador(new Jugador(777, "11", "Brenden Aaronson", DateTime.Parse("2000-10-22"), 1.78, "derecho", 25000000, "EUR", GetPais("Estados Unidos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(778, "17", "Malik Tillman", DateTime.Parse("2002-05-28"), 1.87, "derecho", 1500000, "EUR", GetPais("Estados Unidos"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(779, "13", "Jordan Morris", DateTime.Parse("1994-10-26"), 1.81, "derecho", 5000000, "EUR", GetPais("Estados Unidos"), "Extremo izquierdo"));
                AltaJugador(new Jugador(780, "10", "Christian Pulisic", DateTime.Parse("1998-09-18"), 1.77, "derecho", 42000000, "EUR", GetPais("Estados Unidos"), "Extremo derecho"));
                AltaJugador(new Jugador(781, "7", "Paul Arriola", DateTime.Parse("1995-02-05"), 1.67, "derecho", 3000000, "EUR", GetPais("Estados Unidos"), "Extremo derecho"));
                AltaJugador(new Jugador(782, "21", "Timothy Weah", DateTime.Parse("2000-02-22"), 1.85, "derecho", 12000000, "EUR", GetPais("Estados Unidos"), "Delantero centro"));
                AltaJugador(new Jugador(783, "9", "Jesús Ferreira", DateTime.Parse("2000-12-24"), 1.73, "NDF", 6000000, "EUR", GetPais("Estados Unidos"), "Delantero centro"));
                AltaJugador(new Jugador(784, "19", "Haji Wright", DateTime.Parse("1998-03-27"), 1.93, "derecho", 2500000, "EUR", GetPais("Estados Unidos"), "Delantero centro"));
                AltaJugador(new Jugador(785, "12", "Danny Ward", DateTime.Parse("1993-06-22"), 1.89, "derecho", 6000000, "EUR", GetPais("Gales"), "Portero"));
                AltaJugador(new Jugador(786, "21", "Adam Davies", DateTime.Parse("1992-07-17"), 1.85, "derecho", 700000, "EUR", GetPais("Gales"), "Portero"));
                AltaJugador(new Jugador(787, "1", "Wayne Hennessey", DateTime.Parse("1987-01-24"), 1.97, "derecho", 500000, "EUR", GetPais("Gales"), "Portero"));
                AltaJugador(new Jugador(788, "NDF", "Tom King", DateTime.Parse("1995-03-09"), 1.94, "derecho", 200000, "EUR", GetPais("Gales"), "Portero"));
                AltaJugador(new Jugador(789, "15", "Ethan Ampadu", DateTime.Parse("2000-09-14"), 1.83, "derecho", 13000000, "EUR", GetPais("Gales"), "Defensa central"));
                AltaJugador(new Jugador(790, "6", "Joe Rodon", DateTime.Parse("1997-10-22"), 1.92, "derecho", 8000000, "EUR", GetPais("Gales"), "Defensa central"));
                AltaJugador(new Jugador(791, "5", "Chris Mepham", DateTime.Parse("1997-11-05"), 1.9, "derecho", 4000000, "EUR", GetPais("Gales"), "Defensa central"));
                AltaJugador(new Jugador(792, "NDF", "Oliver Denham", DateTime.Parse("2002-05-04"), 1.82, "derecho", 0, "EUR", GetPais("Gales"), "Defensa central"));
                AltaJugador(new Jugador(793, "4", "Ben Davies", DateTime.Parse("1993-04-24"), 1.81, "izquierdo", 20000000, "EUR", GetPais("Gales"), "Lateral izquierdo"));
                AltaJugador(new Jugador(794, "17", "Rhys Norrington-Davies", DateTime.Parse("1999-04-22"), 1.81, "izquierdo", 1500000, "EUR", GetPais("Gales"), "Lateral izquierdo"));
                AltaJugador(new Jugador(795, "3", "Neco Williams", DateTime.Parse("2001-04-13"), 1.77, "derecho", 8000000, "EUR", GetPais("Gales"), "Lateral derecho"));
                AltaJugador(new Jugador(796, "14", "Connor Roberts", DateTime.Parse("1995-09-23"), 1.75, "derecho", 2500000, "EUR", GetPais("Gales"), "Lateral derecho"));
                AltaJugador(new Jugador(797, "2", "Chris Gunter", DateTime.Parse("1989-07-21"), 1.8, "derecho", 800000, "EUR", GetPais("Gales"), "Lateral derecho"));
                AltaJugador(new Jugador(798, "NDF", "Matt Smith", DateTime.Parse("1999-11-22"), 1.75, "derecho", 450000, "EUR", GetPais("Gales"), "Pivote"));
                AltaJugador(new Jugador(799, "10", "Aaron Ramsey", DateTime.Parse("1990-12-26"), 1.82, "derecho", 3000000, "EUR", GetPais("Gales"), "Mediocentro"));
                AltaJugador(new Jugador(800, "7", "Joe Allen", DateTime.Parse("1990-03-14"), 1.68, "derecho", 1200000, "EUR", GetPais("Gales"), "Mediocentro"));
                AltaJugador(new Jugador(801, "NDF", "Dylan Levitt", DateTime.Parse("2000-11-17"), 1.8, "derecho", 650000, "EUR", GetPais("Gales"), "Mediocentro"));
                AltaJugador(new Jugador(802, "16", "Joe Morrell ", DateTime.Parse("1997-01-03"), 1.85, "derecho", 400000, "EUR", GetPais("Gales"), "Mediocentro"));
                AltaJugador(new Jugador(803, "NDF", "Wes Burns", DateTime.Parse("1994-11-23"), 1.73, "derecho", 800000, "EUR", GetPais("Gales"), "Interior derecho"));
                AltaJugador(new Jugador(804, "NDF", "Rubin Colwill", DateTime.Parse("2002-04-27"), 1.89, "NDF", 1500000, "EUR", GetPais("Gales"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(805, "18", "Jonathan Williams", DateTime.Parse("1993-10-09"), 1.68, "izquierdo", 800000, "EUR", GetPais("Gales"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(806, "20", "Daniel James", DateTime.Parse("1997-11-10"), 1.7, "derecho", 18000000, "EUR", GetPais("Gales"), "Extremo derecho"));
                AltaJugador(new Jugador(807, "8", "Harry Wilson ", DateTime.Parse("1997-03-22"), 1.73, "izquierdo", 17000000, "EUR", GetPais("Gales"), "Extremo derecho"));
                AltaJugador(new Jugador(808, "23", "Rabbi Matondo", DateTime.Parse("2000-09-09"), 1.75, "derecho", 4000000, "EUR", GetPais("Gales"), "Extremo derecho"));
                AltaJugador(new Jugador(809, "11", "Gareth Bale ", DateTime.Parse("1989-07-16"), 1.85, "izquierdo", 3000000, "EUR", GetPais("Gales"), "Extremo derecho"));
                AltaJugador(new Jugador(810, "22", "Sorba Thomas", DateTime.Parse("1999-01-25"), 1.85, "derecho", 2000000, "EUR", GetPais("Gales"), "Extremo derecho"));
                AltaJugador(new Jugador(811, "9", "Brennan Johnson", DateTime.Parse("2001-05-23"), 1.79, "derecho", 15000000, "EUR", GetPais("Gales"), "Mediapunta"));
                AltaJugador(new Jugador(812, "13", "Kieffer Moore", DateTime.Parse("1992-08-08"), 1.95, "derecho", 5000000, "EUR", GetPais("Gales"), "Delantero centro"));
                AltaJugador(new Jugador(813, "19", "Mark Harris", DateTime.Parse("1998-12-29"), 1.82, "derecho", 1200000, "EUR", GetPais("Gales"), "Delantero centro"));
                AltaJugador(new Jugador(814, "1", "Mathew Ryan", DateTime.Parse("1992-04-08"), 1.84, "derecho", 5000000, "EUR", GetPais("Australia"), "Portero"));
                AltaJugador(new Jugador(815, "12", "Andrew Redmayne", DateTime.Parse("1989-01-13"), 1.94, "NDF", 700000, "EUR", GetPais("Australia"), "Portero"));
                AltaJugador(new Jugador(816, "18", "Danny Vukovic", DateTime.Parse("1985-03-27"), 1.87, "derecho", 150000, "EUR", GetPais("Australia"), "Portero"));
                AltaJugador(new Jugador(817, "19", "Harry Souttar ", DateTime.Parse("1998-10-22"), 2.01, "derecho", 5000000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(818, "2", "Milos Degenek", DateTime.Parse("1994-04-28"), 1.87, "derecho", 2500000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(819, "NDF", "Bailey Wright", DateTime.Parse("1992-07-28"), 1.86, "derecho", 800000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(820, "20", "Trent Sainsbury", DateTime.Parse("1992-01-05"), 1.84, "derecho", 750000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(821, "20", "Kye Rowles", DateTime.Parse("1998-06-24"), 1.83, "izquierdo", 600000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(822, "23", "Gianni Stensness ", DateTime.Parse("1999-02-07"), 1.85, "derecho", 600000, "EUR", GetPais("Australia"), "Defensa central"));
                AltaJugador(new Jugador(823, "16", "Aziz Behich", DateTime.Parse("1990-12-16"), 1.7, "izquierdo", 775000, "EUR", GetPais("Australia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(824, "NDF", "Jason Davidson", DateTime.Parse("1991-06-29"), 1.82, "izquierdo", 500000, "EUR", GetPais("Australia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(825, "NDF", "Joel King", DateTime.Parse("2000-10-30"), 1.8, "izquierdo", 500000, "EUR", GetPais("Australia"), "Lateral izquierdo"));
                AltaJugador(new Jugador(826, "2", "Nathaniel Atkinson", DateTime.Parse("1999-06-13"), 1.81, "derecho", 1000000, "EUR", GetPais("Australia"), "Lateral derecho"));
                AltaJugador(new Jugador(827, "5", "Fran Karacic", DateTime.Parse("1996-05-12"), 1.85, "derecho", 1000000, "EUR", GetPais("Australia"), "Lateral derecho"));
                AltaJugador(new Jugador(828, "4", "Rhyan Grant", DateTime.Parse("1991-02-26"), 1.74, "derecho", 750000, "EUR", GetPais("Australia"), "Lateral derecho"));
                AltaJugador(new Jugador(829, "8", "James Jeggo", DateTime.Parse("1992-02-12"), 1.78, "derecho", 800000, "EUR", GetPais("Australia"), "Pivote"));
                AltaJugador(new Jugador(830, "8", "Connor Metcalfe", DateTime.Parse("1999-11-05"), 1.83, "izquierdo", 800000, "EUR", GetPais("Australia"), "Pivote"));
                AltaJugador(new Jugador(831, "NDF", "Kenny Dougall", DateTime.Parse("1993-05-07"), 1.82, "derecho", 600000, "EUR", GetPais("Australia"), "Pivote"));
                AltaJugador(new Jugador(832, "13", "Aaron Mooy", DateTime.Parse("1990-09-15"), 1.74, "derecho", 5000000, "EUR", GetPais("Australia"), "Mediocentro"));
                AltaJugador(new Jugador(833, "10", "Ajdin Hrustic", DateTime.Parse("1996-07-05"), 1.83, "izquierdo", 3000000, "EUR", GetPais("Australia"), "Mediocentro"));
                AltaJugador(new Jugador(834, "NDF", "Riley McGree", DateTime.Parse("1998-11-02"), 1.78, "izquierdo", 2500000, "EUR", GetPais("Australia"), "Mediocentro"));
                AltaJugador(new Jugador(835, "22", "Jackson Irvine", DateTime.Parse("1993-03-07"), 1.89, "derecho", 2000000, "EUR", GetPais("Australia"), "Mediocentro"));
                AltaJugador(new Jugador(836, "23", "Tom Rogic", DateTime.Parse("1992-12-16"), 1.88, "derecho", 1700000, "EUR", GetPais("Australia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(837, "17", "Denis Genreau", DateTime.Parse("1999-05-21"), 1.75, "derecho", 1200000, "EUR", GetPais("Australia"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(838, "NDF", "Awer Mabil", DateTime.Parse("1995-09-15"), 1.79, "derecho", 1800000, "EUR", GetPais("Australia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(839, "19", "Craig Goodwin", DateTime.Parse("1991-12-16"), 1.8, "izquierdo", 1200000, "EUR", GetPais("Australia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(840, "7", "Mathew Leckie", DateTime.Parse("1991-02-04"), 1.81, "derecho", 1000000, "EUR", GetPais("Australia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(841, "NDF", "Ben Folami", DateTime.Parse("1999-06-08"), 1.8, "ambidiestro", 500000, "EUR", GetPais("Australia"), "Extremo izquierdo"));
                AltaJugador(new Jugador(842, "6", "Martin Boyle", DateTime.Parse("1993-04-25"), 1.72, "derecho", 2500000, "EUR", GetPais("Australia"), "Extremo derecho"));
                AltaJugador(new Jugador(843, "21", "Marco Tilio", DateTime.Parse("2001-08-23"), 1.7, "izquierdo", 900000, "EUR", GetPais("Australia"), "Extremo derecho"));
                AltaJugador(new Jugador(844, "NDF", "Nick D'Agostino", DateTime.Parse("1998-02-25"), 1.75, "derecho", 800000, "EUR", GetPais("Australia"), "Extremo derecho"));
                AltaJugador(new Jugador(845, "9", "Jamie Maclaren", DateTime.Parse("1993-07-29"), 1.79, "derecho", 1500000, "EUR", GetPais("Australia"), "Delantero centro"));
                AltaJugador(new Jugador(846, "NDF", "Adam Taggart", DateTime.Parse("1993-06-02"), 1.77, "derecho", 800000, "EUR", GetPais("Australia"), "Delantero centro"));
                AltaJugador(new Jugador(847, "15", "Mitchell Duke", DateTime.Parse("1991-01-18"), 1.86, "derecho", 700000, "EUR", GetPais("Australia"), "Delantero centro"));
                AltaJugador(new Jugador(848, "14", "Bruno Fornaroli ", DateTime.Parse("1987-09-07"), 1.74, "derecho", 600000, "EUR", GetPais("Australia"), "Delantero centro"));
                AltaJugador(new Jugador(849, "1", "Keylor Navas", DateTime.Parse("1986-12-15"), 1.85, "derecho", 8000000, "EUR", GetPais("Costa Rica"), "Portero"));
                AltaJugador(new Jugador(850, "18", "Aaron Cruz", DateTime.Parse("1991-05-25"), 1.87, "derecho", 275000, "EUR", GetPais("Costa Rica"), "Portero"));
                AltaJugador(new Jugador(851, "23", "Leonel Moreira", DateTime.Parse("1990-04-02"), 1.76, "derecho", 275000, "EUR", GetPais("Costa Rica"), "Portero"));
                AltaJugador(new Jugador(852, "3", "Juan Pablo Vargas", DateTime.Parse("1995-06-06"), 1.92, "izquierdo", 1000000, "EUR", GetPais("Costa Rica"), "Defensa central"));
                AltaJugador(new Jugador(853, "15", "Francisco Calvo", DateTime.Parse("1992-07-08"), 1.8, "izquierdo", 1000000, "EUR", GetPais("Costa Rica"), "Defensa central"));
                AltaJugador(new Jugador(854, "6", "Óscar Duarte", DateTime.Parse("1989-06-03"), 1.86, "derecho", 800000, "EUR", GetPais("Costa Rica"), "Defensa central"));
                AltaJugador(new Jugador(855, "19", "Kendall Waston", DateTime.Parse("1988-01-01"), 1.96, "derecho", 225000, "EUR", GetPais("Costa Rica"), "Defensa central"));
                AltaJugador(new Jugador(856, "16", "Ian Lawrence", DateTime.Parse("2002-05-28"), 1.79, "izquierdo", 250000, "EUR", GetPais("Costa Rica"), "Lateral izquierdo"));
                AltaJugador(new Jugador(857, "8", "Bryan Oviedo", DateTime.Parse("1990-02-18"), 1.72, "izquierdo", 200000, "EUR", GetPais("Costa Rica"), "Lateral izquierdo"));
                AltaJugador(new Jugador(858, "4", "Keysher Fuller ", DateTime.Parse("1994-07-12"), 1.83, "derecho", 225000, "EUR", GetPais("Costa Rica"), "Lateral derecho"));
                AltaJugador(new Jugador(859, "22", "Carlos Martínez", DateTime.Parse("1999-03-30"), 1.76, "derecho", 225000, "EUR", GetPais("Costa Rica"), "Lateral derecho"));
                AltaJugador(new Jugador(860, "14", "Orlando Galo", DateTime.Parse("2000-08-11"), 1.76, "derecho", 300000, "EUR", GetPais("Costa Rica"), "Pivote"));
                AltaJugador(new Jugador(861, "20", "Daniel Chacón", DateTime.Parse("2001-04-11"), 1.83, "derecho", 300000, "EUR", GetPais("Costa Rica"), "Pivote"));
                AltaJugador(new Jugador(862, "17", "Yeltsin Tejeda", DateTime.Parse("1992-03-17"), 1.79, "derecho", 275000, "EUR", GetPais("Costa Rica"), "Pivote"));
                AltaJugador(new Jugador(863, "5", "Celso Borges", DateTime.Parse("1988-05-27"), 1.82, "derecho", 225000, "EUR", GetPais("Costa Rica"), "Mediocentro"));
                AltaJugador(new Jugador(864, "13", "Gerson Torres", DateTime.Parse("1997-08-28"), 1.76, "izquierdo", 325000, "EUR", GetPais("Costa Rica"), "Interior derecho"));
                AltaJugador(new Jugador(865, "21", "Brandon Aguilera", DateTime.Parse("2003-06-28"), 1.81, "izquierdo", 325000, "EUR", GetPais("Costa Rica"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(866, "10", "Bryan Ruiz", DateTime.Parse("1985-08-18"), 1.87, "izquierdo", 200000, "EUR", GetPais("Costa Rica"), "Mediocentro ofensivo"));
                AltaJugador(new Jugador(867, "9", "Jewison Bennette", DateTime.Parse("2004-06-15"), 1.75, "izquierdo", 275000, "EUR", GetPais("Costa Rica"), "Extremo izquierdo"));
                AltaJugador(new Jugador(868, "12", "Joel Campbell", DateTime.Parse("1992-06-26"), 1.78, "izquierdo", 2500000, "EUR", GetPais("Costa Rica"), "Extremo derecho"));
                AltaJugador(new Jugador(869, "2", "Carlos Mora", DateTime.Parse("2001-03-18"), 1.78, "derecho", 275000, "EUR", GetPais("Costa Rica"), "Extremo derecho"));
                AltaJugador(new Jugador(870, "7", "Anthony Contreras", DateTime.Parse("2000-01-29"), 1.8, "derecho", 350000, "EUR", GetPais("Costa Rica"), "Delantero centro"));
                AltaJugador(new Jugador(871, "11", "Johan Venegas", DateTime.Parse("1988-11-27"), 1.83, "derecho", 325000, "EUR", GetPais("Costa Rica"), "Delantero centro"));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Selecciones
        private void PrecargarSelecciones()
        {
            //Contamos con países y jugadores, la seleccion debe armar para cada pais una seleccion
            foreach (Pais p in _paises)
            {
                // 1 - Se crea una seleccion por cada país en la lista.
                Seleccion selNueva = new Seleccion(p);
                List<Jugador> misJugadores = JugadoresDe(p);
                foreach (Jugador j in misJugadores)
                {
                    selNueva.AgregarJugador(j);
                }
                AltaSeleccion(selNueva);
            }
        }

        private List<Jugador> JugadoresDe(Pais p)
        {
            List<Jugador> _misJugadores = new List<Jugador>();
            foreach (Jugador j in _jugadores)
            {

                if (j.Pais.Nombre.Equals(p.Nombre))
                {
                    _misJugadores.Add(j);
                }
            }
            return _misJugadores;
        }
        #endregion

        #region Partidos

        private void PrecargarPartidos()
        {
            try
            {
                #region Fase de Grupos

                #region Grupo A
                //PARTIDO 1 | CATAR 0 - 2 ECUADOR
                Partido pfg1 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 20, 13, 00, 00), "A", GetSeleccion(GetPais("Catar")), GetSeleccion(GetPais("Ecuador")));
                //Goles
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 16, GetJugador(475)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 31, GetJugador(475)));
                //Amarillas
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 15, GetJugador(30)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 22, GetJugador(46)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 29, GetJugador(467)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 36, GetJugador(39)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 56, GetJugador(464)));
                pfg1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 78, GetJugador(47)));
                //Rojas
                //Finalizamos el partido
                pfg1.FinalizarPartido();

                //PARTIDO 2 | SENEGAL 0 - 2 PAISES BAJOS
                Partido pfg2 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 21, 13, 00, 00), "A", GetSeleccion(GetPais("Senegal")), GetSeleccion(GetPais("Países Bajos")));
                //Goles
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 84, GetJugador(337)));
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 90, GetJugador(335)));
                //Amarillas
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 56, GetJugador(320)));
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 90, GetJugador(569)));
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 90, GetJugador(580)));
                //Rojas
                pfg2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 87, GetJugador(575)));
                //Finalizamos el partido
                pfg2.FinalizarPartido();

                //PARTIDO 3 | CATAR 1 - 1 SENEGAL (FALSO)
                Partido pfg3 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 25, 10, 00, 00), "A", GetSeleccion(GetPais("Catar")), GetSeleccion(GetPais("Senegal")));
                //Goles
                pfg3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 12, GetJugador(43)));
                pfg3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 43, GetJugador(573)));
                //Amarillas
                //Rojas
                pfg3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 18, GetJugador(42)));
                //Finalizamos el partido
                //pfg3.FinalizarPartido();

                //PARTIDO 4 | ECUADOR 1 - 2 PAISES BAJOS (FALSO)
                Partido pfg4 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 25, 13, 00, 00), "A", GetSeleccion(GetPais("Ecuador")), GetSeleccion(GetPais("Países Bajos")));
                //Goles
                pfg4.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 32, GetJugador(462)));
                pfg4.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 21, GetJugador(317)));
                pfg4.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 55, GetJugador(329)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg4.FinalizarPartido();

                //PARTIDO 5 | CATAR 1 - 3 PAISES BAJOS (FALSO)
                Partido pfg5 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 29, 12, 00, 00), "A", GetSeleccion(GetPais("Catar")), GetSeleccion(GetPais("Países Bajos")));
                //Goles
                pfg5.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 20, GetJugador(36)));
                pfg5.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 35, GetJugador(326)));
                pfg5.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 65, GetJugador(328)));
                pfg5.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 88, GetJugador(332)));
                //Amarillas
                //Rojas
                pfg5.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 89, GetJugador(333)));
                //Finalizamos el partido
                //pfg5.FinalizarPartido();

                //PARTIDO 6 | ECUADOR 2 - 0 SENEGAL (FALSO)
                Partido pfg6 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 29, 12, 00, 00), "A", GetSeleccion(GetPais("Ecuador")), GetSeleccion(GetPais("Senegal")));
                //Goles
                pfg6.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 21, GetJugador(455)));
                pfg6.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 32, GetJugador(466)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg6.FinalizarPartido();
                #endregion

                #region Grupo B
                //PARTIDO 7 | INGLATERRA 6 - 2 IRÁN
                Partido pfg7 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 21, 16, 00, 00), "B", GetSeleccion(GetPais("Inglaterra")), GetSeleccion(GetPais("Irán")));
                //Goles
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 35, GetJugador(279)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 43, GetJugador(282)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 45, GetJugador(284)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 62, GetJugador(282)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 65, GetJugador(370)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 71, GetJugador(271)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 90, GetJugador(285)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 90, GetJugador(370)));
                //Amarillas
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 25, GetJugador(366)));
                pfg7.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 48, GetJugador(364)));
                //Rojas
                //Finalizamos el partido
                pfg7.FinalizarPartido();

                //PARTIDO 8 | ESTADOS UNIDOS 1 - 1 GALES
                Partido pfg8 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 21, 16, 00, 00), "B", GetSeleccion(GetPais("Estados Unidos")), GetSeleccion(GetPais("Gales")));
                //Goles
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 36, GetJugador(782)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 82, GetJugador(809)));
                //Amarillas
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 11, GetJugador(770)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 13, GetJugador(773)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 40, GetJugador(809)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 45, GetJugador(791)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 45, GetJugador(791)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 51, GetJugador(763)));
                pfg8.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 90, GetJugador(772)));
                //Rojas
                //Finalizamos el partido
                pfg8.FinalizarPartido();

                //PARTIDO 9 | INGLATERRA 2 - 0 ESTADOS UNIDOS (FALSO)
                Partido pfg9 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 25, 16, 00, 00), "B", GetSeleccion(GetPais("Inglaterra")), GetSeleccion(GetPais("Estados Unidos")));
                //Goles
                pfg9.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 23, GetJugador(270)));
                pfg9.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 58, GetJugador(270)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg9.FinalizarPartido();

                //PARTIDO 10 | IRÁN 1 - 2 GALES (FALSO)
                Partido pfg10 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 25, 07, 00, 00), "B", GetSeleccion(GetPais("Irán")), GetSeleccion(GetPais("Gales")));
                //Goles
                pfg10.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 80, GetJugador(350)));
                pfg10.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 15, GetJugador(790)));
                pfg10.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 48, GetJugador(792)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg10.FinalizarPartido();

                //PARTIDO 11 | INGLATERRA 2 - 1 GALES (FALSO)
                Partido pfg11 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 29, 16, 00, 00), "B", GetSeleccion(GetPais("Inglaterra")), GetSeleccion(GetPais("Gales")));
                //Goles
                pfg11.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 80, GetJugador(791)));
                pfg11.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 15, GetJugador(272)));
                pfg11.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 48, GetJugador(283)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg11.FinalizarPartido();

                //PARTIDO 12 | IRÁN 1 - 1 ESTADOS UNIDOS (FALSO)
                Partido pfg12 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 29, 16, 00, 00), "B", GetSeleccion(GetPais("Irán")), GetSeleccion(GetPais("Estados Unidos")));
                //Goles
                pfg12.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 21, GetJugador(347)));
                pfg12.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 37, GetJugador(770)));
                //Amarillas
                //Rojas
                //Finalizamos el partido
                //pfg12.FinalizarPartido();
                #endregion

                #region Grupo C
                //PARTIDO 13 | ARGENTINA 1 - 2 ARABIA SAUDITA
                Partido pfg13 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 22, 07, 00, 00), "C", GetSeleccion(GetPais("Argentina")), GetSeleccion(GetPais("Arabia Saudita")));
                //Goles
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 10, GetJugador(23)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 48, GetJugador(450)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 53, GetJugador(437)));
                //Amarillas
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 67, GetJugador(440)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 75, GetJugador(429)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 79, GetJugador(437)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 82, GetJugador(430)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 88, GetJugador(441)));
                pfg13.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 90, GetJugador(426)));
                //Rojas
                //Finalizamos el partido
                pfg13.FinalizarPartido();

                //PARTIDO 14 | MÉXICO 0 - 0 POLONIA
                Partido pfg14 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 22, 13, 00, 00), "C", GetSeleccion(GetPais("México")), GetSeleccion(GetPais("Polonia")));
                //Goles
                //Amarillas
                pfg14.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 29, GetJugador(748)));
                pfg14.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 56, GetJugador(741)));
                pfg14.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 76, GetJugador(641)));
                //Rojas
                //Finalizamos el partido
                pfg14.FinalizarPartido();

                //PARTIDO 15 | POLONIA 1 - 0 ARABIA SAUDITA (FALSO)
                Partido pfg15 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 26, 10, 00, 00), "C", GetSeleccion(GetPais("Polonia")), GetSeleccion(GetPais("Arabia Saudita")));
                //Goles
                pfg15.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 32, GetJugador(652)));
                //Amarillas
                pfg15.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 29, GetJugador(431)));
                pfg15.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 56, GetJugador(741)));
                pfg15.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 72, GetJugador(431)));
                //Rojas
                pfg15.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 72, GetJugador(431)));
                //Finalizamos el partido
                //pfg15.FinalizarPartido();

                //PARTIDO 16 | ARGENTINA 2 - 1 MÉXICO (FALSO)
                Partido pfg16 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 26, 16, 00, 00), "C", GetSeleccion(GetPais("Argentina")), GetSeleccion(GetPais("México")));
                //Goles
                pfg16.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 16, GetJugador(27)));
                pfg16.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 42, GetJugador(758)));
                pfg16.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 84, GetJugador(23)));
                //Amarillas
                pfg16.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 37, GetJugador(24)));
                //Rojas
                pfg16.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 82, GetJugador(749)));
                //Finalizamos el partido
                //pfg16.FinalizarPartido();

                //PARTIDO 17 | ARABIA SAUDITA 1 - 3 MÉXICO (FALSO)
                Partido pfg17 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 30, 16, 00, 00), "C", GetSeleccion(GetPais("Arabia Saudita")), GetSeleccion(GetPais("México")));
                //Goles
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 13, GetJugador(441)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 22, GetJugador(753)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 56, GetJugador(757)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 87, GetJugador(758)));
                //Amarillas
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 28, GetJugador(434)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 43, GetJugador(442)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 72, GetJugador(752)));
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 64, GetJugador(434)));
                //Rojas
                pfg17.AgregarIncidencia(new Incidencia(TipoDeIncidencia.expulsion, 64, GetJugador(434)));
                //Finalizamos el partido
                //pfg17.FinalizarPartido();

                //PARTIDO 18 | POLONIA 1 - 2 ARGENTINA (FALSO)
                Partido pfg18 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 30, 16, 00, 00), "C", GetSeleccion(GetPais("Polonia")), GetSeleccion(GetPais("Argentina")));
                //Goles
                pfg18.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 36, GetJugador(650)));
                pfg18.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 42, GetJugador(23)));
                pfg18.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 58, GetJugador(27)));
                //Amarillas
                pfg18.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 28, GetJugador(9)));
                pfg18.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 64, GetJugador(630)));
                //Rojas
                //Finalizamos el partido
                //pfg18.FinalizarPartido();
                #endregion

                #region Grupo D
                //PARTIDO 19 | DINAMARCA 0 - 0 TÚNEZ
                Partido pfg19 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 22, 10, 00, 00), "D", GetSeleccion(GetPais("Dinamarca")), GetSeleccion(GetPais("Túnez")));
                //Goles
                //Amarillas
                pfg19.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 24, GetJugador(89)));
                pfg19.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 78, GetJugador(95)));
                pfg19.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 86, GetJugador(679)));
                //Rojas
                //Finalizamos el partido
                pfg19.FinalizarPartido();

                //PARTIDO 20 | FRANCIA 4 - 1 AUSTRALIA
                Partido pfg20 = AuxPrecargarPartidoFG(new DateTime(2022, 11, 22, 16, 00, 00), "D", GetSeleccion(GetPais("Francia")), GetSeleccion(GetPais("Australia")));
                //Goles
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 9, GetJugador(839)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 27, GetJugador(149)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 32, GetJugador(153)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 68, GetJugador(154)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 71, GetJugador(153)));
                //Amarillas
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 55, GetJugador(847)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 80, GetJugador(835)));
                pfg20.AgregarIncidencia(new Incidencia(TipoDeIncidencia.amonestacion, 90, GetJugador(832)));
                //Rojas
                //Finalizamos el partido
                pfg20.FinalizarPartido();


                #endregion

                #endregion

                #region Fase Eliminatoria

                //PARTIDO 1 | PAÍSES BAJOS 2 - 0 GALES
                Partido pfe1 = AuxPrecargarPartidoFE(new DateTime(2022, 12, 13, 16, 00, 00), false, false, TipoEtapa.semifinal, GetSeleccion(GetPais("Países Bajos")), GetSeleccion(GetPais("Gales")));
                pfe1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 21, GetJugador(342)));
                pfe1.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 35, GetJugador(341)));
                //PARTIDO 2 | INGLATERRA 2 - 2 ECUADOR GANADOR POR PENALES: ECUADOR
                Partido pfe2 = AuxPrecargarPartidoFE(new DateTime(2022, 12, 14, 16, 00, 00), true, true, TipoEtapa.semifinal, GetSeleccion(GetPais("Inglaterra")), GetSeleccion(GetPais("Ecuador")));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 18, GetJugador(287)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 42, GetJugador(477)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 88, GetJugador(476)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 110, GetJugador(287)));
                // Penales
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(460)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(461)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(462)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(463)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(464)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(465)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(466)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(467)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(266)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(267)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(268)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(269)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(270)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(271)));
                pfe2.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, -1, GetJugador(272)));
                //PARTIDO 3 | PAÍSES BAJOS 1 - 2 ECUADOR 
                Partido pfe3 = AuxPrecargarPartidoFE(new DateTime(2022, 12, 18, 12, 00, 00), true, false, TipoEtapa.final, GetSeleccion(GetPais("Países Bajos")), GetSeleccion(GetPais("Ecuador")));
                pfe3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 24, GetJugador(476)));
                pfe3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 48, GetJugador(340)));
                pfe3.AgregarIncidencia(new Incidencia(TipoDeIncidencia.golConvertido, 115, GetJugador(476)));

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Métodos Auxiliares para Precargar Partidos
        private Partido AuxPrecargarPartidoFG(DateTime fecha, string grupo, Seleccion selA, Seleccion selB)
        {
            Partido partido = new FaseDeGrupos(fecha, grupo);
            partido.AgregarSeleccion(selA);
            partido.AgregarSeleccion(selB);

            AltaPartidoFG((FaseDeGrupos)partido);
            return partido;
        }
        private Partido AuxPrecargarPartidoFE(DateTime fecha, bool huboAlargue, bool huboPenales, TipoEtapa etapa, Seleccion selA, Seleccion selB)
        {
            Partido partido = new FaseEliminatoria(fecha, huboAlargue, huboPenales, etapa);
            partido.AgregarSeleccion(selA);
            partido.AgregarSeleccion(selB);

            AltaPartidoFE((FaseEliminatoria)partido);
            return partido;
        }
        #endregion

        #endregion

        #region Periodistas
        private void PrecargarPeriodistas()
        {
            try
            {
                Periodista p1 = new Periodista("Ana Inés", "Martinez", "anainesm@gmail.com", "anainesm");
                AltaFuncionario(p1);
                Periodista p2 = new Periodista("Rodrigo", "Romano", "rodrigoromano@gmail.com", "rodrigoromano");
                AltaFuncionario(p2);
                Periodista p3 = new Periodista("Silvia", "Pérez", "silviaperez@gmail.com", "silviaperez");
                AltaFuncionario(p3);
                Periodista p4 = new Periodista("Alberto", "Kesman", "albertokesman@gmail.com", "albertokesman");
                AltaFuncionario(p4);
                Periodista p5 = new Periodista("Martín", "Kesman", "martinkesman@gmail.com", "martinkesman");
                AltaFuncionario(p5);
                Periodista p6 = new Periodista("Sandra", "Rodríguez", "sandrarodriguez@gmail.com", "sandrarodriguez");
                AltaFuncionario(p6);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Operadores
        private void PrecargarOperadores()
        {
            try
            {
                Operador o1 = new Operador("Lola", "Mora", "lolamora@gmail.com", "lolamora", new DateTime(2022, 03, 14));
                AltaFuncionario(o1);
                Operador o2 = new Operador("Yordi", "Díaz", "yordidiaz@gmail.com", "yordidiaz", new DateTime(2022, 08, 15));
                AltaFuncionario(o2);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Reseñas
        private void PrecargarResenias()
        {
            try
            {
                //Reseñas de Periodista 1 - Ana Inés
                Resenia r1 = new Resenia(GetPeriodista(1), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "Ecuador y un arranque mundialista que ilusiona", "Oooole, oooole… “¿Quién imaginaba a Ecuador cantando el Ole, Ole en la inauguración del Mundial? Hay días que pueden cambiar la historia. Hay jugadores que pueden cambiar la historia. Ecuador lo hizo ganándole al local en la inauguración del Mundial. Lo consiguió a pesar del contexto que rodeaba al local Qatar. Lo logró porque no sólo tuvo más jerarquía que el rival, sino que absorbió ansiedades, dejó nervios en el vestuario y con un Enner Valencia encendido, superó sin dramas el debut con un 2-0 inapelable.");
                AltaResenia(r1);
                Resenia r2 = new Resenia(GetPeriodista(1), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "Países Bajos golpeó a Senegal sobre el final y se quedó con los tres puntos", "Cuántas veces viste esta película? ¿En cuántas oportunidades un equipo que pateó dos veces al arco terminó ganando? ¿Cuántos partidos hay un equipo que amaga dominarlo, desaprovecha los espacios, se duerme en la tenencia y de repente lo madrugan en el área y chau? Países Bajos se quedó con un resultado que por como estaba la historia del juego, pareció hasta casi exagerado. Un 2-0 sobre un Senegal, ya sin Sadio Mané, que vio pasar el tren de su oportunidad y no se subió. Los europeos comparten la punta del grupo A junto a Ecuador, y se enfrentan este viernes 25.");
                AltaResenia(r2);

                //Reseñas de Periodista 2 - Rodrigo Romano
                Resenia r3 = new Resenia(GetPeriodista(2), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "¡Con el pie derecho! Ecuador venció 2-0 a Qatar con doblete de Valencia", "En el primer partido del Mundial de Qatar, la selección ecuatoriana logró derrotar por 2-0 al país anfitrión gracias a dos goles de Enner Valencia en el primer tiempo. Ecuador fue superior desde el inicio del partido y apenas a los 2 minutos pudo ponerse arriba en el marcador con tanto de Valencia.Sin embargo, el VAR señaló que hubo una posición adelantada previa y el tanto fue anulado. Pero los sudamericanos siguieron dominando y Valencia era el jugador más peligroso.A los 12 minutos el atacante del Fenerbahce recibió una falta en el área por parte del arquero Saad Al Sheeb, el árbitro cobró penal y el mismo jugador se encargó de ejecutarlo para poner el 1 - 0.");
                AltaResenia(r3);
                Resenia r4 = new Resenia(GetPeriodista(2), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "¡Se estrenó con triunfo! Países Bajos venció 2-0 a Senegal", "En partido que se jugó en el Al Thumama Stadium, Países Bajos encontró los goles sobre el final y superó 2-0 a Senegal por el grupo A que también integran Ecuador y Qatar. Cody Gakpo (84') y Davy Klaassen (90') anotaron los goles. El primer tiempo fue bastante parejo en donde si bien Senegal encaró el partido con mayor agresividad en ataque, no tuvo las ideas necesarias para superar el cerco defensivo que le preparó Países Bajos. Con el correr de los minutos, la 'Naranja mecánica' se paró mejor en el terreno de juego, aunque por poca fuerza en ataque solo tuvo aproximaciones en el área de Senegal");
                AltaResenia(r4);

                //Reseñas de Periodista 3 - Silvia Pérez
                Resenia r5 = new Resenia(GetPeriodista(3), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "Qatar, el primer anfitrión que perdió en el debut en un Mundial: deslucida actuación y abandono de los hinchas", "Qatar se convirtió hoy en el primer seleccionado anfitrión en perder en el debut en la historia de los Mundiales tras caer por 2 a 0 frente al Ecuador del entrenador argentino Gustavo Alfaro, con una deslucida actuación que provocó el “abandono” de sus hinchas en el estadio Al Bayt. Qatar sufrió una derrota por un doblete de Enner Valencia en un encuentro en el que mostró graves falencias defensivas y pocas ideas para generar juego y situaciones de gol: no le acertó al arco defendido por Hernán Galindez en todo el encuentro inaugural. El mal desempeño del equipo llevó a muchos de los presentes en el estadio a abandonar las tribunas durante el desarrollo del primer tiempo, por lo que el complemento se llevó a cabo con una gran cantidad de butacas vacías en las tribunas. Además, Qatar se convirtió en el primer país organizador en perder en el estreno, dado que hasta el momento los anfitriones habían cosechado 16 victorias y seis empates.");
                AltaResenia(r5);
                Resenia r6 = new Resenia(GetPeriodista(3), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "Catar 2022: Países Bajos le ganó a Senegal 2-0 y lidera el grupo A junto a Ecuador", "Países Bajos debutó con victoria 2-0 sobre Senegal en la Copa del Mundo 2022, por lo que comparte el primer puesto del grupo A con Ecuador, rival al que enfrentará el próximo viernes a las 13 horas de Uruguay. Fue el conjunto europeo el que estuvo más cerca de abrir la cuenta en los primeros 20 minutos, lapso en el que la situación más peligrosa la tuvo Frenkie de Jong en un gran contragolpe pero no pudo definir.El volante del Barcelona pisó el área, desparramó a dos rivales y demoró el remate, por lo que llegó Kouyaté a despejar. Luego complicó el elenco africano con la velocidad de Ismaila Sarr, habilidoso extremo izquierdo del Watford que por momentos quedó muy solo en su intento por habilitar a Boulaye Dia, el sustituto de Sadio Mané en la formación inicial.En su jugada más peligrosa, Sarr remató y Virgil Van Dijk despejó de cabeza.");
                AltaResenia(r6);

                //Reseñas de Periodista 4 - Alberto Kesman
                Resenia r7 = new Resenia(GetPeriodista(4), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "Ecuador y Catar protagonizaron el partido con menos remates desde 1966", "Este domingo rodó el balón por primera vez en el Mundial de Catar 2022. Ecuador y Catar se encontraron para disputar el primer partido del Grupo A. El conjunto dirigido por Gustavo Alfaro comenzó con el pie derecho a derrotar a los cataríes por 2-0. Sin embargo, este juego dejó algunos datos para la historia. Según Opta, solo hubo 11 tiros en el compromiso inaugural de la Copa del Mundo de 2022 entre Catar(5) y Ecuador(6), sin ningún partido de la Copa del Mundo registrado(desde 1966) con menos intentos de gol.Increíble. Esto demostró que, a pesar del ímpetu con el que arrancó Ecuador y lo reflejó en el marcador transcurrido solamente el primer tiempo, donde se fueron arriba 2 - 0, después de lo ocurrido, Catar no pudo concretar opciones claras de gol, donde se evidenció en las estadísticas cuando se mostró que no pudo dispararle al arco del guardameta Hernán Galíndez. La selección sudamericana hizo su tarea, logró salir vencedor frente al rival que en el papel era el más débil de grupo.Sin embargo, el estratega Alfaro deberá superar los tres tiros al arco que realizó, además de querer tener más chances de gol que la única aparte de los goles convertidos para hacerle frente a Países Bajos. No obstante, la figura del compromiso, Enner Valencia, con el doblete que consiguió en este compromiso, llegó a la cifra de cinco tantos para Ecuador en los mundiales, récord que tenía antes Agustín Delgado.También pudo hacer gol en los últimos cuatro partidos de la Tri, lo que significa que ha convertido más goles que los partidos que ha disputado. Este próximo viernes, la Selección de Ecuador se verá las caras contra Países Bajos, mientras que la Selección de Catar intentará sumar de a tres frente a la siempre peligrosa Selección de Senegal en el segundo turno del día.");
                AltaResenia(r7);
                Resenia r8 = new Resenia(GetPeriodista(4), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "Países Bajos se impuso ante Senegal en su presentación", "En el Al Thumama Stadium y por la primera fecha del grupo A de la Copa Mundial de la FIFA de Catar 2022, Países Bajos se impuso por 2-0 ante Senegal. Pese a que estuvo muy lejos de su mejor versión, a La Oranje le alcanzó para quedarse con la victoria en un partido muy parejo, que contó con escasas de situaciones de peligro, especialmente en la etapa inicial. En este contexto, el combinado europeo fue el que intentó tomar la iniciativa y asumir el protagonismo.De todos modos, si bien controló el 53 % de la posesión de la pelota, tuvo serias complicaciones para traducir dicho dominio en chances para marcar. El conjunto africano, pese a asumir una postura más pasiva, fue el más incisivo en sus ataques.A falta de 25 minutos para el final, Boulaye Dia forzó una buena respuesta por parte de Andries Noppert.Y a los 73', Idrissa Gueye, en la puerta de área y llegando de frente al arco, probó a Noppert y el gigante de 2.03 metros de estatura volvió a lucirse con una notable atajada.En el tramo final, luego de un pasaje en el juego favorable a Senegal, Países Bajos recuperó la tenencia del balón y consiguió romper el cero: a los 84', Frenkie de Jong levantó la cabeza y soltó un excelente pase a la cabeza de Cody Gapko, quien entró a la carrera y anticipó a Edouard Mendy. En la reanudación del juego, los dirigidos por Aliou Cissé estuvieron cerca de nivelar las acciones con un remate de Pape Gueye de media distancia, pero una vez más Noppert, la figura del cotejo, se estiró para mantener su valla invicta. En el último minuto del tiempo de descuento, el elenco conducido por Louis van Gaal liquidó el pleito con un tanto de Davy Klaassen, quien aprovechó un rebote que Mendy había dado ante un débil disparo de Memphis Depay. Con este resultado, Países Bajos alcanzó a Ecuador y ambos se ubican en la cima de la tabla de posiciones con tres puntos.El viernes se enfrentarán los líderes de la zona por un lado, y Catar y Senegal por otro.");
                AltaResenia(r8);

                //Reseñas de Periodista 5 - Martín Kesman
                Resenia r9 = new Resenia(GetPeriodista(5), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "Ecuador derrotó 2 a 0 al anfitrión Catar en el partido inaugural", "Las selecciones de Catar y Ecuador abrieron este Mundial de Catar 2022 en el estadio Al Bayt de la ciudad de Jor. El presidente FIFA, Gianni Infantino, saludó y dio la bienvenida a todos los participantes y selecciones competidoras. La selección ecuatoriana derrotó a los anfitriones por 2 a 0 con los tantos del delantero Enner Valencia a los 15' y a los 32'. Este estadio donde se realizó la ceremonia y el partido inaugural está inspirado en una tienda beduina y conmemora el pasado, el presente y el futuro del país anfitrión.Simula los tradicionales campamentos qataríes.El nombre Al Bayt significa 'la casa' en el idioma árabe.Tiene una capacidad para albergar a 60 mil espectadores sentados además de mil adicionales para la prensa.Cuenta con aire acondicionado, techo, un restaurante y un hotel 5 estrellas.En sus gradas predomina el color rojo, se construyó especialmente para este Mundial y se inauguró en el mes de junio del 2020.");
                AltaResenia(r9);
                Resenia r10 = new Resenia(GetPeriodista(5), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "Senegal cayó 0-2 ante Países Bajos por Mundial Qatar 2022", "Senegal cayó 0-2 ante Países Bajos en el partido de la primera jornada del Grupo A del Mundial Qatar 2022 este lunes 21 de noviembre del 2022 en el estadio Al Thumama. Cody Gakpo (84′) y Davy Klaassen (90+8′) anotaron los únicos goles para la ‘Naranja Mecánica’. La próxima jornada, Países Bajos enfrentará a Ecuador, mientras que Senegal, hará lo mismo ante Qatar.");
                AltaResenia(r10);

                //Reseñas de Periodista 6 - Sandra Rodríguez
                //Resenia r11 = new Resenia(GetPeriodista(6), new DateTime(2022, 11, 20, 19, 00, 00), GetPartido(0), "Ecuador agua la fiesta de la anfitriona en el debut del Mundial de Qatar 2022", "La selección de Ecuador se ha impuesto a la de Catar por 0-2 en el partido inaugural del Mundial 2022 en el estadio Al Khor. El combinado sudamericano ha aguado la fiesta de la inauguración a los locales y se coloca como primer líder del Grupo A, hasta que se complete con el Senegal - Países Bajos de este lunes. La selección anfitriona, eso sí, puede presumir de haber sido la primera en caer derrotada en un partido inaugural del Mundial; un honor más que dudoso.Cabe matizar que no siempre los organizadores han jugado el primer partido, ya que eso ha sucedido en once ocasiones previas, pero se ha convertido en una costumbre reciente. Visto lo visto sobre el césped, tampoco parece que se vaya a ampliar el registro de selecciones campeonas después de jugar un partido inaugural, tres hasta la fecha, porque ni Catar ni Ecuador han mostrado argumentos. ");
                //AltaResenia(r11);
                //Resenia r12 = new Resenia(GetPeriodista(6), new DateTime(2022, 11, 21, 13, 00, 00), GetPartido(1), "Países Bajos le ganó a Senegal con un cabezazo a pocos minutos del final y se quedó con el triunfo en el primer partido del Mundial Qatar 2022", "Un triunfo con mucho de positivo se llevó Países Bajos ante Senegal. Fue por 2 a 0, en el partido correspondiente al grupo A del Mundial de Qatar 2022. Los primeros minutos fueron monopolizados por los africanos que mostraron todo su vértigo y sorprendieron a los dirigidos por Van Gaal en sus intentos por salir tocando. Luego los neerlandeses emparejaron las alternativas y supieron llegarle s su rival. La primera parte tuvo muchas situaciones de los dos lados, pero ninguno pudo abrir el marcador. En el segundo tiempo fue Países Bajos el que empezó a ser más protagonista y Senegal lo esperaba.El equipo europeo mandó a Memphis Depay a la cancha porque el equipo estaba mucho más cerca.Con los espacios abiertos, los senegaleses supieron manejar los contraataques e hicieron figuras al arquero Andries Noppert que salvó en dos ocasiones a su selección.Sin embargo, cuando el partido llegaba a su fin, Cody Gakpo le ganó en las alturas a Edouard Mendy y de nuca abrió el marcador.Un gol que le daba tranquilidad a los neerlandeses.Senegal siguió buscando, pero Países bajos pudo liquidar el partido.Depay quedó mano a mano con el arquero rival que tapó su remate, pero el rebote lo tomó Davy Klaassen que puso el 2 a 0.Triunfo muy trabajado parta Países Bajos que iguala la línea de Ecuador en la tabla.Seleccionado con quien se medirá el próximo viernes.Por otra parte, Senegal se mide ante Qatar en la misma jornada.");
                //AltaResenia(r12);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #endregion

        #region Métodos Resenias
        public void AsociarNuevaResenia(Resenia r, int? idL, Partido par)
        {
            try
            {
                AltaResenia(r);
                Periodista perdiodistaDeResenia = GetPeriodista(idL);
                r.AsociarResenia(perdiodistaDeResenia, par);
            }
            catch (Exception e)
            {
                //Revisar
                //throw e;
                throw new Exception("Error en los datos.");
            }
        }
        #endregion

        #region Métodos Menú (Consola)

        #region Opción 2 del menú
        public void AsignarValor(double valorRef)
        {
            if (valorRef < 0)
            {
                throw new Exception("El monto ingresado deber ser mayor a cero");
            }
            else
            {
                Jugador.Monto = valorRef;
            }

        }
        public void CategoriaFinanciera(double valorRef)
        {
            foreach (Jugador j in _jugadores)
            {
                if (j.Valor < valorRef)
                {
                    j.CatFinanciera = "Estándar";
                }
                else
                {
                    j.CatFinanciera = "VIP";
                }
            }


        }
        public double GetMontoJ()
        {
            return Jugador.Monto;
        }

        #endregion

        #region Opcicón 3 del menú
        public List<Partido> MostrarPartidosDeJugador(int id)
        {
            List<Partido> partidos = new List<Partido>();
            //Obtenemos el objeto jugador con el id ingresado por parámetro
            Jugador j = GetJugador(id);

            foreach (Partido p in _partidos)
            {
                foreach (Seleccion s in p.SeleccionesEnfrentadas)
                {
                    foreach (Jugador juga in s.Jugadores)
                    {//Si el jugador ingresado por parámetro es igual al jugador de la selección que se está recorriendo, lo agrega a la lista
                        if (juga == j)
                        {
                            partidos.Add(p);
                        }


                    }

                }

            }
            return partidos;
        }

        public string DatosPartido()
        {
            string datosPartido = "";
            foreach (Partido p in _partidos)
            {
                datosPartido = $"Fecha y hora: {p.Fecha}, Selecciones: {p.MostrarSeleccionesEnfrentadas()}, Cantidad de incidencias: {p.MostrarCantidadIncidencias()}";
            }
            return datosPartido;

        }

        #endregion

        #region Opción 4 del menú
        public List<Jugador> MostrarJugadoresExpulsados()
        {
            List<Jugador> jugadoresExp = new List<Jugador>();

            foreach (Partido p in _partidos)
            {
                foreach (Incidencia inci in p.Incidencias)
                {
                    if (inci.TipoIncidencia == TipoDeIncidencia.expulsion)
                    {//Si el tipo de incidencia es expulsión, agregamos al jugador a la lista
                        jugadoresExp.Add(inci.Jugador);
                        //Ordenamos la lista antes de mostrarla
                        jugadoresExp.Sort();
                    }
                }
            }
            //Retornamos la lista de los jugadores expulsados
            return jugadoresExp;
        }
        #endregion

        #region Opción 5 del menú
        //Creamos un método que nos devuelve una lista (porque puede ser que en mas de un partido hubo la misma cantidad de goles)
        public List<Partido> PartidosConMasGoles(string nomSel)
        {
            //Creamos la lista que vamos a retornar. Es una lista, por si hay varios partidos con la misma cantidad de goles máaximos
            List<Partido> ret = new List<Partido>();

            //Convertimos la primera letra de cada palabra que nos ingresen en mayúscula
            string nomSelConvertido = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomSel);

            //Obtenemos el objeto selección
            Seleccion selec = GetSeleccion(GetPais(nomSelConvertido));

            //Definimos variable para la maxima cantidad de goles
            int max = 0;

            foreach (Partido p in _partidos)
            {
                //Inicializamos contador de goles por partido
                int cantGoles = 0;
                //Recorremos las selecciones enfrentadas
                foreach (Seleccion s in p.SeleccionesEnfrentadas)
                {
                    //Si la seleccion coincide con la que le pasamos por parámetros
                    if (s == selec)
                    {
                        //Busca en las incidencias y suma 1 al contador de goles por cada gol que encuentre
                        foreach (Incidencia inci in p.Incidencias)
                        {
                            if (inci.TipoIncidencia == TipoDeIncidencia.golConvertido)
                            {
                                cantGoles++;

                            }

                        }

                    }
                }
                //Despues que ya contamos la cantidad de goles, ahi recien lo comparamos con el maximo (goles por partido)
                if (cantGoles > max)
                {//Si la cantidad de goles que encontró es mayor al máximo guardado anteriormente, iguala el valor encontrado al max
                    max = cantGoles;
                    //Borra de la lista al partido anterior con mayor cantidad de goles
                    ret.Clear();
                    //Agregamos el nuevo partido con mayor cantidad de goles
                    ret.Add(p);
                }
                else if (cantGoles == max)
                {//Si la cantidad de goles del partido, es igual al máximo, guarda el partido en la lista 
                    ret.Add(p);
                }
            }
            //Retornamos la lista con los partidos en los que se hicieron más goles 
            return ret;
        }

        #endregion

        #region Opción 6 del menú
        public List<Jugador> MostrarJugadoresConGoles()
        {
            List<Jugador> jugadoresConGoles = new List<Jugador>();

            foreach (Partido p in _partidos)
            {
                foreach (Incidencia inci in p.Incidencias)
                {//Verificamos que el tipo de incidencia sea gol, antes de agregar al jugador a la lista
                    if (inci.TipoIncidencia == TipoDeIncidencia.golConvertido)
                    {
                        //Si el jugador no se encuentra previamente en la lista de jugadores con goles, lo agreamos a la lista
                        if (!jugadoresConGoles.Contains(inci.Jugador))
                        {
                            //Agregamos al jugador a la lista
                            jugadoresConGoles.Add(inci.Jugador);
                        }


                    }
                }
            }
            //Devolvemos la lista de jugadores con más goles
            return jugadoresConGoles;
        }

        public Funcionario Login(string email, string pass)
        {
            foreach (Funcionario f in _funcionarios)
            {
                if (f.Email.Equals(email) && f.Password.Equals(pass))
                {

                    return f;
                }
            }
            return null;
        }

        #endregion

        #endregion
    }
}
