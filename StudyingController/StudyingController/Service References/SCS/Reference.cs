﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyingController.SCS {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Session", Namespace="http://schemas.datacontract.org/2004/07/StudyingControllerService")]
    [System.SerializableAttribute()]
    public partial class Session : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ControllerServiceException", Namespace="http://schemas.datacontract.org/2004/07/StudyingControllerService")]
    [System.SerializableAttribute()]
    public partial class ControllerServiceException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReasonField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Reason {
            get {
                return this.ReasonField;
            }
            set {
                if ((object.ReferenceEquals(this.ReasonField, value) != true)) {
                    this.ReasonField = value;
                    this.RaisePropertyChanged("Reason");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SCS.IControllerService")]
    public interface IControllerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControllerService/Login", ReplyAction="http://tempuri.org/IControllerService/LoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(StudyingController.SCS.ControllerServiceException), Action="http://tempuri.org/IControllerService/LoginControllerServiceExceptionFault", Name="ControllerServiceException", Namespace="http://schemas.datacontract.org/2004/07/StudyingControllerService")]
        StudyingController.SCS.Session Login([System.ServiceModel.MessageParameterAttribute(Name="login")] string login1, string password);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IControllerService/Login", ReplyAction="http://tempuri.org/IControllerService/LoginResponse")]
        System.IAsyncResult BeginLogin(string login, string password, System.AsyncCallback callback, object asyncState);
        
        StudyingController.SCS.Session EndLogin(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IControllerServiceChannel : StudyingController.SCS.IControllerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public StudyingController.SCS.Session Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((StudyingController.SCS.Session)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ControllerServiceClient : System.ServiceModel.ClientBase<StudyingController.SCS.IControllerService>, StudyingController.SCS.IControllerService {
        
        private BeginOperationDelegate onBeginLoginDelegate;
        
        private EndOperationDelegate onEndLoginDelegate;
        
        private System.Threading.SendOrPostCallback onLoginCompletedDelegate;
        
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
        
        public event System.EventHandler<LoginCompletedEventArgs> LoginCompleted;
        
        public StudyingController.SCS.Session Login(string login1, string password) {
            return base.Channel.Login(login1, password);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLogin(string login, string password, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogin(login, password, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public StudyingController.SCS.Session EndLogin(System.IAsyncResult result) {
            return base.Channel.EndLogin(result);
        }
        
        private System.IAsyncResult OnBeginLogin(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string login = ((string)(inValues[0]));
            string password = ((string)(inValues[1]));
            return this.BeginLogin(login, password, callback, asyncState);
        }
        
        private object[] OnEndLogin(System.IAsyncResult result) {
            StudyingController.SCS.Session retVal = this.EndLogin(result);
            return new object[] {
                    retVal};
        }
        
        private void OnLoginCompleted(object state) {
            if ((this.LoginCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LoginCompleted(this, new LoginCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LoginAsync(string login, string password) {
            this.LoginAsync(login, password, null);
        }
        
        public void LoginAsync(string login, string password, object userState) {
            if ((this.onBeginLoginDelegate == null)) {
                this.onBeginLoginDelegate = new BeginOperationDelegate(this.OnBeginLogin);
            }
            if ((this.onEndLoginDelegate == null)) {
                this.onEndLoginDelegate = new EndOperationDelegate(this.OnEndLogin);
            }
            if ((this.onLoginCompletedDelegate == null)) {
                this.onLoginCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLoginCompleted);
            }
            base.InvokeAsync(this.onBeginLoginDelegate, new object[] {
                        login,
                        password}, this.onEndLoginDelegate, this.onLoginCompletedDelegate, userState);
        }
    }
}
