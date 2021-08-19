using System;

namespace Coorth {
    public readonly struct Result {
        
        public readonly bool IsSuccess;
        
        public readonly string Error;
        
        private Result(bool isSuccess) {
            this.IsSuccess = isSuccess;
            this.Error = null;
        }
        
        private Result(string error) {
            this.IsSuccess = false;
            this.Error = error;
        }
        
        public static Result Success() {
            return new Result(true);
        }

        public static Result Failure(string error) {
            return new Result(error);
        }
        
        public static Result<T> Success<T>(T value) {
            return Result<T>.Success(value);
        }

        public static Result<T> Failure<T>(string error) {
            return Result<T>.Failure(error);
        }
    }
    
    
    public readonly struct Result<T> {
        
        public readonly T Value;
        
        public readonly bool IsSuccess;
        
        public readonly string Error;
        
        public readonly Exception Exception;
        
        private Result(bool isSuccess, T value) {
            this.Value = value;
            this.IsSuccess = isSuccess;
            this.Error = null;
            this.Exception = null;
        }
        
        private Result(string error) {
            this.Value = default;
            this.IsSuccess = false;
            this.Error = error;
            this.Exception = null;
        }
        
        private Result(Exception e) {
            this.Value = default;
            this.IsSuccess = false;
            this.Error = null;
            this.Exception = e;
        }
        
        public static Result<T> Success(T value) {
            return new Result<T>(true, value);
        }

        public static Result<T> Failure(string err) {
            return new Result<T>(err);
        }
        
        public static Result<T> Failure(Exception e) {
            return new Result<T>(e);
        }

        public string GetError() {
            if (this.Error != null) {
                return this.Error;
            }
            return this.Exception?.ToString() ?? "unknown";
        }
    }
}