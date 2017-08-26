using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.Service
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/28 14:13:33
	===============================================================================================================================*/
    public class SampleService
    {
        private readonly ISampleDependency _sample;

        public SampleService(ISampleDependency sample)
        {
            _sample = sample;
        }

        public bool Start()
        {
            Console.WriteLine("Sample Service Started.");
            Console.WriteLine("Sample Dependency: {0}", _sample);
            return _sample != null;
        }

        public bool Stop()
        {
            return _sample != null;
        }
    }

    public interface ISampleDependency
    {
    }

    public class SampleDependency : ISampleDependency
    {
    }
}
