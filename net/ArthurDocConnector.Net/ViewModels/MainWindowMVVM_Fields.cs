using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ArthurDoc.Client.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ArthurDocConnector.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        
        private Visibility _stepOneVisibility;
        private Visibility _stepTwoVisibility;
        private Visibility _stepThreeVisibility;
        private bool _isFileLoaded;

        public Visibility ShowStepOne
        {
            get => _stepOneVisibility;
            set { _stepOneVisibility = value; RaisePropertyChanged(nameof(ShowStepOne)); }
        }

        public Visibility ShowStepTwo
        {
            get => _stepTwoVisibility;
            set { _stepTwoVisibility = value; RaisePropertyChanged(nameof(ShowStepTwo)); }
        }
        public Visibility ShowStepThree
        {
            get => _stepThreeVisibility;
            set { _stepThreeVisibility = value; RaisePropertyChanged(nameof(ShowStepThree)); }
        }

        private NewReqestParameters _requestParameters;

        public bool IsSendFileActive => IsFileLoaded && !string.IsNullOrEmpty(RequestName) && !string.IsNullOrEmpty(TemplateGuid);


        public bool IsFileLoaded
        {
            get => _isFileLoaded;
            set
            {
                _isFileLoaded = value;
                RaisePropertyChanged(nameof(IsFileLoaded));
                RaisePropertyChanged(nameof(IsSendFileActive));
            }
        }

        private bool _mergeFile;
        public bool MergeFile
        {
            get => _mergeFile;
            set
            {
                _mergeFile = value;
                _requestParameters.MergeResultFiles = value;
                RaisePropertyChanged(nameof(MergeFile));
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged(nameof(FileName));
            }
        }


        private string _requestName;
        public string RequestName
        {
            get => _requestName;
            set
            {
                _requestName = value;
                RaisePropertyChanged(nameof(RequestName));
                RaisePropertyChanged(nameof(IsSendFileActive));
            }
        }


        private string _templateGuid;
        public string TemplateGuid
        {
            get => _templateGuid;
            set
            {
                _templateGuid = value;
                _requestParameters.TemplateGuid = value;
                RaisePropertyChanged(nameof(TemplateGuid));
                RaisePropertyChanged(nameof(IsSendFileActive));
            }
        }

        private ObservableCollection<DownloadedFile> _files;

        public ObservableCollection<DownloadedFile> ReceivedFiles
        {
            get => _files;
            set
            {
                _files = value;
                RaisePropertyChanged(nameof(ReceivedFiles));
            }
        }


        private string _statusBarColor;

        public string StatusBarColor
        {
            get => _statusBarColor;
            set
            {
                _statusBarColor = value;
                RaisePropertyChanged(nameof(StatusBarColor));
            }
        }


        private string _spinnerMessage;

        public string SpinnerMessage
        {
            get => _spinnerMessage;
            set
            {
                _spinnerMessage = value;
                RaisePropertyChanged(nameof(SpinnerMessage));
            }
        }

        private Visibility _spinnerVisibility;

        public Visibility SpinnerVisibility
        {
            get => _spinnerVisibility;
            set
            {
                _spinnerVisibility = value;
                RaisePropertyChanged(nameof(SpinnerVisibility));
            }
        }

        private bool _requestNameEnable;
        private bool _chooseFileEnabled;
        private bool _mergeFileEnabled;
        public bool RequestNameEnable
        {
            get => _requestNameEnable;
            set
            {
                _requestNameEnable = value;
                RaisePropertyChanged(nameof(RequestNameEnable));
            }
        }
        public bool ChooseFileEnabled
        {
            get => _chooseFileEnabled;
            set
            {
                _chooseFileEnabled = value;
                RaisePropertyChanged(nameof(ChooseFileEnabled));
            }
        }
        public bool MergeFileEnabled
        {
            get => _mergeFileEnabled;
            set
            {
                _mergeFileEnabled = value;
                RaisePropertyChanged(nameof(MergeFileEnabled));
            }
        }
    }
}
