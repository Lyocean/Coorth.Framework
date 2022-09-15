﻿using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth.Tasks; 

public static class TaskExtension {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Forget(this Task task) {
        if (task == null) {
            throw new ArgumentNullException();
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Forget(this in ValueTask task) {
        //Do nothing
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> FromResult<T>(in T result) => new(result);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask FromTask(in Task task) => new(task);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> FromTask<T>(in Task<T> task) => new(task);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask ToValueTask(this Task result) => new(result);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> ToValueTask<T>(this Task<T> result) => new(result);
}