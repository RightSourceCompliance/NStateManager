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
using NStateManager.Sync;
using Xunit;

namespace NStateManager.Tests
{
    public class StateTransitionParameterizedTests
    {
        [Fact]
        public void Constructor_throws_ArgumentNullException_if_Condition_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new StateTransitionParameterized<Sale, SaleState, SaleEvent, string>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , toState: SaleState.Complete
                , condition: null
                , name: "test"
                , priority: 1));
        }

        [Fact]
        public void Execute_throws_ArgumentException_if_parameter_wrong_type()
        {
            var sut = new StateTransitionParameterized<Sale, SaleState, SaleEvent, string>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , toState: SaleState.Complete
                , condition: (_, parameter) => parameter == "yes"
                , name: "test"
                , priority: 1);

            Assert.Throws<ArgumentException>(() => sut.Execute(new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, new Sale(saleId: 9), request: 0)));
        }

        [Fact]
        public void Execute_transitions_if_condition_met()
        {
            var testSale = new Sale(saleId: 55) { State = SaleState.Open} ;

            var sut = new StateTransitionParameterized<Sale, SaleState, SaleEvent, string>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , toState: SaleState.Complete
                , condition: (_, parameter) => parameter == "yes"
                , name: "test"
                , priority: 1);

            sut.Execute(new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, testSale, request: "yes"));

            Assert.Equal(SaleState.Complete, testSale.State);
        }

        [Fact]
        public void Execute_doesnt_transition_if_condition_not_met()
        {
            var testSale = new Sale(saleId: 55) { State = SaleState.Open };
            var sut = new StateTransitionParameterized<Sale, SaleState, SaleEvent, string>(
                stateAccessor: sale => sale.State
                , stateMutator: (sale, newState) => sale.State = newState
                , toState: SaleState.Complete
                , condition: (_, parameter) => parameter == "yes"
                , name: "test"
                , priority: 1);

            sut.Execute(new ExecutionParameters<Sale, SaleEvent>(SaleEvent.Pay, testSale, request: "NO"));

            Assert.Equal(SaleState.Open, testSale.State);
        }
    }
}
