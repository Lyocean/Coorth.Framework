﻿namespace Coorth.Framework; 

[System, Guid("775695A2-954C-456D-B962-EA7A07DDAC1D")]
public class DescriptionSystem : SystemBase {
    protected override void OnAdd() {
        BindComponent<DescriptionComponent>();
    }
}