﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using MediaManager.Helpers;
using MediaManager.Model;

namespace MediaManager.ViewModel
{
    public class ProcurarConteudoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ConteudoGrid> _Conteudos = new ObservableCollection<ConteudoGrid>();
        public ObservableCollection<ConteudoGrid> Conteudos { get { return _Conteudos; } set { _Conteudos = value; OnPropertyChanged("Conteudos"); } }

        public ProcurarConteudoViewModel(Helper.Enums.ContentType contentType)
        {
            Conteudos.Add(new ConteudoGrid { Title = "Carregando...", FolderPath = "Carregando...", IsSelected = false });

            LoadConteudos(contentType);
        }

        public async void LoadConteudos(Helper.Enums.ContentType contentType)
        {
            ObservableCollection<ConteudoGrid> conteudos = new ObservableCollection<ConteudoGrid>();

            switch (contentType)
            {
                case Helper.Enums.ContentType.movie:
                    break;

                case Helper.Enums.ContentType.show:
                    break;

                case Helper.Enums.ContentType.anime:
                    break;

                case Helper.Enums.ContentType.movieShowAnime:
                    DirectoryInfo[] dirSeries = Helper.retornarDiretoriosSeries();
                    DirectoryInfo[] dirAnimes = Helper.retornarDiretoriosAnimes();
                    DirectoryInfo[] dirFilmes = Helper.retornarDiretoriosFilmes();

                    foreach (var dir in dirSeries)
                    {
                        if (!DatabaseHelper.VerificaSeExiste(dir.FullName))
                        {
                            SeriesData data = await API_Requests.GetSeriesAsync(dir.Name, false);
                            if (data.Series.Length == 0)
                            {
                                ConteudoGrid conteudo = new ConteudoGrid();
                                conteudo.ContentType = Helper.Enums.ContentType.show;
                                conteudo.FolderPath = dir.FullName;
                                conteudo.IsNotFound = true;
                                conteudos.Add(conteudo);
                            }
                            else if (data.Series.Length > 0 && !DatabaseHelper.VerificaSeExiste(data.Series[0].IDApi))
                            {
                                ConteudoGrid conteudo = data.Series[0];
                                conteudo.ContentType = Helper.Enums.ContentType.show;
                                conteudo.FolderPath = dir.FullName;
                                conteudo.IsSelected = true;
                                conteudo.Video = data.Series[0];
                                conteudo.Video.ContentType = Helper.Enums.ContentType.show;
                                conteudo.Video.FolderPath = dir.FullName;
                                conteudos.Add(conteudo);
                            }
                        }
                    }

                    foreach (var dir in dirAnimes)
                    {
                        if (!DatabaseHelper.VerificaSeExiste(dir.FullName))
                        {
                            SeriesData data = await API_Requests.GetSeriesAsync(dir.Name, false);
                            if (data.Series == null || data.Series.Length == 0)
                            {
                                ConteudoGrid conteudo = new ConteudoGrid();
                                conteudo.ContentType = Helper.Enums.ContentType.anime;
                                conteudo.FolderPath = dir.FullName;
                                conteudo.IsNotFound = true;
                                conteudos.Add(conteudo);
                            }
                            else if (data.Series.Length > 0 && !DatabaseHelper.VerificaSeExiste(data.Series[0].IDApi))
                            {
                                ConteudoGrid conteudo = data.Series[0];
                                conteudo.ContentType = Helper.Enums.ContentType.anime;
                                conteudo.FolderPath = dir.FullName;
                                conteudo.IsSelected = true;
                                conteudo.Video = data.Series[0];
                                conteudo.Video.ContentType = Helper.Enums.ContentType.anime;
                                conteudo.Video.FolderPath = dir.FullName;
                                (conteudo.Video as Serie).IsAnime = true;
                                conteudos.Add(conteudo);
                            }
                        }
                    }

                    //foreach (var dir in dirFilmes) TODO Fazer funfar
                    //{
                    //    if (!DatabaseHelper.VerificaSeExiste(dir.FullName))
                    //    {
                    //        filmes = await Helper.API_PesquisarConteudoAsync(dir.Name, Helper.Enums.TipoConteudo.movie.ToString(), false);
                    //        if (filmes.Count != 0 && !DatabaseHelper.VerificaSeExiste(filmes[0].Video.ids.trakt))
                    //            conteudos.Add(new ConteudoGrid { Nome = filmes[0].Video.title, Pasta = dir.FullName, TipoConteudo = Helper.Enums.TipoConteudo.movie, TraktSlug = filmes[0].Video.ids.slug, IsSelected = true });
                    //    }
                    //}
                    break;

                default:
                    break;
            }

            Conteudos = conteudos;

            //Conteudos.Clear();

            //foreach (var item in conteudos)
            //{
            //    Conteudos.Add(item);
            //}
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members
    }
}