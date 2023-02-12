using System;

namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public sealed class ManagerAttribute : Attribute {
}

public interface IManager : IDisposable {
}

public abstract class Manager : Disposable, IManager {
}