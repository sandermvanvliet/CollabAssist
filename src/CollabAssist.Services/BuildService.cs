﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
using CollabAssist.Output;

namespace CollabAssist.Services
{
    public class BuildService
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;

        public BuildService(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public async Task<bool> HandleBuild(Build build)
        {
            build = await _inputHandler.LinkBuildWithPr(build).ConfigureAwait(false);
            if (build.HasPullRequestLinked)
            {
                await _outputHandler.NotifyFailedPullRequestBuild(build).ConfigureAwait(false);
            }
            return false;
        }
    }
}
