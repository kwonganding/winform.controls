using System;

namespace System
{
    public delegate void CallbackVoidHandler();

    public delegate void CallbackObjectHandler<T>(T obj);

    public delegate TR CallbackHandler<T, TR>(T obj);

    public delegate void CallbackObjectHandler2<T1, T2>(T1 obj1, T2 obj2);
}
