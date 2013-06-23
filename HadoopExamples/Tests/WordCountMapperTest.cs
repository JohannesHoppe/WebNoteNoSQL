using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Machine.Specifications.Model;

namespace Tests
{
    public class When_sending_data
    {
        static string textContent;

        Establish context = () => { textContent = Resource1.davinci_txt; };
    }
}
