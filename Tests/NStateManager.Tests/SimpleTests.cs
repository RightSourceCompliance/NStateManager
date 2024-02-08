#region Copyright (c) 2018 Scott L. Carter
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
using NSimpleTester;
using Xunit;

namespace NStateManager.Tests
{
    public class SimpleTests
    {
        [Fact]
        public void Simple_Tests()
        {
            new AssemblyTester(typeof(FunctionAction<>).Assembly)
                .ExcludeClass("ExecutionParameters`2")
                .ExcludeClass("FunctionAction`1")
                .ExcludeClass("FunctionActionBase`1")
                .ExcludeClass("FunctionActionParameterized`2")
                .ExcludeClass("StateConfigurationBase`3")
                .ExcludeClass("StateTransitionBase`3")
                .ExcludeClass("StateTransitionDynamic`3")
                .ExcludeClass("StateTransitionDynamicBase`3")
                .ExcludeClass("StateTransitionDynamicParameterized`4")
                .ExcludeClass("StateTransitionNonDynamic`3")
                .ExcludeClass("StateTransitionResult`2")
                .ExcludeClass("TransitionEventArgs`3")
                .ExcludeClass("TriggerAction`2")
                .ExcludeClass("TriggerActionBase`2")
                .ExcludeClass("TriggerActionParameterized`3")
                .ExcludeClass("StateMachine`3")
                .ExcludeClass("StateConfiguration`3")
                .ExcludeClass("StateTransition`3")
                .ExcludeClass("StateTransitionAutoDynamic`3")
                .ExcludeClass("StateTransitionAutoDynamicParameterized`4")
                .ExcludeClass("StateTransitionAutoFallback`3")
                .ExcludeClass("StateTransitionAutoFallbackParameterized`4")
                .ExcludeClass("StateTransitionAutoForward`3")
                .ExcludeClass("StateTransitionAutoForwardParameterized`4")
                .ExcludeClass("StateTransitionParameterized`4")
                .ExcludeClass("ConfigurationSummary`2")
                .ExcludeClass("CsvExporter`2")
                .ExcludeClass("DotGraphExporter`2")
                .ExcludeClass("StateDetails`2")
                .ExcludeClass("TransitionDetails`2")
                .ExcludeClass("NStateManager.Async.StateConfiguration`3", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateMachine`3", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransition`3", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionAutoDynamic`3", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionAutoDynamicParameterized`4", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionAutoFallback`3", isFullName: true)
                .ExcludeClass("StateTransitionAutoFallbackParameterized`4")
                .ExcludeClass("NStateManager.Async.StateTransitionAutoForward`3", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionAutoFallbackParameterized`4", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionAutoForwardParameterized`4", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionParameterized`4", isFullName: true)
                .ExcludeClass("NStateManager.Async.StateTransitionDynamic`3", isFullName:true)
                .ExcludeClass("DynamicTransitionFailureException")
                .TestAssembly();
        }
    }
}