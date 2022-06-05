using System;

namespace Coorth; 

public readonly record struct Result {
        
    public readonly bool IsSuccess;
        
    public readonly string? Error;
        
    private Result(bool isSuccess) {
        IsSuccess = isSuccess;
        Error = null;
    }
        
    private Result(string error) {
        IsSuccess = false;
        Error = error;
    }
        
    public static Result Success() => new(true);

    public static Result Failure(string error) => new(error);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}
    
public readonly struct Result<T> {
        
    public readonly T? Value;
        
    public readonly bool IsSuccess;
        
    public readonly string? Error;
        
    public readonly Exception? Exception;
        
    private Result(bool isSuccess, T value) {
        Value = value;
        IsSuccess = isSuccess;
        Error = null;
        Exception = null;
    }
        
    private Result(string error) {
        Value = default;
        IsSuccess = false;
        Error = error;
        Exception = null;
    }
        
    private Result(Exception e) {
        Value = default;
        IsSuccess = false;
        Error = null;
        Exception = e;
    }
        
    public static Result<T> Success(T value) => new(true, value);

    public static Result<T> Failure(string err) => new(err);

    public static Result<T> Failure(Exception e) => new(e);

    public string GetError() {
        if (Error != null) {
            return Error;
        }
        return Exception?.ToString() ?? "unknown";
    }

    public void Deconstruct(out bool isSuccess, out T? value) {
        isSuccess = IsSuccess;
        value = Value;
    }
}