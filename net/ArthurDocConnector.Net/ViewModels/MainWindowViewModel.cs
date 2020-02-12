using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using ArthurDoc.Client;
using ArthurDoc.Client.Models;
using log4net;
using Ninject;
using Prism.Mvvm;

namespace ArthurDocConnector.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        private string _currentJobId = DefaultValues.DefaultGuid;
        private static readonly ILog _logger = log4net.LogManager.GetLogger("ArthurDocClientGui");
        [Inject]
        public MainWindowViewModel()
        {
            DefaultGuidParameters();
        }

        private void DefaultGuidParameters()
        {
            StatusBarColor = "White";
            ShowStepOne = Visibility.Visible;
            ShowStepTwo = Visibility.Hidden;
            ShowStepThree = Visibility.Visible;
            StartUpGui();
            ReceivedFiles = new ObservableCollection<DownloadedFile>();
            SpinnerMessage = "Processing..";
            SetSpinnerVisiblity(Visibility.Hidden);
            _requestParameters = new NewReqestParameters();
            RequestName = string.Empty;
            TemplateGuid = string.Empty;
        }
        private async void SetSpinnerVisiblity(Visibility visibility)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() =>
                    SpinnerVisibility = visibility
                ));
        }

        private async void SendFileToArthurDoc()
        {
            if (!string.IsNullOrEmpty(RequestName))
            {
                SetSpinnerVisiblity(Visibility.Visible);
                SetGui(false);
                _logger.Info("Prepare to send Request");
                await CreateJobInArthurDoc();
                if (!_currentJobId.Equals(DefaultValues.ErrorCode))
                {
                    _logger.Info("Before CheckStatus");
                    await CheckJobStatus();
                    _logger.Info("Before JobResult");
                    await GetJobResult();
                }
                
            }
            else
            {
                MessageBox.Show("Please enter RequestName.", "Validation", MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }


        private async System.Threading.Tasks.Task CreateJobInArthurDoc()
        {
            await System.Threading.Tasks.Task.Factory.StartNew(async () =>
                {
                    while (_currentJobId.Equals(DefaultValues.DefaultGuid))
                    {
                        StatusBarColor = "Orange";
                        ShowStepTwo = Visibility.Visible;
                        _currentJobId = await Core.Instance.SendNewJobRequest(ArthurDocJobCommunicationStatus, _requestParameters, RequestName, MergeFile);
                        if (!_currentJobId.Equals(DefaultValues.DefaultGuid) && !_currentJobId.Equals(DefaultValues.ErrorCode))
                            break;
                        if (_currentJobId.Equals(DefaultValues.ErrorCode))
                        {
                            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));
                            SetSpinnerVisiblity(Visibility.Hidden);
                            break;
                        }
                        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));
                    }

                }
            );

            while (_currentJobId.Equals(DefaultValues.DefaultGuid))
            {
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private async System.Threading.Tasks.Task CheckJobStatus()
        {
            await System.Threading.Tasks.Task.Factory.StartNew(async () =>
            {
                var checkJobStatus = true;
                while (checkJobStatus)
                {
                    StatusBarColor = "White";
                    checkJobStatus = !await Core.Instance.GetJobStatus(ArthurDocJobCommunicationStatus, _currentJobId);
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));
                }

                StatusBarColor = "Green";

            });

            while (StatusBarColor != "Green")
            {
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private async System.Threading.Tasks.Task GetJobResult()
        {
            await System.Threading.Tasks.Task.Factory.StartNew(async () =>
            {
                var result = new List<DownloadedFile>();
                var checkJobStatus = true;
                while (checkJobStatus)
                {
                    StatusBarColor = "White";
                    result = await Core.Instance.GetJobResult(ArthurDocJobCommunicationStatus, _currentJobId);
                    checkJobStatus = false;
                }

                if (result != null)
                {
                    ReceivedFiles = new ObservableCollection<DownloadedFile>();
                    DirectoryInfo dI = new DirectoryInfo($@"{_requestParameters.Path}\\ArthurDoc_GeneratedFiles\\");

                    if (!dI.Exists)
                        dI.Create();
                    foreach (var files in result)
                    {

                        var finalPath = new StringBuilder(dI.FullName).Append(files.Name);
                        
                        File.WriteAllBytes(finalPath.ToString(), files.FileContent);
                        files.Path = finalPath.ToString();

                        await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                            new Action(() =>
                                ReceivedFiles.Add(files)
                            ));

                    }
                    ShowStepThree = Visibility.Visible;
                    StatusBarColor = "Green";
                    SetSpinnerVisiblity(Visibility.Hidden);
                }
            });
        }

        
        /// <summary>
        /// Callback to inform User about Request status
        /// </summary>
        /// <param name="status">Request status</param>
        /// <param name="color">Information color</param>
        public void ArthurDocJobCommunicationStatus(string status, string color)
        {
            SpinnerMessage = status;
            StatusBarColor = color;
        }

        private void StartUpGui()
        {
            MergeFileEnabled = true;
            ChooseFileEnabled = true;
            RequestNameEnable = true;
        }

        private void SetGui(bool enable)
        {
            MergeFileEnabled = enable;
            ChooseFileEnabled = enable;
            RequestNameEnable = enable;

        }
    }
}
