using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Digital.OrderService.Integration.Hadoop
{
    public class HadoopFileInfo
    {
        public string File { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
    }
}
