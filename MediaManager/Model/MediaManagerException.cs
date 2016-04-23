﻿// Developed by: Gabriel Duarte
// 
// Created at: 04/02/2016 20:36

using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Helpers;

namespace MediaManager.Model
{
    public class MediaManagerException
    {
        public MediaManagerException(Exception e)
        {
            lstDetalhes = new List<string>();
            Exception = e;
        }

        public MediaManagerException(string e)
        {
            lstDetalhes = new List<string>();
            Exception = new Exception(e);
        }

        private List<string> lstDetalhes { get; }

        private Exception Exception { get; }

        public void TratarException(string sErro = "Ocorreu um erro na aplicação.", bool bIsSilencioso = true)
        {
            var sDetalhes = "";

            if (sErro.Last() != '.')
            {
                sErro += ".";
            }

            sDetalhes += $"Detalhes: {Exception}";

            foreach (string item in lstDetalhes)
            {
                sDetalhes += Environment.NewLine + item;
            }

            string sMensagem = sErro + Environment.NewLine + sDetalhes;

            if (bIsSilencioso)
            {
                Helper.LogMessage(sMensagem);
            }
            else
            {
                Helper.LogMessage(sMensagem);
                Helper.MostrarMensagem(sErro + Environment.NewLine + Environment.NewLine +
                                       "Verifique mais detalhes no Log.",
                                       Enums.eTipoMensagem.Erro);
            }
        }
    }
}
