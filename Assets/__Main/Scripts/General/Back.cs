using System;
using UnityEngine;
using Interface;

namespace Main
{
    internal sealed class Back : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI debugText;

        private BackImpl impl;
        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            impl = new(debugText);
        }

        private void OnDisable()
        {
            impl.Dispose();

            debugText = null;
            impl = null;
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
                impl.Start();
            }

            impl.Update();
        }
    }

    internal sealed class BackImpl : IDisposable, INullExistable, IEventable
    {
        private Debug debug;
        private Main main;

        internal BackImpl(TMPro.TextMeshProUGUI debugText)
        {
            this.debug = new(debugText);
            this.main = new();
        }

        public void Dispose()
        {
            debug.Dispose();
            main.Dispose();

            debug = null;
            main = null;
        }

        public bool IsNullExist()
        {
            if (debug == null) return true;
            if (main == null) return true;

            if (debug.IsNullExist()) return true;
            if (main.IsNullExist()) return true;

            return false;
        }

        public void Start()
        {
            if (IsNullExist()) return;

            debug.Start();
            main.Start();
        }

        public void Update()
        {
            if (IsNullExist()) return;

            debug.Update();
            main.Update();
        }
    }

    internal sealed class Debug : IDisposable, INullExistable, IEventable
    {
        private TMPro.TextMeshProUGUI debugText;

        int cnt = 0;
        float preT = 0f;

        float fps = 0f;
        float allocatedMemory = 0f;
        float unusedReservedMemory = 0f;
        float reservedMemory = 0f;
        float memoryP = 0f;

        internal Debug(TMPro.TextMeshProUGUI debugText) => this.debugText = debugText;
        public void Dispose() => debugText = null;
        public bool IsNullExist() => debugText == null;
        public void Start()
        {
            if (IsNullExist()) return;
        }
        public void Update()
        {
            if (IsNullExist()) return;

            // FPSの計算(0.5秒ごと)
            cnt++;
            float t = Time.realtimeSinceStartup - preT;
            if (t >= 0.5f)
            {
                fps = cnt / t;
                cnt = 0;
                preT = Time.realtimeSinceStartup;
            }

            // 使用メモリ数の取得
            allocatedMemory = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong().ByteToMegabyte();
            unusedReservedMemory = UnityEngine.Profiling.Profiler.GetTotalUnusedReservedMemoryLong().ByteToMegabyte();
            reservedMemory = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong().ByteToMegabyte();
            memoryP = allocatedMemory / reservedMemory;

            // デバッグテキストを更新(小数点以下2桁)
            debugText.text =
                $"FPS: {fps:F2}\n" +
                $"Memory(MB): {allocatedMemory:F2}/{reservedMemory:F2} ({memoryP:P2}, {unusedReservedMemory:F2} unused)";
        }
    }

    internal static class BackEx
    {
        internal static float ByteToMegabyte(this long n)
        {
            return (n >> 10) / 1024f;
        }
    }
}