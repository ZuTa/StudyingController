﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyingController.SCS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SCS.IControllerService")]
    public interface IControllerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControllerService/IsValidLogin", ReplyAction="http://tempuri.org/IControllerService/IsValidLoginResponse")]
        bool IsValidLogin(string login, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IControllerServiceChannel : StudyingController.SCS.IControllerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ControllerServiceClient : System.ServiceModel.ClientBase<StudyingController.SCS.IControllerService>, StudyingController.SCS.IControllerService {
        
        public ControllerServiceClient() {
        }
        
        public ControllerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ControllerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ControllerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ControllerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsValidLogin(string login, string password) {
            return base.Channel.IsValidLogin(login, password);
        }
    }
}