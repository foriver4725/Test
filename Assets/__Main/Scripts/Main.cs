using System;
using Interface;
using Ex;

namespace Main
{
    internal sealed class Main : IDisposable, INullExistable, IEventable
    {
        internal Main() { }
        public void Dispose() { }
        public bool IsNullExist() => false;

        public void Start()
        {
            if (IsNullExist()) return;
        }

        public void Update()
        {
            if (IsNullExist()) return;
        }
    }
}