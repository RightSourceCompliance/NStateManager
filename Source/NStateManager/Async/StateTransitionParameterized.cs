﻿#region Copyright (c) 2018 Scott L. Carter
//
//Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in
//compliance with the License. You may obtain a copy of the License at
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software distributed under the License is 
//distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and limitations under the License.
#endregion
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NStateManager.Async
{
    internal class StateTransitionParameterized<T, TState, TTrigger, TParam> : StateTransitionNonDynamic<T, TState, TTrigger>
        where TParam : class
    {
        public Func<T, TParam, CancellationToken, Task<bool>> ConditionAsync { get; }

        public StateTransitionParameterized(Func<T, TState> stateAccessor, Action<T, TState> stateMutator, TState toState, Func<T, TParam, CancellationToken, Task<bool>> conditionAsync, string name, uint priority)
            : base(stateAccessor, stateMutator, toState, name, priority)
        {
            ConditionAsync = conditionAsync ?? throw new ArgumentNullException(nameof(conditionAsync));
        }

        public override async Task<StateTransitionResult<TState, TTrigger>> ExecuteAsync(ExecutionParameters<T, TTrigger> parameters
          , StateTransitionResult<TState, TTrigger> currentResult = null)
        {
            if (!(parameters.Request is TParam typeSafeParam))
            { throw new ArgumentException($"Expected a {typeof(TParam).Name} parameter, but received a {parameters.Request?.GetType().Name ?? "null"}."); }

            var startState = currentResult != null ? currentResult.StartingState : StateAccessor(parameters.Context);

            if (parameters.CancellationToken.IsCancellationRequested)
            {
                return GetFreshResult(parameters
                  , currentResult
                  , startState
                  , transitionDefined: true
                  , conditionMet: false
                  , wasCancelled: true); }

            if (!await ConditionAsync(parameters.Context, typeSafeParam, parameters.CancellationToken)
               .ConfigureAwait(continueOnCapturedContext: false))
            { return GetFreshResult(parameters, currentResult, startState, transitionDefined: true, conditionMet: false, wasCancelled: parameters.CancellationToken.IsCancellationRequested); }

            StateMutator(parameters.Context, ToState);
            var transitionResult = currentResult == null
                ? new StateTransitionResult<TState, TTrigger>(parameters.Trigger, startState, startState, ToState, Name)
                : new StateTransitionResult<TState, TTrigger>(parameters.Trigger, startState, currentResult.CurrentState, ToState, Name);

            return transitionResult;
        }
    }
}