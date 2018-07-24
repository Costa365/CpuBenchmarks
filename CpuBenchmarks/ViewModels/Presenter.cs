using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CpuBenchmarks.Models;
using System.Threading.Tasks;

namespace CpuBenchmarks.ViewModels
{
    public class Presenter : ObservableObject
    {
        private string _cpuModel;
        private string _cpuLogo;
        private string _benchMarkResult;
        private bool _buttonEnabled;
        private Benchmark _benchMarkAnalyser;
        private ObservableCollection<CpuParam> _benchMarkResults;            

        public Presenter()
        {
            Initialise();
            GetBenchmarks(false);
        }
        
        public string CpuModel
        {
            get { return _cpuModel; }
            set
            {
                _cpuModel = value;
                RaisePropertyChangedEvent("CpuModel");
            }
        }

        public string BenchmarkResult
        {
            get { return _benchMarkResult; }
            set
            {
                _benchMarkResult = value;
                RaisePropertyChangedEvent("BenchmarkResult");
            }
        }

        public string CpuLogo
        {
            get { return _cpuLogo; }
            set
            {
                _cpuLogo = value;
                RaisePropertyChangedEvent("CpuLogo");
            }
        }

        public ICommand GetBenchmarkCommand
        {
            get { return new DelegateCommand(GetBenchmark); }
        }        

        private void GetBenchmark()
        {
            List<CpuParam> res = _benchMarkAnalyser.GetBenchMarkResult(CpuModel);
            BenchMarkResults = new ObservableCollection<CpuParam>(res.ToArray());
            CpuLogo = Logo.GetLogo(BenchMarkResults.Count>0 ? BenchMarkResults[0].Value.ToString() : "");
        }

        public bool ButtonEnabled
        {
            get { return _buttonEnabled;  }
            set
            {
                _buttonEnabled = value;
                RaisePropertyChangedEvent("ButtonEnabled");
            }
        }       

        public ICommand RefreshBenchmarksCommand
        {
            get { return new DelegateCommand(RefreshBenchmarks); }
        }

        private void RefreshBenchmarks()
        {
            Initialise();
            GetBenchmarks(true);
        }

        private void Initialise()
        {
            ButtonEnabled = false;
            _benchMarkAnalyser = new Benchmark();
            CpuModel = "Downloading Data...";
            CpuLogo = "";
            BenchMarkResults = null;            
        }

        private void GetBenchmarks(bool forceRefresh)
        {
            Task.Run(async () => {
                await _benchMarkAnalyser.GetBenchmarkData(forceRefresh);
                CpuModel = "i7-5500U";
                ButtonEnabled = true;
            });
        }

        public ObservableCollection<CpuParam> BenchMarkResults
        {
            get { return _benchMarkResults; }
            set
            {
                _benchMarkResults = value;
                RaisePropertyChangedEvent("BenchmarkResults");
            }
        }
    }
}