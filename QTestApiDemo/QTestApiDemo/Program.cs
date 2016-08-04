using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTestApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            QTestApi qtestApi = new QTestApi("http://qasservice.qtestnet.com");
            qtestApi.Login("", "");
            var newProjectId = qtestApi.CreateProject("DOTNET","DOTNET API",DateTime.Now,DateTime.Now.AddDays(1.0));
            qtestApi.CreateTestModule(newProjectId, ".NET_Module", "");
            qtestApi.Logout();
        }

    }
}
