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
#region Copyright (c) 2024 Yardi Systems, Inc.
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
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace NStateManager.Tests.Async
{
    public class StateTransitionDynamicTests
    {
        [Fact]
        public void Constructor_throws_ArgumentNullException_if_StateFuncAsync_null()
        {
            Assert.Throws<ArgumentNullException>(() => new NStateManager.Async.StateTransitionDynamic<Sale, SaleState, SaleEvent>(
                    stateAccessor: sale => sale.State
                    , stateMutator: (sale, newState) => sale.State = newState
                    , stateFunctionAsync: null
                    , name: "test"
                    , priority: 1));
        }

        [Fact]
        public async Task ExecuteAsync_doesnt_execute_if_already_cancelled()
        {
            var sut = new NStateManager.Async.StateTransitionDynamic<Sale, SaleState, SaleEvent>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , stateFunctionAsync: (sale, cancelToken) => Task.FromResult(SaleState.Complete)
                , name: "test"
                , priority: 1);
            using (var cancellationSource = new CancellationTokenSource())
            {
                cancellationSource.Cancel();
                var sale = new Sale(saleId: 87) { State = SaleState.Open };
                var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale, cancellationToken: cancellationSource.Token);
                var result = await sut.ExecuteAsync(parameters);
                Assert.True(result.WasCancelled);
                Assert.Equal(SaleState.Open, sale.State);
                Assert.Equal(SaleState.Open, result.CurrentState);
            }
        }

        [Fact]
        public async Task ExecuteAsync_transitions_based_on_StateFuncAsync()
        {
            var sut = new NStateManager.Async.StateTransitionDynamic<Sale, SaleState, SaleEvent>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , stateFunctionAsync: (sale, cancelToken) => Task.FromResult(SaleState.Complete)
                , name: "test"
                , priority: 1);

            using (var cancellationSource = new CancellationTokenSource())
            {
                var sale = new Sale(saleId: 87) { State = SaleState.Open };
                var parameters = new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, sale, cancellationSource.Token);
                var result = await sut.ExecuteAsync(parameters);
                Assert.Equal(SaleState.Complete, sale.State);
                Assert.Equal(SaleState.Complete, result.CurrentState);
                Assert.Equal(SaleState.Open, result.PreviousState);
                Assert.False(result.WasCancelled);
            }
        }
    }
}
