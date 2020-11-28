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

namespace NStateManager
{
    internal static class StateTransitionFactory<T, TState, TTrigger>
        where TState : IComparable
    {
        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, bool> condition, string name, uint priority = 1)
        {
            return new Sync.StateTransition<T, TState, TTrigger>(stateMachine.StateAccessor, stateMachine.StateMutator, toState, name, priority, condition);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, TState toState, Func<T, bool> condition, TState triggerState, string name, uint priority = 1)
        {
             return new Sync.StateTransitionAutoFallback<T, TState, TTrigger>(stateMachine, startState, triggerState, toState, condition, name, priority); 
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState triggerState, Func<T, bool> condition, TState toState, string name, uint priority = 1)
        {
            return new Sync.StateTransitionAutoForward<T, TState, TTrigger>(stateMachine, triggerState, toState, condition, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, Func<T, TState> stateFunction, TState triggerState, string name, uint priority = 1)
        {
            return new Sync.StateTransitionAutoDynamic<T, TState, TTrigger>(stateMachine, startState, stateFunction, triggerState, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, Func<T, TRequest, TState> stateFunction, TState triggerState, string name, uint priority = 1)
            where TRequest : class
        {
            return new Sync.StateTransitionAutoDynamicParameterized<T, TState, TTrigger, TRequest>(stateMachine, startState, stateFunction, triggerState, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, TRequest, bool> condition, string name, uint priority = 1)
            where TRequest : class
        {
            return new Sync.StateTransitionParameterized<T, TState, TTrigger, TRequest>(stateMachine.StateAccessor, stateMachine.StateMutator, toState, condition, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, TState toState, Func<T, TRequest, bool> condition, TState triggerState, string name, uint priority = 1)
            where TRequest : class
        {
            return new Sync.StateTransitionAutoFallbackParameterized<T, TState, TTrigger, TRequest>(stateMachine, startState, triggerState, toState, condition, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Sync.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, TRequest, bool> condition, TState triggerState, string name, uint priority = 1)
            where TRequest : class
        {
            return new Sync.StateTransitionAutoForwardParameterized<T, TState, TTrigger, TRequest>(stateMachine, triggerState, toState, condition, name, priority);
        }

        //public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(IStateMachine<T, TState, TTrigger> stateMachine, TTrigger triggerEvent, Func<T, TState> stateFunction, string name = null, uint priority = 1)
        //{
        //    return null;
        //}

        //public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(IStateMachine<T, TState, TTrigger> stateMachine, TTrigger triggerEvent, Func<T, TRequest, TState> stateFunction, string name = null, uint priority = 1)
        //    where TRequest : class
        //{
        //    return null;
        //}

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, CancellationToken, Task<bool>> conditionAsync, string name, uint priority = 1)
        {
            return new Async.StateTransition<T, TState, TTrigger>(stateMachine.StateAccessor, stateMachine.StateMutator, toState, conditionAsync, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, Func<T, TState> stateFunction, TState triggerState, string name, uint priority = 1)
        {
            return new Async.StateTransitionAutoDynamic<T, TState, TTrigger>(stateMachine, startState, stateFunction, triggerState, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, Func<T, TRequest, TState> stateFunction, TState triggerState, string name, uint priority = 1)
            where TRequest : class
        {
            return new Async.StateTransitionAutoDynamicParameterized<T, TState, TTrigger, TRequest>(stateMachine, startState, stateFunction, triggerState, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, TState toState, Func<T, CancellationToken, Task<bool>> conditionAsync, TState triggerState, string name, uint priority = 1)
        {
            return new Async.StateTransitionAutoFallback<T, TState, TTrigger>(stateMachine, startState, triggerState, toState, conditionAsync, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, CancellationToken, Task<bool>> conditionAsync, TState triggerState, string name, uint priority = 1)
        {
            return new Async.StateTransitionAutoForward<T, TState, TTrigger>(stateMachine, triggerState, toState, conditionAsync, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Sync.IStateMachine<T, TState, TTrigger> stateMachine, Func<T, TState> stateFunc, string name, uint priority = 1)
        {
            return new StateTransitionDynamic<T, TState, TTrigger>(stateMachine.StateAccessor, stateMachine.StateMutator, stateFunc, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(Async.IStateMachine<T, TState, TTrigger> stateMachine, Func<T, TState> stateFunc, string name, uint priority = 1)
        {
            return new StateTransitionDynamic<T, TState, TTrigger>(stateMachine.StateAccessor, stateMachine.StateMutator, stateFunc, name, priority);
        }

        //public static StateTransitionBase<T, TState, TTrigger> GetStateTransition(IStateMachine<T, TState, TTrigger> stateMachine, Func<T, CancellationToken, Task<TState>> stateFuncAsync, string name, uint priority = 1)
        //{
        //    return new StateTransitionDynamicAsync<T, TState, TTrigger>(stateMachine.StateAccessor, stateMachine.StateMutator, stateFuncAsync, name, priority);
        //}

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TParam>(Sync.IStateMachine<T, TState, TTrigger> stateMachine, Func<T, TParam, TState> stateFunc, string name, uint priority = 1)
            where TParam : class
        {
            return new StateTransitionDynamicParameterized<T, TState, TTrigger, TParam>(stateMachine.StateAccessor, stateMachine.StateMutator, stateFunc, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TRequest>(Async.IStateMachine<T, TState, TTrigger> stateMachine, Func<T, TRequest, TState> stateFuncAsync, string name, uint priority = 1)
            where TRequest : class
        {
            return new StateTransitionDynamicParameterized<T, TState, TTrigger, TRequest>(stateMachine.StateAccessor, stateMachine.StateMutator, stateFuncAsync, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TParam>(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState toState, Func<T, TParam, CancellationToken, Task<bool>> conditionAsync, string name, uint priority = 1)
            where TParam: class
        {
            return new Async.StateTransitionParameterized<T, TState, TTrigger, TParam>(stateMachine.StateAccessor, stateMachine.StateMutator, toState, conditionAsync, name, priority);
        }

        public static StateTransitionBase<T, TState, TTrigger> GetStateTransition<TParam>(Async.IStateMachine<T, TState, TTrigger> stateMachine, TState startState, TState toState, Func<T, TParam, CancellationToken, Task<bool>> conditionAsync, TState triggerState, string name, uint priority = 1)
            where TParam : class
        {
            return new Async.StateTransitionAutoFallbackParameterized<T, TState, TTrigger, TParam>(stateMachine, startState, toState, triggerState, conditionAsync, name, priority);
        }
    }
}