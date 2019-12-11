using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
