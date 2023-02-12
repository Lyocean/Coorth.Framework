using System;
using Coorth.Errors;

namespace Coorth; 

public readonly record struct Result {
        
    public readonly bool IsSuccess;
        
    public readonly object? Error;
        
    private Result(bool isSuccess) {
        IsSuccess = isSuccess;
        Error = null;
    }
        
    private Result(IError error) {
        IsSuccess = false;
        Error = error;
    }
    
    private Result(string error) {
        IsSuccess = false;
        Error = error;
    }
        
    public static Result Success() => new(true);

    public static Result Failure(IError error) => new(error);

    public static Result Failure(string error) => new(error);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}
    
public readonly struct Result<T> {
        
    public readonly T? Value;
        
    public readonly bool IsSuccess;
        
    public readonly object? Error;
    
    private Result(bool isSuccess, T value) {
        Value = value;
        IsSuccess = isSuccess;
        Error = null;
    }
      
    private Result(IError error) {
        Value = default;
        IsSuccess = false;
        Error = error;
    }
    
    private Result(string error) {
        Value = default;
        IsSuccess = false;
        Error = error;
    }
        
    private Result(Exception e) {
        Value = default;
        IsSuccess = false;
        Error = e;
    }
        
    public static Result<T> Success(T value) => new(true, value);

    public static Result<T> Failure(string err) => new(err);

    public static Result<T> Failure(Exception e) => new(e);

    public static Result<T> Failure(IError err) => new(err);

    public string GetError() {
        return Error?.ToString() ?? "unknown";
    }

    public void Deconstruct(out bool isSuccess, out T? value) {
        isSuccess = IsSuccess;
        value = Value;
    }
}