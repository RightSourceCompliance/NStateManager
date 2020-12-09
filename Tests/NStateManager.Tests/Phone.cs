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
namespace NStateManager.Tests
{
    public class Phone
    {
        public string Name { get; set; }
        public PhoneState State { get; set; }
    }

    public enum PhoneState
    {
        OnHook,
        InRinging,
        OffHook,
        ReadyToDial,
        Dialing,
        GettingCallerStatus,
        OutRinging,
        Busy,
        Connected,
        OnHold,
        Recording,
        OutOfService
    }

    public enum PhoneEvent
    {
        IncomingCall,
        Answer,
        PickUp,
        Dialing,
        DialingDone,
        HangUp,
        CallerBusy,
        Ringing,
        CallerPickedUp,
        PutOnHold,
        RemoveHold,
        StartRecording,
        StopRecording,
        RemoveFromService,
        ReturnToService,
        LineDisruption
    }
}