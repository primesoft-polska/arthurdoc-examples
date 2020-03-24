using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ArthurDoc.Client.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;

namespace ArthurDocConnector.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        private DelegateCommand _getFileCmd = null;

        public ICommand GetFileCmd
        {
            get { return _getFileCmd = _getFileCmd ?? new DelegateCommand(GetFile); }
        }

        private DelegateCommand _sendFileToArthurDocCmd = null;

        public ICommand SendFileToArthurDocCmd
        {
            get { return _sendFileToArthurDocCmd = _sendFileToArthurDocCmd ?? new DelegateCommand(SendFileToArthurDoc); }
        }


        private DelegateCommand _restartGuiCmd = null;

        public ICommand RestartGuiCmd
        {
            get { return _restartGuiCmd = _restartGuiCmd ?? new DelegateCommand(DefaultGuidParameters); }
        }

        private DelegateCommand _closeAppCmd = null;

        public ICommand CloseAppCmd
        {
            get { return _closeAppCmd = _closeAppCmd ?? new DelegateCommand(CloseApp); }
        }

        private void CloseApp()
        {

            if (MessageBox.Show("Czy na pewno chcesz zamkąć ArthurDocConnector?", "Zamykanie ArthurDocConnector",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();    
            }
        }

        public void GetFile()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Plik XML (*.xml)|*.xml| Arkusz excel (*.xlsx, *.xls)|*.xlsx;*.xls| Plik JSON (*.json) | *.json",
                FilterIndex = 1
            };

            var result = ofd.ShowDialog();

            if (result.HasValue)
            {
                IsFileLoaded = result.Value;
                if (ofd.FileName.Contains(".xml"))
                {
                    _requestParameters.IsXml = true;
                    _requestParameters.IsJson = false;
                    _requestParameters.XmlBody = File.ReadAllText(ofd.FileName);

                }
                else if (ofd.FileName.Contains(".json"))
                {
                    _requestParameters.IsJson = true;
                    _requestParameters.IsXml = false;
                    _requestParameters.JsonBody = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(ofd.FileName));

                }
                else
                {
                    if (result.Value)
                    {
                        _requestParameters.IsJson = false;
                        _requestParameters.IsXml = false;
                        _requestParameters.FileContent = File.ReadAllBytes(ofd.FileName);
                    }
                }
                IsFileLoaded = true;
                FileName = ofd.FileName;
                _requestParameters.Path = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf(@"\", StringComparison.CurrentCultureIgnoreCase));

            }
        }
    }
}
