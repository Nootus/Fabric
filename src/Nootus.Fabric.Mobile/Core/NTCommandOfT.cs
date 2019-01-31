using System;

namespace Nootus.Fabric.Mobile.Core
{
    public class NTCommand<T> : NTCommand where T: class
    {
        public NTCommand(Action<T> execute): base((Action<object>) execute) {}
    }
}
