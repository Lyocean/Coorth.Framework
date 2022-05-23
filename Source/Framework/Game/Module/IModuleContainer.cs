using System;

namespace Coorth.Framework;

internal interface IModuleContainer {
    void OnAddModule(Type type, Module module);
    void OnRemoveModule(Type type, Module module);
}