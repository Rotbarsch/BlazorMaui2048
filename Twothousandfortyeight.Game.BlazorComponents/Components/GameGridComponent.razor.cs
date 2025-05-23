﻿using Microsoft.AspNetCore.Components;
using Twothousandfortyeight.Game.Services;

// ReSharper disable once CheckNamespace
namespace Twothousandfortyeight.Game.BlazorComponents;

public partial class GameGridComponent
{
    [Inject] 
    public required IGameService GameService { get; set; }

    protected override void OnParametersSet()
    {
        GameService.RegisterOnGridChangedCallback(StateHasChanged);

        base.OnParametersSet();
    }
}